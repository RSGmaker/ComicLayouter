﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
//using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Runtime.InteropServices;
//using System.ComponentModel.Design;
//using System.Windows.Forms.Design;

namespace ComicLayouter
{
    /// <summary>
    /// The "built-in" create.swf window
    /// </summary>
    public partial class WalfasWindow : Form
    {
        /// <summary>
        /// internet explorer variant for flash
        /// </summary>
        public class WT : WebBrowser
        {
            protected override void OnLayout(LayoutEventArgs levent)
            {
                SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                //DefaultBackColor = Color.White;
                base.OnLayout(levent);
            }
        }
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg,
            IntPtr wParam, IntPtr lParam);
        public static WalfasWindow _this=null;
        public Form1 form1;
        public bool cropmode;
        public string TITLE;
        public bool fullscreen;
        public Rectangle last;
        public FormWindowState lastWindowState;
        public Point LP = Point.Empty;
        public bool capt = false;
        /// <summary>
        /// how big the chrome of a window is.
        /// chrome refers to the title bar of an application.
        /// </summary>
        public int chrome = 0;
        public int TC = 0;
        public double OPC;
        public System.Threading.Thread thread;
        public bool customrightclick = true;
        public List<Bitmap> BL = new List<Bitmap>();
        public List<string> BS = new List<string>();
        bool IE;

        /// <summary>
        /// hidden text used in auto spellchecknig.
        /// </summary>
        System.Windows.Controls.TextBox SC = new System.Windows.Controls.TextBox();
        /// <summary>
        /// list of typed words, when you press F3 this is imported into spellcheckform.
        /// </summary>
        string textbuffer;
        Point lastposition = Point.Empty;
        ToolTip TP = new ToolTip();
        public bool customresolution = false;
        public WalfasWindow(Form1 form,bool useIE=false)
        {
            IE = useIE;
            //IE = true;
            InitializeComponent();
            this.form1 = form;
            Crop = new Rectangle(0, 0, 0, 0);
            TITLE = Text;
            chrome = (Width - ClientSize.Width)/2;
            TC = (Height - chrome) - ClientSize.Height;
            Focus();
            OPC = 1;

            form.Focus();
            webBrowser1.Focus();
            webBrowser1.IsWebBrowserContextMenuEnabled = false;
            //webBrowser1.Document.ActiveElement.AttachEventHandler
            webBrowser1.Document.ContextMenuShowing += Document_ContextMenuShowing;
            Control C = webBrowser1;
            this.BackColor = Color.FromArgb(89, 89, 89);
            if (false)
            {
                panel1.Controls.Remove(webBrowser1);
                Walfas F = new Walfas();
                F.Controls.Add(webBrowser1);
                panel1.Controls.Add(F);
                F.Focus();
                F.BringToFront();
            }
            ////TransparencyKey = Color.Empty;
            //SC = new TextBox();
            SC.SpellCheck.IsEnabled = true;
            if (System.IO.File.Exists(System.IO.Directory.GetCurrentDirectory() + "\\CL Dictionary.txt"))
            {
                SC.SpellCheck.CustomDictionaries.Add(new Uri(System.IO.Directory.GetCurrentDirectory() + "\\CL Dictionary.txt"));
            }
            else
            {
                MessageBox.Show("\"CL Dictionary.txt\" could not be loaded, spellcheck will use the default english dictionary only.");
            }
            
            textbuffer = "";
            this.AutoScroll = false;
            panel1.AutoScroll = this.AutoScroll;
            HorizontalScroll.Maximum = 0;
            VerticalScroll.Maximum = 0;
        }

        private void Document_ContextMenuShowing(object sender, HtmlElementEventArgs e)
        {
            //throw new NotImplementedException();
            contextMenuStrip1.Show(Cursor.Position);
        }

        /// <summary>
        /// mouse position
        /// </summary>
        public Point MPos
        {
            get
            {
                Point P = Cursor.Position;
                int C = 0;
                int T = 0;
                if (!fullscreen)
                {
                    C = chrome;
                    T = TC;
                }
                P = new Point((P.X - Location.X) - C,(P.Y - Location.Y) - T);
                return P;
            }
        }
        /// <summary>
        /// gets the position of the create.swf
        /// </summary>
        public Point webpos
        {
            get
            {
                int C = 0;
                int T = 0;
                if (!fullscreen)
                {
                    C = chrome;
                    T = TC;
                }
                return new Point(Location.X + C, Location.Y + T);
            }
        }
        const int WM_NCHITTEST = 0x84;
        const int HTTRANSPARENT = -1;

        /// <summary>
        /// multithreaded bitmap save spamming
        /// </summary>
        public void Bsave()
        {
            while (true)
            {
                Bitmap B = null;
                string s = "";
                lock (BL)
                {
                    if (BL.Count > 0)
                    {
                        B = BL[0];
                        s = BS[0];
                        BL.RemoveAt(0);
                        BS.RemoveAt(0);
                    }
                }
                if (B != null)
                {
                    B.Save(s);
                    B.Dispose();
                }
                System.Threading.Thread.Sleep(20);
            }
        }

