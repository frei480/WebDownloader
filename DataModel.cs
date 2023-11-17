using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebDownloader
{
    internal class DataModel
    {
        public string name { get; set; } = "";
        public string path { get; set; } = "";
       public string folder { get; set; } = "";
        public DataModel(string _name, string _path, string _folder)
        { 
            name= _name;
            path= _path;
            folder= _folder;
        }
        public void DwnldFile()
        {
            WebClient myWebClient = new WebClient();
            myWebClient.DownloadFile(this.path, this.folder);
        }
    }
}
