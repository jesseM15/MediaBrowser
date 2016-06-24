namespace BackgroundWorkerTutorial
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
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.prgTest = new System.Windows.Forms.ProgressBar();
            this.bgwTest = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(45, 19);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(195, 13);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "Push Start to begin background worker.";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(101, 59);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(101, 217);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // prgTest
            // 
            this.prgTest.Location = new System.Drawing.Point(48, 109);
            this.prgTest.Name = "prgTest";
            this.prgTest.Size = new System.Drawing.Size(187, 23);
            this.prgTest.TabIndex = 3;
            // 
            // bgwTest
            // 
            this.bgwTest.WorkerReportsProgress = true;
            this.bgwTest.WorkerSupportsCancellation = true;
            this.bgwTest.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwTest_DoWork);
            this.bgwTest.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwTest_ProgressChanged);
            this.bgwTest.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwTest_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.prgTest);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblInfo);
            this.Name = "Form1";
            this.Text = "Background Worker Tutorial";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar prgTest;
        private System.ComponentModel.BackgroundWorker bgwTest;
    }
}

