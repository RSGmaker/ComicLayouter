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
    public partial class CustomLayout : Form
    {
        public CustomPanel dragging = null;
        public Form1 form = null;
        public List<Bitmap> imagelist = null;
        public List<CustomRow> rows;
        public Size LSize;
        public bool allowredo = true;
        public CustomLayout()
        {
            InitializeComponent();
            rows = new List<CustomRow>();
        }

        private void CustomLayout_Load(object sender, EventArgs e)
        {
            LSize = Size;
            panel1.Size = new System.Drawing.Size(ClientSize.Width - 2/*-18*/, panel1.Height/* ClientSize.Height - panel1.Location.Y*/);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging != null)
            {
                //dragging.Location = new Point(e.X - (dragging.Width / 2), e.Y - (dragging.Height / 2));
            }
        }
        public void redopanels()
        {
            if (imagelist == null)
            {
                imagelist = form.img;
            }
            redopanels(imagelist);
        }
        public void redopanels(List<Bitmap> img)
        {
            if (!allowredo)
            {
                return;
            }
            allowredo = false;
            panel1.BackColor = form.Borders.SeperatorColor;
            int i = 0;
            int r = 0;
            int Y = 0;
            int H = 0;
            int PH = 0;
            if (checkBox1.Checked)
            {
                List<Bitmap> IMG = new List<Bitmap>();

            }
            this.SuspendLayout();
            while (i < img.Count)
            {
                //if (!lastlist.Contains(form.img[i]))
                if (r >= rows.Count)
                {
                    CustomRow CR = new CustomRow();
                    
                    CR.CL = this;
                    CR.Width = panel1.Width;// - 18;
                    
                    rows.Add(CR);
                    panel1.Controls.Add(CR);
                }
                /*if (rows[r].itemcount < rows[r].panels)
                {
                    r++;
                    continue;
                }*/
                
                if (r > 0)
                {
                    //rows[r].Location = new Point(0, rows[r - 1].Location.Y + rows[r - 1].Height);
                    rows[r].Location = new Point(0, Y);
                }
                int c = 0;
                List<Bitmap> BL = new List<Bitmap>();
                if (!checkBox1.Checked)
                {
                    //horizontaly sorted panels
                    while (i < img.Count && c < rows[r].itemcount)
                    {
                        BL.Add(img[i]);
                        c++;
                        i++;
                    }
                }
                else
                {
                    //verticly sorted panels
                    while (i < img.Count && c < rows[r].itemcount)
                    {
                        //current image index
                        int j = 0;
                        //row iterator
                        int rr = 0;
                        //column iterator
                        int cc = 0;
                        bool ok = !(r==0 && c==0);
                        while (ok)
                        {
                            while (rr < rows.Count && ok)
                            {
                                if (cc >= c && rr >= r)
                                {
                                    //check if we finished
                                    ok = false;
                                }
                                //if this has the column were on(otherwise skip)
                                if (rows[rr].itemcount > cc && ok)
                                {
                                    
                                    if (j < img.Count - 1)
                                    {
                                        j++;
                                    }
                                    else
                                    {
                                        //if this code executes, then i've messed up...
                                        j = j;
                                    }
                                }
                                rr++;
                                
                            }
                            cc++;
                            rr = 0;
                        }
                        BL.Add(img[j]);
                        c++;
                        i++;
                    }
                }
                rows[r].setpanels(BL);
                H = rows[r].Height;
                PH = Y + H;
                if (form.Borders.Seperator > 0)
                {
                    //H = (int)Math.Ceiling(((double)form.Borders.Seperator / rows[r].Height) * rows[r].Height);
                    H += (int)Math.Ceiling(((double)form.Borders.Seperator / img[i-1].Height) * H);
                }
                Y += H;
                r++;
                if (BL.Count == 0)
                {
                    r--;
                }
                
                
                //i++;
            }
            panel1.Height = PH;
            while (r < rows.Count)
            {
                panel1.Controls.Remove(rows[r]);
                rows.RemoveAt(r);
            }
            this.ResumeLayout(true);
            allowredo = true;
        }

        private void CustomLayout_VisibleChanged(object sender, EventArgs e)
        {
            if (!Visible)
            {
                return;
            }
            redopanels();
            //Width = form.MW;
            /*if (lastlist == null)
            {
                lastlist = new List<Bitmap>();
            }
            int i = 0;
            while (i < lastlist.Count)
            {
                if (!form.img.Contains(lastlist[i]))
                {
                    lastlist.RemoveAt(i);
                    i--;
                }
                i++;
            }
            i = 0;
            while (i < form.img.Count)
            {
                if (!lastlist.Contains(form.img[i]))
                {
                    CustomPanel CP = new CustomPanel();
                    CP.SetImage(form.img[i]);
                    panel1.Controls.Add(CP);
                    lastlist.Add(form.img[i]);
                }
                i++;
            }*/
        }

        private void CustomLayout_ResizeEnd(object sender, EventArgs e)
        {
            if (Size != LSize)
            {
                panel1.Size = new System.Drawing.Size(ClientSize.Width-2/*-18*/,panel1.Height/* ClientSize.Height - panel1.Location.Y*/);
                int r = 0;
                while (r < rows.Count)
                {
                    rows[r].Width = panel1.Width;//-18;
                    r++;
                }
                redopanels();
                LSize = Size;
            }
        }
        /// <summary>
        /// the minimum scaling that can be used without any pixelation.
        /// </summary>
        public int getscale
        {
            get
            {
                int scale = int.MaxValue;
                int i = 0;
                while (i < rows.Count)
                {
                    scale = Math.Min(scale, rows[i].itemcount);
                    i++;
                }
                return scale;
            }
        }
        /// <summary>
        /// how wide the comic should be when it is saved.
        /// </summary>
        public int maxwidth
        {
            get
            {
                return form.MW * getscale;
            }
        }

        public bool fixsize()
        {
            if (rows.Count > 0)
            {
                int W = rows[0].panel1.Width;
                if (maxwidth != W)
                {
                    int WW = maxwidth - W;
                    //Width += WW;
                    Size sz = Size;
                    Size = new Size(Width + WW, Height);
                    OnResizeEnd(null);
                    Invalidate();
                    if (sz != Size)
                    {
                        return fixsize();
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rows.Count > 0)
            {
                if (!fixsize())
                {
                    return;
                }
                int W = rows[0].panel1.Width;
                if (maxwidth == W)
                {
                    Bitmap B = new Bitmap(maxwidth, panel1.Height);
                    panel1.DrawToBitmap(B, new Rectangle(0, 0, B.Width, B.Height));
                    if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        B.Save(saveFileDialog1.FileName);
                    }
                    B.Dispose();
                }
                /*int W = rows[0].panel1.Width;
                if (form.MW == W)
                {
                    Bitmap B = new Bitmap(form.MW, panel1.Height);
                    panel1.DrawToBitmap(B,new Rectangle(0,0,B.Width,B.Height));
                    if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        B.Save(saveFileDialog1.FileName);
                    }
                    B.Dispose();
                }
                else
                {
                    int WW = form.MW - W;
                    //Width += WW;
                    Size = new Size(Width + WW, Height);
                    OnResizeEnd(null);
                    Invalidate();
                    button1_Click(sender, e);
                }*/
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form.bordersSeperatorsToolStripMenuItem_Click(null, null);
            //form.Borders.ShowDialog();
            redopanels();
        }

        /// <summary>
        /// prioritizes numerical sorting, used in animated comics.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private int filesort(string a, string b)
        {
            a = System.IO.Path.GetFileName(a);
            b = System.IO.Path.GetFileName(b);
            string ta = System.Text.RegularExpressions.Regex.Match(a, @"\d+").Value;
            string tb = System.Text.RegularExpressions.Regex.Match(b, @"\d+").Value;
            int i = 0;
            if (ta.Length > 0 && tb.Length > 0)
            {
                int ai = 0;
                int bi = 0;
                if (int.TryParse(ta, out ai) && int.TryParse(tb, out bi))
                {
                    i = ai.CompareTo(bi);
                }
            }
            //int i = ta.CompareTo(tb);
            if (i != 0)
            {
                return i;
            }
            return a.CompareTo(b);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show(this,"To use this feature you must select a root folder that contains a panel1 panel2 panel3...etc folders containing the frames for that panel.","Explanation", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            if (!fixsize())
            {
                return;
            }
            if (MessageBox.Show(this, "To use this feature you must select an animation folder that contains subfolders for:panel1 panel2 panel3...etc,these folders must contain pngs for each frame of animation.\n\nThe generated comic frames are saved into the 'anicomicframes' folder.\nit is recommended to use gimp to compile the animated gif from the comic frames this feature generates.\n(use filters->animation->'optimize for gif', to keep the file size and performance reasonable.)", "Explanation", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                
                //fixsize();
                string OText = Text;
                string root = folderBrowserDialog1.SelectedPath;
                if (System.IO.Directory.Exists(root))
                {
                    List<String> dirs = new List<string>();
                    dirs.AddRange(System.IO.Directory.GetDirectories(root));
                    dirs.AddRange(System.IO.Directory.GetFiles(root, "panel*.png"));
                    var i = 0;
                    var maxcount = 0;
                    List<List<Bitmap>> panels = new List<List<Bitmap>>();
                    Text = "Searching panel frames";
                    while (i < dirs.Count)
                    {
                        if (System.IO.Path.GetFileName(dirs[i]).ToLower().StartsWith("panel"))
                        {
                            List<Bitmap> panel = new List<Bitmap>();
                            var c = 0;
                            List<string> T = null;
                            if (System.IO.Directory.Exists(dirs[i]))
                            {
                                T = new List<string>(System.IO.Directory.GetFiles(dirs[i], "*.png"));
                                T.Sort(filesort);
                            }
                            else if (System.IO.File.Exists(dirs[i]))
                            {
                                T = new List<string>();
                                T.Add(dirs[i]);
                            }
                            if (T != null)
                            {
                                string[] images = T.ToArray();
                                while (c < images.Length)
                                {
                                    System.IO.FileStream F = new System.IO.FileStream(images[c], System.IO.FileMode.Open);
                                    Bitmap B = (Bitmap)Bitmap.FromStream(F);
                                    F.Close();
                                    panel.Add(B);
                                    c++;
                                }

                                if (panel.Count > 0)
                                {
                                    panels.Add(panel);
                                    if (panel.Count > maxcount)
                                    {
                                        maxcount = panel.Count;
                                    }
                                }
                            }
                        }
                        i++;
                    }
                    if (panels.Count == 0)
                    {
                        Text = OText;
                        MessageBox.Show("The folder selected was not valid to make an animated comic.\nno panel folders with images were found.");
                        return;
                    }
                    i = 0;
                    List<Bitmap> anicomics = new List<Bitmap>();
                    Bitmap BT = new Bitmap(maxwidth, panel1.Height);
                    while (i < maxcount)
                    {
                        Text = i + " of " + maxcount + " frames generated";
                        var c = 0;
                        List<Bitmap> LB = new List<Bitmap>();
                        form.button3_Click(null, null);
                        while (c < panels.Count)
                        {
                            Bitmap TB = panels[c][i % panels[c].Count];
                            LB.Add(TB);
                            c++;
                        }
                        redopanels(LB);
                        Invalidate();

                        //Bitmap B = new Bitmap(maxwidth, panel1.Height);
                        panel1.DrawToBitmap(BT, new Rectangle(0, 0, BT.Width, BT.Height));
                        

                        //Bitmap B = CompileComic(LB);
                        if (!System.IO.Directory.Exists("anicomicframes"))
                        {
                            System.IO.Directory.CreateDirectory("anicomicframes");
                        }
                        string T = "anicomicframes\\" + i + ".png";
                        {
                            //B.Save(saveFileDialog1.FileName);
                            BT.Save(T);
                        }
                        
                        i++;
                    }
                    BT.Dispose();
                    Text = "Success!";
                    i = 0;
                    while (i < panels.Count)
                    {
                        var c = 0;
                        while (c < panels[i].Count)
                        {
                            panels[i][c].Dispose();
                            c++;
                        }
                        i++;
                    }
                    panels = null;

                    {
                        MessageBox.Show(this, "The animated comic frames were saved into the 'anicomicframes' folder successfully!\n\nusing gimp to assemble the generated frames is recommended.", "Finished generating animated comic frames");
                    }
                }
                Text = OText;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            redopanels();
        }
    }
}
