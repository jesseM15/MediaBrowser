using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace MediaBrowser
{
    public static class ListBoxWorker
    {
        public static void BroadFilterSelected(string filter, ListBox lbxNarrow, ListView lvwMedia, List<Video> videos)
        {
            if (filter.Equals("All"))
            {
                lbxNarrow.DataSource = new List<Video>();
                ListViewWorker.UpdateListView(lvwMedia, videos, "All");
            }
            else if (filter.Equals("Year"))
            {
                lbxNarrow.DataSource = DB.GetDistinctYears();
            }
            else if (filter.Equals("Genre"))
            {
                lbxNarrow.DataSource = DB.GetGenres();
            }
            else if (filter.Equals("Director"))
            {
                List<string> directors = DB.GetDirectors();
                if (directors.Count > 0)
                {
                    lbxNarrow.DataSource =
                    new BindingSource(ListHelper.OrderByLastNames(directors), null);
                    lbxNarrow.DisplayMember = "Key";
                    lbxNarrow.ValueMember = "Value";
                }
            }
            else if (filter.Equals("Writer"))
            {
                List<string> writers = DB.GetWriters();
                if (writers.Count > 0)
                {
                    lbxNarrow.DataSource =
                    new BindingSource(ListHelper.OrderByLastNames(writers), null);
                    lbxNarrow.DisplayMember = "Key";
                    lbxNarrow.ValueMember = "Value";
                }
            }
            else if (filter.Equals("Actor"))
            {
                List<string> actors = DB.GetActors();
                if (actors.Count > 0)
                {
                    lbxNarrow.DataSource =
                    new BindingSource(ListHelper.OrderByLastNames(actors), null);
                    lbxNarrow.DisplayMember = "Key";
                    lbxNarrow.ValueMember = "Value";
                }
            }
            else if (filter.Equals("Rating"))
            {
                List<float> ratings = new List<float>();
                for (float n = 1; n < 11; n++)
                {
                    ratings.Add(n);
                }
                lbxNarrow.DataSource = ratings;
            }
        }
    }
}
