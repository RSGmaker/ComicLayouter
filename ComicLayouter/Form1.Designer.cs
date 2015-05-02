namespace ComicLayouter
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveComicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsGIFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shiftUpPgUPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shiftDownPgDOWNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setDelayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extrasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.widthMismatchStretchingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.captureModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cropSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bordersSeperatorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previewGifToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stretchingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCreateswfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createAnimatedComicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customPanelLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "png";
            this.saveFileDialog1.Filter = "comic png|*.png";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button1.Location = new System.Drawing.Point(166, 362);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(41, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Save";
            this.toolTip1.SetToolTip(this.button1, "Save the completed comic.");
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button2.Location = new System.Drawing.Point(166, 27);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(41, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Open";
            this.toolTip1.SetToolTip(this.button2, "Load images,(you can also drag files straight into this program)");
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(160, 372);
            this.panel1.TabIndex = 3;
            this.toolTip1.SetToolTip(this.panel1, "Double clicking on an image opens it up in paint.net or gimp if the image editor " +
        "is already opened");
            this.panel1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.panel1_PreviewKeyDown);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button3.Location = new System.Drawing.Point(166, 56);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(41, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Clear";
            this.toolTip1.SetToolTip(this.button3, "Remove ALL images");
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button4.Location = new System.Drawing.Point(166, 133);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(41, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "▲";
            this.toolTip1.SetToolTip(this.button4, "Move selected image up(Page Up)");
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button5.Location = new System.Drawing.Point(166, 162);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(41, 23);
            this.button5.TabIndex = 6;
            this.button5.Text = "▼";
            this.toolTip1.SetToolTip(this.button5, "Move selected image down(Page Down)");
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button6.ForeColor = System.Drawing.Color.Red;
            this.button6.Location = new System.Drawing.Point(166, 191);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(41, 23);
            this.button6.TabIndex = 7;
            this.button6.Text = "X";
            this.toolTip1.SetToolTip(this.button6, "Delete the selected image(Delete)");
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button7.Location = new System.Drawing.Point(166, 85);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(41, 23);
            this.button7.TabIndex = 8;
            this.button7.Text = "?";
            this.toolTip1.SetToolTip(this.button7, "About");
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button8.Location = new System.Drawing.Point(166, 252);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(41, 23);
            this.button8.TabIndex = 10;
            this.button8.Text = "Anim";
            this.toolTip1.SetToolTip(this.button8, "Preview & adjust animation timing");
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button9.Location = new System.Drawing.Point(166, 281);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(41, 23);
            this.button9.TabIndex = 11;
            this.button9.Text = "Swf";
            this.toolTip1.SetToolTip(this.button9, "Opens Create.swf in a special window with many extra features");
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 150;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.frameToolStripMenuItem,
            this.extrasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(215, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadImagesToolStripMenuItem,
            this.clearImagesToolStripMenuItem,
            this.saveComicToolStripMenuItem,
            this.saveAsGIFToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // loadImagesToolStripMenuItem
            // 
            this.loadImagesToolStripMenuItem.Name = "loadImagesToolStripMenuItem";
            this.loadImagesToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.loadImagesToolStripMenuItem.Text = "&Load Images";
            this.loadImagesToolStripMenuItem.ToolTipText = "Load images,(you can also drag files straight into this program)";
            this.loadImagesToolStripMenuItem.Click += new System.EventHandler(this.loadImagesToolStripMenuItem_Click);
            // 
            // clearImagesToolStripMenuItem
            // 
            this.clearImagesToolStripMenuItem.Name = "clearImagesToolStripMenuItem";
            this.clearImagesToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.clearImagesToolStripMenuItem.Text = "&Clear Images";
            this.clearImagesToolStripMenuItem.ToolTipText = "Remove ALL images";
            this.clearImagesToolStripMenuItem.Click += new System.EventHandler(this.clearImagesToolStripMenuItem_Click);
            // 
            // saveComicToolStripMenuItem
            // 
            this.saveComicToolStripMenuItem.Name = "saveComicToolStripMenuItem";
            this.saveComicToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.saveComicToolStripMenuItem.Text = "Save &Comic";
            this.saveComicToolStripMenuItem.ToolTipText = "Save the completed comic";
            this.saveComicToolStripMenuItem.Click += new System.EventHandler(this.saveComicToolStripMenuItem_Click);
            // 
            // saveAsGIFToolStripMenuItem
            // 
            this.saveAsGIFToolStripMenuItem.Name = "saveAsGIFToolStripMenuItem";
            this.saveAsGIFToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.saveAsGIFToolStripMenuItem.Text = "Save as &Animation";
            this.saveAsGIFToolStripMenuItem.ToolTipText = "Save the images as an animated gif";
            this.saveAsGIFToolStripMenuItem.Click += new System.EventHandler(this.saveAsGIFToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.ToolTipText = "Close the program";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // frameToolStripMenuItem
            // 
            this.frameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shiftUpPgUPToolStripMenuItem,
            this.shiftDownPgDOWNToolStripMenuItem,
            this.deleteDeleteToolStripMenuItem,
            this.setDelayToolStripMenuItem,
            this.editToolStripMenuItem,
            this.pasteImageToolStripMenuItem,
            this.duplicateToolStripMenuItem});
            this.frameToolStripMenuItem.Name = "frameToolStripMenuItem";
            this.frameToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.frameToolStripMenuItem.Text = "&Frame";
            this.frameToolStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frameToolStripMenuItem_MouseDown);
            // 
            // shiftUpPgUPToolStripMenuItem
            // 
            this.shiftUpPgUPToolStripMenuItem.Name = "shiftUpPgUPToolStripMenuItem";
            this.shiftUpPgUPToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.shiftUpPgUPToolStripMenuItem.Text = "Shift &Up(Pg UP)";
            this.shiftUpPgUPToolStripMenuItem.ToolTipText = "Move the selected panel up";
            this.shiftUpPgUPToolStripMenuItem.Click += new System.EventHandler(this.shiftUpPgUPToolStripMenuItem_Click);
            // 
            // shiftDownPgDOWNToolStripMenuItem
            // 
            this.shiftDownPgDOWNToolStripMenuItem.Name = "shiftDownPgDOWNToolStripMenuItem";
            this.shiftDownPgDOWNToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.shiftDownPgDOWNToolStripMenuItem.Text = "Shift &Down(Pg DOWN)";
            this.shiftDownPgDOWNToolStripMenuItem.ToolTipText = "Move the selected panel down";
            this.shiftDownPgDOWNToolStripMenuItem.Click += new System.EventHandler(this.shiftDownPgDOWNToolStripMenuItem_Click);
            // 
            // deleteDeleteToolStripMenuItem
            // 
            this.deleteDeleteToolStripMenuItem.Name = "deleteDeleteToolStripMenuItem";
            this.deleteDeleteToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.deleteDeleteToolStripMenuItem.Text = "&Delete(Delete)";
            this.deleteDeleteToolStripMenuItem.ToolTipText = "delete the panel";
            this.deleteDeleteToolStripMenuItem.Click += new System.EventHandler(this.deleteDeleteToolStripMenuItem_Click);
            // 
            // setDelayToolStripMenuItem
            // 
            this.setDelayToolStripMenuItem.Name = "setDelayToolStripMenuItem";
            this.setDelayToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.setDelayToolStripMenuItem.Text = "S&et delay(F8)";
            this.setDelayToolStripMenuItem.ToolTipText = "Set how long this frame stays visible in an animated gif";
            this.setDelayToolStripMenuItem.Click += new System.EventHandler(this.setDelayToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.editToolStripMenuItem.Text = "Ed&it";
            this.editToolStripMenuItem.ToolTipText = "Opens the panel in gimp or paint.net if they are already open";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // pasteImageToolStripMenuItem
            // 
            this.pasteImageToolStripMenuItem.Name = "pasteImageToolStripMenuItem";
            this.pasteImageToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.pasteImageToolStripMenuItem.Text = "&Paste image";
            this.pasteImageToolStripMenuItem.ToolTipText = "Imports copied image from the clipboard";
            this.pasteImageToolStripMenuItem.Click += new System.EventHandler(this.pasteImageToolStripMenuItem_Click);
            // 
            // extrasToolStripMenuItem
            // 
            this.extrasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.widthMismatchStretchingToolStripMenuItem,
            this.captureModeToolStripMenuItem,
            this.cropSettingsToolStripMenuItem,
            this.bordersSeperatorsToolStripMenuItem,
            this.previewGifToolStripMenuItem,
            this.stretchingToolStripMenuItem,
            this.openCreateswfToolStripMenuItem,
            this.createAnimatedComicToolStripMenuItem,
            this.customPanelLayoutToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.extrasToolStripMenuItem.Name = "extrasToolStripMenuItem";
            this.extrasToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.extrasToolStripMenuItem.Text = "&Extras";
            // 
            // widthMismatchStretchingToolStripMenuItem
            // 
            this.widthMismatchStretchingToolStripMenuItem.Checked = true;
            this.widthMismatchStretchingToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.widthMismatchStretchingToolStripMenuItem.Name = "widthMismatchStretchingToolStripMenuItem";
            this.widthMismatchStretchingToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.widthMismatchStretchingToolStripMenuItem.Text = "Scale i&mage to fit comic";
            this.widthMismatchStretchingToolStripMenuItem.ToolTipText = "If checked will scale any panels that are not wide enough to prevent blank space";
            this.widthMismatchStretchingToolStripMenuItem.Click += new System.EventHandler(this.widthMismatchStretchingToolStripMenuItem_Click);
            // 
            // captureModeToolStripMenuItem
            // 
            this.captureModeToolStripMenuItem.Name = "captureModeToolStripMenuItem";
            this.captureModeToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.captureModeToolStripMenuItem.Text = "&Auto Clipboard(F10)";
            this.captureModeToolStripMenuItem.ToolTipText = "Toggles clipboard monitoring";
            this.captureModeToolStripMenuItem.Click += new System.EventHandler(this.captureModeToolStripMenuItem_Click);
            // 
            // cropSettingsToolStripMenuItem
            // 
            this.cropSettingsToolStripMenuItem.Name = "cropSettingsToolStripMenuItem";
            this.cropSettingsToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.cropSettingsToolStripMenuItem.Text = "C&rop Settings(F9)";
            this.cropSettingsToolStripMenuItem.ToolTipText = "Sets panel cropping settings";
            this.cropSettingsToolStripMenuItem.Click += new System.EventHandler(this.cropSettingsToolStripMenuItem_Click);
            // 
            // bordersSeperatorsToolStripMenuItem
            // 
            this.bordersSeperatorsToolStripMenuItem.Name = "bordersSeperatorsToolStripMenuItem";
            this.bordersSeperatorsToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.bordersSeperatorsToolStripMenuItem.Text = "&Borders && Seperators";
            this.bordersSeperatorsToolStripMenuItem.ToolTipText = "sets the borders and separators that go around panels";
            this.bordersSeperatorsToolStripMenuItem.Click += new System.EventHandler(this.bordersSeperatorsToolStripMenuItem_Click);
            // 
            // previewGifToolStripMenuItem
            // 
            this.previewGifToolStripMenuItem.Name = "previewGifToolStripMenuItem";
            this.previewGifToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.previewGifToolStripMenuItem.Text = "&Preview Gif";
            this.previewGifToolStripMenuItem.ToolTipText = "Opens a window that lets you see how the frames look like as an animated gif and " +
    "lets you edit timing";
            this.previewGifToolStripMenuItem.Click += new System.EventHandler(this.previewGifToolStripMenuItem_Click);
            // 
            // stretchingToolStripMenuItem
            // 
            this.stretchingToolStripMenuItem.Name = "stretchingToolStripMenuItem";
            this.stretchingToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.stretchingToolStripMenuItem.Text = "&Stretching";
            this.stretchingToolStripMenuItem.ToolTipText = "Lets you set a static size for the output image";
            this.stretchingToolStripMenuItem.Click += new System.EventHandler(this.stretchingToolStripMenuItem_Click);
            // 
            // openCreateswfToolStripMenuItem
            // 
            this.openCreateswfToolStripMenuItem.Name = "openCreateswfToolStripMenuItem";
            this.openCreateswfToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.openCreateswfToolStripMenuItem.Text = "&Open Create.swf";
            this.openCreateswfToolStripMenuItem.ToolTipText = "Opens Create.swf in a special window with many extra features";
            this.openCreateswfToolStripMenuItem.Click += new System.EventHandler(this.openCreateswfToolStripMenuItem_Click);
            // 
            // createAnimatedComicToolStripMenuItem
            // 
            this.createAnimatedComicToolStripMenuItem.Name = "createAnimatedComicToolStripMenuItem";
            this.createAnimatedComicToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.createAnimatedComicToolStripMenuItem.Text = "Create &Animated comic";
            this.createAnimatedComicToolStripMenuItem.ToolTipText = "Create a series of comics to put into an animated gif";
            this.createAnimatedComicToolStripMenuItem.Click += new System.EventHandler(this.createAnimatedComicToolStripMenuItem_Click);
            // 
            // customPanelLayoutToolStripMenuItem
            // 
            this.customPanelLayoutToolStripMenuItem.Name = "customPanelLayoutToolStripMenuItem";
            this.customPanelLayoutToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.customPanelLayoutToolStripMenuItem.Text = "C&ustom panel layout";
            this.customPanelLayoutToolStripMenuItem.ToolTipText = "Compose a comic with any number of columns";
            this.customPanelLayoutToolStripMenuItem.Click += new System.EventHandler(this.customPanelLayoutToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.aboutToolStripMenuItem.Text = "&About(F1)";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Select the root animation folder";
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // duplicateToolStripMenuItem
            // 
            this.duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
            this.duplicateToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.duplicateToolStripMenuItem.Text = "D&uplicate";
            this.duplicateToolStripMenuItem.Click += new System.EventHandler(this.duplicateToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(215, 397);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(8000, 4000);
            this.MinimumSize = new System.Drawing.Size(231, 435);
            this.Name = "Form1";
            this.Text = "ComicLayouter by RSGmaker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd_1);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadImagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearImagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveComicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsGIFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shiftUpPgUPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shiftDownPgDOWNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteDeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setDelayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extrasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem captureModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cropSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bordersSeperatorsToolStripMenuItem;
        public System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem previewGifToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCreateswfToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stretchingToolStripMenuItem;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.ToolStripMenuItem widthMismatchStretchingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createAnimatedComicToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem customPanelLayoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicateToolStripMenuItem;
    }
}

