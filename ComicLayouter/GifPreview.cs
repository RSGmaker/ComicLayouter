using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NAudio.Wave;
using System.IO;
using System.Diagnostics;

namespace ComicLayouter
{
    public partial class GifPreview : Form
    {
        public Form1 F;
        //int del = 0;
        double del = 0;
        int ddel = 500;
        float slfps = 2;
        bool Playing=false;
        int CD = 500;
        ComicPanel LFrame = null;
        DateTime last;
        ComicPanel lastframe;

        private IWavePlayer audioDevice;
        private WaveStream audio;
        public string audiopath = "";
        public bool audioenabled = false;

        public AdvancedGifMenu advmenu;

        public string maxtime = "00:00";

        Graphics panelG;

        public GifPreview(Form1 form)
        {
            InitializeComponent();
            F = form;
            last = DateTime.Now;
            timer1.Start();
            
            //thetimer = new System.Threading.Timer((a) => { pictureBox1.Invalidate(); }, null, 0, 10);
            lsize = Size;

            advmenu = new AdvancedGifMenu();
            advmenu.gif = this;
        }

        

        private void GifPreview_Load(object sender, EventArgs e)
        {
            panelG = panel3.CreateGraphics();
            panelG.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            panelG.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            panelG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            panelG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            if (F.SelectedPanel == null)
            {
                if (F.panel1.Controls.Count > 0)
                {
                    F.panel1.Controls[0].Focus();
                }
                return;
            }
            textBox1.Text = "" + F.defaultdelay;
            if (F.audiopath != "")
            {
                advmenu.LoadAudio(F.audiopath,false);
            }

            DoubleBuffered = true;

            
        }
        public int forcetime=0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Playing)
            {
                updatetimer();
            }
            if (F.SelectedPanel != null)
            {
                
            }
            fixframe();
            /*if (lastframe != F.SelectedPanel && F.SelectedPanel != null)
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                }
                Bitmap B = F.img[F.SelectedPanel.ind];
                pictureBox1.Image = F.Crop(B);
                if (pictureBox1.Image == B)
                {
                    pictureBox1.Image = new Bitmap(B);
                }
                pictureBox1.Invalidate();
                stop = true;
                slfps = 1000f / F.SelectedPanel.delay;
                textBox3.Text = "" + slfps;
                textBox4.Text = "" + F.SelectedPanel.delay;
                stop = false;
                lastframe = F.SelectedPanel;
            }*/
            if ((!Playing || ddel<1) && forcetime==0)
            {
                //lastbmp = F.SelBMP;
                return;
            }
            if (F.SelectedPanel == null)
            {
                if (F.panel1.Controls.Count > 0)
                {
                    F.panel1.Controls[0].Focus();
                }
                return;
            }
            if (lastframe == null)
            {
                lastframe = F.SelectedPanel;
                stop = true;
                slfps = 1000f / F.SelectedPanel.delay;
                textBox3.Text = "" + slfps;
                textBox4.Text = "" + F.SelectedPanel.delay;
                stop = false;
            }

            //del+=10;
            if (Playing)
            {
                del += DateTime.Now.Subtract(last).TotalMilliseconds;
            }
            else
            {
                del += forcetime;
            }
            last = DateTime.Now;
            
            
            int MD = F.SelectedPanel.delay;
            MD = CurrentDelay;
            if (MD == 0)
            {
                MD = ddel;
            }
            label7.Text = "Frame"+F.SelectedPanel.ind+ " Delay:" + MD;
            //label7.Text = "Current Delay:" + MD;
            if (Playing)
            {
                //timer1.Interval = Math.Max(MD / 2,10);
                timer1.Interval = 10;
            }
            bool ok = false;
            bool started = false;
            bool frameskip = true;
            while (del >= MD && MD>0 && (!started || frameskip))
            {
                //del -=ddel;
                del -= MD;
                ok = true;
                Next();
                //MD = F.SelBMP.delay;
                if (F.SelectedPanel.delay>0)
                {
                    MD = F.SelectedPanel.delay;
                }
                //MD = CurrentDelay;

                ////MD = F.SelectedPanel.delay;
                /*if (MD == 0)
                {
                    MD = ddel;
                }*/
                started = true;
                /*if (F.SelectedPanel.delay > 0)
                {
                    MD = F.SelectedPanel.delay;
                }*/
                //pictureBox1.Image = F.img[F.SelBMP.ind];
            }
            if (ok/* && forcetime<1*/ && false)
            {
                int ft = forcetime;
                forcetime = 0;
                timer1_Tick(null, null);
                forcetime = ft;
                if (ft < 1)
                {
                    pictureBox1.Invalidate();
                }
                /*if (Playing)
                {
                    timer1.Interval = Math.Max(MD / 4,10);
                }*/
                //fixframe();
            }
            
