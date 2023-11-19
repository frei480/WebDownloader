using HtmlAgilityPack;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using WinForm = System.Windows.Forms;


namespace WebDownloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string folder = "";
        public MainWindow()
        {
            InitializeComponent();

        }

        private void btnFolder_Click(object sender, RoutedEventArgs e)
        {
            WinForm.FolderBrowserDialog dialog = new WinForm.FolderBrowserDialog();
            dialog.InitialDirectory = "D:\\Downloads";
            WinForm.DialogResult result = dialog.ShowDialog();

            if (result == WinForm.DialogResult.OK)
            {
                textBoxFolder.Text = dialog.SelectedPath;
                folder = dialog.SelectedPath;
            }
        }
        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {

            Downloader.Start(folder, this);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Версия на сайте:
            GetVersionfromWeb();
        }

        private async Task GetVersionfromWeb()
        {
            // Xpatch //*[@id="page_maincontainer"]/div[2]/div[1]/div[1]/div[1]/text()
            // Full Xpatch /html/body/div[7]/div[2]/div[1]/div[1]/div[1]/text()
            string url = "https://www.tflex.ru/downloads/";
            string getVersion = await Task.Run(() => { 
            var web = new HtmlAgilityPack.HtmlWeb();
            HtmlDocument doc =  web.Load(url);
            return doc.DocumentNode.SelectNodes("//*[@id=\"page_maincontainer\"]/div[2]/div[1]/div[1]/div[1]")[0].InnerText;            
            });

            string pattern = @"[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+";
            string version = "";
            RegexOptions options = RegexOptions.Multiline;
            foreach (Match m in Regex.Matches(getVersion, pattern, options))
            {
                version = m.Value;
            }

            labelVersion.Content = "Версия на сайте: " + version;
        }
    }
}
