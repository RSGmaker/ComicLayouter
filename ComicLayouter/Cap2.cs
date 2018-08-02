namespace EyeOpen.Imaging
{
    using System;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    ///using mshtml;
    using System.Runtime.InteropServices;

    public class HtmlToBitmapConverter
    {
        private const int SleepTimeMiliseconds = 5000;

        public Bitmap Render(string html, Size size)
        {
            var browser = CreateBrowser(size);

            browser.Navigate("about:blank");
            browser.Document.Write(html);

            return GetBitmapFromControl(browser, size);
        }

        public Bitmap Render(Uri uri, Size size)
        {
            var browser = CreateBrowser(size);

            NavigateAndWaitForLoad(browser, uri, 0);

            return GetBitmapFromControl(browser, size);
        }

        public void NavigateAndWaitForLoad(WebBrowser browser, Uri uri, int waitTime)
        {
            browser.Navigate(uri);
            var count = 0;

            while (browser.ReadyState != WebBrowserReadyState.Complete)
            {
                Thread.Sleep(SleepTimeMiliseconds);

                Application.DoEvents();
                count++;

                if (count > waitTime / SleepTimeMiliseconds)
                {
                    break;
                }
            }

            while (browser.Document.Body == null)
            {
                Application.DoEvents();
            }

            HideScrollBars(browser);
        }

        private void HideScrollBars(WebBrowser browser)
        {
            const string Hidden = "hidden";
            //var document = (IHTMLDocument2)browser.Document.DomDocument;
            dynamic document = browser.Document.DomDocument;
            //var style = (IHTMLStyle2)document.body.style;
            var style = document.body.style;
            style.overflowX = Hidden;
            style.overflowY = Hidden;
        }

        private WebBrowser CreateBrowser(Size size)
        {
            var
                newBrowser =
                    new WebBrowser
                    {
                        ScrollBarsEnabled = false,
                        ScriptErrorsSuppressed = true,
                        Size = size
                    };

            newBrowser.BringToFront();

            return newBrowser;
        }

        public Bitmap GetBitmapFromControl(WebBrowser browser, Size size)
        {
            var bitmap = new Bitmap(size.Width, size.Height);

            NativeMethods.GetImage(browser.Document.DomDocument, bitmap, Color.White);
            return bitmap;
        }

        public Bitmap GetBitmapFromControl2(object Interface, Size size)
        {
            var bitmap = new Bitmap(size.Width, size.Height);

            NativeMethods.GetImage(Interface, bitmap, Color.White);
            return bitmap;
        }
    }
    public static class NativeMethods
    {
        private const int SM_CXVSCROLL = 2;

        [ComImport]
        [Guid("0000010D-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IViewObject
        {
            void Draw([MarshalAs(UnmanagedType.U4)] uint dwAspect, int lindex, IntPtr pvAspect, [In] IntPtr ptd, IntPtr hdcTargetDev, IntPtr hdcDraw, [MarshalAs(UnmanagedType.Struct)] ref Rect lprcBounds, [In] IntPtr lprcWBounds, IntPtr pfnContinue, [MarshalAs(UnmanagedType.U4)] uint dwContinue);
        }

        public static int GetSystemMetrics()
        {
            return GetSystemMetrics(SM_CXVSCROLL);
        }

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int smIndex);

        public static void GetImage(object obj, Image destination, Color backgroundColor)
        {
            using (var graphics = Graphics.FromImage(destination))
            {
                var deviceContextHandle = IntPtr.Zero;
                var rectangle =
                    new Rect
                    {
                        Right = destination.Width,
                        Bottom = destination.Height
                    };

                graphics.Clear(backgroundColor);

                try
                {
                    deviceContextHandle = graphics.GetHdc();

                    var viewObject = obj as IViewObject;
                    viewObject.Draw(1, -1, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, deviceContextHandle, ref rectangle, IntPtr.Zero, IntPtr.Zero, 0);
                }
                finally
                {
                    if (deviceContextHandle != IntPtr.Zero)
                    {
                        graphics.ReleaseHdc(deviceContextHandle);
                    }
                }
            }
            Clipboard.SetImage(destination);
        }
    }
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Rect
    {
        public int Left;

        public int Top;

        public int Right;

        public int Bottom;
    }
}