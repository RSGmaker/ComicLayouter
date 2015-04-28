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
    public partial class FlashPlayer : UserControl
    {
        public FlashPlayer()
        {
            InitializeComponent();
        }
        AxShockwaveFlashObjects.AxShockwaveFlash flash;
        public string Movie
        {
            get
            {
                return flash.Movie;
            }
            set
            {
                flash.Movie = value;
            }
        }
        public object getinterface()
        {
            return flash.GetOcx();
        }

        private void FlashPlayer_Load(object sender, EventArgs e)
        {
            flash = new AxShockwaveFlashObjects.AxShockwaveFlash();
            flash.Dock = DockStyle.Fill;
            Controls.Add(flash);

        }
    }
}
