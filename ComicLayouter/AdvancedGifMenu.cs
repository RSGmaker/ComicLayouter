using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ComicLayouter
{
    public partial class AdvancedGifMenu : Form
    {
        public GifPreview gif;
        public AdvancedGifMenu()
        {
            InitializeComponent();
        }

        private void AdvancedGifMenu_Load(object sender, EventArgs e)
        {
            if (!File.Exists("ffmpeg.exe"))
            {
                button3.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                label2.ForeColor = Color.Blue;
                //Font fnt = new Font(Font.FontFamily,)
                label2.Font = new Font(label2.Font,FontStyle.Underline/* | FontStyle.Bold | FontStyle.Italic*/);
                label2.Cursor = Cursors.Hand;
                //label2.Font.Underline = true;
            }
        }

        private void AdvancedGifMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gif.button3_Click(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog1.FileName;
                LoadAudio(path);
            }
        }

        public void LoadAudio(string path,bool showerror=true)
        {

            bool loaded = gif.LoadAudio(path);
            //if (gif.LoadAudio(path))
            if (File.Exists(path))
            {
                checkBox1.Checked = true;
                label1.Text = path;
                toolTip1.SetToolTip(label1, path);
                gif.F.audiopath = path;
                if (!loaded && showerror)
                {
                    MessageBox.Show("The selected file cannot be played in the gif previewer, exported videos may still support the audio.");
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            gif.audioenabled = checkBox1.Checked;
            if (checkBox1.Checked)
            {
                gif.F.audiopath = label1.Text;
            }
            else
            {
                gif.F.audiopath = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            gif.PreArg = "";
            gif.SaveVideo();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            gif.SaveGimpLayers();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked && File.Exists(label1.Text))
            {
                gif.PreArg = "-loop 1 ";
                gif.SaveVideo();
            }
            else
            {
                MessageBox.Show("You must have an audio file selected to use this.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            gif.SaveVideo(false);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (!File.Exists("ffmpeg.exe"))
            {
                System.Diagnostics.Process.Start("http://sta.sh/01ufy9lcztoy");
            }
        }
    }
}
