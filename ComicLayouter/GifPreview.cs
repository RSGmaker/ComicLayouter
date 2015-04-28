using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ComicLayouter
{
    public partial class GifPreview : Form
    {
        public Form1 F;
        int del = 0;
        int ddel = 500;
        float slfps = 2;
        bool Playing=false;
        int CD = 500;
        ComicPanel LFrame = null;
        DateTime last;
        ComicPanel lastframe;
        public GifPreview(Form1 form)
        {
            InitializeComponent();
            F = form;
            last = DateTime.Now;
            timer1.Start();
            lsize = Size;
        }

        private void GifPreview_Load(object sender, EventArgs e)
        {
            if (F.SelectedPanel == null)
            {
                if (F.panel1.Controls.Count > 0)
                {
                    F.panel1.Controls[0].Focus();
                }
                return;
            }
            textBox1.Text = "" + F.defaultdelay;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (F.SelectedPanel != null)
            {
                
            }
            if (lastframe != F.SelectedPanel && F.SelectedPanel != null)
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
                stop = true;
                slfps = 1000f / F.SelectedPanel.delay;
                textBox3.Text = "" + slfps;
                textBox4.Text = "" + F.SelectedPanel.delay;
                stop = false;
                lastframe = F.SelectedPanel;
            }
            if (!Playing || ddel<1)
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
            del += (int)DateTime.Now.Subtract(last).TotalMilliseconds;
            last = DateTime.Now;
            
            int MD = F.SelectedPanel.delay;
            MD = CurrentDelay;
            if (MD == 0)
            {
                MD = ddel;
            }
            label7.Text = "Current Delay:" + MD;
            while (del >= MD && MD>0)
            {
                //del -=ddel;
                del -= MD;

                Next();
                //MD = F.SelBMP.delay;
                MD = CurrentDelay;
                if (MD == 0)
                {
                    MD = ddel;
                }
                //pictureBox1.Image = F.img[F.SelBMP.ind];
            }
            //lastbmp = F.SelBMP;
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
                            B = (ComicPanel)F.panel1.Controls[i];
                            if (B.delay > 9)
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
        public void Next()
        {
            if (F.SelectedPanel.ind + 1 < F.panel1.Controls.Count)
            {
                ComicPanel B = (ComicPanel)F.panel1.Controls[F.SelectedPanel.ind + 1];
                F.SelectedPanel.Unselect();
                F.panel1.VerticalScroll.Value += B.Height;
                B.Select();
            }
            else
            {
                if (F.panel1.Controls.Count > 0)
                {
                    ComicPanel B = (ComicPanel)F.panel1.Controls[0];
                    F.SelectedPanel.Unselect();
                    F.panel1.VerticalScroll.Value = F.panel1.VerticalScroll.Minimum;
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
        public void Prev()
        {
            if (F.SelectedPanel.ind - 1 >= 0)
            {
                ComicPanel B = (ComicPanel)F.panel1.Controls[F.SelectedPanel.ind - 1];
                F.SelectedPanel.Unselect();
                F.panel1.VerticalScroll.Value -= B.Height;
                B.Select();
            }
            else
            {
                if (F.panel1.Controls.Count > 0)
                {
                    ComicPanel B = (ComicPanel)F.panel1.Controls[F.panel1.Controls.Count - 1];
                    F.SelectedPanel.Unselect();
                    F.panel1.VerticalScroll.Value = F.panel1.VerticalScroll.Maximum;
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
            Playing = true;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            last = DateTime.Now;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Playing = false;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            pictureBox1.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Next();
            pictureBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Prev();
            pictureBox1.Focus();
        }

        private void GifPreview_SizeChanged(object sender, EventArgs e)
        {
            Size T = new Size(Size.Width - lsize.Width, Size.Height - lsize.Height);
            foreach (Control item in Controls)
            {
                if (item != pictureBox1)
                {
                    item.Location = new Point(item.Location.X + T.Width, item.Location.Y + T.Height);
                }
            }
            pictureBox1.Size = new Size(pictureBox1.Size.Width + T.Width, pictureBox1.Size.Height + T.Height);
            lsize = Size;
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
        }

        private void GifPreview_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                Next();
            }
            if (e.KeyCode == Keys.Left)
            {
                Prev();
            }
            if (e.KeyCode == Keys.Space)
            {
                if (Playing)
                {
                    button2_Click(null, null);
                }
                else
                {
                    button4_Click(null, null);
                }
            }
        }

        private void pictureBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                Next();
            }
            if (e.KeyCode == Keys.Left)
            {
                Prev();
            }
            if (e.KeyCode == Keys.Space)
            {
                if (Playing)
                {
                    button2_Click(null, null);
                }
                else
                {
                    button4_Click(null, null);
                }
            }
        }

    }
}
