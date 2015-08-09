namespace ComicLayouter
{
    partial class WalfasWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fullScreenModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.httpwalfasorgToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCreateswfSaveFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.takeSnapshotF6ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.walfasAndComicLayouterControlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spellCheckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.changeWindowSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.overlay1 = new ComicLayouter.Overlay();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.overlay1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(389, 100);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 30;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // webBrowser1
            // 
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.Location = new System.Drawing.Point(49, 75);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(284, 262);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Url = new System.Uri("", System.UriKind.Relative);
            this.webBrowser1.WebBrowserShortcutsEnabled = false;
            // 
            // timer3
            // 
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.overlay1);
            this.panel1.Controls.Add(this.webBrowser1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(644, 442);
            this.panel1.TabIndex = 4;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fullScreenModeToolStripMenuItem,
            this.httpwalfasorgToolStripMenuItem,
            this.toolStripMenuItem2,
            this.takeSnapshotF6ToolStripMenuItem,
            this.walfasAndComicLayouterControlsToolStripMenuItem,
            this.openCreateswfSaveFolderToolStripMenuItem,
            this.changeWindowSizeToolStripMenuItem,
            this.spellCheckToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(248, 180);
            // 
            // fullScreenModeToolStripMenuItem
            // 
            this.fullScreenModeToolStripMenuItem.Name = "fullScreenModeToolStripMenuItem";
            this.fullScreenModeToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.fullScreenModeToolStripMenuItem.Text = "Full Screen Mode!(F4)";
            this.fullScreenModeToolStripMenuItem.Click += new System.EventHandler(this.fullScreenModeToolStripMenuItem_Click);
            // 
            // httpwalfasorgToolStripMenuItem
            // 
            this.httpwalfasorgToolStripMenuItem.Name = "httpwalfasorgToolStripMenuItem";
            this.httpwalfasorgToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.httpwalfasorgToolStripMenuItem.Text = "http://www.walfas.org";
            this.httpwalfasorgToolStripMenuItem.Click += new System.EventHandler(this.httpwalfasorgToolStripMenuItem_Click);
            // 
            // openCreateswfSaveFolderToolStripMenuItem
            // 
            this.openCreateswfSaveFolderToolStripMenuItem.Name = "openCreateswfSaveFolderToolStripMenuItem";
            this.openCreateswfSaveFolderToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.openCreateswfSaveFolderToolStripMenuItem.Text = "Open Create.swf save folder(F10)";
            this.openCreateswfSaveFolderToolStripMenuItem.Click += new System.EventHandler(this.openCreateswfSaveFolderToolStripMenuItem_Click);
            // 
            // takeSnapshotF6ToolStripMenuItem
            // 
            this.takeSnapshotF6ToolStripMenuItem.Name = "takeSnapshotF6ToolStripMenuItem";
            this.takeSnapshotF6ToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.takeSnapshotF6ToolStripMenuItem.Text = "Take Snapshot(F6)";
            this.takeSnapshotF6ToolStripMenuItem.Click += new System.EventHandler(this.takeSnapshotF6ToolStripMenuItem_Click);
            // 
            // walfasAndComicLayouterControlsToolStripMenuItem
            // 
            this.walfasAndComicLayouterControlsToolStripMenuItem.Name = "walfasAndComicLayouterControlsToolStripMenuItem";
            this.walfasAndComicLayouterControlsToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.walfasAndComicLayouterControlsToolStripMenuItem.Text = "Controls(F1)";
            this.walfasAndComicLayouterControlsToolStripMenuItem.Click += new System.EventHandler(this.walfasAndComicLayouterControlsToolStripMenuItem_Click);
            // 
            // spellCheckToolStripMenuItem
            // 
            this.spellCheckToolStripMenuItem.Name = "spellCheckToolStripMenuItem";
            this.spellCheckToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.spellCheckToolStripMenuItem.Text = "SpellCheck(F3)";
            this.spellCheckToolStripMenuItem.Click += new System.EventHandler(this.spellCheckToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Enabled = false;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(247, 22);
            this.toolStripMenuItem2.Text = "-------------------------";
            // 
            // changeWindowSizeToolStripMenuItem
            // 
            this.changeWindowSizeToolStripMenuItem.Name = "changeWindowSizeToolStripMenuItem";
            this.changeWindowSizeToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.changeWindowSizeToolStripMenuItem.Text = "Change Window Size(F11)";
            this.changeWindowSizeToolStripMenuItem.Click += new System.EventHandler(this.changeWindowSizeToolStripMenuItem_Click);
            // 
            // overlay1
            // 
            this.overlay1.Location = new System.Drawing.Point(434, 302);
            this.overlay1.Name = "overlay1";
            this.overlay1.Size = new System.Drawing.Size(100, 50);
            this.overlay1.TabIndex = 3;
            this.overlay1.TabStop = false;
            // 
            // WalfasWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 442);
            this.Controls.Add(this.panel1);
            this.Name = "WalfasWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Create.swf F1:Help";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WalfasWindow_FormClosed);
            this.Load += new System.EventHandler(this.WalfasWindow_Load);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.WalfasWindow_Scroll);
            this.Click += new System.EventHandler(this.WalfasWindow_Click);
            this.Resize += new System.EventHandler(this.WalfasWindow_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.overlay1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private Overlay overlay1;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem httpwalfasorgToolStripMenuItem;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fullScreenModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCreateswfSaveFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem takeSnapshotF6ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem walfasAndComicLayouterControlsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem spellCheckToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeWindowSizeToolStripMenuItem;
    }
}