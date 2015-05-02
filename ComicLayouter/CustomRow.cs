using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ComicLayouter
{
    /// <summary>
    /// A row of CustomPanels
    /// </summary>
    public partial class CustomRow : UserControl
    {
        public int itemcount = 1;
        public CustomLayout CL;
        public Size LSize;
        public CustomRow()
        {
            InitializeComponent();
        }

        private void CustomRow_Load(object sender, EventArgs e)
        {
            /*SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;*/
            LSize = Size;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (panel1.Controls.Count >= itemcount)
            {
                itemcount = panel1.Controls.Count+1;
                CL.redopanels();
            }
        }
        public int panels
        {
            get
            {
                return panel1.Controls.Count;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (itemcount > 1)
            {
                itemcount--;
                CL.redopanels();
            }
        }
        public void setpanels(List<Bitmap> BL)
        {
            panel1.Controls.Clear();
            BackColor = CL.form.Borders.BorderColor;
            int i = 0;
            int W = panel1.Width / BL.Count;
            int H = 58;
            while (i < BL.Count)
            {
                CustomPanel CP = new CustomPanel();
                CP.Location = new Point(W * i, 0);
                /*if (i == BL.Count - 1)
                {
                    //ensure there is no leftover blank space
                    CP.Width = panel1.Width - CP.Location.X;
                }
                else*/
                {
                    CP.Width = W;
                }
                
                CP.parent = CL;
                int T = CP.SetImage(BL[i]);
                if (T > H)
                {
                    H = T;
                }
                panel1.Controls.Add(CP);
                i++;
            }
            Height = H;
            panel1.Height = Height;
            itemcount = BL.Count;
        }

        private void CustomRow_Resize(object sender, EventArgs e)
        {
            int X = Size.Width - LSize.Width;
            int Y = Size.Height - LSize.Height;
            button1.Location = new Point(Width - 54, button1.Location.Y);
            button2.Location = new Point(Width - 26, button2.Location.Y);
            panel1.Width = Width - 60;
        }
    }
}
