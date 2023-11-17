using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WebDownloader
{
    internal class DataModel
    {
        public string name { get; set; } = "";
        public string path { get; set; } = "";
       public string folder { get; set; } = "";

        public ProgressBar progressBar { get; set; }
        public CheckBox checkBox { get; }
        public DataModel(string _name, string _path, string _folder, ProgressBar _progressBar, CheckBox _checkBox)
        { 
            name= _name;
            path= _path;
            folder= _folder;
            progressBar = _progressBar;
            checkBox = _checkBox;
        }
        public void DwnldFile()
        {
            WebClient myWebClient = new WebClient();
            myWebClient.DownloadFile(this.path, this.folder);
        }
    }
}
