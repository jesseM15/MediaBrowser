using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaBrowser
{
    public partial class FormEditVideoData : Form
    {

        public Video currentVideo;

        public FormEditVideoData(Video video)
        {
            InitializeComponent();
            currentVideo = video;
        }

        private void FormEditVideoData_Load(object sender, EventArgs e)
        {
            txtYear.MaxLength = 4;
            PopulateForm();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FormSearch s = new FormSearch(currentVideo);
            var result = s.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                currentVideo = s.searchVideo;
                PopulateForm();
            }
        }

        private void btnChangePoster_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            if (ofd.FileName != null)
            {
                picPoster.Image = new Bitmap(ofd.FileName);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            currentVideo.Title = txtTitle.Text;
            currentVideo.Year = txtYear.Text;
            List<string> genres = new List<string>(txtGenres.Text.Split(',').ToList());
            for (int g = 0; g < genres.Count; g++)
            {
                genres[g] = genres[g].Trim();
            }
            currentVideo.Genre = genres;
            List<string> directors = new List<string>(txtDirectors.Text.Split(',').ToList());
            for (int d = 0; d < directors.Count; d++)
            {
                directors[d] = directors[d].Trim();
            }
            currentVideo.Director = directors;
            List<string> writers = new List<string>(txtWriters.Text.Split(',').ToList());
            for (int w = 0; w < writers.Count; w++)
            {
                writers[w] = writers[w].Trim();
            }
            currentVideo.Writer = writers;
            List<string> actors = new List<string>(txtActors.Text.Split(',').ToList());
            for (int a = 0; a < actors.Count; a++)
            {
                actors[a] = actors[a].Trim();
            }
            currentVideo.Actor = actors;
            currentVideo.Plot = txtPlot.Text;
            currentVideo.Length = txtLength.Text;
            currentVideo.Rating = txtRating.Text;
            currentVideo.MediaImage = new Bitmap(picPoster.Image);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void PopulateForm()
        {
            lblFilePath.Text = "Editing data for: " + currentVideo.FilePath;
            txtTitle.Text = currentVideo.Title;
            txtYear.Text = currentVideo.Year;
            txtGenres.Text = BuildStringFromList(currentVideo.Genre);
            txtDirectors.Text = BuildStringFromList(currentVideo.Director);
            txtWriters.Text = BuildStringFromList(currentVideo.Writer);
            txtActors.Text = BuildStringFromList(currentVideo.Actor);
            txtPlot.Text = currentVideo.Plot;
            txtRating.Text = currentVideo.Rating;
            txtLength.Text = currentVideo.Length;
            picPoster.Image = currentVideo.MediaImage;
        }

        private string BuildStringFromList(List<string> input)
        {
            string output = "";
            for (int n = 0; n < input.Count; n++)
            {
                output += input[n];
                if (n + 1 != input.Count)
                {
                    output += ",";
                }
            }
            return output;
        }

    }
}
