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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbxNarrow = new System.Windows.Forms.ListBox();
            this.lbxBroad = new System.Windows.Forms.ListBox();
            this.lvwMedia = new System.Windows.Forms.ListView();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(688, 24);
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
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbxNarrow);
            this.panel1.Controls.Add(this.lbxBroad);
            this.panel1.Location = new System.Drawing.Point(12, 36);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(166, 372);
            this.panel1.TabIndex = 1;
            // 
            // lbxNarrow
            // 
            this.lbxNarrow.FormattingEnabled = true;
            this.lbxNarrow.Location = new System.Drawing.Point(8, 122);
            this.lbxNarrow.Name = "lbxNarrow";
            this.lbxNarrow.Size = new System.Drawing.Size(148, 238);
            this.lbxNarrow.TabIndex = 1;
            this.lbxNarrow.SelectedIndexChanged += new System.EventHandler(this.lbxNarrow_SelectedIndexChanged);
            // 
            // lbxBroad
            // 
            this.lbxBroad.FormattingEnabled = true;
            this.lbxBroad.Location = new System.Drawing.Point(8, 8);
            this.lbxBroad.Name = "lbxBroad";
            this.lbxBroad.Size = new System.Drawing.Size(148, 108);
            this.lbxBroad.TabIndex = 0;
            this.lbxBroad.SelectedIndexChanged += new System.EventHandler(this.lbxBroad_SelectedIndexChanged);
            // 
            // lvwMedia
            // 
            this.lvwMedia.Location = new System.Drawing.Point(184, 36);
            this.lvwMedia.Name = "lvwMedia";
            this.lvwMedia.Size = new System.Drawing.Size(492, 372);
            this.lvwMedia.TabIndex = 2;
            this.lvwMedia.UseCompatibleStateImageBehavior = false;
            // 
            // FormMediaBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 420);
            this.Controls.Add(this.lvwMedia);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMediaBrowser";
            this.Text = "Media Browser";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sourceDirectoriesToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox lbxNarrow;
        private System.Windows.Forms.ListBox lbxBroad;
        private System.Windows.Forms.ListView lvwMedia;
    }
}

