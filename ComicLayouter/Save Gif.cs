using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ComicLayouter
{
    public partial class Save_Gif : Form
    {
        public Save_Gif()
        {
            InitializeComponent();
            delay = int.Parse(textBox1.Text);
            fps = float.Parse(textBox2.Text);
        }
        public bool OK;
        public int delay;
        public int quality = 10;
        protected float fps;
        protected bool stop = false;

        private void button1_Click(object sender, EventArgs e)
        {
            OK = true;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!stop)
            {
                if (textBox2.Text != "")
                {
                    stop = true;
                    float T;
                    if (float.TryParse(textBox2.Text, out T))
                    {
                        fps = T;
                        delay = (int)(1000f / fps);
                        textBox1.Text = "" + delay;
                    }
                    else
                    {
                        textBox2.Text = "" + fps;
                    }
                    stop = false;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!stop)
            {
                stop = true;
                int T;
                if (int.TryParse(textBox1.Text, out T))
                {
                    delay = T;
                    fps = 1000f / delay;
                    textBox2.Text = "" + fps;
                }
                else
                {
                    textBox1.Text = "" + delay;
                }
                stop = false;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            quality = trackBar1.Value;
            toolTip1.SetToolTip(trackBar1, "" + trackBar1.Value);
            //toolTip1.Show("" + trackBar1.Value, this, trackBar1.Location);
        }

        private void Save_Gif_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "" + fps;
            }
        }
    }
}
