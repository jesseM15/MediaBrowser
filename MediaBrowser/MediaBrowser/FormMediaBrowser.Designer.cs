namespace MediaBrowser
{
    partial class FormMediaBrowser
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sourceDirectoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lbxNarrow = new System.Windows.Forms.ListBox();
            this.lbxBroad = new System.Windows.Forms.ListBox();
            this.lvwMedia = new System.Windows.Forms.ListView();
            this.pnlVideoInfo = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.picPoster = new System.Windows.Forms.PictureBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.lblLength = new System.Windows.Forms.Label();
            this.lblRating = new System.Windows.Forms.Label();
            this.rtxGenre = new System.Windows.Forms.RichTextBox();
            this.rtxPlot = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.pnlVideoInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(862, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sourceDirectoriesToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // sourceDirectoriesToolStripMenuItem
            // 
            this.sourceDirectoriesToolStripMenuItem.Name = "sourceDirectoriesToolStripMenuItem";
            this.sourceDirectoriesToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.sourceDirectoriesToolStripMenuItem.Text = "Source Directories";
            this.sourceDirectoriesToolStripMenuItem.Click += new System.EventHandler(this.sourceDirectoriesToolStripMenuItem_Click);
            // 
            // pnlFilter
            // 
            this.pnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFilter.Controls.Add(this.lbxNarrow);
            this.pnlFilter.Controls.Add(this.lbxBroad);
            this.pnlFilter.Location = new System.Drawing.Point(12, 36);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(5);
            this.pnlFilter.Size = new System.Drawing.Size(140, 372);
            this.pnlFilter.TabIndex = 1;
            // 
            // lbxNarrow
            // 
            this.lbxNarrow.FormattingEnabled = true;
            this.lbxNarrow.Location = new System.Drawing.Point(8, 122);
            this.lbxNarrow.Name = "lbxNarrow";
            this.lbxNarrow.Size = new System.Drawing.Size(122, 238);
            this.lbxNarrow.TabIndex = 1;
            this.lbxNarrow.SelectedIndexChanged += new System.EventHandler(this.lbxNarrow_SelectedIndexChanged);
            // 
            // lbxBroad
            // 
            this.lbxBroad.FormattingEnabled = true;
            this.lbxBroad.Location = new System.Drawing.Point(8, 8);
            this.lbxBroad.Name = "lbxBroad";
            this.lbxBroad.Size = new System.Drawing.Size(122, 108);
            this.lbxBroad.TabIndex = 0;
            this.lbxBroad.SelectedIndexChanged += new System.EventHandler(this.lbxBroad_SelectedIndexChanged);
            // 
            // lvwMedia
            // 
            this.lvwMedia.Location = new System.Drawing.Point(158, 36);
            this.lvwMedia.Name = "lvwMedia";
            this.lvwMedia.Size = new System.Drawing.Size(399, 372);
            this.lvwMedia.TabIndex = 2;
            this.lvwMedia.UseCompatibleStateImageBehavior = false;
            this.lvwMedia.SelectedIndexChanged += new System.EventHandler(this.lvwMedia_SelectedIndexChanged);
            // 
            // pnlVideoInfo
            // 
            this.pnlVideoInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlVideoInfo.Controls.Add(this.rtxPlot);
            this.pnlVideoInfo.Controls.Add(this.rtxGenre);
            this.pnlVideoInfo.Controls.Add(this.lblRating);
            this.pnlVideoInfo.Controls.Add(this.lblLength);
            this.pnlVideoInfo.Controls.Add(this.lblYear);
            this.pnlVideoInfo.Controls.Add(this.picPoster);
            this.pnlVideoInfo.Controls.Add(this.lblTitle);
            this.pnlVideoInfo.Location = new System.Drawing.Point(563, 36);
            this.pnlVideoInfo.Name = "pnlVideoInfo";
            this.pnlVideoInfo.Size = new System.Drawing.Size(285, 372);
            this.pnlVideoInfo.TabIndex = 3;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(5, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(273, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Title";
            // 
            // picPoster
            // 
            this.picPoster.Location = new System.Drawing.Point(118, 70);
            this.picPoster.Name = "picPoster";
            this.picPoster.Size = new System.Drawing.Size(160, 297);
            this.picPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPoster.TabIndex = 1;
            this.picPoster.TabStop = false;
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(6, 70);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(29, 13);
            this.lblYear.TabIndex = 2;
            this.lblYear.Text = "Year";
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(6, 90);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(40, 13);
            this.lblLength.TabIndex = 3;
            this.lblLength.Text = "Length";
            // 
            // lblRating
            // 
            this.lblRating.AutoSize = true;
            this.lblRating.Location = new System.Drawing.Point(6, 110);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(38, 13);
            this.lblRating.TabIndex = 4;
            this.lblRating.Text = "Rating";
            // 
            // rtxGenre
            // 
            this.rtxGenre.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxGenre.Location = new System.Drawing.Point(6, 130);
            this.rtxGenre.Name = "rtxGenre";
            this.rtxGenre.ReadOnly = true;
            this.rtxGenre.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtxGenre.Size = new System.Drawing.Size(106, 42);
            this.rtxGenre.TabIndex = 6;
            this.rtxGenre.Text = "";
            // 
            // rtxPlot
            // 
            this.rtxPlot.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxPlot.Location = new System.Drawing.Point(6, 189);
            this.rtxPlot.Name = "rtxPlot";
            this.rtxPlot.ReadOnly = true;
            this.rtxPlot.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtxPlot.Size = new System.Drawing.Size(106, 178);
            this.rtxPlot.TabIndex = 7;
            this.rtxPlot.Text = "";
            // 
            // FormMediaBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 420);
            this.Controls.Add(this.pnlVideoInfo);
            this.Controls.Add(this.lvwMedia);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMediaBrowser";
            this.Text = "Media Browser";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlVideoInfo.ResumeLayout(false);
            this.pnlVideoInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sourceDirectoriesToolStripMenuItem;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.ListBox lbxNarrow;
        private System.Windows.Forms.ListBox lbxBroad;
        private System.Windows.Forms.ListView lvwMedia;
        private System.Windows.Forms.Panel pnlVideoInfo;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.PictureBox picPoster;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.RichTextBox rtxPlot;
        private System.Windows.Forms.RichTextBox rtxGenre;
    }
}

