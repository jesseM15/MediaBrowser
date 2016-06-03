using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaBrowser
{
    public abstract class Media
    {
        private string _fileName;
        private string _filePath;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        public Media()
        {
            _fileName = "";
            _filePath = "";
        }
    }
}
