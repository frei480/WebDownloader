using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WebDownloader
{
    internal class DataModel
    {
        public string name { get; set; } = "";
        public string filename { get; set; } = "";
       public string folder { get; set; } = "";

        public ProgressBar progressBar { get; set; }
       // public CheckBox checkBox { get; }
       public bool checkBox { get; }
        public DataModel(string _name, string _filename, string _folder, ProgressBar _progressBar, bool _checkBox)//CheckBox _checkBox)
        { 
            name= _name;
            filename= _filename;
            folder= _folder;
            progressBar = _progressBar;
            checkBox = _checkBox;
        }
        public void DwnldFile()
        {
                if (checkBox)
                {
                    using (WebClient myWebClient = new WebClient())
                    {
                    myWebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                    myWebClient.DownloadFileAsync(new Uri(Paths.url + this.filename), this.folder + "\\" + this.filename.Replace("%20", " "));
                    }
                }
            }
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {

            progressBar.Dispatcher.BeginInvoke(
            System.Windows.Threading.DispatcherPriority.Normal,
            new DispatcherOperationCallback(delegate
                  {
                      progressBar.Value = e.ProgressPercentage;
                      return null;
                  }), null);
            
        }

    }
}
