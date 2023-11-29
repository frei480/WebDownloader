using System;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media;


namespace WebDownloader
{
    internal class ObjectVM : INotifyPropertyChanged
    {
        #region Реализация интерфейса INotifyPropertyChanged
        /// <summary> Реализация интерфейса INotifyPropertyChanged </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
        /// <summary> Имя пакета </summary>
        public string Name { get; set; } = "";

        /// <summary> версия </summary>
        public string FileVersion { get; set; } = "";

        /// <summary> Относительный путь пакета</summary>
        public string RelativePath { get; set; } = "";

        /// <summary> Относительный загрузки пакета</summary>
        public string Folder { get; set; } = "";

        /// <summary> Путь для загрузки пакета </summary>
        public string Filename { get; set; } = "";

        /// <summary> Подлежит загрузке </summary>
        public bool IsDownload { get; set; } = false;

        /// <summary> Для прогрессбара </summary>
        private int _Progress = 0;
        public int Progress
        {
            get => _Progress;
            set
            {
                _Progress = value;
                OnPropertyChanged();
            }
        }

        /// <summary> Цвет текста на форме </summary>
        public SolidColorBrush myColor { get; set; } = Brushes.Black;

        public void DwnldFile()
        {

            if (!Directory.Exists(this.Folder)) Directory.CreateDirectory(this.Folder);

            //label.Dispatcher.BeginInvoke(() => label.Content = "Скачиваю");
            using (WebClient myWebClient = new WebClient())
            {
                myWebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                myWebClient.DownloadFileAsync(new Uri(Paths.url + this.Filename), this.Folder + "\\" + this.Filename.Replace("%20", " "));
                myWebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(UnZip);
            }
        }
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.Progress = e.ProgressPercentage;
        }
        public void UnZip(object sender, AsyncCompletedEventArgs e)
        {

            string zipPath = this.Folder + "\\" + this.Filename.Replace("%20", " ");
            if (!zipPath.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)) return;
            this.FileVersion = "Распаковка...";
            OnPropertyChanged(nameof(this.FileVersion));

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            if (System.IO.File.Exists(zipPath))
            {
                using (FileStream zipToOpen = new FileStream(zipPath, FileMode.Open))
                {

                    using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read, false, Encoding.GetEncoding(866)))
                    {

                        archive.ExtractToDirectory(this.Folder, true);
                    }
                }

            }

            this.FileVersion = "Готово!";
            OnPropertyChanged(nameof(this.FileVersion));
        }
    }
}
