using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;

namespace ComicLayouter
{
    /// <summary>
    /// This usercontrol acts as a buffer protecting walfaswindow from crashing if the activeX interop dll was removed.
    /// </summary>
    public partial class Walfas : UserControl
    {
        public Walfas()
        {
            InitializeComponent();
        }

        private void Walfas_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            Dock = DockStyle.Fill;
            //BackColor = Color.Transparent;
            BackColor = Color.White;
        }
    }
}
