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
    public partial class CustomResolution : Form
    {
        public CustomResolution(int width,int height,bool activated)
        {
            InitializeComponent();
            this.width = width;
            this.height = height;
            textBox1.Text = "" + width;
            textBox2.Text = "" + height;
            checkBox1.Checked = activated;
        }

        private void CustomResolution_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = checkBox1.Checked;
            textBox2.Enabled = checkBox1.Checked;
        }
        public int width;
        public int height;
        public bool CRenabled
        {
            get
            {
                return width > 0 && height > 0 && checkBox1.Checked;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int temp = 0;
            if (int.TryParse(textBox1.Text, out temp))
            {
                if (temp > 0)
                {
                    width = temp;
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int temp = 0;
            if (int.TryParse(textBox2.Text, out temp))
            {
                if (temp > 0)
                {
                    height = temp;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
