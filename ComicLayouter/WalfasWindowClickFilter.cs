using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Runtime.InteropServices;

namespace ComicLayouter
{
    // Creates a  message filter.
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public class WalfasWindowClickFilter : IMessageFilter
    {
        public bool PreFilterMessage(ref Message m)
        {
            // Blocks all the messages relating to the right mouse button. 
            if (m.Msg == 516)
            {
                WalfasWindow W = WalfasWindow._this;
                if (W != null && !W.cropmode && W.customrightclick && GetForegroundWindow() == W.Handle)
                {
                    W.rightclick();
                    //W.contextMenuStrip1.Show(W, new System.Drawing.Point(Cursor.Position.X - (W.Left + W.chrome), Cursor.Position.Y - (W.Top + W.TC)));
                    return true;
                }
            }
            return false;
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();
    }
}
