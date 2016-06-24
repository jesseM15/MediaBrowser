using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;         // used to simulate heavy computing being done

namespace BackgroundWorkerTutorial
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            bgwTest.RunWorkerAsync(50);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bgwTest.CancelAsync();
        }

        private void bgwTest_DoWork(object sender, DoWorkEventArgs e)
        {
            int time = (int)e.Argument;
            string result = "";
            for (int i = 0; i < 100; i++)
            {
                if (bgwTest.CancellationPending)
	            {
	                e.Cancel = true;
	                break;
	            }
                Thread.Sleep(time);
                result += "+";
                bgwTest.ReportProgress(i);
            }
            e.Result = result;
        }

        private void bgwTest_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            prgTest.Value = e.ProgressPercentage;
        }

        private void bgwTest_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Background Worker Cancelled.");
            }
            else
            {
                MessageBox.Show(e.Result + " returned.");
            }
        }

        
    }
}
