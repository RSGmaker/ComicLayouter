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
        public string Quality
        {
            get
            {
                return flash.Quality2;
            }
            set
            {
                flash.Quality2 = value;
            }
        }
        public Color BackgroundColor
        {
            get
            {
                var C = flash.BackgroundColor;
                return Color.FromArgb(C >> 16 & 255, C >> 8 & 255, C & 255);
            }
            set
            {
                int C = (value.R << 16) + (value.G << 8) + (value.B);
                flash.BackgroundColor = C;
            }
        }
        public object getinterface()
        {
            return flash.GetOcx();
        }

        public void Rewind()
        {
            flash.Rewind();
        }

        private void FlashPlayer_Load(object sender, EventArgs e)
        {
            flash = new AxShockwaveFlashObjects.AxShockwaveFlash();
            flash.Dock = DockStyle.Fill;
            Controls.Add(flash);
            //flash.WMode = "Transparent";
            //BackgroundColor = Color.PowderBlue;

        }
        public void SetBGColor(Color color)
        {
            int C = (color.R<< 16)+(color.G<< 8)+(color.B);
            flash.BackgroundColor = C;
        }
        
    }
}