            //pictureBox1.Invalidate();
            //lastbmp = F.SelBMP;
        }

        Bitmap buffer;
        Graphics bufferG;

        private void fixframe(bool force=false)
        {
            if (force || (lastframe != F.SelectedPanel && F.SelectedPanel != null))
            {
                /*if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                }*/
                /*if (pictureBox1.Image != null && !F.img.Contains((Bitmap)pictureBox1.Image))
                {
                    pictureBox1.Image.Dispose();
                }*/
                Bitmap B = F.img[F.SelectedPanel.ind];
                if (F.Cropper.IWidth != 100 || F.Cropper.IHeight != 100)
                {
                    //pictureBox1.Image = F.Crop(B);
                    B = F.Crop(B);
                }
                else
                {
                    //pictureBox1.Image = B;
                }
                if (pictureBox1.Image == B)
                {
                    //pictureBox1.Image = new Bitmap(B);
                }
                if (buffer == null || !buffer.Size.Equals(panel3.Size))
                {
                    if (buffer != null)
                    {
                        bufferG.Dispose();
                        buffer.Dispose();
                    }
                    buffer = new Bitmap(panel3.Width, panel3.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                    bufferG = Graphics.FromImage(buffer);
                    bufferG.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
                    bufferG.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                    bufferG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;


                    //the default InterpolationMode is very very slow.

                    //less slow
                    //bufferG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
                    //fastest
                    bufferG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    //Text = "RND:" + new Random().Next();
                }
                Bitmap R = buffer;
                Graphics G = bufferG;
                /*Bitmap R = new Bitmap(pictureBox1.Width, pictureBox1.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                Graphics G = Graphics.FromImage(R);*/
                G.DrawImage(B, 0, 0, R.Width, R.Height);
                //G.Dispose();
                //panelG.Clear(Color.Red);
                //panelG.DrawImage(buffer, 0, 0);

                pictureBox1.Image = R;

                //pictureBox1.Invalidate();
                stop = true;
                slfps = 1000f / F.SelectedPanel.delay;
                textBox3.Text = "" + slfps;
                textBox4.Text = "" + F.SelectedPanel.delay;
                stop = false;
                lastframe = F.SelectedPanel;
            }
        }
        public int CurrentDelay
        {
            get
            {
                if (F.SelectedPanel != null && F.SelectedPanel != LFrame)
                {
                    ComicPanel B = F.SelectedPanel;
                    LFrame = B;
                    int i = B.ind;
                    while (i > -2)
                    {
                        if (i == -1)
                        {
                            CD = F.defaultdelay;
                        }
                        else
                        {
                            /*B = (ComicPanel)F.panel1.Controls[i];
                            if (B.delay > 9)
                            {
                                CD = B.delay;
                                return B.delay;
                            }*/
                            B = (ComicPanel)F.panel1.Controls[i];
                            if (B.delay > 0)
                            {
                                CD = B.delay;
                                return B.delay;
                            }
                        }
                        i--;
                    }
                }
                //return F.defaultdelay;
                return CD;
            }
        }
        public int GetDelayTo(int frame)
        {
            int delay = 0;
            int i = 0;
            int cd = ddel;
            int count = F.img.Count;
            while (i < (frame-1) && i < count)
            {
                ComicPanel B = (ComicPanel)F.panel1.Controls[i];
                cd = B.delay;
                if (cd<1)
                {
                    cd = ddel;
                }
                delay += cd;
                i++;
            }
            return delay;
        }
        public int GetMaxDelay()
        {
            return GetDelayTo(F.img.Count);
        }
        public void Next()
        {
            if (F.SelectedPanel.ind + 1 < F.panel1.Controls.Count)
            {
                F.SuspendLayout();
                ComicPanel B = (ComicPanel)F.panel1.Controls[F.SelectedPanel.ind + 1];
                F.SelectedPanel.Unselect();
                /*try
                {
                    F.panel1.VerticalScroll.Value += B.Height;
                }
                catch
                {
                    F.panel1.VerticalScroll.Value = F.panel1.VerticalScroll.Minimum;
                }*/
                //F.panel1.VerticalScroll.Value += B.Height;
                B.Select();
                F.ResumeLayout();
            }
            else
            {
                if (F.panel1.Controls.Count > 0)
                {
                    F.SuspendLayout();
                    ComicPanel B = (ComicPanel)F.panel1.Controls[0];
                    F.SelectedPanel.Unselect();
                    //F.panel1.VerticalScroll.Value = F.panel1.VerticalScroll.Minimum;
                    B.Select();
                    if (Playing)
                    {
                        LoopAudio();
                    }
                    last = DateTime.Now;
                    F.ResumeLayout();
                }
                else
                {
                    return;
                }

            }
            if (F.SelectedPanel != null)
            {
                stop = true;
                slfps = 1000f / F.SelectedPanel.delay;
                textBox3.Text = "" + slfps;
                textBox4.Text = "" + F.SelectedPanel.delay;
                stop = false;
            }
        }
        public void Prev()
        {
            if (F.SelectedPanel.ind - 1 >= 0)
            {
                ComicPanel B = (ComicPanel)F.panel1.Controls[F.SelectedPanel.ind - 1];
                F.SelectedPanel.Unselect();
                /*try
                {
                    F.panel1.VerticalScroll.Value -= B.Height;
                }
                catch
                {
                    F.panel1.VerticalScroll.Value = F.panel1.VerticalScroll.Maximum;
                }*/
                B.Select();
            }
            else
            {
                if (F.panel1.Controls.Count > 0)
                {
                    ComicPanel B = (ComicPanel)F.panel1.Controls[F.panel1.Controls.Count - 1];
                    F.SelectedPanel.Unselect();
                    /*try
                    {
                        F.panel1.VerticalScroll.Value = F.panel1.VerticalScroll.Maximum;
                    }
                    catch
                    {
                    }*/
                    B.Select();
                }
                else
                {
                    return;
                }

            }
            if (F.SelectedPanel != null)
            {
                stop = true;
                slfps = 1000f / F.SelectedPanel.delay;
                textBox3.Text = "" + slfps;
                textBox4.Text = "" + F.SelectedPanel.delay;
                stop = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (stop)
            {
                return;
            }
            stop = true;
            int T;
            if (int.TryParse(textBox1.Text, out T))
            {
                ddel = T;
                fps = 1000f / ddel;
                textBox2.Text = "" + fps;
                if (ddel > 0)
                {
                    F.defaultdelay = ddel;
                }
            }
            else
            {
                textBox1.Text = "" + ddel;
            }
            stop = false;
        }
        public bool stop;
        public float fps=2;
        public Size lsize;

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (stop)
            {
                return;
            }
            if (textBox2.Text != "")
            {
                stop = true;
                float T;
                if (float.TryParse(textBox2.Text, out T))
                {
                    fps = T;
                    ddel = (int)(1000f / fps);
                    textBox1.Text = "" + ddel;
                    if (ddel > 0)
                    {
                        F.defaultdelay = ddel;
                    }
                }
                else
                {
                    textBox2.Text = "" + fps;
                }
                stop = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StartPlayback();
        }

        public void StartPlayback()
        {
            Playing = true;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            timer1.Interval = 10;
            last = DateTime.Now;
            TimeSpan T;
            if (audio != null && audioenabled)
            {
                //T = new TimeSpan(CD * TimeSpan.TicksPerMillisecond);
                T = new TimeSpan((GetDelayTo(F.SelectedPanel.ind+1)+(int)del) * TimeSpan.TicksPerMillisecond);
                audio.CurrentTime = T;
                audioDevice.Volume = 0;

                audioDevice.Play();
                System.Threading.Thread.Sleep(1);
                audio.CurrentTime = T;
                System.Threading.Thread.Sleep(latency);
                audioDevice.Volume = 0.5f;
            }

            int d = GetMaxDelay();
            /*T = new TimeSpan(d * TimeSpan.TicksPerMillisecond);
            maxtime = (((int)T.TotalMinutes) + "").PadLeft(2, '0') + ":" + (T.Seconds + "").PadLeft(2, '0');*/
            updatetimer(true);

        }
        public void updatetimer(bool maxtimer=false)
        {

            int d = GetMaxDelay();
            if (maxtimer)
            {
                TimeSpan T = new TimeSpan(d * TimeSpan.TicksPerMillisecond);
                maxtime = (((int)T.TotalMinutes) + "").PadLeft(2, '0') + ":" + (T.Seconds + "").PadLeft(2, '0');
            }
            /*TimeSpan T = new TimeSpan(d * TimeSpan.TicksPerMillisecond);
            maxtime = (((int)T.TotalMinutes) + "").PadLeft(2,'0') + ":" + (T.Seconds+"").PadLeft(2, '0');*/
            string ct = getcurrenttime();
            string txt = ct + "/" + maxtime;
            if (txt != label8.Text)
            {
                label8.Text = txt;
            }
            if (maxtimer)
            {
                int MD = CurrentDelay;
                if (MD == 0)
                {
                    MD = ddel;
                }
                //label7.Text = "Current Delay:" + MD;
                label7.Text = "Frame" + F.SelectedPanel.ind + " Delay:" + MD;
            }
        }

        public string FormatTime(TimeSpan time)
        {
            TimeSpan T = time;
            string dec = (""+(T.TotalSeconds - T.Seconds));
            if (T.TotalSeconds == T.Seconds)
            {
                dec = "";
            }
            else
            {
                if (dec.IndexOf(".")<0)
                {
                    dec = dec + ".0";
                }
                dec = "."+dec.Split('.')[1];
                if (dec.Length>3)
                {
                    dec = dec.Remove(3);
                }
            }
            string ret = (((int)T.TotalMinutes) + "").PadLeft(2, '0') + ":" + (T.Seconds + "").PadLeft(2, '0') + dec;

            return ret;
        }

        public string getcurrenttime()
        {
            int d = GetDelayTo(F.SelectedPanel.ind) + (int)del;
            TimeSpan T = new TimeSpan(d * TimeSpan.TicksPerMillisecond);
            return FormatTime(T);
            //return (((int)T.TotalMinutes) + "").PadLeft(2, '0') + ":" + (T.Seconds + "").PadLeft(2, '0');
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StopPlayback();
        }

        public void StopPlayback()
        {
            Playing = false;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            //pictureBox1.Focus();
            inputBox.Focus();
            StopAudio();
        }

        public void StopAudio()
        {
            if (audioDevice != null)
            {
                //audioDevice.Stop();
                audioDevice.Pause();
            }
        }
        public void LoopAudio()
        {
            if (audioDevice != null)
            {
                if (audioenabled && audio != null)
                {
                    audio.CurrentTime = new TimeSpan(0 * TimeSpan.TicksPerMillisecond);
                    if (audioDevice.PlaybackState != PlaybackState.Playing)
                    {
                        audioDevice.Play();
                    }
                    System.Threading.Thread.Sleep(latency);
                }
                else
                {
                    //audioDevice.Stop();
                    audioDevice.Pause();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Forward();
        }

        public void Forward()
        {
            Next();
            del = 0;
            //pictureBox1.Focus();
            inputBox.Focus();
            timer1_Tick(null, null);
            updatetimer(true);

            /*int MD = CurrentDelay;
            if (MD == 0)
            {
                MD = ddel;
            }*/
        }
        public void Backward()
        {
            Prev();
            del = 0;
            //pictureBox1.Focus();
            inputBox.Focus();
            timer1_Tick(null, null);
            updatetimer(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Backward();
        }

        private void GifPreview_SizeChanged(object sender, EventArgs e)
        {
            Size T = new Size(Size.Width - lsize.Width, Size.Height - lsize.Height);
            foreach (Control item in Controls)
            {
                if (item != panel3)
                {
                    item.Location = new Point(item.Location.X + T.Width, item.Location.Y + T.Height);
                }
            }
            panel3.Size = new Size(panel3.Size.Width + T.Width, panel3.Size.Height + T.Height);

            panelG.Dispose();
            panelG = panel3.CreateGraphics();
            panelG.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            panelG.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            panelG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            panelG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            

            //panel3.Size = pictureBox1.Size;
            lsize = Size;

            fixframe(true);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (stop)
            {
                return;
            }
            stop = true;
            int T;
            if (F.SelectedPanel != null && int.TryParse(textBox4.Text, out T))
            {
                F.SelectedPanel.delay = T;
                slfps = 1000f / F.SelectedPanel.delay;
                textBox3.Text = "" + slfps;
            }
            else
            {
                textBox4.Text = "" + F.SelectedPanel.delay;
            }
            stop = false;
            updatetimer(true);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.ToLower() == "infinity")
            {
                textBox3.Text = "0";
            }
            if (stop)
            {
                return;
            }
            if (textBox3.Text == "0")
            {
                if (F.SelectedPanel != null)
                {
                    stop = true;
                    F.SelectedPanel.delay = 0;
                    textBox3.Text = "0";
                    textBox4.Text = "0";
                    stop = false;
                }
                return;
            }
            if (textBox3.Text != "")
            {
                stop = true;
                float T;
                if (F.SelectedPanel != null && float.TryParse(textBox3.Text, out T))
                {
                    slfps = T;
                    F.SelectedPanel.delay = (int)(1000f / slfps);
                    textBox4.Text = "" + F.SelectedPanel.delay;
                }
                else
                {
                    textBox3.Text = "" + slfps;
                }
                stop = false;
            }
            updatetimer(true);
        }

        public static Control FindFocusedControl(Control control)
        {
            var container = control as IContainerControl;
            while (container != null)
            {
                control = container.ActiveControl;
                container = control as IContainerControl;
            }
            return control;
        }

        private void GifPreview_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (FindFocusedControl(this) is TextBox)
            {
                return;
            }*/
            Control FC = FindFocusedControl(this);
            if (FC != inputBox && FC is TextBox)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    inputBox.Focus();
                }
                return;
            }
            if (e.KeyCode == Keys.Right)
            {
                Forward();
                //e.SuppressKeyPress = true;
                //e.Handled = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                Backward();
                //e.SuppressKeyPress = true;
                //e.Handled = true;
            }
            if (e.KeyCode == Keys.Space)
            {
                if (Playing)
                {
                    StopPlayback();
                }
                else
                {
                    StartPlayback();
                }
                //e.SuppressKeyPress = true;
                //e.Handled = true;
            }
            inputBox.Focus();
            e.Handled = true;
            e.SuppressKeyPress = true;
            //e.Handled = e.SuppressKeyPress;
            //pictureBox1.Focus();
            //panel1.Focus();
        }

        private void pictureBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            /*if (e.KeyCode == Keys.Right)
            {
                Forward();
            }
            if (e.KeyCode == Keys.Left)
            {
                Backward();
            }
            if (e.KeyCode == Keys.Space)
            {
                if (Playing)
                {
                    StopPlayback();
                }
                else
                {
                    StartPlayback();
                }
            }
            pictureBox1.Focus();*/
        }

        public void SaveGimpLayers()
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                int delay = ddel;
                int i = 0;
                while (i < F.panel1.Controls.Count)
                {
                    ComicPanel CP = (ComicPanel)F.panel1.Controls[i];
                    if (CP.delay>0)
                    {
                        delay = CP.delay;
                    }
                    Bitmap B = F.img[i];
                    B.Save(folderBrowserDialog1.SelectedPath + "/frame" + ("" + i).PadLeft(5, '0') + "("+delay+" ms)" + ".png");
                    i++;
                }
            }
        }
        public string VideoArg = "";
        public string PreArg = "";
        public int GetFramedelay()
        {
            int cd = ddel;
            int i = 0;
            while (i < F.panel1.Controls.Count)
            {
                ComicPanel CP = (ComicPanel)F.panel1.Controls[i];
                if (CP.delay > 0 && CP.delay < cd)
                {
                    cd = CP.delay;
                }
                i++;
            }
            return cd;
            //return (int)(1000f / cd);
        }
        public void SaveVideo(bool preparevideo=true)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string file = saveFileDialog1.FileName;
            /*Save_Gif S = new Save_Gif();
            S.trackBar1.Visible = false;
            //S.label1.Visible = false;
            S.label1.Text = "Choose a target framerate.";
            S.textBox2.Text = "30";
            S.Text = "Frame sequence framerate(Video)";
            S.ShowDialog();
            if (!S.OK)
            {
                return;
            }*/

            UseWaitCursor = true;
            advmenu.UseWaitCursor = true;

            int delay = GetFramedelay();
            int fps = (int)(1000f / delay);
            if (fps>30)
            {
                Save_Gif S = new Save_Gif();
                S.trackBar1.Visible = false;
                //S.label1.Visible = false;
                S.label1.Text = "Choose a target framerate.";
                S.textBox2.Text = ""+fps;
                S.Text = "Frame sequence framerate(Video)";
                S.ShowDialog();
                if (!S.OK)
                {
                    return;
                }
                delay = (int)float.Parse(S.textBox1.Text);
                fps = (int)float.Parse(S.textBox2.Text);
            }
            //int delay = (int)float.Parse(S.textBox1.Text);
            //int fps = (int)float.Parse(S.textBox2.Text);
            if (preparevideo)
            {
                if (!Directory.Exists("Fseq"))
                {
                    Directory.CreateDirectory("Fseq");
                }
                else
                {
                    string[] f = Directory.GetFiles("Fseq");
                    int i = 0;
                    while (i < f.Length)
                    {
                        File.Delete(f[i]);
                        i++;
                    }
                }
                //gif does not work with variable image sizes.
                ExportSequence("Fseq\\", delay, file.EndsWith(".gif"));
            }
            /*if (File.Exists(file))
            {
                File.Delete(file);
            }*/
            string Acmd = "";
            if (/*audioenabled*/advmenu.checkBox1.Checked && advmenu.label1.Text!="")
            {

                //-q:a 3
                //Acmd = "-i \"" + advmenu.label1.Text + "\" "+ "-b:a 192k ";
                Acmd = "-i \"" + advmenu.label1.Text + "\" " + "-b:a 160k ";
            }
            
            string video_args = VideoArg;
            string vinput = "-framerate " + fps + "/1 -i Fseq/frame%05d.png ";
            if (file.EndsWith(".png"))
            {
                //tell ffmpeg to produce an animated png.
                video_args = VideoArg +"-f apng ";
            }
            if (file.EndsWith(".gif"))
            {
                string command = "ffmpeg "+vinput+" -vf \"fps="+fps+",scale="+F.img[0].Width+":-1:flags=lanczos, palettegen\" -y /tmp/palette.png";
                Process.Start("CMD.exe", "/C " + command).WaitForExit();

                command = "ffmpeg "+vinput+ " -i /tmp/palette.png -lavfi \"fps=" + fps + ",scale=" + F.img[0].Width + ":-1:flags=lanczos [x]; [x][1:v] paletteuse\" -y \"" + file+"\"";
                Process.Start("CMD.exe", "/C " + command);
            }
            else
            {
                string strCmdText;
                //string command = "ffmpeg -loop 1 -framerate 1/5 -i img%%03d.png -i music.mp3 -c:v libx264 -r 30 -pix_fmt yuv420p -shortest out.mp4";
                //string command = "ffmpeg -framerate 1/"+fps+" -i img%%03d.png -i music.mp3 -c:v libx264 -r 30 -pix_fmt yuv420p -shortest out.mp4";
                //string command = "ffmpeg -framerate 1/" + fps + " -i Fseq/frame%%05d.png -i music.mp3 -c:v libx264 -r 30 -pix_fmt yuv420p -shortest \""+saveFileDialog1.FileName+"\"";
                //string command = "ffmpeg -framerate 1/" + fps + " -i Fseq/frame%%05d.png "+Acmd+"-r 30 -pix_fmt yuv420p \"" + saveFileDialog1.FileName + "\"";
                //string command = "ffmpeg -framerate 1/" + fps + " -i Fseq/frame%05d.png " + Acmd + "-c:v libx264 -r 30 -pix_fmt yuv420p \"" + saveFileDialog1.FileName + "\"";
                //string command = "ffmpeg -framerate " + fps + "/1 -i Fseq/frame%05d.png " + Acmd + "-c:v libx264 -r 30 -pix_fmt yuv420p -shortest \"" + saveFileDialog1.FileName + "\"";
                //string command = "ffmpeg " + PreArg + "-framerate " + fps + "/1 -i Fseq/frame%05d.png " + video_args + Acmd + "-r 30 -pix_fmt yuv420p -shortest \"" + saveFileDialog1.FileName + "\"";
                string command = "ffmpeg " + PreArg + vinput + video_args + Acmd + "-r "+fps+" -pix_fmt yuv420p -shortest -y \"" + saveFileDialog1.FileName + "\"";
                strCmdText = /*"/C " + */command;
                //strCmdText = "/C copy /b Image1.jpg + Archive.rar Image2.jpg";
                System.Diagnostics.Process.Start("CMD.exe", "/C " + strCmdText);
            }

            /*Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            //cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            //cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine(command);
            cmd.StandardInput.Flush();*/
            UseWaitCursor = false;
            advmenu.UseWaitCursor = false;
        }
        public void ExportSequence(string directory,int framedelay, bool resize = false)
        {
            if (Playing)
            {
                /*Playing = false;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                pictureBox1.Focus();*/
                StopPlayback();
            }
            /*if (thetimer != null)
            {
                thetimer.Dispose();
                thetimer = null;
            }*/
            timer1.Stop();
            bool started = false;
            //int fps = (int)float.Parse(S.textBox1.Text);
            int fps = framedelay;
            forcetime = fps;
            CD = 0;

            ComicPanel CP = (ComicPanel)F.panel1.Controls[0];
            F.SelectedPanel.Unselect();
            /*try
            {
                F.panel1.VerticalScroll.Value = 0;
            }
            catch
            {
            }*/
            CP.Select();

            int frame = 0;

            int lastpanel = -1;
            MemoryStream MS = null;
            ProgressWindow PW = new ProgressWindow();
            PW.Show();
            ProgressBar PB = PW.progressBar1;
            PB.Maximum = F.panel1.Controls.Count;
            PB.Step = 1;
            PW.Text = "Exporting sequence... Framerate:" + (int)(1000f / fps);
            Size first = Size.Empty;
            //PW.label1.Text = "Exporting your frame sequence please wait...";
            while (!started/* || CurrentDelay!=0*/ || F.SelectedPanel.ind != 0)
            {
                //started = true;
                timer1_Tick(null, null);
                if (!started || F.SelectedPanel.ind != 0)
                {
                    if (F.SelectedPanel.ind != 0)
                    {
                        started = true;
                    }
                    if (F.SelectedPanel.ind != lastpanel)
                    {
                        Bitmap TB = F.img[F.SelectedPanel.ind];
                        int W = TB.Width - (TB.Width & 1);
                        int H = TB.Height - (TB.Height & 1);
                        Bitmap B = TB;
                        bool clone = false;
                        if (first.IsEmpty)
                        {
                            first.Width = W;
                            first.Height = H;
                        }
                        else if (resize)
                        {
                            W = first.Width;
                            H = first.Height;
                        }
                        if (B.Width != W || B.Height != H)
                        {
                            //B = TB.Clone(new Rectangle(0, 0, W, H), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                            Size s = new Size(W, H);
                            Bitmap ret = new Bitmap(s.Width, s.Height);
                            Graphics G = Graphics.FromImage(ret);
                            Point P = Point.Empty;
                            //Point P = new Point((int)(B.Size.Width * (IX * 0.01)), (int)(B.Size.Height * (IY * 0.01)));
                            //Size Sz = new Size((int)(B.Size.Width * (IWidth * 0.01)), (int)(B.Size.Height * (IHeight * 0.01)));
                            Size Sz = s;
                            G.DrawImage(B, new Rectangle(Point.Empty, ret.Size), new Rectangle(P, Sz), GraphicsUnit.Pixel);
                            G.Dispose();
                            B = ret;
                            //return ret;

                            clone = true;
                        }
                        Bitmap temp = B;
                        if (F.Cropper.IWidth != 100 && F.Cropper.IHeight != 100)
                        {
                            B = F.Crop(B);
                        }
                        if (temp != B && temp != TB)
                        {
                            temp.Dispose();
                        }
                        temp = B;
                        B = F.GetStretch(B);
                        if (temp != B && temp != TB)
                        {
                            temp.Dispose();
                        }
                        temp = null;
                        //B = F.GetStretch(F.Crop(B));
                        clone = true;

                        //B.Save(directory + "/frame" + ("" + frame).PadLeft(5, '0') + ".png");
                        if (MS != null)
                        {
                            MS.Dispose();
                        }
                        MS = new MemoryStream();
                        B.Save(MS, System.Drawing.Imaging.ImageFormat.Png);
                        if (clone && B != TB)
                        {
                            B.Dispose();
                        }
                        B = null;
                        PB.PerformStep();
                        PW.Invalidate();
                        GC.Collect();
                    }
                    using (var fileStream = File.Create(directory + "/frame" + ("" + frame).PadLeft(5, '0') + ".png"))
                    {
                        MS.Seek(0, SeekOrigin.Begin);
                        MS.CopyTo(fileStream);
                    }
                    frame++;
                }

            }
            PW.Close();
            PW.Dispose();
            forcetime = 0;
            timer1.Start();


            //thetimer = new System.Threading.Timer((a) => { pictureBox1.Invalidate(); }, null, 0, 10);
        }
        System.Threading.Timer thetimer;
        public void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Save_Gif S = new Save_Gif();
                S.trackBar1.Visible = false;
                //S.label1.Visible = false;
                S.label1.Text = "Choose a target framerate.";
                S.textBox2.Text = "30";
                S.Text = "Frame sequence framerate";
                S.ShowDialog();
                if (S.OK)
                {
                    ExportSequence(folderBrowserDialog1.SelectedPath, (int)float.Parse(S.textBox1.Text));
                }
            }
        }
        public int latency = 20;
        public bool LoadAudio(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                return false;
            }
            WaveOut WO = new WaveOut();
            //WO.DesiredLatency = latency;
            latency = WO.DesiredLatency;
            audioDevice = WO;
            WaveStream wav = GetStream(path);
            if (wav == null)
            {
                return false;
            }
            audio = wav;
            audiopath = path;

            audioDevice.Init(audio);

            audioDevice.Volume = 0.0f;

            audioenabled = true;
            audioDevice.Play();
            audioDevice.Pause();
            System.Threading.Thread.Sleep(1);
            audioDevice.Volume = 0.5f;
            return true;
        }
        

        public WaveStream GetStream(string path)
        {
            WaveStream ret = null;
            string lp = path.ToLower();
            if (lp.EndsWith(".wav"))
            {
                ret = new WaveFileReader(path);
                if (ret.WaveFormat.Encoding != WaveFormatEncoding.Pcm && ret.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
                {
                    ret = WaveFormatConversionStream.CreatePcmStream(ret);
                    ret = new BlockAlignReductionStream(ret);
                }
            }
            else if (lp.EndsWith(".mp3"))
            {
                ret = new Mp3FileReader(path);
            }
            else if (lp.EndsWith(".aiff"))
            {
                ret = new AiffFileReader(path);
            }
            else if (lp.EndsWith(".wma"))
            {
                ret = new NAudio.WindowsMediaFormat.WMAFileReader(path);
            }
            return ret;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            advmenu.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CD = 0;
            del = 0;
            timer1_Tick(null, null);


            ComicPanel B = (ComicPanel)F.panel1.Controls[0];
            F.SelectedPanel.Unselect();
            /*try
            {
                F.panel1.VerticalScroll.Value = 0;
            }
            catch
            {
            }*/
            B.Select();
            last = DateTime.Now;
            timer1_Tick(null, null);
        }

        private void GifPreview_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            inputBox.Focus();
        }

        private void GifPreview_Click(object sender, EventArgs e)
        {
            inputBox.Focus();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            timer1_Tick(null, null);
        }
    }
}
