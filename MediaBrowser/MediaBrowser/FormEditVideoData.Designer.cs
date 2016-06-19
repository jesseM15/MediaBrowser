namespace MediaBrowser
{
    partial class FormEditVideoData
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
            this.lblFilePath = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.txtGenres = new System.Windows.Forms.TextBox();
            this.lblGenres = new System.Windows.Forms.Label();
            this.txtDirectors = new System.Windows.Forms.TextBox();
            this.lblDirectors = new System.Windows.Forms.Label();
            this.txtWriters = new System.Windows.Forms.TextBox();
            this.lblWriters = new System.Windows.Forms.Label();
            this.txtActors = new System.Windows.Forms.TextBox();
            this.lblActors = new System.Windows.Forms.Label();
            this.txtPlot = new System.Windows.Forms.TextBox();
            this.lblPlot = new System.Windows.Forms.Label();
            this.txtRating = new System.Windows.Forms.TextBox();
            this.lblRating = new System.Windows.Forms.Label();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.lblLength = new System.Windows.Forms.Label();
            this.lblPoster = new System.Windows.Forms.Label();
            this.picPoster = new System.Windows.Forms.PictureBox();
            this.btnChangePoster = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(12, 9);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(45, 13);
            this.lblFilePath.TabIndex = 0;
            this.lblFilePath.Text = "FilePath";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(12, 35);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(30, 13);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Title:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(67, 32);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(158, 20);
            this.txtTitle.TabIndex = 2;
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(67, 58);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(158, 20);
            this.txtYear.TabIndex = 4;
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(12, 61);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(32, 13);
            this.lblYear.TabIndex = 3;
            this.lblYear.Text = "Year:";
            // 
            // txtGenres
            // 
            this.txtGenres.Location = new System.Drawing.Point(67, 84);
            this.txtGenres.Name = "txtGenres";
            this.txtGenres.Size = new System.Drawing.Size(158, 20);
            this.txtGenres.TabIndex = 6;
            // 
            // lblGenres
            // 
            this.lblGenres.AutoSize = true;
            this.lblGenres.Location = new System.Drawing.Point(12, 87);
            this.lblGenres.Name = "lblGenres";
            this.lblGenres.Size = new System.Drawing.Size(44, 13);
            this.lblGenres.TabIndex = 5;
            this.lblGenres.Text = "Genres:";
            // 
            // txtDirectors
            // 
            this.txtDirectors.Location = new System.Drawing.Point(67, 110);
            this.txtDirectors.Name = "txtDirectors";
            this.txtDirectors.Size = new System.Drawing.Size(158, 20);
            this.txtDirectors.TabIndex = 8;
            // 
            // lblDirectors
            // 
            this.lblDirectors.AutoSize = true;
            this.lblDirectors.Location = new System.Drawing.Point(12, 113);
            this.lblDirectors.Name = "lblDirectors";
            this.lblDirectors.Size = new System.Drawing.Size(52, 13);
            this.lblDirectors.TabIndex = 7;
            this.lblDirectors.Text = "Directors:";
            // 
            // txtWriters
            // 
            this.txtWriters.Location = new System.Drawing.Point(67, 136);
            this.txtWriters.Name = "txtWriters";
            this.txtWriters.Size = new System.Drawing.Size(158, 20);
            this.txtWriters.TabIndex = 10;
            // 
            // lblWriters
            // 
            this.lblWriters.AutoSize = true;
            this.lblWriters.Location = new System.Drawing.Point(12, 139);
            this.lblWriters.Name = "lblWriters";
            this.lblWriters.Size = new System.Drawing.Size(43, 13);
            this.lblWriters.TabIndex = 9;
            this.lblWriters.Text = "Writers:";
            // 
            // txtActors
            // 
            this.txtActors.Location = new System.Drawing.Point(67, 162);
            this.txtActors.Name = "txtActors";
            this.txtActors.Size = new System.Drawing.Size(158, 20);
            this.txtActors.TabIndex = 12;
            // 
            // lblActors
            // 
            this.lblActors.AutoSize = true;
            this.lblActors.Location = new System.Drawing.Point(12, 165);
            this.lblActors.Name = "lblActors";
            this.lblActors.Size = new System.Drawing.Size(40, 13);
            this.lblActors.TabIndex = 11;
            this.lblActors.Text = "Actors:";
            // 
            // txtPlot
            // 
            this.txtPlot.Location = new System.Drawing.Point(67, 188);
            this.txtPlot.Multiline = true;
            this.txtPlot.Name = "txtPlot";
            this.txtPlot.Size = new System.Drawing.Size(158, 56);
            this.txtPlot.TabIndex = 14;
            // 
            // lblPlot
            // 
            this.lblPlot.AutoSize = true;
            this.lblPlot.Location = new System.Drawing.Point(12, 191);
            this.lblPlot.Name = "lblPlot";
            this.lblPlot.Size = new System.Drawing.Size(28, 13);
            this.lblPlot.TabIndex = 13;
            this.lblPlot.Text = "Plot:";
            // 
            // txtRating
            // 
            this.txtRating.Location = new System.Drawing.Point(67, 250);
            this.txtRating.Name = "txtRating";
            this.txtRating.Size = new System.Drawing.Size(158, 20);
            this.txtRating.TabIndex = 16;
            // 
            // lblRating
            // 
            this.lblRating.AutoSize = true;
            this.lblRating.Location = new System.Drawing.Point(12, 253);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(41, 13);
            this.lblRating.TabIndex = 15;
            this.lblRating.Text = "Rating:";
            // 
            // txtLength
            // 
            this.txtLength.Location = new System.Drawing.Point(67, 276);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(158, 20);
            this.txtLength.TabIndex = 18;
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(12, 279);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(43, 13);
            this.lblLength.TabIndex = 17;
            this.lblLength.Text = "Length:";
            // 
            // lblPoster
            // 
            this.lblPoster.AutoSize = true;
            this.lblPoster.Location = new System.Drawing.Point(246, 35);
            this.lblPoster.Name = "lblPoster";
            this.lblPoster.Size = new System.Drawing.Size(40, 13);
            this.lblPoster.TabIndex = 19;
            this.lblPoster.Text = "Poster:";
            // 
            // picPoster
            // 
            this.picPoster.Location = new System.Drawing.Point(292, 32);
            this.picPoster.Name = "picPoster";
            this.picPoster.Size = new System.Drawing.Size(160, 277);
            this.picPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPoster.TabIndex = 20;
            this.picPoster.TabStop = false;
            // 
            // btnChangePoster
            // 
            this.btnChangePoster.Location = new System.Drawing.Point(349, 315);
            this.btnChangePoster.Name = "btnChangePoster";
            this.btnChangePoster.Size = new System.Drawing.Size(103, 23);
            this.btnChangePoster.TabIndex = 21;
            this.btnChangePoster.Text = "Change Poster";
            this.btnChangePoster.UseVisualStyleBackColor = true;
            this.btnChangePoster.Click += new System.EventHandler(this.btnChangePoster_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(296, 344);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(377, 344);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 23;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FormEditVideoData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 377);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnChangePoster);
            this.Controls.Add(this.picPoster);
            this.Controls.Add(this.lblPoster);
            this.Controls.Add(this.txtLength);
            this.Controls.Add(this.lblLength);
            this.Controls.Add(this.txtRating);
            this.Controls.Add(this.lblRating);
            this.Controls.Add(this.txtPlot);
            this.Controls.Add(this.lblPlot);
            this.Controls.Add(this.txtActors);
            this.Controls.Add(this.lblActors);
            this.Controls.Add(this.txtWriters);
            this.Controls.Add(this.lblWriters);
            this.Controls.Add(this.txtDirectors);
            this.Controls.Add(this.lblDirectors);
            this.Controls.Add(this.txtGenres);
            this.Controls.Add(this.lblGenres);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblFilePath);
            this.Name = "FormEditVideoData";
            this.Text = "Edit Video Data";
            this.Load += new System.EventHandler(this.FormEditVideoData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.TextBox txtGenres;
        private System.Windows.Forms.Label lblGenres;
        private System.Windows.Forms.TextBox txtDirectors;
        private System.Windows.Forms.Label lblDirectors;
        private System.Windows.Forms.TextBox txtWriters;
        private System.Windows.Forms.Label lblWriters;
        private System.Windows.Forms.TextBox txtActors;
        private System.Windows.Forms.Label lblActors;
        private System.Windows.Forms.TextBox txtPlot;
        private System.Windows.Forms.Label lblPlot;
        private System.Windows.Forms.TextBox txtRating;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.Label lblPoster;
        private System.Windows.Forms.PictureBox picPoster;
        private System.Windows.Forms.Button btnChangePoster;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}