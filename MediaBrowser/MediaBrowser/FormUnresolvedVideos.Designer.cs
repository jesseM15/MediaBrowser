namespace MediaBrowser
{
    partial class FormUnresolvedVideos
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
            this.pnlUnresolvedFiles = new System.Windows.Forms.Panel();
            this.lbxUnresolvedFiles = new System.Windows.Forms.ListBox();
            this.pnlQueryTerms = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.picPoster = new System.Windows.Forms.PictureBox();
            this.pnlUnresolvedFiles.SuspendLayout();
            this.pnlQueryTerms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlUnresolvedFiles
            // 
            this.pnlUnresolvedFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUnresolvedFiles.Controls.Add(this.lbxUnresolvedFiles);
            this.pnlUnresolvedFiles.Location = new System.Drawing.Point(12, 12);
            this.pnlUnresolvedFiles.Name = "pnlUnresolvedFiles";
            this.pnlUnresolvedFiles.Padding = new System.Windows.Forms.Padding(5);
            this.pnlUnresolvedFiles.Size = new System.Drawing.Size(201, 293);
            this.pnlUnresolvedFiles.TabIndex = 0;
            // 
            // lbxUnresolvedFiles
            // 
            this.lbxUnresolvedFiles.FormattingEnabled = true;
            this.lbxUnresolvedFiles.HorizontalScrollbar = true;
            this.lbxUnresolvedFiles.Location = new System.Drawing.Point(8, 8);
            this.lbxUnresolvedFiles.Name = "lbxUnresolvedFiles";
            this.lbxUnresolvedFiles.Size = new System.Drawing.Size(185, 277);
            this.lbxUnresolvedFiles.TabIndex = 0;
            // 
            // pnlQueryTerms
            // 
            this.pnlQueryTerms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlQueryTerms.Controls.Add(this.btnSearch);
            this.pnlQueryTerms.Controls.Add(this.txtYear);
            this.pnlQueryTerms.Controls.Add(this.lblYear);
            this.pnlQueryTerms.Controls.Add(this.txtTitle);
            this.pnlQueryTerms.Controls.Add(this.lblTitle);
            this.pnlQueryTerms.Location = new System.Drawing.Point(219, 12);
            this.pnlQueryTerms.Name = "pnlQueryTerms";
            this.pnlQueryTerms.Padding = new System.Windows.Forms.Padding(5);
            this.pnlQueryTerms.Size = new System.Drawing.Size(165, 94);
            this.pnlQueryTerms.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(57, 61);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search OMDB";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(44, 35);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(113, 20);
            this.txtYear.TabIndex = 3;
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(8, 38);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(32, 13);
            this.lblYear.TabIndex = 2;
            this.lblYear.Text = "Year:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(44, 9);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(113, 20);
            this.txtTitle.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(8, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(30, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Title:";
            // 
            // picPoster
            // 
            this.picPoster.Location = new System.Drawing.Point(424, 12);
            this.picPoster.Name = "picPoster";
            this.picPoster.Size = new System.Drawing.Size(166, 277);
            this.picPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPoster.TabIndex = 2;
            this.picPoster.TabStop = false;
            // 
            // FormUnresolvedVideos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 316);
            this.Controls.Add(this.picPoster);
            this.Controls.Add(this.pnlQueryTerms);
            this.Controls.Add(this.pnlUnresolvedFiles);
            this.Name = "FormUnresolvedVideos";
            this.Text = "Unresolved Videos";
            this.Load += new System.EventHandler(this.FormUnresolvedVideos_Load);
            this.pnlUnresolvedFiles.ResumeLayout(false);
            this.pnlQueryTerms.ResumeLayout(false);
            this.pnlQueryTerms.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlUnresolvedFiles;
        private System.Windows.Forms.ListBox lbxUnresolvedFiles;
        private System.Windows.Forms.Panel pnlQueryTerms;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox picPoster;
    }
}