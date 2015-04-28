using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Controls;

namespace ComicLayouter
{
    public partial class SpellCheckForm : Form
    {
        System.Windows.Controls.TextBox T = new System.Windows.Controls.TextBox();
        //old dictionary before the dictionary.txt file was implemented.
        /*string[] dict = { "alice", "margatroid", "aya", "shameimaru", "byakuren", "hijiri", "chen", "chiyuri", "kitashirakawa", "cirno", "daiyousei", "eirin", "yagokoro", "elis", "ellen", "elly", "flandre", "scarlet", "flower", "tank", "fujiwara", "mokou", "gengetsu", "genjii", "hatate", "himekaidou", "hieda", "akyu", "hina", "kagiyama", "hong", "meiling", "horıkawa", "raıko", "hourai", "ichirin", "kumoi", "iku", "nagae", "kaguya", "houraisan", "kana", "anaberal", "kanako", "yasaka", "kasen", "ibara", "keine", "kamishirasawa", "kıjın", "seıja", "kikuri", "kisume", "koakuma", "kogasa", "tatara", "koishi", "komeiji", "komachi", "onozuka", "konngara", "kotohime", "kurumi", "kyouko", "kasodani", "layla", "prismriver", "letty", "whiterock", "lily", "white", "louise", "luna", "child", "lunasa", "prismriver", "lyrica", "prismriver", "mai", "mamizou", "futatsuiwa", "maribel", "han", "marisa", "kirisame", "medicine", "melancholy", "meira", "merlin", "prismriver", "mima", "minamitsu", "murasa", "minoriko", "aki", "momiji", "inubashiri", "mononobe", "futo", "mugetsu", "myouren", "hijiri", "mystia", "lorelei", "nazrin", "nitori", "kawashiro", "nue", "houjuu", "orange", "parsee", "mizuhashi", "patchouli", "knowledge", "ran", "yakumo", "reimu", "hakurei", "reisen", "reisen", "udongein", "inaba", "remilia", "scarlet", "renko", "usami", "rika", "rikako", "asakura", "rin", "kaenbyou", "rin", "satsuki", "rinnosuke", "morichika", "rumia", "ruukoto", "saigyou", "ayakashi", "sakuya", "izayoi", "sanae", "kochiya", "sara", "sariel", "satori", "komeiji", "seiga", "kaku", "shanghai", "shikieiki", "yamaxanadu", "shingyoku", "shinki", "shizuha", "aki", "shou", "toramaru", "soga", "tojiko", "sokrates", "star", "sapphire", "suika", "ibuki", "sukuma", "shınmyoumaru", "sunny", "milk", "suwako", "moriya", "tenshi", "hinanawi", "tewi", "inaba", "tokiko", "toyosatomimi", "miko", "tsukumo", "benben", "tsukumo", "yatsunashi", "unzan", "utsuho", "reiuji", "watatsuki", "toyohime", "watatsuki", "yorihime", "wriggle", "nightbug", "yamame", "kurodani", "yoshika", "miyako", "youki", "konpaku", "youmu", "konpaku", "yukari", "yakumo", "yuki", "yumeko", "yumemi", "okazaki", "yuugenmagan", "yuugi", "hoshiguma", "yuuka", "kazami", "yuyuko", "saigyouji",
                        "danmaku","sdm","kourindou","youkai","eientei","hakugyokurou","sanzu","makai","mayohiga","muenzuka","mugenkan","gensokyo","reimaden","senkai","genbu"};*/
        public SpellCheckForm(string text="")
        {
            InitializeComponent();
            T.SpellCheck.IsEnabled = true;
            T.SpellCheck.CustomDictionaries.Add(new Uri(System.IO.Directory.GetCurrentDirectory() + "\\Dictionary.txt"));
            ST = new System.Windows.Forms.RichTextBox();
            richTextBox1.Text = text;
        }