        protected override void WndProc(ref Message m)
        {
            //mouse clicks cannot be captured from here...

            /*if (m.Msg == 127 || m.Msg == 14 || m.Msg == 13)
            {
                //this is a basic update message lets not bother with these.
                base.WndProc(ref m);
                return;
            }*/
            
            List<int> ID = new List<int>();
            List<int> I = ID;
            

            
            //text changed
            /*ID.Add(14);
            ID.Add(13);
            ID.Add(12);*/

            List<int> R = new List<int>();
            I = R;
            /*R.Add(160);
            R.Add(174);
            R.Add(132);
            R.Add(528);

            I.Add(33);
            I.Add(70);
            I.Add(28);
            I.Add(134);
            I.Add(6);
            I.Add(71);*/
            //if (m.Msg == 528 || m.Msg == 132 || m.Msg == 674)
            /*if (R.IndexOf(m.Msg)>-1)
            {
                return;
            }
            //return;
            if (ID.IndexOf(m.Msg) < 0)
            {
                //Console.WriteLine(":" + m.Msg);
                ID.Add(-222);
                Text = "code:" + m.Msg;
            }*/
            if (m.Msg == (int)WM_NCHITTEST)
            {
                m.Result = (IntPtr)HTTRANSPARENT;
                //m.Result = m.Result;
                base.WndProc(ref m);
            }
            else
                base.WndProc(ref m);
        }
        public Bitmap BC;
        public Graphics BCG;
        public void DoCapture()
        {
            if (capt)
            {
                return;
            }
            capt = true;
            Invalidate();
            pictureBox1.Invalidate();
            Application.DoEvents();
            System.Threading.Thread.Sleep(1);
            if (TransparencyKey != Color.Empty)
            {
                if (fullscreen)
                {
                    Rectangle C = Crop;
                    if (Crop.Size.Width <= 0 || Crop.Size.Height <= 0)
                    {
                        //return B;
                        C = new Rectangle(0, 0, Width, Height);
                    }
                    string s = System.IO.Path.GetFullPath("capture") + "\\" + form1.img.Count + ".png";
                    //Rectangle C = Crop;
                    if (BC == null || BC.Size.Equals(Crop.Size))
                    {
                        if (BC != null)
                        {
                            BCG.Dispose();
                            BCG = null;
                        }
                        BC = new Bitmap(C.Width, C.Height);
                        BCG = Graphics.FromImage(BC);
                    }
                    BCG.CopyFromScreen(Crop.Location, Point.Empty, BC.Size);
                    Bitmap B = new Bitmap(BC);
                    if (!System.IO.Directory.Exists("capture"))
                    {
                        System.IO.Directory.CreateDirectory("capture");
                    }
                    lock (BL)
                    {
                        BL.Add(new Bitmap(B));
                        BS.Add(s);
                    };
                    if (thread == null)
                    {
                        thread = new System.Threading.Thread(Bsave);
                        thread.Start();
                    }
                    form1.LoadImages(s, B);
                }
                else
                {
                    Rectangle C = Crop;
                    if (Crop.Size.Width <= 0 || Crop.Size.Height <= 0)
                    {
                        C = new Rectangle(0, 0, Width, Height);
                    }
                    string s = System.IO.Path.GetFullPath("capture") + "\\" + form1.img.Count + ".png";

                    if (BC == null || BC.Size.Equals(Crop.Size))
                    {
                        if (BC != null)
                        {
                            BCG.Dispose();
                            BCG = null;
                        }
                        BC = new Bitmap(C.Width, C.Height);
                        BCG = Graphics.FromImage(BC);
                    }
                    BCG.CopyFromScreen(Crop.Location, Point.Empty, BC.Size);
                    Bitmap B = new Bitmap(BC);
                    if (!System.IO.Directory.Exists("capture"))
                    {
                        System.IO.Directory.CreateDirectory("capture");
                    }
                    lock (BL)
                    {
                        BL.Add(new Bitmap(B));
                        BS.Add(s);
                    };
                    if (thread == null)
                    {
                        thread = new System.Threading.Thread(Bsave);
                        thread.Start();
                    }
                    form1.LoadImages(s, B);
                }
            }
            else
            {
                string s = System.IO.Path.GetFullPath("capture") + "\\" + form1.img.Count + ".png";
                Bitmap B = Capture();
                if (!System.IO.Directory.Exists("capture"))
                {
                    System.IO.Directory.CreateDirectory("capture");
                }
                B.Save(s);
                form1.LoadImages(new string[] { s });
            }
            capt = false;
        }
        const int WM_KEYDOWN = 0x100;
        const int WM_SYSKEYDOWN = 0x104;
        const int WM_KEYUP = 0x101;
        const int WM_SYSKEYUP = 0x105;
        public int Interval;
        public bool AutoSpellCheckActive = false;
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((msg.Msg == WM_KEYUP) || (msg.Msg == WM_SYSKEYUP))
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
            //if (keyData != Keys.F3)
            if (AutoSpellCheckActive)
            {
                string s = keyData.ToString();
                if (s == "Oem7")
                {
                    //this is the comma button on my keyboard idk if it's like that for other keyboards...
                    s = "'";
                }
                if ((lastposition != Cursor.Position || keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.Up || keyData == Keys.Down) && keyData != Keys.F3)
                {
                    //in this case it is likely the user is doing something so i clear spellcheck data to prevent spellcheck from unneccessary complaints.
                    SC.Text = "";
                    textbuffer = "";
                }
                if (s.Length > 1 && keyData != Keys.Back)
                {
                    //the user pressed a key that can't be described by a character so it begins spellchecking the last sequence of text.
                    if (SC.Text.Length>2 && SC.GetSpellingErrorStart(0) >= 0)
                    {
                        
                        System.Windows.Controls.SpellingError SE = SC.GetSpellingError(0);
                        string[] SG = new List<string>(SE.Suggestions).ToArray();
                        string tmp = "";
                        int delay = 2000;
                        if (SG.Length > 0)
                        {
                            //check for spelling suggestions
                            tmp = "";
                            var i = 0;
                            while (i < SG.Length)
                            {
                                tmp = tmp + ", you mean " + SG[i]+"?";
                                delay += 1000;
                                i++;
                                i = SG.Length;
                            }
                        }
                        TP.Show("Unknown:" + SC.Text+tmp, this, Cursor.Position.X - Location.X, Cursor.Position.Y - Location.Y, delay);
                    }
                    else
                    {
                        //Text = "no errors!";
                    }
                    
                    //adds the typed word into the textbuffer.
                        if (textbuffer == "")
                        {
                            textbuffer = SC.Text;
                        }
                        else
                        {
                            textbuffer = textbuffer + " " + SC.Text;
                        }
                }
                if (keyData == Keys.Back)
                {
                    //deletes the last character
                    if (SC.Text.Length > 0)
                    {
                        if (SC.Text.Length == 1)
                        {
                            SC.Text = "";
                        }
                        else
                        {
                            SC.Text = SC.Text.Substring(0, SC.Text.Length - 1);
                        }
                    }
                }
                else if (s.Length > 1 && keyData != Keys.F3)
                {
                    SC.Text = "";
                }
                else if (s.Length == 1)
                {
                    //add the typed character to the current word
                    SC.Text = SC.Text + s;
                }
            }
           
