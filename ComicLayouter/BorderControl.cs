using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ComicLayouter
{
    public partial class BorderControl : Form
    {
        public BorderControl()
        {
            InitializeComponent();
            BorderColor = Color.White;
            SeperatorColor = Color.White;
            Border = new Size(0, 0);
            Seperator = 0;
            Outline = 0;
            OutlineColor = Color.Black;
        }
        public Color BorderColor;
        public Color SeperatorColor;
        public Size Border;
        public int Seperator;
        public int Outline;
        public Color OutlineColor;


        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            panel1.BackColor = BorderColor;
            colorDialog1.Color = BorderColor;
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BorderColor = colorDialog1.Color;
                panel1.BackColor = BorderColor;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int T;
            if (int.TryParse(textBox1.Text, out T))
            {
                if (T >= 0)
                {
                    Border = new Size(T, Border.Height);
                }
            }
            else
            {
                textBox1.Text = "" + Border.Width;
            }
        }

        private void BorderControl_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int T;
            if (int.TryParse(textBox2.Text, out T))
            {
                if (T >= 0)
                {
                    Border = new Size(Border.Width, T);
                }
            }
            else
            {
                textBox2.Text = "" + Border.Height;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int T;
            if (int.TryParse(textBox3.Text, out T))
            {
                if (T >= 0)
                {
                    Seperator = T;
                }
            }
            else
            {
                textBox3.Text = "" + Seperator;
            }
        }

        private void panel2_DoubleClick(object sender, EventArgs e)
        {
            panel2.BackColor = SeperatorColor;
            colorDialog1.Color = SeperatorColor;
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SeperatorColor = colorDialog1.Color;
                panel2.BackColor = SeperatorColor;
            }
        }


        private void panel3_DoubleClick(object sender, EventArgs e)
        {
            panel3.BackColor = OutlineColor;
            colorDialog1.Color = OutlineColor;
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                OutlineColor = colorDialog1.Color;
                panel3.BackColor = OutlineColor;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int T;
            if (int.TryParse(textBox4.Text, out T))
            {
                if (T >= 0)
                {
                    Outline = T;
                }
            }
            else
            {
                textBox3.Text = "" + Seperator;
            }
        }
    }
}