        private void Textinput_Load(object sender, EventArgs e)
        {
            if (richTextBox1.Text=="" && Clipboard.ContainsText())
            {
                richTextBox1.Text = Clipboard.GetText();
            }
            richTextBox1_TextChanged(null, null);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        public string ltext="";
        public System.Windows.Forms.RichTextBox ST;
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            T.Text = richTextBox1.Text;
            int i = 0;
            bool ok = true;
            string S = T.Text;
            ST.Rtf = richTextBox1.Rtf;
            int o = richTextBox1.SelectionStart;
            int lg = richTextBox1.SelectionLength;
            ST.SelectionColor = Color.Black;
            ST.Select(i, richTextBox1.Text.Length-i);
            ST.SelectionColor = Color.Black;
            while (i < S.Length)
            {
                if (ok)
                {
                    if (T.GetSpellingErrorStart(i) >= 0)
                    {
                        int l = S.IndexOf(' ', i + 1);
                        if (l < 0)
                        {
                            l = S.Length;
                        }
                        l = l - i;
                        ST.Select(i, l);
                        string st =ST.SelectedText.ToLower();
                        ST.SelectionColor = Color.Red;
                        ST.SelectionLength = 0;
                        ST.SelectionColor = Color.Black;
                    }
                    else
                    {
                        
                        int l = S.IndexOf(' ', i + 1);
                        if (l < 0)
                        {
                            l = S.Length;
                        }
                        l = l - i;
                    }
                    ok = false;
                }
                if (S[i] == ' ')
                {
                    ok = true;
                }
                i++;
            }
            richTextBox1.Rtf = ST.Rtf;
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.SelectionStart = o;
            richTextBox1.SelectionLength = lg;
            richTextBox1.SelectionColor = Color.Black;
            ltext = T.Text;
            
        }

        // Append text of the given color.
        void AppendText(System.Windows.Forms.RichTextBox box, Color color, string text)
        {
            int start = box.TextLength;
            box.AppendText(text);
            int end = box.TextLength;

            // Textbox may transform chars, so (end-start) != text.Length
            box.Select(start, end - start);
            {
                box.SelectionColor = color;
                // could set box.SelectionBackColor, box.SelectionFont too.
            }
            box.SelectionLength = 0; // clear
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.Text);
            Close();
        }

        private void richTextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void richTextBox1_MouseHover(object sender, EventArgs e)
        {
           
        }
        public static int findprevspace(string text, int index)
        {
            while (index >= 0 && text[index] != ' ')
            {
                index--;
            }
            return index;
        }

        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("test");
            if (e.Button != System.Windows.Forms.MouseButtons.Right)
            {
                return;
            }
            //if ((e.Button & System.Windows.Forms.MouseButtons.Right) != System.Windows.Forms.MouseButtons.None)
            try
            {
                int i = 1;
                //int i = richTextBox1.Text.LastIndexOf(' ', 0, richTextBox1.SelectionStart);
                i = findprevspace(richTextBox1.Text, richTextBox1.SelectionStart);
                if (i == -1)
                {
                    i = 0;
                }
                i++;
                //if (i > 0)
                {
                    //SpellingError SE = T.GetSpellingError(richTextBox1.SelectionStart);
                    SpellingError SE = T.GetSpellingError(i);
                    bool error = T.GetSpellingErrorStart(i) >= 0;
                    if (error)
                    {
                        //MessageBox.Show(SE.Suggestions.ToArray()[0]);
                        string S = "";
                        string[] SG = SE.Suggestions.ToArray();
                        contextMenuStrip1.Items.Clear();
                        if (SG.Length > 0)
                        {
                            S = SG[0];
                            for (int j = 1; j < SG.Length; j++)
                            {
                                //S = S + "\n" + SG[j];
                                contextMenuStrip1.Items.Add(SG[j]);
                            }
                        }
                        contextMenuStrip1.Show(Cursor.Position);
                        //toolTip1.Show("Spelling suggestions:"+S,this,2000);
                        //toolTip1.AutomaticDelay = 1000000;
                        //toolTip1.SetToolTip(richTextBox1, "Spelling suggestions:\n" + S);
                        //toolTip1.Show("Spelling suggestions:" + S, this, 10000);


                    }
                    else
                    {
                        //toolTip1.SetToolTip(richTextBox1, null);
                    }
                }
            }
            catch
            {
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int i = richTextBox1.SelectionStart;
            if (richTextBox1.Text[i] != ' ')
            {
                i = findprevspace(richTextBox1.Text, i);
                if (i < 0)
                {
                    i = 0;
                }
                else
                {
                    i++;
                }
            }
            int l = richTextBox1.Text.IndexOf(' ', i + 1);
            if (l == -1)
            {
                l = richTextBox1.Text.Length;
            }
            l = l-i;
            richTextBox1.Select(i, l);
            richTextBox1.SelectedText = e.ClickedItem.Text;
            string RRR = richTextBox1.Rtf;
        }
    }
}