            lastposition = Cursor.Position;
            if (!ghostmode && keyData == (Keys.Add | Keys.Control))
            {
                OPC += 0.01;
                if (OPC > 1)
                {
                    OPC = 1;
                }
                Opacity = OPC;
            }
            if (!ghostmode && keyData == (Keys.Subtract | Keys.Control))
            {
                OPC -= 0.01;
                if (OPC < 0)
                {
                    OPC = 0;
                }
                Opacity = OPC;
            }
            if (keyData == Keys.F4 || (keyData == (Keys.Alt | Keys.Enter)))
            {
                togglefullscreen();
            }
            if (keyData == Keys.F2)
            {
                SendMessage(Handle, WM_KEYDOWN, new IntPtr(0x2E),IntPtr.Zero);
                System.Threading.Thread.Sleep(10);
                SendMessage(Handle, WM_KEYUP, new IntPtr(0x2E), IntPtr.Zero);
            }
            if (keyData == Keys.F3)
            {
                //richTextBox1.BringToFront();
            }
            if (keyData == Keys.F5)
            {
                if (timer3.Enabled)
                {
                    timer3.Stop();
                }
                else
                {
                    AskInterval A = new AskInterval(1000.0 / timer3.Interval);
                    A.ShowDialog();
                    if (A.OK)
                    {
                        double D;
                        if (double.TryParse(A.textBox1.Text, out D))
                        {
                            int i = (int)(1000.0 / D);
                            //timer3.Interval = i;
                            Interval = i;
                            timer3.Interval = 10;
                            timer3.Start();
                        }
                    }
                }
            }
            #if DEBUG
            if (keyData == Keys.F7)
            {
                timer2.Enabled = !timer2.Enabled;
                if (timer2.Enabled)
                {
                    overlay1.BringToFront();
                }
                else
                {
                    overlay1.SendToBack();
                }
            }
#endif
            try
            {
                if (keyData == Keys.NumPad2 || keyData == Keys.NumPad1 || keyData == Keys.NumPad3)
                {
                    panel1.VerticalScroll.Value += (Height >> 1);
                }
                if (keyData == Keys.NumPad8 || keyData == Keys.NumPad7 || keyData == Keys.NumPad9)
                {
                    panel1.VerticalScroll.Value -= (Height >> 1);
                }
                if (keyData == Keys.NumPad4 || keyData == Keys.NumPad1 || keyData == Keys.NumPad7)
                {
                    panel1.HorizontalScroll.Value -= (Width >> 1);
                }
                if (keyData == Keys.NumPad6 || keyData == Keys.NumPad3 || keyData == Keys.NumPad9)
                {
                    panel1.HorizontalScroll.Value += (Width >> 1);
                }
            }
            catch
            {
            }
            
