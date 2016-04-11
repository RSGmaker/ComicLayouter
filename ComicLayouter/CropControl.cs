using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ComicLayouter
{
    public partial class CropControl : Form
    {
        public Form1 F;
        public CropControl(Form1 F)
        {
            this.F = F;
            InitializeComponent();
        }
        public int IWidth
        {
            get
            {
                //return trackBar1.Value;
                return trackBar1.Value - trackBar3.Value;
            }
        }
        public int IHeight
        {
            get
            {
                //return trackBar2.Value;
                return trackBar2.Value - trackBar4.Value;
            }
        }
        public int IX
        {
            get
            {
                return trackBar3.Value;
            }
            set
            {
                trackBar3.Value = value;
            }
        }
        public int IY
        {
            get
            {
                return trackBar4.Value;
            }
            set
            {
                trackBar4.Value = value;
            }
        }
        public int IX2
        {
            get
            {
                return trackBar1.Value;
            }
            set
            {
                trackBar1.Value = value;
            }
        }
        public int IY2
        {
            get
            {
                return trackBar2.Value;
            }
            set
            {
                trackBar2.Value = value;
            }
        }
        public Size CropSize(Size size)
        {
            return new Size((int)(size.Width * (IWidth * 0.01)), (int)(size.Height * (IHeight * 0.01)));
        }
        public Bitmap Crop(Bitmap B)
        {
            if (IWidth == 100 && IHeight == 100)
            {
                //return B;
                return new Bitmap(B);
            }
            else
            {
                Size s = CropSize(B.Size);
                Bitmap ret = new Bitmap(s.Width, s.Height);
                Graphics G = Graphics.FromImage(ret);
                Point P = new Point((int)(B.Size.Width * (IX * 0.01)), (int)(B.Size.Height * (IY * 0.01)));
                Size Sz = new Size((int)(B.Size.Width * (IWidth * 0.01)), (int)(B.Size.Height * (IHeight * 0.01)));
                G.DrawImage(B, new Rectangle(Point.Empty, ret.Size), new Rectangle(P, Sz), GraphicsUnit.Pixel);
                G.Dispose();
                return ret;
            }
        }

        //the direction the crop is based on.
        public int anchorH = 1;
        public int anchorV = 1;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.NumPad1)
            {
                anchorH = 0;
                anchorV = 2;
            }
            if (keyData == Keys.NumPad2)
            {
                anchorH = 1;
                anchorV = 2;
            }
            if (keyData == Keys.NumPad3)
            {
                anchorH = 2;
                anchorV = 2;
            }
            if (keyData == Keys.NumPad4)
            {
                anchorH = 0;
                anchorV = 1;
            }
            if (keyData == Keys.NumPad5)
            {
                anchorH = 1;
                anchorV = 1;
            }
            if (keyData == Keys.NumPad6)
            {
                anchorH = 2;
                anchorV = 1;
            }
            if (keyData == Keys.NumPad7)
            {
                anchorH = 0;
                anchorV = 0;
            }
            if (keyData == Keys.NumPad8)
            {
                anchorH = 1;
                anchorV = 0;
            }
            if (keyData == Keys.NumPad9)
            {
                anchorH = 2;
                anchorV = 0;
            }
            Text = "CropControl " + anchorH + "," + anchorV;
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            TrackBar T = (TrackBar)sender;
            toolTip1.SetToolTip(T, T.Value + "%");
            toolTip1.Show(T.Value + "%", T, 2000);
            Update();
        }

        private void CropControl_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(trackBar2, trackBar2.Value + "%");
            toolTip1.Show(trackBar2.Value + "%", trackBar2, 2000);
        }
        public void Update()
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
            }
            if (F.SelectedPanel != null)
            {
                pictureBox1.Image = Crop(F.img[F.SelectedPanel.ind]);
            }
        }

        private void CropControl_VisibleChanged(object sender, EventArgs e)
        {
            Update();
        }
    }
}
