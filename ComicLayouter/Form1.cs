using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;
using System.Reflection;
using System.Drawing.Imaging;
using System.Management;
using System.Linq;

namespace ComicLayouter
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// The source images of the ComicPanels.
        /// </summary>
        public List<Bitmap> img = new List<Bitmap>();
        public ComicPanel _SelectedPanel;
        public ComicPanel SelectedPanel
        {
            get
            {
                if (_SelectedPanel != null && panel1.Controls.Contains(_SelectedPanel))
                {
                    return _SelectedPanel;
                }
                if (panel1.Controls.Count > 0)
                {
                    _SelectedPanel = (ComicPanel)panel1.Controls[0];
                    return _SelectedPanel;
                }
                return null;
            }
            set
            {
                _SelectedPanel = value;
            }
        }
        /// <summary>
        /// whether autoclipboard is active or not
        /// </summary>
        public bool CaptureMode = true;
        public CropControl Cropper;
        public BorderControl Borders;
        /// <summary>
        /// default delay set for gifs.
        /// </summary>
        public int defaultdelay = 500;
        /// <summary>
        /// size to stretch rendered comic/gif to
        /// </summary>
        public Size Stretch=Size.Empty;
        string version;
        public FormWindowState fs;
        /// <summary>
        /// max panel width, used in resize calculations.
        /// </summary>
        public int MW;

        //andysnap autoclipboard feature(currently disabled)
        public string andysnapfolder = "";
        public List<string> andysnapfiles = new List<string>();
        public int andysnapdelay = 0;

        /// <summary>
        /// true if user clicked save as animation, this is used to determine if png is an apng.
        /// </summary>
        public bool saveanimation = false;


        public void SetVersion(FileVersionInfo s)
        {
            string V = s.FileMajorPart + "." + s.FileMinorPart;
            if (s.FilePrivatePart > 0)
            {
                string A = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                version = V + A[s.FilePrivatePart-1];
            }
            else
            {
                version = V;
            }
        }
        /// <summary>
        /// experimental, unused
        /// </summary>
        /// <param name="Image"></param>
        /// <returns></returns>
        public static byte[] ConvertGif(Bitmap Image)
        {

            System.IO.MemoryStream objStream = new System.IO.MemoryStream();
            ImageCodecInfo objImageCodecInfo = GetEncoderInfo("image/gif");
            EncoderParameters objEncoderParameters;
            try
            {
                if (Image == null)
                    throw new Exception("ImageObject is not initialized.");
                objEncoderParameters = new EncoderParameters(2);
                objEncoderParameters.Param[0] = new EncoderParameter(Encoder.Compression,
                 (long)EncoderValue.CompressionLZW);
                objEncoderParameters.Param[1] = new EncoderParameter(Encoder.Quality, 0L);
                //objEncoderParameters.Param[2] = new EncoderParameter(Encoder.ColorDepth, 24L);
                Image.Save(objStream, objImageCodecInfo, objEncoderParameters);
            }
            catch
            {
                throw;
            }
            return objStream.ToArray();
        }
        /// <summary>
        /// experimental, unused
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
        /// <summary>
        /// experimental, unused
        /// </summary>
        /// <param name="B"></param>
        /// <param name="BW"></param>
        /// <param name="delay"></param>
        public void WriteGifImg(Bitmap B, System.IO.BinaryWriter BW, int delay)
        {
            WriteGifImg(ConvertGif(B), BW, delay);
        }
        /// <summary>
        /// experimental, unused
        /// </summary>
        /// <param name="B"></param>
        /// <param name="BW"></param>
        /// <param name="delay"></param>
        public void WriteGifImg(byte[] B, System.IO.BinaryWriter BW,int delay)
        {
            byte[] Delay = { (byte)(delay >> 8), (byte)(delay & 255) };
            B[785] = Delay[0]; //5 secs delay
            B[786] = Delay[1];
            B[798] = (byte)(B[798] | 0X87);
            BW.Write(B, 781, 18);
            BW.Write(B, 13, 768);
            BW.Write(B, 799, B.Length - 800);
        }
        public Form1()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            SetVersion(fvi);
            InitializeComponent();
            Text = Text.Replace("ComicLayouter", "ComicLayouter" + version);
            TITLE = Text;
            Cropper = new CropControl(this);
            Borders = new BorderControl();
            panel1.BackColor = Borders.SeperatorColor;
            fs = WindowState;
        }
        public Form1(string[] arg)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            SetVersion(fvi);
            InitializeComponent();
            if (arg != null && arg.Length>0)
            {
                LoadImages(combine(arg));
                redopanel();
            }
            Text = Text.Replace("ComicLayouter", "ComicLayouter" + version);
            TITLE = Text;
            Cropper = new CropControl(this);
            Borders = new BorderControl();
            panel1.BackColor = Borders.SeperatorColor;
            fs = WindowState;
        }
        /// <summary>
        /// The default text for the titlebar, used for resetting the title after displaying a message.
        /// </summary>
        public string TITLE;

        /// <summary>
        /// combine commandline argument strings into something usable.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string[] combine(string[] str)
        {
            if (str.Length == 0)
            {
                return null;
            }
            List<string> ret = new List<string>();
            string s = "";
            for (int i = 0; i < str.Length; i++)
            {
                s = s + str[i];
                if (System.IO.File.Exists(s))
                {
                    ret.Add(s);
                    s = "";
                }
            }
            return ret.ToArray();
        }

        public Size CropSize(Size size)
        {
            return new Size((int)(size.Width * (Cropper.IWidth * 0.01)), (int)(size.Height * (Cropper.IHeight * 0.01)));
        }

        public Bitmap Crop(Bitmap B)
        {
            return Cropper.Crop(B);
            if (Cropper.IWidth == 100 && Cropper.IHeight == 100)
            {
                return B;
            }
            else
            {
                Size s = CropSize(B.Size);
                Bitmap ret = new Bitmap(s.Width,s.Height);
                Graphics G = Graphics.FromImage(ret);
                int W = B.Width - ret.Width;
                int H = B.Height - ret.Height;
                int W2 = W / 2;
                int H2 = H / 2;
                int W3 = W2;
                int H3 = H2;
                if (Cropper.anchorH == 0)
                {
                    W3 = 0;
                }
                if (Cropper.anchorV == 0)
                {
                    H3 = 0;
                }
                if (Cropper.anchorH == 2)
                {
                    W3 = B.Width-W;
                }
                if (Cropper.anchorV == 2)
                {
                    H3 = B.Height - H;
                }

                G.DrawImage(B, new Rectangle(new Point(0, 0), ret.Size), new Rectangle(new Point(W3, H3), ret.Size), GraphicsUnit.Pixel);
                G.Dispose();
                return ret;
            }
        }
        /// <summary>
        /// returns the bitmap back as is if no effects are needed(no new bitmap generated)
        /// if effects are needed it does them, but it first scales the image to half size to reduce memory usage.
        /// </summary>
        /// <param name="B"></param>
        /// <returns></returns>
        public Bitmap GetThumbnail(Bitmap B)
        {
            
            //if ((B.Width >> 1) > panel1.Width)
            if ((Borders.Outline == 0 && Borders.Border.IsEmpty && Borders.Seperator == 0 && Cropper.IWidth == 100 && Cropper.IHeight == 100) || (((B.Width >> 1) > panel1.Width) || (B.Height) > panel1.Height))
            {
                return Borderify(Crop(B));
            }
            else
            {
                Size s = new Size(Borders.Border.Width >> 1, Borders.Border.Height >> 1);
                B = Borderify(Crop(new Bitmap(B,B.Width >> 1, B.Height >> 1)),s,Borders.Outline >> 1);
                return B;

            }
        }
        public Bitmap GetStretch(Bitmap B)
        {
            if (Stretch.IsEmpty || (Stretch.Height<=0 && Stretch.Width <= 0))
            {
                return B;
            }
            else
            {
                if (Stretch.Width > 0 && Stretch.Height > 0)
                {
                    return new Bitmap(B, Stretch);
                }
                if (Stretch.Width > 0 && Stretch.Height <= 0)
                {
                    float f = Stretch.Width / ((float)B.Width);
                    return new Bitmap(B, new Size(Stretch.Width,(int)(B.Height * f)));
                }
                if (Stretch.Width <= 0 && Stretch.Height > 0)
                {
                    float f = Stretch.Height / ((float)B.Height);
                    return new Bitmap(B, new Size((int)(B.Width * f),Stretch.Height));
                }
                return new Bitmap(B, Stretch);
            }
        }
        public Bitmap Borderify(Bitmap B)
        {
            return Borderify(B, Borders.Border,Borders.Outline);
        }
        public Bitmap Borderify(Bitmap B,Size border,int Outline)
        {
            //return new Bitmap(B);
            if (border.Width == 0 && border.Height == 0)
            {
                //return new Bitmap(B);
                return GetStretch(B);
            }
            else
            {
                int BW = border.Width * 2;
                int BH = border.Height * 2;
                int OL = Outline * 2;

                Bitmap ret = new Bitmap(B.Width + (BW) + (OL), B.Height + (BH) + (Outline * 2));
                Graphics G = Graphics.FromImage(ret);
                G.Clear(Borders.BorderColor);
                G.FillRectangle(new SolidBrush(Borders.OutlineColor), new Rectangle(border.Width, border.Height, B.Width + (OL), B.Height + (OL)));
                //G.DrawRectangle(new Pen(OutlineColor),new Rectangle(Border.Width,Border.Height,B.Width+1,B.Height+1));

                G.DrawImage(B, new Rectangle(new Point(border.Width + Outline, border.Height + Outline), B.Size), new Rectangle(Point.Empty, B.Size), GraphicsUnit.Pixel);
                G.Dispose();
                //return new Bitmap(ret);
                return GetStretch(ret);
                /*Bitmap ret = new Bitmap(B.Width + (BW) + (OL), B.Height + (BH) + (OL));
                Graphics G = Graphics.FromImage(ret);
                G.Clear(Borders.BorderColor);
                G.FillRectangle(new SolidBrush(Borders.OutlineColor), new Rectangle(border.Width, border.Height, B.Width + (OL), B.Height + (OL)));
                //G.DrawRectangle(new Pen(Borders.OutlineColor),new Rectangle(Borders.Border.Width,Borders.Border.Height,B.Width+1,B.Height+1));

                G.DrawImage(B, new Rectangle(new Point(border.Width+Borders.Outline, border.Height+Outline), B.Size), new Rectangle(Point.Empty, B.Size), GraphicsUnit.Pixel);
                G.Dispose();
                //return new Bitmap(ret);
                return GetStretch(ret);*/
            }
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                LoadImages(openFileDialog1.FileNames);
                redopanel();
            }
        }
        /// <summary>
        /// an object used for thread locking while gif encoding is in process.
        /// </summary>
        public Object giflock;
        public int gifprogress;
        public float gifpercent;
        public int gifquality;
        public int gifdelay;
        /// <summary>
        /// may need to change later, used to determine if height needs to maintain aspect ratio when width resizes itself
        /// </summary>
        public bool scalemismatch
        {
            get
            {
                return widthMismatchStretchingToolStripMenuItem.Checked;
            }
        }
        /// <summary>
        /// Creates a comic from the loaded comicpanels
        /// </summary>
        /// <returns></returns>
        private Bitmap CompileComic()
        {
            return CompileComic(img);
        }
        /// <summary>
        /// takes a list of bitmaps with the border and crop settings and creats a comic
        /// </summary>
        /// <param name="IMG"></param>
        /// <returns></returns>
        private Bitmap CompileComic(List<Bitmap> IMG)
        {
            int X = 0;
            int Y = 0;
            int BW = Borders.Border.Width;
            int BH = Borders.Border.Height;
            int BW2 = Borders.Border.Width * 2;
            int BH2 = Borders.Border.Height * 2;
            Bitmap[] LB = new Bitmap[IMG.Count];
            for (int i = 0; i < IMG.Count; i++)
            {
                Bitmap B = IMG[i];
                if (Cropper.IWidth != 100 || Cropper.IHeight != 100)
                {
                    B = Crop(B);
                }
                LB[i] = Borderify(B);
                Size S = LB[i].Size;
                X = Math.Max(X, S.Width);

            }
            for (int i = 0; i < IMG.Count; i++)
            {
                Size S = LB[i].Size;
                if (scalemismatch)
                {
                    double D = (((double)X) / S.Width);
                    S.Height = (int)(S.Height * D);
                }
                Y += S.Height;
                if (Borders.Seperator <= 0)
                {
                    Y -= BH2;
                }
                if (i < IMG.Count - 1)
                {
                    Y += Borders.Seperator;
                }

            }

            Bitmap ret = new Bitmap(X, Y);
            Graphics G = Graphics.FromImage(ret);
            G.Clear(Borders.SeperatorColor);
            Y = 0;
            for (int i = 0; i < IMG.Count; i++)
            {
                Bitmap B = LB[i];
                if (scalemismatch)
                {
                    double D = (((double)X / B.Width));
                    G.DrawImage(B, 0, Y, X, (int)(B.Height * D));
                }
                else
                {
                    G.DrawImage(B, 0, Y, B.Width, B.Height);
                }
                Y += B.Height;

                if (Borders.Seperator <= 0)
                {
                    Y -= BH2;
                }
                if (i < IMG.Count - 1)
                {
                    Y += Borders.Seperator;
                }
            }
            G.Dispose();
            return ret;
        }
        /// <summary>
        /// the save button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //saveanimation = false;
            //saveFileDialog1.Filter = "comic png|*.png";
            if (img.Count>0 && saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                if (saveFileDialog1.FileName.ToLower().EndsWith(".png"))
                {
                    if (!saveanimation)
                    {
                        Bitmap B = CompileComic();
                        B.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    else
                    {
                        if (!System.IO.File.Exists("apngasm.exe"))
                        {
                            if (MessageBox.Show(this, "apngasm.exe is missing, comiclayouter needs this program to create animated png files.\n\nDo you want to go to the website to get it?", "Missing apng assembler", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                            {
                                MessageBox.Show("Make sure not to get the gui version of apngasm(bin-win32.zip).\nPlace apngasm.exe into the comiclayouter directory once downloaded");
                                System.Diagnostics.Process.Start("http://sourceforge.net/projects/apngasm/files/");
                            }
                            
                            return;
                        }
                        string tmp = System.IO.Path.GetTempPath() + "CL\\";
                        if (System.IO.Directory.Exists(tmp))
                        {
                            //make sure directory is cleared before using.
                            System.IO.Directory.Delete(tmp,true);
                        }
                        if (!System.IO.Directory.Exists(tmp))
                        {
                            System.IO.Directory.CreateDirectory(tmp);
                        }
                        //
                        int DDel = defaultdelay;
                        if (DDel <= 0)
                        {
                            DDel = 500;
                        }
                        //int defaultfps = 1000 / DDel;
                        int defaultfps = 100000 / DDel;
                        if (defaultfps <= 0)
                        {
                            defaultfps = 1000;
                        }
                        int D = ((ComicPanel)panel1.Controls[0]).delay;
                        if (D > 9)
                        {
                            DDel = D;
                        }
                        for (int i = 0; i < img.Count; i++)
                        {
                            //M.SetLength(0);
                            D = ((ComicPanel)panel1.Controls[i]).delay;
                            string si = "" + i;
                            //ensure proper numerical sorting will work as expected.
                            si.PadLeft(4, '0');
                            if (D > 9)
                            {
                                DDel = D;
                            }
                            if (Cropper.IWidth == 100 && Cropper.IHeight == 100)
                            {
                                Borderify(img[i]).Save(tmp + si + ".png");
                            }
                            else
                            {
                                Borderify(Crop(img[i])).Save(tmp + si + ".png");
                            }
                            System.IO.StreamWriter SW = new System.IO.StreamWriter(tmp + si + ".txt");
                            int fps = 100000 / DDel;
                            if (fps <= 0)
                            {
                                fps = defaultfps;
                            }
                            SW.WriteLine("delay=100/" + fps);
                            SW.Close();
                            //B = ConvertGif(Borderify(Crop(img[i])));
                        }
                        //
                        if (System.IO.File.Exists("output.png"))
                        {
                            System.IO.File.Delete("output.png");
                        }
                        Process P = new Process();
                        //P.StartInfo = new ProcessStartInfo("apngasm.exe", "output.png "+tmp + "*.png 100 " + defaultfps + " -kc");
                        P.StartInfo = new ProcessStartInfo("apngasm.exe", "output.png " + tmp + "*.png 100 " + defaultfps + "");
                        P.StartInfo.UseShellExecute = false;
                        P.Start();
                        Hide();
                        P.WaitForExit();
                        
                        if (System.IO.File.Exists("output.png"))
                        {
                            if (System.IO.File.Exists(saveFileDialog1.FileName))
                            {
                                System.IO.File.Delete(saveFileDialog1.FileName);
                            }
                            System.IO.File.Copy("output.png", saveFileDialog1.FileName);
                            //MessageBox.Show("Animated png was saved successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Failed to create animated png.");
                        }
                        Show();
                    }
                }
                else if (saveFileDialog1.FileName.ToLower().EndsWith(".gif"))
                {
                    Save_Gif S = new Save_Gif();
                    S.textBox1.Text = "" + defaultdelay;
                    S.ShowDialog();
                    if (S.OK)
                    {
                        if (GP != null && !GP.IsDisposed)
                        {
                            GP.Close();
                        }
                        System.Threading.Thread Th = new System.Threading.Thread(encode);
                        gifprogress = 0;
                        for (int i = 0; i < Controls.Count; i++)
                        {
                            Controls[i].Enabled = false;
                        }
                        UseWaitCursor = true;
                        giflock = new object();
                        gifprogress = 0;
                        gifdelay = S.delay;
                        gifpercent = 0;
                        //quality is reversed this flips it to normal
                        gifquality = (20 - S.quality);
                        Th.Start();
                        timer1.Start();
                        
                    }
                }
            }
        }
        private byte[] GifAnimation = { 33, 255, 11, 78, 69, 84, 83, 67, 65, 80, 69, 50, 46, 48, 3, 1, 0, 0, 0 };
        /// <summary>
        /// starts encoding the panels as an animated gif.
        /// </summary>
        public void encode()
        {
            if (false)
            {
                //more experimental gif encoding
                //i never did find a good encoder :/

                //Variable declaration
                //StringCollection stringCollection;
                /*System.IO.MemoryStream memoryStream;
                System.IO.BinaryWriter binaryWriter;
                Image image;
                Byte[] buf1;
                Byte[] buf2;
                Byte[] buf3;
                //Variable declaration

                //stringCollection = a_StringCollection_containing_images;

                //Response.ContentType = "Image/gif";
                memoryStream = new System.IO.MemoryStream();
                buf2 = new Byte[19];
                buf3 = new Byte[8];
                buf2[0] = 33;  //extension introducer
                buf2[1] = 255; //application extension
                buf2[2] = 11;  //size of block
                buf2[3] = 78;  //N
                buf2[4] = 69;  //E
                buf2[5] = 84;  //T
                buf2[6] = 83;  //S
                buf2[7] = 67;  //C
                buf2[8] = 65;  //A
                buf2[9] = 80;  //P
                buf2[10] = 69; //E
                buf2[11] = 50; //2
                buf2[12] = 46; //.
                buf2[13] = 48; //0
                buf2[14] = 3;  //Size of block
                buf2[15] = 1;  //
                buf2[16] = 0;  //
                buf2[17] = 0;  //
                buf2[18] = 0;  //Block terminator
                buf3[0] = 33;  //Extension introducer
                buf3[1] = 249; //Graphic control extension
                buf3[2] = 4;   //Size of block
                buf3[3] = 9;   //Flags: reserved, disposal method, user input, transparent color
                buf3[4] = 2;  //Delay time low byte
                buf3[5] = 0;   //Delay time high byte
                buf3[6] = 255; //Transparent color index
                buf3[7] = 0;   //Block terminator
                System.IO.FileStream F = System.IO.File.Create(saveFileDialog1.FileName);
                binaryWriter = new System.IO.BinaryWriter(F);
                for (int i = 0; i < img.Count; i++)
                {
                    //image = Bitmap.FromFile(stringCollection[picCount]);
                    //image.Save(memoryStream, ImageFormat.Gif);
                    byte[] B = ConvertGif((Bitmap)img[i]);
                    memoryStream.Write(B, 0, B.Length);
                    buf1 = memoryStream.ToArray();

                    if (i == 0)
                    {
                        //only write these the first time....
                        binaryWriter.Write(buf1, 0, 781); //Header & global color table
                        binaryWriter.Write(buf2, 0, 19); //Application extension
                    }

                    binaryWriter.Write(buf3, 0, 8); //Graphic extension
                    binaryWriter.Write(buf1, 781, buf1.Length - 782); //Image data

                    if (i == img.Count - 1)
                    {
                        //only write this one the last time....
                        binaryWriter.Write(0x3B); //Image terminator
                    }

                    memoryStream.SetLength(0);
                }
                binaryWriter.Close();
                //M.WriteTo(F);
                F.Close();*/
                //Response.End();
                System.IO.MemoryStream M = new System.IO.MemoryStream();
                System.IO.BinaryWriter BW = new System.IO.BinaryWriter(M);
                float P = 100f / img.Count;
                float T = 0;
                int DDel = gifdelay;
                byte[] B = ConvertGif(Borderify(Crop(img[0])));
                B[10] = (byte)(B[10] & 0X78); //No global color table.
                BW.Write(B, 0, 13);
                BW.Write(GifAnimation);
                int D = ((ComicPanel)panel1.Controls[0]).delay;
                if (D > 9)
                {
                    DDel = D;
                }
                WriteGifImg(B, BW, DDel / 10);
                for (int i = 1; i < img.Count; i++)
                {
                    //M.SetLength(0);
                    lock (giflock)
                    {
                        gifpercent = T;
                        gifprogress = i;
                    }
                    D = ((ComicPanel)panel1.Controls[i]).delay;
                    if (D > 9)
                    {
                        DDel = D;
                    }
                    B = ConvertGif(Borderify(Crop(img[i])));
                    WriteGifImg(B, BW, DDel / 10);
                    T += P;
                }
                BW.Write(B[B.Length - 1]);
                System.IO.FileStream F = System.IO.File.Create(saveFileDialog1.FileName);
                M.WriteTo(F);
                F.Close();
            }
            else
            {
                //the current least terrible gif encoder.

                System.IO.MemoryStream M = new System.IO.MemoryStream();
                //GifEncoder G = new GifEncoder(M);
                
                Gif.Components.AnimatedGifEncoder G = new Gif.Components.AnimatedGifEncoder();
                G.SetQuality(gifquality);
                G.SetDelay(gifdelay);
                G.SetRepeat(0);

                G.Start(M);
                
                float P = 100f / img.Count;
                float T = 0;
                int DDel = gifdelay;
                for (int i = 0; i < img.Count; i++)
                {
                    lock (giflock)
                    {
                        gifpercent = T;
                        gifprogress = i;
                    }
                    int D = ((ComicPanel)panel1.Controls[i]).delay;
                    if (D > 9)
                    {
                        DDel = D;
                    }
                    TimeSpan TS = TimeSpan.FromMilliseconds((int)(DDel / 10f));
                    G.AddFrame(img[i], (int)(DDel / 10f));
                    T += P;
                }
                G.Finish();
                System.IO.FileStream F = System.IO.File.Create(saveFileDialog1.FileName);
                M.WriteTo(F);
                F.Close();
            }
            lock (giflock)
            {
                gifpercent = 100;
                gifprogress = -1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView1_MouseDown(null, null);
        }
        public void LoadImages(string path,Bitmap Override)
        {
            img.Add(Override);
            panel1.Controls.Add(new ComicPanel(this, System.IO.Path.GetFileName(path), img[img.Count - 1], img.Count - 1));
            //Form1_ResizeEnd_1(null, null);
            addpanel();
        }
        public void LoadImages(string[] SS)
        {
            for (int i = 0; i < SS.Length; i++)
            {
                if (System.IO.File.Exists(SS[i]))
                {

                    if (SS[i].EndsWith(".gif"))
                    {
                        if (MessageBox.Show("you have chosen to load in a gif file\nanimated gifs are not fast too load(if its not animated then it wont matter)", "load gif?", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                        {
                            if (!System.IO.Directory.Exists("temp"))
                            {
                                System.IO.Directory.CreateDirectory("temp");
                            }
                            Gif.Components.GifDecoder Gr = new Gif.Components.GifDecoder();
                            string LT = Text;
                            Text = "Reading file...";
                            Gr.Read(SS[i]);
                            for (int c = 0; c < Gr.GetFrameCount(); c++)
                            {
                                Text = "grabbing frame " + (c+1) + "/" + Gr.GetFrameCount();
                                Image I = Gr.GetFrame(c);
                                string temp = "temp/" + System.IO.Path.GetFileName(SS[i]) + "." + c + ".png";
                                LoadImages(temp,new Bitmap(I));
                                System.Threading.Thread thread = new System.Threading.Thread(Lthread);
                                thread.Start(new object[]{I,temp});
                                
                            }
                        }
                    }
                    else
                    {
                        System.IO.FileStream F = new System.IO.FileStream(SS[i], System.IO.FileMode.Open);
                        Bitmap B = (Bitmap)Bitmap.FromStream(F);
                        F.Close();
                        img.Add(B);
                    }
                    panel1.Controls.Add(new ComicPanel(this,System.IO.Path.GetFileName(SS[i]), img[img.Count - 1], img.Count - 1));
                    addpanel();
                }
                
            }
        }
        /// <summary>
        /// for multithreaded image saving.
        /// </summary>
        /// <param name="O"></param>
        public void Lthread(object O)
        {
            object[] o = (object[])O;
            Image I = (Image)o[0];
            string temp = (string)o[1];
            I.Save(temp);
            I.Dispose();
        }
        public void BMPCommands(Keys key, ComicPanel bmp)
        {
            List<ComicPanel> B = new List<ComicPanel>();
            foreach (var item in panel1.Controls)
            {
                B.Add((ComicPanel)item);
            }
            int off = 0;
            bool redo = false;
            int indx = bmp.ind;
            if (key == Keys.F10)
            {
                if (!timer2.Enabled)
                {
                    Text = "Capture Mode! " + TITLE;
                    if (!System.IO.Directory.Exists("temp"))
                    {
                        System.IO.Directory.CreateDirectory("temp");
                    }
                    checkAndysnap();
                    timer2.Start();
                }
                else
                {
                    Text = TITLE;
                    timer2.Stop();
                }
            }
            if (key == Keys.Delete)
            {
                img[bmp.ind].Dispose();
                ((ComicPanel)panel1.Controls[bmp.ind]).pictureBox1.Image.Dispose();
                img.RemoveAt(bmp.ind);
                panel1.Controls.RemoveAt(bmp.ind);
                B.Remove(bmp);
                redo = true;
            }
            else if (key == Keys.PageUp)
            {
                if (bmp.ind > 0)
                {
                    Bitmap bm = img[bmp.ind];
                    ((ComicPanel)panel1.Controls[bmp.ind - 1]).ind++;
                    bmp.ind--;
                    B.Remove(bmp);
                    B.Insert(bmp.ind, bmp);
                    img.Remove(bm);
                    img.Insert(bmp.ind, bm);
                    redo = true;
                    
                }
                //off = 1;
                //off = bmp._ind;
                if (off < 0)
                {
                    off = 0;
                }
                if (off > img.Count)
                {
                    off = img.Count - 1;
                }
                //SelectedPanel = bmp;
            }
            else if (key == Keys.PageDown)
            {
                if (bmp.ind < img.Count - 1)
                {
                    Bitmap bm = img[bmp.ind];
                    ((ComicPanel)panel1.Controls[bmp.ind + 1]).ind--;
                    bmp.ind++;
                    redo = true;
                    B.Remove(bmp);
                    B.Insert(bmp.ind, bmp);
                    img.Remove(bm);
                    img.Insert(bmp.ind, bm);
                    
                }
                //off = -1;
                //off = bmp._ind;
                if (off < 0)
                {
                    off = 0;
                }
                if (off > img.Count)
                {
                    off = img.Count - 1;
                }
                //SelectedPanel = bmp;
            }
            else if (key == Keys.F8)
            {
                Save_Gif S = new Save_Gif();
                S.trackBar1.Visible = false;
                S.label1.Visible = false;
                S.Text = "Individual frame delay";
                if (bmp.delay!=0)
                {
                    S.textBox1.Text = "" + bmp.delay;
                }
                S.ShowDialog();
                if (S.OK)
                {
                    bmp.delay = S.delay;
                }
            }
            if (redo)
            {
                redopanel(B.ToArray());
                if (panel1.Controls.Contains(bmp))
                {
                    panel1.Controls[panel1.Controls.IndexOf(bmp) + off].Focus();
                }
            }
             
            if (key == Keys.Delete)
            {
                panel1.VerticalScroll.Value = Math.Max(Math.Min((indx - 1) * bmpsize, panel1.VerticalScroll.Maximum), 0);
            }
        }
        public int bmpsize = 64;

        public void addpanel()
        {
            int TMW = 0;
            for (int i = 0; i < img.Count; i++)
            {
                TMW = Math.Max(TMW, img[i].Width);
            }
            if (MW != TMW)
            {
                Form1_ResizeEnd_1(null, null);
            }
            else
            {
                panel1.Size = new Size(Width - 71, ClientRectangle.Height - panel1.Location.Y);
                //bmpsize = panel1.Width - 32;
                //if (panel1.Controls.Count > 0 && panel1.Controls[panel1.Controls.Count - 1].Bounds.Bottom > panel1.Height)
                {
                    bmpsize = panel1.Width - 18;
                }



                ComicPanel[] B = new ComicPanel[panel1.Controls.Count];
                for (int i = 0; i < B.Length; i++)
                {
                    B[i] = (ComicPanel)panel1.Controls[i];
                }
                int Y = 0;
                for (int i = 0; i < B.Length; i++)
                {
                    ComicPanel item = B[i];
                    int H = 0;
                    if (Borders.Seperator > 0)
                    {
                        H = (int)Math.Ceiling(((double)Borders.Seperator / img[i].Height) * item.Height);
                    }
                    if (i == B.Length - 1)
                    {
                        item.ind = i;
                        item.Location = new Point(0, Y);
                        double RW = (img[i].Width / ((double)MW));

                        double D = ((double)item.pictureBox1.Image.Height) / item.pictureBox1.Image.Width;
                        item.ratio = 1;
                        if (!widthMismatchStretchingToolStripMenuItem.Checked)
                        {
                            item.ratio = RW;
                        }
                        if (!widthMismatchStretchingToolStripMenuItem.Checked)
                        {
                            item.Size = new System.Drawing.Size(bmpsize, (int)((bmpsize) * D));
                        }
                        else
                        {
                            item.Size = new System.Drawing.Size(bmpsize, (int)((bmpsize) * D));
                        }


                        panel1.Controls.Add(item);
                        item.TabIndex = i;
                        item.SetImage(img[i]);
                    }
                    Y += item.Size.Height + H;
                }
            }
        }

        /// <summary>
        /// although inefficient and slow, the other methods i've tried just haven't behaved.
        /// </summary>
        /// <param name="neworder"></param>
        public void redopanel()
        {
            this.SuspendLayout();
            ComicPanel[] B = new ComicPanel[panel1.Controls.Count];
            for (int i = 0; i < B.Length; i++)
            {
                B[i] = (ComicPanel)panel1.Controls[i];
            }
            panel1.Controls.Clear();
            int Y = 0;
            List<Control> LC = new List<Control>();
            for (int i = 0; i < B.Length; i++)
            {
                ComicPanel item = B[i];
                item.ind = i;
                item.Location = new Point(0, Y);
                //resizes comicpanel to simulate how it would look when rendered in a comic.

                double RW = (img[i].Width / ((double)MW));

                double D = ((double)item.pictureBox1.Image.Height) / item.pictureBox1.Image.Width;

                item.ratio = 1;
                if (!widthMismatchStretchingToolStripMenuItem.Checked)
                {
                    //stretch out the comicpanel to visualize the scaling effect.
                    item.ratio = RW;
                }
                if (!widthMismatchStretchingToolStripMenuItem.Checked)
                {
                    item.Size = new System.Drawing.Size(bmpsize, (int)((bmpsize) * D));
                }
                else
                {
                    item.Size = new System.Drawing.Size(bmpsize, (int)((bmpsize) * D));
                }
                
                int H = 0;
                if (Borders.Seperator > 0)
                {
                    //add empty space to simulate seperators.
                    H = (int)Math.Ceiling(((double)(Borders.Seperator) / img[i].Height) * item.Height);
                }
                Y += item.Size.Height+H;
                LC.Add(item);
                panel1.Controls.Add(item);
                item.TabIndex = i;
                item.SetImage(img[i]);
            }
            
            this.ResumeLayout(true);
            //panel1.Invalidate();
        }

        /// <summary>
        /// although inefficient and slow, the other methods i've tried just haven't behaved.
        /// </summary>
        /// <param name="neworder"></param>
        public void redopanel(ComicPanel[] neworder)
        {
            panel1.Controls.Clear();
            for (int i = 0; i < neworder.Length; i++)
            {
                panel1.Controls.Add(neworder[i]);
            }
            redopanel();
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] S = e.Data.GetFormats();
            try
            {
                string[] SS = (string[])(e.Data.GetData("FileDrop"));
                if (SS.Length == 1 && !System.IO.File.Exists(SS[0]))
                {
                    List<string> L = new List<string>(System.IO.Directory.GetFiles(SS[0]));
                    L.Sort(comp);
                    SS = L.ToArray();
                    
                }
                LoadImages(SS);
                redopanel();

            }
            catch
            {
            }
            Form1_ResizeEnd_1(null,null);
            
        }

        /// <summary>
        /// compares strings with tweaks for numerical sorting
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static int comp(string s1, string s2)
        {
            s1 = repnumber(s1, 0);
            s2 = repnumber(s2, 0);
            return s1.CompareTo(s2);
        }
        /// <summary>
        /// adds padding to numbers inside of text for proper numerical sorting.
        /// </summary>
        /// <param name="S"></param>
        /// <param name="startindex"></param>
        /// <returns></returns>
        public static string repnumber(string S,int startindex)
        {
            int start = -1;
            string ret = S;
            for (int i = startindex; i < S.Length; i++)
            {
                if (isnumber(S[i]))
                {
                    if (start == -1)
                    {
                        start = i;
                    }
                }
                else
                {
                    if (start != -1)
                    {
                        int c;
                        string tmp = S.Substring(start, (i - start));
                        string begin = S.Substring(0, start);
                        string end = S.Substring(i);
                        
                        if (int.TryParse(tmp, out c))
                        {
                            string numb = ("" + c).PadLeft(8, '0');
                            tmp = begin + numb + end;
                            return (repnumber(tmp, (begin + numb).Length + 1));
                        }
                        else
                        {
                            start = -1;
                        }
                    }
                }
            }
            return ret;
        }
        public static bool isnumber(char chr)
        {
            return (chr == '0' || chr == '1' || chr == '2' || chr == '3' || chr == '4' || chr == '5' || chr == '6' || chr == '7' || chr == '8' || chr == '9');
        }

        /// <summary>
        /// tells windows to allow files to be dragged into this window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        public void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < img.Count; i++)
            {
                img[i].Dispose();
                ((ComicPanel)panel1.Controls[i]).pictureBox1.Image.Dispose();
            }
            img.Clear();
            panel1.Controls.Clear();
        }

        private void panel1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            sender = sender;
        }

        const int WM_KEYDOWN = 0x100;
        const int WM_SYSKEYDOWN = 0x104;
        const int WM_KEYUP = 0x101;
        const int WM_SYSKEYUP = 0x105;
        /// <summary>
        /// recalculate panel images.
        /// </summary>
        public void redobmp()
        {
            for (int i = 0; i < img.Count; i++)
            {
                ((ComicPanel)panel1.Controls[i]).SetImage(img[i]);
            }
            redopanel();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((msg.Msg == WM_KEYUP) || (msg.Msg == WM_SYSKEYUP))
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
            if (CaptureMode)
            {
                if (keyData == Keys.F1)
                {
                    button7_Click(null, null);
                }
                if (keyData == Keys.F9)
                {
                    Cropper.ShowDialog();
                    redobmp();
                }
                if (keyData == Keys.F10)
                {
                    if (!timer2.Enabled)
                    {
                        if (Clipboard.ContainsImage())
                        {
                            Clipboard.Clear();
                        }
                        Text = "Capture Mode! " + TITLE;
                        if (!System.IO.Directory.Exists("temp"))
                        {
                            System.IO.Directory.CreateDirectory("temp");
                        }
                        checkAndysnap();
                        timer2.Start();
                    }
                    else
                    {
                        Text = TITLE;
                        timer2.Stop();
                    }
                }
            }
            if (keyData == Keys.F12)
            {
                CaptureMode = true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (SelectedPanel != null && panel1.Controls.Contains(SelectedPanel))
            {
                BMPCommands(Keys.PageUp, SelectedPanel);
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (SelectedPanel != null && panel1.Controls.Contains(SelectedPanel))
            {
                BMPCommands(Keys.PageDown, SelectedPanel);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (SelectedPanel != null && panel1.Controls.Contains(SelectedPanel))
            {
                BMPCommands(Keys.Delete, SelectedPanel);
            }
        }
        /// <summary>
        /// the ?/About button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ComicLayouter "+version+" by RSGmaker\n\nF8:(GIF)Individual frame delay(cannot be cleared afterwards)\nF9:CropControl(no preview)\nF10:Auto PrintScreen Capture(saves captures into \"temp\" folder)", "About");
        }
        int MV;
        int MT = 0;
        /// <summary>
        /// this runs alongside the gif encoding, to give information and visually show the frames being processed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (gifprogress == -1)
            {
                Text = TITLE;
                UseWaitCursor = false;
                for (int i = 0; i < Controls.Count; i++)
                {
                    Controls[i].Enabled = true;
                }
                ((ComicPanel)panel1.Controls[panel1.Controls.Count-1]).Unselect();
                timer1.Stop();
            }
            else
            {
                int progress;
                float percent;
                lock (giflock)
                {
                    progress = gifprogress;
                    percent = gifpercent;
                }
                    if (gifprogress >= 0)
                    {
                        Text = percent + "%  "+TITLE;
                        int V = progress << 6;
                        V = (int)(panel1.VerticalScroll.Maximum * ((percent * 0.01f)));
                        int I = 6;
                        I += ((V - panel1.VerticalScroll.Value) >> 3);
                        //I = V;
                        if (I < 4)
                        {
                            I = 4;
                        }
                        if (I + panel1.VerticalScroll.Value < V)
                        {
                            panel1.VerticalScroll.Value += I;
                        }
                        else
                        {
                            panel1.VerticalScroll.Value = V;
                        }
                        if (progress > 0)
                        {
                            ((ComicPanel)panel1.Controls[progress - 1]).Unselect();
                        }
                        ((ComicPanel)panel1.Controls[progress]).Select();
                        ((ComicPanel)panel1.Controls[progress]).Focus();
                    }
            }
        }

        /// <summary>
        /// autoclipboard code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (!Clipboard.ContainsText() && Clipboard.ContainsImage())
            {
                try
                {
                    string s = System.IO.Path.GetFullPath("temp") + "\\" + img.Count + ".png";
                    Image I = Clipboard.GetImage();

                    Clipboard.SetText(s);
                    I.Save(s, System.Drawing.Imaging.ImageFormat.Png);
                    LoadImages(new string[] { s });
                }
                catch
                {
                }
            }
            else
            {
                //andysnap auto import feature.
                if (andysnapfolder != "")
                {
                    if (andysnapdelay < 15)
                    {
                        andysnapdelay++;
                    }
                    else
                    {
                        string[] temp = System.IO.Directory.GetFiles(andysnapfolder, "*.png");
                        if (andysnapfiles.Count != temp.Length)
                        {
                            System.Threading.Thread.Sleep(500);
                            int i = 0;
                            while (i < temp.Length)
                            {
                                if (!andysnapfiles.Contains(temp[i]))
                                {
                                    try
                                    {
                                        string[] blah = { temp[i] };
                                        LoadImages(blah);
                                        andysnapfiles.Add(temp[i]);
                                    }
                                    catch
                                    {
                                    }
                                }
                                i++;
                            }
                        }
                        andysnapdelay = 0;
                    }
                }
            }
        }
        private void checkAndysnap()
        {
            //uncommenting this will reenable andysnap auto importing when autoclipboard is activated,
            //it was disabled as andysnap is easy enough to use that it isn't neccessary and may only get in the way instead.

            /*Process[] P = Process.GetProcessesByName("AndySnap");
            if (P.Length > 0)
            {
                if (System.IO.Directory.Exists(@"C:\Program Files (x86)\AndySnap"))
                {
                    andysnapfolder = @"C:\Program Files (x86)\AndySnap";
                    andysnapfiles.AddRange(System.IO.Directory.GetFiles(andysnapfolder, "*.png"));
                    Text = "Capture+andysnap " + TITLE;
                }
                else if (System.IO.Directory.Exists(@"C:\Program Files\AndySnap"))
                {
                    andysnapfolder = @"C:\Program Files\AndySnap";
                    andysnapfiles.AddRange(System.IO.Directory.GetFiles(andysnapfolder, "*.png"));
                    Text = "Capture+andysnap " + TITLE;
                }
            }*/
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(new WalfasWindowClickFilter());
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            panel1.Size = new Size(panel1.Size.Width, ClientRectangle.Height - panel1.Location.Y);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button7_Click(null, null);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void loadImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1_MouseDown(null, null);
        }

        private void clearImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3_Click(null, null);
        }

        private void saveComicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveanimation = false;
            string F = saveFileDialog1.Filter;
            saveFileDialog1.Filter = "Comic strip|*.png";
            button1_Click(null, null);
            saveFileDialog1.Filter = F;
        }

        private void saveAsGIFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveanimation = true;
            string F = saveFileDialog1.Filter;
            //if (System.IO.File.Exists("apngasm.exe"))
            {
                saveFileDialog1.Filter = "all supported files|*.png;*.gif|animated png|*.png|animated gif|*.gif";
            }
            /*else
            {
                saveFileDialog1.Filter = "animated gif|*.gif";
            }*/
            
            button1_Click(null, null);
            saveFileDialog1.Filter = F;
            saveanimation = false;
        }

        private void shiftUpPgUPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button4_Click(null, null);
        }

        private void shiftDownPgDOWNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button5_Click(null, null);
        }

        private void deleteDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BMPCommands(Keys.Delete, SelectedPanel);
        }

        private void setDelayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BMPCommands(Keys.F8, SelectedPanel);
        }

        private void captureModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ProcessCmdKey(ref null, Keys.F10);
            if (!timer2.Enabled)
            {
                if (Clipboard.ContainsImage())
                {
                    Clipboard.Clear();
                }
                Text = "Capture Mode! " + TITLE;
                if (!System.IO.Directory.Exists("temp"))
                {
                    System.IO.Directory.CreateDirectory("temp");
                }
                andysnapfolder = "";
                checkAndysnap();
                timer2.Start();
            }
            else
            {
                Text = TITLE;
                timer2.Stop();
            }
        }

        private void cropSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cropper.ShowDialog();
            redobmp();
        }


        public void bordersSeperatorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Borders.ShowDialog();
            redobmp();
            panel1.BackColor = Borders.SeperatorColor;
        }

        public Size LSZ = Size.Empty;

        private void Form1_ResizeEnd_1(object sender, EventArgs e)
        {
            if (timer1.Enabled == false && Size != LSZ)
            {
                LSZ =Size;
                MW = 0;
                foreach (Control item in Controls)
                {
                    if (item != panel1)
                    {
                        item.Location = new Point(Width - 65, item.Location.Y);
                    }
                }
                for (int i = 0; i < img.Count; i++)
                {
                    MW = Math.Max(MW, img[i].Width);
                }
                panel1.Size = new Size(Width - 71, ClientRectangle.Height - panel1.Location.Y);
                //bmpsize = panel1.Width - 32;
                //if (panel1.Controls.Count > 0 && panel1.Controls[panel1.Controls.Count - 1].Bounds.Bottom > panel1.Height)
                {
                    bmpsize = panel1.Width - 18;
                }
                /*else
                {
                    bmpsize = panel1.Width;
                }*/
                redopanel();
            }
        }
        GifPreview GP;
        private void previewGifToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GP == null || GP.IsDisposed)
            {
                GP = new GifPreview(this);
            }
            GP.Show();
        }

        private void frameToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            pasteImageToolStripMenuItem.Visible = (!Clipboard.ContainsText() && Clipboard.ContainsImage());
            Process[] pc = Process.GetProcesses();
            string path = "";
            string name = "";
            /*foreach (var item in pc)
            {
                //if (item.MainWindowTitle.Contains("Paint.NET"))
                if (item.ProcessName.ToLower() == "paintdotnet")
                {
                    path = @"C:\Program Files\Paint.NET\PaintDotNet.exe";
                    name = item.ProcessName;
                }
                if (item.ProcessName.Contains("gimp"))
                {
                    path = @"C:\Program Files\GIMP 2\bin\"+item.ProcessName+".exe";
                    name = item.ProcessName;
                }
                if (item.ProcessName.ToLower() == "mspaint" && name == "")
                {
                    path = @"C:\Windows\System32\mspaint.exe";
                    name = item.ProcessName;
                }
            }*/

            var wmiQueryString = "SELECT ProcessId, ExecutablePath, CommandLine FROM Win32_Process";
            using (var searcher = new ManagementObjectSearcher(wmiQueryString))
            using (var results = searcher.Get())
            {
                var query = from p in Process.GetProcesses()
                            join mo in results.Cast<ManagementObject>()
                            //on true equals true
                            on p.Id equals (int)(uint)mo["ProcessId"]
                            select new
                            {
                                Process = p,
                                Path = (string)mo["ExecutablePath"],
                                CommandLine = (string)mo["CommandLine"],
                            };
                foreach (var item in query)
                {
                    // Do what you want with the Process, Path, and CommandLine
                    //item.Process

                    if (item.Process.ProcessName.ToLower() == "paintdotnet")
                    {
                        //path = @"C:\Program Files\Paint.NET\PaintDotNet.exe";
                        path = item.Path;
                        name = item.Process.ProcessName;
                    }
                    if (item.Process.ProcessName.Contains("gimp"))
                    {
                        //path = @"C:\Program Files\GIMP 2\bin\" + item.Process.ProcessName + ".exe";
                        path = item.Path;
                        name = item.Process.ProcessName;
                    }
                    if (item.Process.ProcessName.ToLower() == "mspaint" && name == "")
                    {
                        //path = @"C:\Windows\System32\mspaint.exe";
                        path = item.Path;
                        name = item.Process.ProcessName;
                    }
                }
            }
            if (path != "")
            {
                if (!System.IO.File.Exists(path))
                {
                    path = "";
                }
            }

            if (path != "" && SelectedPanel != null)
            {
                name = System.IO.Path.GetFileName(path);
                editToolStripMenuItem.Text = name;
                editToolStripMenuItem.Enabled = true;
                //editToolStripMenuItem.Visible = true;

            }
            else
            {
                editToolStripMenuItem.Enabled = false;
                //editToolStripMenuItem.Visible = false;
            }
        }
        /// <summary>
        /// detects which image editor is open and pauses comiclayouter until
        /// the selected panel image has been saved.
        /// </summary>
        public void EditImage()
        {
            Process[] pc = Process.GetProcesses();
            string path = "";
            string name = "";
            Process cp = null;
            /*foreach (var item in pc)
            {
                //if (item.MainWindowTitle.Contains("Paint.NET"))
                if (item.ProcessName.ToLower() == "paintdotnet")
                {
                    path = @"C:\Program Files\Paint.NET\PaintDotNet.exe";
                    name = item.ProcessName;
                    cp = item;
                }
                if (item.ProcessName.ToLower().Contains("gimp"))
                {
                    path = @"C:\Program Files\GIMP 2\bin\" + item.ProcessName + ".exe";
                    name = item.ProcessName;
                    cp = item;
                }
                if (item.ProcessName.ToLower() == "mspaint" && name == "")
                {
                    path = @"C:\Windows\System32\mspaint.exe";
                    name = item.ProcessName;
                    cp = item;
                }
            }*/
            var wmiQueryString = "SELECT ProcessId, ExecutablePath, CommandLine FROM Win32_Process";
            using (var searcher = new ManagementObjectSearcher(wmiQueryString))
            using (var results = searcher.Get())
            {
                var query = from p in Process.GetProcesses()
                            join mo in results.Cast<ManagementObject>()
                            on p.Id equals (int)(uint)mo["ProcessId"]
                            //on true equals true
                            select new
                            {
                                Process = p,
                                Path = (string)mo["ExecutablePath"],
                                CommandLine = (string)mo["CommandLine"],
                            };
                foreach (var item in query)
                {
                    // Do what you want with the Process, Path, and CommandLine
                    //item.Process

                    if (item.Process.ProcessName.ToLower() == "paintdotnet")
                    {
                        //path = @"C:\Program Files\Paint.NET\PaintDotNet.exe";
                        path = item.Path;
                        name = item.Process.ProcessName;
                    }
                    if (item.Process.ProcessName.Contains("gimp"))
                    {
                        //path = @"C:\Program Files\GIMP 2\bin\" + item.Process.ProcessName + ".exe";
                        path = item.Path;
                        name = item.Process.ProcessName;
                    }
                    if (item.Process.ProcessName.ToLower() == "mspaint" && name == "")
                    {
                        //path = @"C:\Windows\System32\mspaint.exe";
                        path = item.Path;
                        name = item.Process.ProcessName;
                    }
                }
            }
            if (path != "")
            {
                if (!System.IO.File.Exists(path))
                {
                    path = "";
                }
            }
            if (!System.IO.Directory.Exists("temp"))
            {
                System.IO.Directory.CreateDirectory("temp");
            }
            if (SelectedPanel != null && path != "")
            {
                //save the image so the editor can open it.
                string s = "\\temp\\comic frame " + SelectedPanel.ind + ".png";
                s = System.IO.Path.GetFullPath(s);
                img[SelectedPanel.ind].Save(s);
                DateTime D = System.IO.File.GetLastWriteTime(s);
                Process P = new Process();
                P.StartInfo = new ProcessStartInfo(path, "\"" + s + "\"");
                P.StartInfo.UseShellExecute = false;
                P.Start();
                ShowInTaskbar = false;
                Hide();
                bool ok = false;
                //wait and hide until the image has been saved or until the editor is closed.
                //the reason it hides is so the user can't try to interact with a frozen comiclayouter.
                while (!ok)
                {
                    System.Threading.Thread.Sleep(500);
                    if ((cp == null || cp.HasExited) && P.HasExited)
                    {
                        ok = true;
                    }
                    if (!System.IO.File.GetLastWriteTime(s).Equals(D))
                    {
                        ok = true;
                    }
                }
                Show();
                ShowInTaskbar = true;
                System.Threading.Thread.Sleep(500);
                System.IO.FileStream F = new System.IO.FileStream(s, System.IO.FileMode.Open);
                Bitmap B = (Bitmap)Bitmap.FromStream(F);
                img[SelectedPanel.ind] = B;
                SelectedPanel.SetImage(B);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditImage();
        }
        WalfasWindow WW;

        private void openCreateswfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WW == null || WW.IsDisposed)
            {
                //determine whether to use flashs activeX directly to run create.swf or use the default internet explorer engine
                if (System.IO.File.Exists("Interop.ShockwaveFlashObjects.dll"))
                {
                    try
                    {
                        WW = new WalfasWindow(this, false);
                    }
                    catch
                    {
                        WW = new WalfasWindow(this, true);
                    }
                }
                else
                {
                    WW = new WalfasWindow(this, true);
                }
            }
            try
            {
                WW.Show();
            }
            catch
            {
            }
        }

        private void stretchingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stretcher S = new Stretcher(Stretch);
            S.ShowDialog();
            Stretch = S.GetStretch;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            previewGifToolStripMenuItem_Click(null, null);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            openCreateswfToolStripMenuItem_Click(null, null);
        }

        private void captureWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //old test command
            //CaptureWindow C = new CaptureWindow(Handle);
            //C.Show();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (fs != WindowState)
            {
                Form1_ResizeEnd_1(null, null);
                fs = WindowState;
            }
        }

        private void widthMismatchStretchingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            widthMismatchStretchingToolStripMenuItem.Checked = !widthMismatchStretchingToolStripMenuItem.Checked;
            redopanel();
            //LSZ = Size.Empty;
            //Form1_ResizeEnd_1(null, null);
        }

        /// <summary>
        /// get copied image and imports it into a new panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pasteImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Clipboard.ContainsText() && Clipboard.ContainsImage())
            {
                try
                {
                    string s = System.IO.Path.GetFullPath("temp") + "\\" + img.Count + ".png";
                    Image I = Clipboard.GetImage();

                    Clipboard.SetText(s);
                    I.Save(s, System.Drawing.Imaging.ImageFormat.Png);
                    LoadImages(new string[] { s });
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// prioritizes numerical sorting, used in animated comics.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private int filesort(string a, string b)
        {
            a = System.IO.Path.GetFileName(a);
            b = System.IO.Path.GetFileName(b);
            string ta = System.Text.RegularExpressions.Regex.Match(a, @"\d+").Value;
            string tb = System.Text.RegularExpressions.Regex.Match(b, @"\d+").Value;
            int i = 0;
            if (ta.Length > 0 && tb.Length > 0)
            {
                int ai = 0;
                int bi = 0;
                if (int.TryParse(ta, out ai) && int.TryParse(tb, out bi))
                {
                    i = ai.CompareTo(bi);
                }
            }
            if (i != 0)
            {
                return i;
            }
            //if the two arguments could not be numerically sorted then do alphabetical sorting.
            return a.CompareTo(b);
        }

        private void createAnimatedComicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show(this,"To use this feature you must select a root folder that contains a panel1 panel2 panel3...etc folders containing the frames for that panel.","Explanation", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            if (MessageBox.Show(this, "To use this feature you must select an animation folder that contains subfolders for:panel1 panel2 panel3...etc,these folders must contain pngs for each frame of animation.\n\nThe generated comic frames are saved into the 'anicomicframes' folder.\nit is recommended to use gimp to compile the animated gif from the comic frames this feature generates.\n(use filters->animation->'optimize for gif', to keep the file size and performance reasonable.)", "Explanation", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string OText = Text;
                string root = folderBrowserDialog1.SelectedPath;
                if (System.IO.Directory.Exists(root))
                {
                    //string[] dirs = System.IO.Directory.GetDirectories(root);
                    List<String> dirs = new List<string>();
                    dirs.AddRange(System.IO.Directory.GetDirectories(root));
                    dirs.AddRange(System.IO.Directory.GetFiles(root, "panel*.png"));
                    dirs.Sort(filesort);
                    var i = 0;
                    var maxcount = 0;
                    List<List<Bitmap>> panels = new List<List<Bitmap>>();
                    Text = "Searching panel frames";
                    //while (i < dirs.Length)
                    while (i < dirs.Count)
                    {
                        if (System.IO.Path.GetFileName(dirs[i]).ToLower().StartsWith("panel"))
                        {
                            List<Bitmap> panel = new List<Bitmap>();
                            var c = 0;
                            List<string> T = null;
                            if (System.IO.Directory.Exists(dirs[i]))
                            {
                                T = new List<string>(System.IO.Directory.GetFiles(dirs[i], "*.png"));
                                T.Sort(filesort);
                            }
                            else if (System.IO.File.Exists(dirs[i]))
                            {
                                T = new List<string>();
                                T.Add(dirs[i]);
                            }
                            if (T != null)
                            {
                                string[] images = T.ToArray();
                                while (c < images.Length)
                                {
                                    System.IO.FileStream F = new System.IO.FileStream(images[c], System.IO.FileMode.Open);
                                    Bitmap B = (Bitmap)Bitmap.FromStream(F);
                                    F.Close();
                                    panel.Add(B);
                                    c++;
                                }

                                if (panel.Count > 0)
                                {
                                    panels.Add(panel);
                                    if (panel.Count > maxcount)
                                    {
                                        maxcount = panel.Count;
                                    }
                                }
                            }
                        }
                        i++;
                    }
                    if (panels.Count == 0)
                    {
                        Text = OText;
                        MessageBox.Show("The folder selected was not valid to make an animated comic.\nno panel folders with images were found.");
                        return;
                    }
                    i = 0;
                    //this feature is nice for previewing the finished product but can cause alot of memory usage possibly crashing comiclayouter.
                    bool load_animated_comic_on_finish = true;
                    List<Bitmap> anicomics = new List<Bitmap>();
                    while (i < maxcount)
                    {
                        Text = i + " of " + maxcount + " frames generated";
                        var c = 0;
                        List<Bitmap> LB = new List<Bitmap>();
                        while (c < panels.Count)
                        {
                            Bitmap TB = panels[c][i % panels[c].Count];
                            LB.Add(TB);
                            c++;
                        }
                        Bitmap B = CompileComic(LB);
                        if (!System.IO.Directory.Exists("anicomicframes"))
                        {
                            System.IO.Directory.CreateDirectory("anicomicframes");
                        }
                        string T = "anicomicframes\\" + i + ".png";
                        B.Save(T);
                        if (load_animated_comic_on_finish)
                        {
                            anicomics.Add(B);
                        }
                        i++;
                    }
                    Text = "Success!";
                    i = 0;
                    while (i < panels.Count)
                    {
                        var c = 0;
                        while (c < panels[i].Count)
                        {
                            panels[i][c].Dispose();
                            c++;
                        }
                        i++;
                    }
                    panels = null;
                    
                    
                    if (load_animated_comic_on_finish)
                    {
                        //clear one final time so we can load the completed comic frames
                        button3_Click(null, null);
                        i = 0;
                        string[] comicframes = new string[maxcount];
                        //reset border and crop tool as they will just cause issues at this point.
                        Borders = new BorderControl();
                        Cropper = new CropControl(this);
                        try
                        {
                            while (i < maxcount)
                            {
                                LoadImages("anicomicframes\\" + i + ".png", anicomics[i]);
                                i++;
                            }
                            MessageBox.Show(this, "The animated comic frames were saved into the 'anicomicframes' folder successfully!\n\nusing gimp to assemble the generated frames is recommended.", "Finished generating animated comic frames");
                        }
                        catch
                        {
                            //if it fails let the user know it still completed the task, and clear the data to keep the program stable.
                            button3_Click(null, null);
                            MessageBox.Show("the animated comic frames were generated in the 'anicomicframes' successfully.\nbut comiclayouter was not able to auto reimport them, using gimp to assemble the generated frames is recommended.");
                            //MessageBox.Show("An error has occured.\nthe animated comic frames were generated in the 'anicomicframes' successfully.\nthis error is likely an out of memory error,you may still be able to load the images manually into comiclayouter it may function however using gimp to compile large gifs is reccommended instead.");
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "The animated comic frames were saved into the 'anicomicframes' folder successfully!\n\nusing gimp to assemble the generated frames is recommended.", "Finished generating animated comic frames");
                    }
                }
                Text = OText;
            }
        }

        private void customPanelLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img.Count > 1)
            {
                Hide();
                CustomLayout C = new CustomLayout();
                C.form = this;
                C.ShowDialog();
                Show();
            }
            else
            {
                MessageBox.Show("You need multiple images loaded before you can use this.");
            }
        }

        private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedPanel != null)
            {
                Bitmap B = new Bitmap(img[SelectedPanel.ind]);
                LoadImages("", B);
            }
        }
    }
}