            if (keyData == Keys.F1)
            {
                HelpWindow HW = new HelpWindow();
                HW.ShowDialog();
            }
            if (keyData == Keys.F3)
            {
                if (textbuffer.Length < 4)
                {
                    textbuffer = "";
                }
                SpellCheckForm TI = new SpellCheckForm(textbuffer);
                TI.checkBox1.Checked = AutoSpellCheckActive;
                TI.ShowDialog();
                AutoSpellCheckActive = TI.checkBox1.Checked;
            }
            if (keyData == Keys.F6)
            {
                DoCapture();
            }
            if (keyData == (Keys.Menu | Keys.Alt))
            {
                //DoCapture();
                contextMenuStrip1.Show(Cursor.Position);
            }
            if (keyData == Keys.F9)
            {
                var X = Cursor.Position.X;
                var Y = Cursor.Position.Y;
                if (!fullscreen)
                {
                    X -= (Left + chrome);
                    Y -= (Top + TC);
                }
                if (TransparencyKey != Color.Empty)
                {
                    TransparencyKey = Color.Empty;
                    if (!cropmode)
                    {
                        Text = TITLE;
                    }
                    else
                    {
                        Text = "Cropping mode! left click to set top-left, right for bottom-right!";
                    }
                }
                else if (X>=0 && Y >= 0 && X < ClientSize.Width && Y < ClientSize.Height)
                {
                    
                    Bitmap B = Capture();
                    Color NC = B.GetPixel(X, Y);
                    NC = Color.FromArgb(255, NC);
                    TransparencyKey = NC;
                    B.Dispose();
                    //win32 versions
                    if (false)
                    {
                        //Opacity
                        SetLayeredWindowAttributes(Handle, 0, (byte)(OPC * 100), LWA_ALPHA);
                        //Transparencykey(it seems that it uses ABGR however)
                        SetLayeredWindowAttributes(Handle, (uint)(NC.ToArgb()), 0, LWA_COLORKEY);
                    }
                    Text = "WD:Color Sync Mode!"+TransparencyKey.ToString() + TITLE;
                }
            }
            if (keyData == Keys.F10)
            {
                opensavedata();
            }
            if (keyData == Keys.F11)
            {
                showresolutionmenu();
            }
            if (keyData == Keys.F12)
            {
                Bitmap B = _Capture();
                pictureBox1.Image = B;
                pictureBox1.Location = Point.Empty;
                pictureBox1.Size = B.Size;
                if (!cropmode)
                {
                    pictureBox1.BringToFront();
                    cropmode = true;
                    Text = "(F12)Cropping mode! left click to set top-left, right for bottom-right!";
                }
                else
                {
                    pictureBox1.SendToBack();
                    cropmode = false;
                    Text = TITLE;
                }
                if (timer2.Enabled)
                {
                    overlay1.Location = Point.Empty;
                    overlay1.BringToFront();
                    
                }
            }
            if (keyData == (Keys.R | Keys.Control))
            {
                if (Waffle == webBrowser1)
                {
                    //open create.swf with IE
                    webBrowser1.Refresh(WebBrowserRefreshOption.Completely);
                    //webBrowser1.Navigate("file://" + System.Environment.CurrentDirectory + @"/create.swf");
                }
                else
                {
                    //open create.swf in ActiveX flash player.
                    ((FlashPlayer)Waffle).Rewind();
                    //((FlashPlayer)Waffle).Movie = "file://" + System.Environment.CurrentDirectory + @"/create.swf";
                    //FlashPlayer FP = ((FlashPlayer)Waffle);
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void showresolutionmenu()
        {
            //Waffle.Size
            CustomResolution CR = new CustomResolution(Waffle.Width, Waffle.Height, customresolution);

            CR.ShowDialog();
            if (CR.CRenabled)
            {
                ///Waffle.Dock = DockStyle.None;
                customresolution = true;
                Waffle.Size = new Size(CR.width, CR.height);
                pictureBox1.Size = Waffle.Size;
                this.AutoScroll = true;
            }
            else
            {
                ///Waffle.Dock = DockStyle.Fill;
                customresolution = false;
                Size = new Size(Size.Width + (CR.width - Waffle.Width), Size.Height + (CR.height - Waffle.Height));
                this.AutoScroll = false;
            }
            panel1.AutoScroll = this.AutoScroll;
        }
        private void opensavedata()
        {
            string S = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            S = S + @"\Macromedia\Flash Player\#SharedObjects\";
            if (System.IO.Directory.Exists(S))
            {
                string[] D = System.IO.Directory.GetDirectories(S);
                if (D.Length == 1)
                {
                    S = D[0] + @"\localhost\walfas_create_savedata.sol";
                    if (System.IO.File.Exists(S))
                    {
                        System.Diagnostics.Process P = new System.Diagnostics.Process();
                        P.StartInfo = new System.Diagnostics.ProcessStartInfo("explorer", "/select," + S);
                        P.Start();
                    }
                    else
                    {
                        MessageBox.Show("could not find offline savedata.");
                    }
                }
            }
        }
        private void togglefullscreen()
        {
            /*if (!fullscreen)
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    WindowState = FormWindowState.Normal;
                }
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                last = new Rectangle(Location, Size);
                Location = new Point(0, 0);
                Size = Screen.PrimaryScreen.Bounds.Size;
                fullscreen = true;
            }
            else
            {
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                Location = last.Location;
                Size = last.Size;
                fullscreen = false;
            }*/
            if (!fullscreen)
            {
                last = new Rectangle(Location, Size);
                lastWindowState = this.WindowState;

                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                ///this.Bounds = Screen.PrimaryScreen.Bounds;
                Rectangle R = new Rectangle(Screen.PrimaryScreen.Bounds.Location, Screen.PrimaryScreen.Bounds.Size);
                using (Graphics graphics = this.CreateGraphics())
                {
                    /*return new Size((Screen.PrimaryScreen.Bounds.Width * (int)graphics.DpiX) / 96
                      , (Screen.PrimaryScreen.Bounds.Height * (int)graphics.DpiY) / 96);*/
                    R.Width = (Screen.PrimaryScreen.Bounds.Width * (int)graphics.DpiX) / 96;
                    R.Height = (Screen.PrimaryScreen.Bounds.Height * (int)graphics.DpiY) / 96;
                }
                this.Bounds = R;

                fullscreen = true;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;

                Location = last.Location;
                Size = last.Size;

                if (lastWindowState != this.WindowState)
                {
                    WindowState = lastWindowState;
                }
                fullscreen = false;
            }
            TopMost = fullscreen;
        }
        /// <summary>
        /// generic walfas container the reason its generic is to prevent run time errors when the ActiveX dll info is looked up/called.
        /// </summary>
        public Control Waffle;
        object WI;
        private void WFL()
        {
            FlashPlayer A = new FlashPlayer();
            A.Dock = DockStyle.Fill;
            panel1.Controls.Add(A);
            A.BringToFront();
            Waffle = A;
            WI = A.getinterface();
            webBrowser1.Visible = false;
            webBrowser1.SendToBack();
            //Controls.Remove(webBrowser1);
        }
        private static String HexConverter(System.Drawing.Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        private string builddocument(Color color,bool local,string quality)
        {
            string style = "margin:0;padding:0;";
            /*if (color != Color.White)
            {
                style = style+"background - color:"+HexConverter(color)+";";
            }*/
            string ret = "<html><body style=\""+style+"\">";
            ret = ret + "<!--This file was generated by ComicLayouter, it's used to configure create.swf-->";
            string path = "http://www.walfas.org/flash/create.swf";
            //string path = "< EMBED type = application / x - shockwave - flash height =\"100%\" width=\"100%\" src=" + System.Environment.CurrentDirectory + "/create.swf menu=\"false\" fullscreen=\"yes\">";
            if (local)
            {
                path = System.Environment.CurrentDirectory + "/create.swf";
            }
            string vars = "";
            if (quality != "")
            {
                vars = "quality=\"" + quality + "\"";
            }
            //ret = ret + "< EMBED type = \"application / x - shockwave - flash\" height =\"100%\" width=\"100%\" src=\"" + path + "\" "+vars+">";
            ret = ret + "<embed height =\"100%\" width=\"100%\" wmode=\"transparent\" src=\"" + path + "\" " + vars + ">";
            ret = ret + "</body></html>";
            string f = System.Environment.CurrentDirectory+"/IE-engine.html";
            if (System.IO.File.Exists(f))
            {
                System.IO.File.Delete(f);
            }
            System.IO.StreamWriter SW = new System.IO.StreamWriter(f);
            SW.Write(ret);
            SW.Close();
            return f;
        }
        private void WalfasWindow_Load(object sender, EventArgs e)
        {
            _this = this;
            Waffle = webBrowser1;
            //webBrowser1.Navigate()
            /*webBrowser1.Navigate("about:blank");
            if (webBrowser1.Document != null)
            {
                webBrowser1.Document.Write(string.Empty);
            }
            //webBrowser1.DocumentText = html;
            webBrowser1.DocumentText = "<html><body><EMBED type=application/x-shockwave-flash height=\"100%\" width=\"100%\" src="+ System.Environment.CurrentDirectory + "/create.swf menu=\"false\" fullscreen=\"yes\"></body></html>";*/
            WI = webBrowser1.Document.DomDocument;
            if (!IE)
            {
                try
                {
                    WFL();
                }
                catch (Exception ee)
                {
                    //MessageBox.Show("crash:" + ee.Message);
                }
            }
            else
            {
                customrightclick = false;
                customrightclick = true;
            }
            if (webBrowser1.Visible)
            {
                Waffle = webBrowser1;
                WI = webBrowser1.Document.DomDocument;
            }
            else
            {
                //TITLE = "Flash:" + TITLE;
            }
            Text = TITLE;
            if (!System.IO.File.Exists("create.swf"))
            {
                //ask to download create.swf if they dont have it.
                if (MessageBox.Show("\"create.swf\" was not found in this directory.\n\nWould you like to download it to this folder?\n(if not create.swf will run in online mode)", "create.swf not found", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    bool ok = false;
                    try
                    {
                        System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
                        string url = @"http://www.walfas.org/flash/create.swf";
                        string file = "create.swf";
                        bool exD = false;
                        if (MessageBox.Show("Would you like to download the extended version of create.swf?\nIf not then the original will be downloaded instead.","Download create.swf extended?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            url = @"https://github.com/RSGmaker/Create.swf-stuff/raw/master/create.swf/create.swf";
                            file = "create.swf extended";
                            exD = true;
                        }
                        System.Net.WebClient Client = new System.Net.WebClient();
                        Client.DownloadFile(url, System.Environment.CurrentDirectory + @"/create.swf");
                        Client.Dispose();
                        System.IO.FileInfo F = new System.IO.FileInfo(System.Environment.CurrentDirectory + @"/create.swf");
                        if (F.Length > 1000)
                        {
                            if (!exD || System.IO.File.Exists("dictionary.txt"))
                            {
                                MessageBox.Show(file + " has finished downloading", "Success");
                            }
                            else
                            {
                                if (MessageBox.Show(file + " has finished downloading.\n\nCreate.swf extended has built-in support for spellchecking, would you like ComicLayouter to download the \"dictionary.txt\" needed to enable this feature?", "Download spellchecker dictionary?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    Client = new System.Net.WebClient();
                                    Client.DownloadFile(@"https://github.com/RSGmaker/Create.swf-stuff/raw/master/create.swf/dictionary.txt", System.Environment.CurrentDirectory + @"/dictionary.txt");
                                    Client.Dispose();
                                    F = new System.IO.FileInfo(System.Environment.CurrentDirectory + @"/dictionary.txt");
                                    file = "create.swf extended spellchecker dictionary";
                                    if (F.Length > 1000)
                                    {
                                        MessageBox.Show(file + " has finished downloading", "Success");
                                    }
                                }
                            }
                            ok = true;
                        }
                    }
                    catch
                    {
                    }
                    if (!ok)
                    {
                        MessageBox.Show("There was a problem downloading create.swf","Download error");
                    }
                }
            }
            if (System.IO.File.Exists("create.swf"))
            {
                if (Waffle == webBrowser1)
                {
                    this.Icon = Properties.Resources.IE;
                    //open create.swf with IE
                    webBrowser1.Navigate("file://" + builddocument(Color.Blue,true,""));
                    //webBrowser1.Navigate("file://" + System.Environment.CurrentDirectory + @"/create.swf");
                    /*dynamic D = webBrowser1.ActiveXInstance;
                    D.Quality = "Low";*/
                }
                else
                {
                    //open create.swf in ActiveX flash player.
                    ((FlashPlayer)Waffle).Movie = "file://" + System.Environment.CurrentDirectory + @"/create.swf";
                    FlashPlayer FP = ((FlashPlayer)Waffle);
                }
                var Csize = new System.IO.FileInfo("create.swf").Length;
                var VanillaCreateSwfSize = 3473857;
                AutoSpellCheckActive = (Csize == VanillaCreateSwfSize) || !System.IO.File.Exists("dictionary.txt");
            }
            else
            {
                if (Waffle == webBrowser1)
                {
                    //open create.swf with IE
                    //webBrowser1.Navigate(@"http://www.walfas.org/flash/create.swf");
                    webBrowser1.Navigate("file://" + builddocument(Color.White, false, ""));
                }
                else
                {
                    //open create.swf in ActiveX flash player.
                    ((FlashPlayer)Waffle).Movie = @"http://www.walfas.org/flash/create.swf";
                }
                AutoSpellCheckActive = true;
            }
            pictureBox1.Dock = DockStyle.None;
            ///Waffle.Dock = DockStyle.Fill;
            Waffle.Location = new Point(0, 0);
            Waffle.Size = ClientSize;
            pictureBox1.SendToBack();
            
            Opacity = OPC;
            #if DEBUG
            overlay1.SendToBack();
#else
            panel1.Controls.Remove(overlay1);
#endif
            normalstyle = GetWindowLong(Handle, GWL_EXSTYLE);
        }
        public Rectangle Crop = Rectangle.Empty;
        public Bitmap Cframe;
        public Bitmap Capture()
        {
            Bitmap B = Cframe;
            if (B == null)
            {
                B = _Capture();
            }
            //Bitmap B = _Capture();
            Rectangle FC = new Rectangle(Crop.X, Crop.Y, Crop.Width, Crop.Height);
            if (FC.Right > B.Width)
            {
                FC.Width = B.Width - FC.X;
            }
            if (FC.Bottom > B.Height)
            {
                FC.Height = B.Height - FC.Y;
            }
            if (FC.Width <= 0 || FC.Height <= 0)
            {
                return (Bitmap)B.Clone();
            }
            try
            {
                Bitmap ret = new Bitmap(FC.Width, FC.Height);

                Graphics G = Graphics.FromImage(ret);
                G.DrawImage(B, new Rectangle(0, 0, FC.Width, FC.Height), FC, GraphicsUnit.Pixel);
                G.Dispose();

                return ret;
            }
            catch
            {
            }
            return null;
        }
        public Bitmap _Capture()
        {
            EyeOpen.Imaging.HtmlToBitmapConverter H = new EyeOpen.Imaging.HtmlToBitmapConverter();
            //return H.GetBitmapFromControl(webBrowser1, webBrowser1.Size);
            //return H.GetBitmapFromControl2(axShockwaveFlash1.GetOcx(), Waffle.Size);
            if (Waffle == webBrowser1)
            {
                return H.GetBitmapFromControl((WebBrowser)Waffle,Waffle.Size);
            }
            return H.GetBitmapFromControl2(WI, Waffle.Size);

            Bitmap bitmap = new Bitmap(webBrowser1.Width, webBrowser1.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Point P = new Point(Location.X, Location.Y);
            int W = Width - webBrowser1.Width;
            int W2 = W / 2;
            P.X += W2;
            P.Y += Height;
            P.Y -= W2;
            P.Y -= webBrowser1.Height;
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(P, Point.Empty, webBrowser1.Size);
                }
                return bitmap;
            }
        }


        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Crop.Location = e.Location;
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Crop.Size = new Size(e.X - Crop.Location.X, e.Y - Crop.Location.Y);
            }
            Invalidate();
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //draw the crop bounds
            if (!capt)
            {
                Brush B = new SolidBrush(Color.FromArgb(192, Color.Black));
                Rectangle[] R = new Rectangle[9];
                int X = Crop.Left;
                int Y = Crop.Top;
                //top left
                if (Crop.Width > 0 && Crop.Height > 0)
                {
                    R[0] = new Rectangle(0, 0, Crop.X, Crop.Y);
                    R[1] = new Rectangle(Crop.X, 0, Crop.Width, Crop.Y);
                    R[2] = new Rectangle(Crop.Right, 0, Waffle.Width - Crop.Right, Crop.Y);


                    R[3] = new Rectangle(0, Crop.Y, Crop.X, Crop.Height);
                    R[4] = new Rectangle(Crop.Right, Crop.Y, Waffle.Width - Crop.Right, Crop.Height);


                    R[5] = new Rectangle(0, Crop.Bottom, Crop.X, Waffle.Height - Crop.Bottom);
                    R[6] = new Rectangle(Crop.X, Crop.Bottom, Crop.Width, Waffle.Height - Crop.Bottom);
                    R[7] = new Rectangle(Crop.Right, Crop.Bottom, Waffle.Width - Crop.Right, Waffle.Height - Crop.Bottom);

                    
                }
                if (fullscreen)
                {
                    R[8] = new Rectangle(new Point((MPos.X - 16) + panel1.HorizontalScroll.Value, (MPos.Y - 16) + panel1.VerticalScroll.Value), new Size(32, 32));
                }
                e.Graphics.FillRectangles(B, R);
                Pen P = Pens.Blue;
                e.Graphics.DrawLine(P, new Point(Crop.X - 32, Crop.Y), new Point(Crop.X + 32, Crop.Y));
                e.Graphics.DrawLine(P, new Point(Crop.X, Crop.Y - 32), new Point(Crop.X, Crop.Y + 32));
                if (Crop.Width > 0 && Crop.Height > 0)
                {
                    P = Pens.Red;
                    e.Graphics.DrawLine(P, new Point(Crop.Right - 32, Crop.Bottom), new Point(Crop.Right + 32, Crop.Bottom));
                    e.Graphics.DrawLine(P, new Point(Crop.Right, Crop.Bottom - 32), new Point(Crop.Right, Crop.Bottom + 32));
                }
                //Cursor.Position.X
                
            }
        }

        private void WalfasWindow_Resize(object sender, EventArgs e)
        {
            if (cropmode)
            {
                Bitmap B = _Capture();
                pictureBox1.Image = B;
            }
            else if (!customresolution)
            {
                pictureBox1.Location = new Point(0, 0);
                pictureBox1.Size = this.ClientSize;
                
                Waffle.Location = pictureBox1.Location;
                Waffle.Size = pictureBox1.Size;
            }
        }

        public const int WS_EX_LAYERED = 0x80000;
        public const int LWA_ALPHA = 0x2;
        public const int LWA_COLORKEY = 0x1;

        public const int WS_EX_TRANSPARENT = 0x00000020;
        public const int GWL_EXSTYLE = (-20);
        int normalstyle;
        bool ghostmode = false;

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        [DllImport("user32.dll")]
        static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
        
        /// <summary>
        /// Checks if the window should become ghosted.
        /// ghosting is for making the walfasdesktop feature less of a pain to use when you need to manage multiple windows.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (cropmode && LP != Cursor.Position)
            {
                //Invalidate();
                pictureBox1.Invalidate();
            }
            if (fullscreen && TransparencyKey != Color.Empty)
            {
                if (ghostmode && Cursor.Position.X == 0 && Cursor.Position.Y == 0)
                {
                    Opacity = OPC;
                    ghostmode = false;
                    Focus();
                    SetWindowLong(Handle, GWL_EXSTYLE, normalstyle);
                }
                else if (!ghostmode && Cursor.Position.X >= Screen.PrimaryScreen.Bounds.Width - 1 && Cursor.Position.Y >= Screen.PrimaryScreen.Bounds.Height - 1)
                {
                    Opacity = OPC * 0.2;
                    ghostmode = true;
                    int extendedStyle = GetWindowLong(Handle, GWL_EXSTYLE);
                    normalstyle = GetWindowLong(Handle, GWL_EXSTYLE);
                    SetWindowLong(Handle, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & System.Windows.Forms.MouseButtons.Left) == System.Windows.Forms.MouseButtons.Left)
            {
                if (pictureBox1.Bounds.Contains(e.Location))
                {
                    Crop.Location = e.Location;
                }
            }
            if ((e.Button & System.Windows.Forms.MouseButtons.Right) == System.Windows.Forms.MouseButtons.Right)
            {
                if (pictureBox1.Bounds.Contains(e.Location))
                {
                    Crop.Size = new Size(e.X - Crop.Location.X, e.Y - Crop.Location.Y);
                }
            }
            string coords = "X:" + Crop.X + " Y:" + Crop.Y + " Width:" + Crop.Width + " Height:" + Crop.Height;
            if (Crop.Width < 1 || Crop.Height < 1)
            {
                coords = "No crop set";
            }
            Text = "(F12) " + coords + " use left & right mouse buttons to set crop";
            toolTip1.ToolTipTitle = Text;
            if (toolTip1.Active)
            {
                //
            }
            
        }
        public MouseButtons MB=MouseButtons.None;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            MB = MB | e.Button;
            if (fullscreen && MB != System.Windows.Forms.MouseButtons.None)
            {
                toolTip1.Show("Cropping Info", this, 0, 0);
                toolTip1.Active = true;
                toolTip1.ToolTipTitle = Text;
                toolTip1.Show("Cropping Info", this, 0, 0);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            {
                toolTip1.Hide(this);
            }
            MB -= (e.Button & MB);
            Text = TITLE;
        }
        #if DEBUG
        public Point[] Gen(Size S,Random R,int total)
        {
            List<Point> L = new List<Point>();
            while (total > 0)
            {
                L.Add(new Point((int)(S.Width * R.NextDouble()), (int)(S.Height * R.NextDouble())));
                total--;
            }
            return L.ToArray();
        }
#endif

        private void timer2_Tick(object sender, EventArgs e)
        {
            #if DEBUG
            Image I = Cframe;
            //webBrowser1.Invalidate();
            Cframe = _Capture();
            if (Cframe != null && !Cframe.Size.IsEmpty)
            {
                /*Graphics G = Graphics.FromImage(Cframe);
                SolidBrush S = new SolidBrush(TransparencyKey);
                Random R = new Random();
                G.FillClosedCurve(S, Gen(Size, R, 9));
                G.Dispose();*/
                //G.FillClosedCurve(S, Gen(Cframe.Size, R, 9));// --Produces a virus alert
                overlay1.Image = Cframe;
                //overlay1.Invalidate();
                overlay1.Size = Cframe.Size;
                overlay1.Location = new Point(0, 1);
                overlay1.Invalidate();
                if (I != null)
                {
                    I.Dispose();
                }
                
            }
            else
            {
                Cframe.Dispose();
                Cframe = (Bitmap)I;
            }
#endif
        }
        public DateTime LastCapture;
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (LastCapture == null || DateTime.Now.Subtract(LastCapture).TotalMilliseconds > Interval)
            {
                DoCapture();
                LastCapture = DateTime.Now;
            }
        }
        public int vscroll;

        private void WalfasWindow_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Click(object sender, EventArgs e)
        {
            SC.Text = "";
        }

        private void WalfasWindow_Click(object sender, EventArgs e)
        {
            SC.Text = "";
        }

        private void WalfasWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            _this = null;
        }

        private void fullScreenModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            togglefullscreen();
        }

