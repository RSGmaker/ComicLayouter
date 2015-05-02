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
    public partial class CustomPanel : UserControl
    {
        bool selected = false;
        public CustomLayout parent;
        Point mouseOrigin = Point.Empty;
        public Bitmap B;
        public CustomPanel()
        {
            InitializeComponent();
        }

        private void CustomPanel_Load(object sender, EventArgs e)
        {
            //SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //BackColor = Color.Transparent;
            //parent = (CustomLayout)Parent.Parent;
            pictureBox1.Dock = DockStyle.Fill;
        }
        public int SetImage(Bitmap B)
        {
            this.B = B;
            pictureBox1.Image = parent.form.Borderify(B);
            int H = (int)(Width * (B.Height / ((float)B.Width)));
            Height = H;
            return H;
        }

        private void CustomPanel_MouseDown(object sender, MouseEventArgs e)
        {
            //parent = (CustomLayout)Parent.Parent.Parent;
            selected = true;
            parent.dragging = this;
            mouseOrigin = e.Location;
        }

        private void CustomPanel_MouseUp(object sender, MouseEventArgs e)
        {
            //parent = (CustomLayout)Parent.Parent.Parent;
            selected = false;
            parent.dragging = null;
        }

        /// <summary>
        /// customlayouts original design was a drag and drop system
        /// this is left over code.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (selected)
            {
                //Location = new Point(Location.X + (e.X - (mouseOrigin.X)), Location.Y + (e.Y - (mouseOrigin.Y)));
            }
        }

        /// <summary>
        /// an attempt at seeing if i could send shortcuts to the main comiclayouter form which would've enabled changing panel order and deleting panels
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Form1 CL = parent.form;
            CL.BMPCommands(keyData, ((ComicPanel)CL.panel1.Controls[CL.img.IndexOf(B)]));
            parent.redopanels();
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
