using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaBrowser
{
    class Browser
    {
        private string _state;

        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        public Browser()
        {
            _state = "";
        }
    }
}
