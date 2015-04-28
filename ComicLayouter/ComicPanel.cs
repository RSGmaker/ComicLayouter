using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ComicLayouter
{
    /// <summary>
    /// The visual representation of a Panel/Frame used for a Comic/Gif.
    /// this is the images you see in the GUI.
    /// </summary>
    public partial class ComicPanel : UserControl
    {
        public Form1 form1;
        public int _ind;
        
        public int delay = 0;
        public double ratio = 1;
        public int ind
        {
            get
            {
                return _ind;
            }
            set
            {
                _ind = value;
            }
        }
        public ComicPanel(Form1 form,string str,Image img,int i)
        {
            InitializeComponent();
            label1.Text = str;
            form1 = form;
            SetImage(img);
            ind = i;
            if (_ind > 0)
            {
                Control C = form1.panel1.Controls[_ind - 1];
                Location = new Point(0, C.Bounds.Bottom);
            }
            else
            {
                Location = Point.Empty;
            }
        }
        public void SetImage(Image i)
        {
            Bitmap B = form1.Borderify(form1.Crop((Bitmap)i));
            Bitmap P;
            
            Size S = B.Size;
            if (Width < S.Width)
            {
                double D;
                D = B.Height / ((double)B.Width);
                S = new Size(Width, (int)(Width * D));
            }
            
            S.Width = (int)(S.Width / ratio);
            if (_ind > 0 || ratio != 1)
            {
                P = new Bitmap((int)(S.Width), S.Height);
                Graphics G = Graphics.FromImage(P);
                G.DrawImage(B, new Rectangle(Point.Empty, new Size((int)(P.Width * ratio),P.Height)), new Rectangle(new Point(0, form1.Borders.Border.Height), new Size(B.Width, B.Height - form1.Borders.Border.Height)), GraphicsUnit.Pixel);
                G.Dispose();
            }
            else
            {
                P = new Bitmap(B, S);
            }
            
            pictureBox1.Image = P;
            if (B != i)
            {
                B.Dispose();
            }
        }

        private void BMP_Load(object sender, EventArgs e)
        {

        }
        public void Select()
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label1.ForeColor = Color.Blue;
            form1.SelectedPanel = this;
            if (!ContainsFocus)
            {
            }
        }
        public void Unselect()
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            label1.ForeColor = Color.Black;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            form1.BMPCommands(keyData, this);
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void BMP_Enter(object sender, EventArgs e)
        {
            Select();
            
        }

        private void BMP_Leave(object sender, EventArgs e)
        {
            Unselect();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Focus();
        }

        private void pictureBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            form1.BMPCommands(e.KeyCode, this);
        }

        private void BMP_Validating(object sender, CancelEventArgs e)
        {
            //Location = new Point(0, _ind * 64);
        }

        private void BMP_Resize(object sender, EventArgs e)
        {
            pictureBox1.Location = Point.Empty;
            pictureBox1.Size = Size;
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            form1.EditImage();
        }
    }
}
