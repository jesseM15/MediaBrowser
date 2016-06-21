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
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlUnresolvedFiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlUnresolvedFiles
            // 
            this.pnlUnresolvedFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUnresolvedFiles.Controls.Add(this.lbxUnresolvedFiles);
            this.pnlUnresolvedFiles.Location = new System.Drawing.Point(12, 12);
            this.pnlUnresolvedFiles.Name = "pnlUnresolvedFiles";
            this.pnlUnresolvedFiles.Padding = new System.Windows.Forms.Padding(5);
            this.pnlUnresolvedFiles.Size = new System.Drawing.Size(393, 180);
            this.pnlUnresolvedFiles.TabIndex = 0;
            // 
            // lbxUnresolvedFiles
            // 
            this.lbxUnresolvedFiles.FormattingEnabled = true;
            this.lbxUnresolvedFiles.HorizontalScrollbar = true;
            this.lbxUnresolvedFiles.Location = new System.Drawing.Point(8, 8);
            this.lbxUnresolvedFiles.Name = "lbxUnresolvedFiles";
            this.lbxUnresolvedFiles.Size = new System.Drawing.Size(375, 160);
            this.lbxUnresolvedFiles.TabIndex = 0;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(411, 12);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(411, 169);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FormUnresolvedVideos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 207);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.pnlUnresolvedFiles);
            this.Name = "FormUnresolvedVideos";
            this.Text = "Unresolved Videos";
            this.Load += new System.EventHandler(this.FormUnresolvedVideos_Load);
            this.pnlUnresolvedFiles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlUnresolvedFiles;
        private System.Windows.Forms.ListBox lbxUnresolvedFiles;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnClose;
    }
}