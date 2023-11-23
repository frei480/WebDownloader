using System;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;

namespace WebDownloader
{
    internal class DataModel
    {
        public string Name { get; set; } = "";
        public string Filename { get; set; } = "";
        public string Folder { get; set; } = "";
        public ProgressBar progressBar { get; set; }
        public CheckBox checkBox { get; set; }
        public Label label { get; set; }

        public bool checkBoxCheked { get; }
        public DataModel(string _name, string _filename, string _folder, ProgressBar _progressBar, CheckBox _checkBox, Label _label)
        {
            Name = _name;
            Filename = _filename;
            Folder = _folder;
            progressBar = _progressBar;
            checkBox = _checkBox;
            checkBoxCheked = _checkBox.Dispatcher.Invoke(() => _checkBox.IsChecked ?? false);
            label = _label;
        }
        public void DwnldFile()
        {
            
            if(!Directory.Exists(this.Folder)) Directory.CreateDirectory(this.Folder);

            label.Dispatcher.BeginInvoke(()=> label.Content="Скачиваю");
            using (WebClient myWebClient = new WebClient())
            {
                myWebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                myWebClient.DownloadFileAsync(new Uri(Paths.url + this.Filename), this.Folder + "\\" + this.Filename.Replace("%20", " "));
                myWebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(UnZip);
            }
        }

        public void UnZip(object sender, AsyncCompletedEventArgs e)
        {
          
            string zipPath = this.Folder + "\\" + this.Filename.Replace("%20", " ");
            if (!zipPath.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)) return;
            label.Dispatcher.BeginInvoke(() => label.Content = "Распаковка...");
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            if (File.Exists(zipPath))
            {
                using (FileStream zipToOpen = new FileStream(zipPath, FileMode.Open))
                {
                   
                    using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read, false, Encoding.GetEncoding(866)))
                    {
                       
                        archive.ExtractToDirectory(this.Folder, true);
                    }
                }
            
            }
            label.Dispatcher.BeginInvoke(() => label.Content = "Готово!");

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
