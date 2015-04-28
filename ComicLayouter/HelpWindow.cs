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
    public partial class HelpWindow : Form
    {
        public HelpWindow()
        {
            InitializeComponent();
        }

        private void HelpWindow_Load(object sender, EventArgs e)
        {
            panel1.Dock = DockStyle.Fill;
            panel2.Dock = DockStyle.Fill;
            panel2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (panel1.Visible)
            {
                panel1.Visible = false;
                panel2.Visible = true;
                button1.Text = "Close";
            }
            else
            {
                Close();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