        private void httpwalfasorgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.walfas.org/");
        }

        private void openCreateswfSaveFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            opensavedata();
        }

        private void takeSnapshotF6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCapture();
        }

        private void walfasAndComicLayouterControlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpWindow HW = new HelpWindow();
            HW.ShowDialog();
        }

        private void spellCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void changeWindowSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showresolutionmenu();
        }

        private void lowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mediumToolStripMenuItem.Checked = false;
            highToolStripMenuItem.Checked = false;
            lowToolStripMenuItem.Checked = true;
            if (!IE)
            {
                FlashPlayer FP = (FlashPlayer)Waffle;
                FP.Quality = "Low";
            }
            else
            {
                if (MessageBox.Show("You are running create.swf in the IE engine.\nWe cannot edit the quality at run time externally.\nWould you still like to apply this setting?(create.swf will restart)", "Apply Setting", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    webBrowser1.Navigate("file://" + builddocument(Color.White, System.IO.File.Exists("create.swf"), "Low"));
                }
            }
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mediumToolStripMenuItem.Checked = true;
            highToolStripMenuItem.Checked = false;
            lowToolStripMenuItem.Checked = false;
            if (!IE)
            {
                FlashPlayer FP = (FlashPlayer)Waffle;
                FP.Quality = "Medium";
            }
            else
            {
                if (MessageBox.Show("You are running create.swf in the IE engine.\nWe cannot edit the quality at run time externally.\nWould you still like to apply this setting?(create.swf will restart)", "Apply Setting", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    webBrowser1.Navigate("file://" + builddocument(Color.White, System.IO.File.Exists("create.swf"), "Medium"));
                }
            }
        }

        private void highToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mediumToolStripMenuItem.Checked = false;
            highToolStripMenuItem.Checked = true;
            lowToolStripMenuItem.Checked = false;
            if (!IE)
            {
                FlashPlayer FP = (FlashPlayer)Waffle;
                FP.Quality = "High";
            }
            else
            {
                if (MessageBox.Show("You are running create.swf in the IE engine.\nWe cannot edit the quality at run time externally.\nWould you still like to apply this setting?(create.swf will restart)", "Apply Setting", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    webBrowser1.Navigate("file://" + builddocument(Color.White, System.IO.File.Exists("create.swf"), "High"));
                }
            }
        }
        public void rightclick()
        {
            lastposition = Cursor.Position;
            contextMenuStrip1.Show(this, new System.Drawing.Point(Cursor.Position.X - (Left + chrome), Cursor.Position.Y - (Top + TC)));
        }

        private void changeBackgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!IE)
                {
                    FlashPlayer FP = (FlashPlayer)Waffle;
                    FP.BackgroundColor = colorDialog1.Color;
                }
                else
                {
                    webBrowser1.Document.BackColor = colorDialog1.Color;
                }
            }
        }

        private void getImageURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var T = openFileDialog1.FileName;
                Clipboard.SetText(T);
                T = "Copied URL to clipboard:" + T;
                /*toolTip1.SetToolTip(Waffle, T);
                toolTip1.Show(T ,Waffle,2000);*/
            }
        }

        private void WalfasWindow_Leave(object sender, EventArgs e)
        {
            //Remove focus from waffle, to prevent background inputs.
            pictureBox1.Focus();
        }

        private void WalfasWindow_Enter(object sender, EventArgs e)
        {
            Waffle.Focus();
        }
    }
}
