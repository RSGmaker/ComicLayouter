using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ComicLayouter
{
    public partial class Stretcher : Form
    {
        public Stretcher()
        {
            InitializeComponent();
        }
        public Stretcher(Size S)
        {
            InitializeComponent();
            if (S != Size.Empty)
            {
                checkBox1.Checked = true;
                W = S.Width;
                H = S.Height;
                textBox1.Text = "" + W;
                textBox2.Text = "" + H;
            }
        }
        public Boolean Use=false;

        public int W = 0;
        public int H = 0;
        public Size GetStretch
        {
            get
            {
                if (Use)
                {
                    return new Size(W, H);
                }
                return Size.Empty;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out W))
            {
                textBox1.Text = "" + W;
            }
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox2.Text, out H))
            {
                textBox2.Text = "" + H;
            }
        }

        private void Stretcher_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = checkBox1.Checked;
            textBox2.Enabled = checkBox1.Checked;
            Use = checkBox1.Checked;
        }

        private void Stretcher_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            //Hide();
        }
    }
}
