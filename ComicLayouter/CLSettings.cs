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
    public partial class CLSettings : Form
    {

        public CLSettings()
        {
            InitializeComponent();
        }
        public bool useflashengine
        {
            get
            {
                return Config.Get<bool>("useflashengine", "False");
            }
            set
            {
                Config.Set("useflashengine", value);
            }
        }
        public string imageeditor
        {
            get
            {
                return Config.Get<string>("imageeditor", "");
            }
            set
            {
                Config.Set("imageeditor", value);
            }
        }

        private void CLSettings_Load(object sender, EventArgs e)
        {

        }
    }
}
