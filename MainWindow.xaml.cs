using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Media;
using WinForm = System.Windows.Forms;
using Label = System.Windows.Controls.Label;


namespace WebDownloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string folder = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnFolder_Click(object sender, RoutedEventArgs e)
        {
            WinForm.FolderBrowserDialog dialog = new WinForm.FolderBrowserDialog();
            dialog.InitialDirectory = "D:\\Downloads";
            WinForm.DialogResult result = dialog.ShowDialog();

            if (result == WinForm.DialogResult.OK)
            {
                textBoxFolder.Text = dialog.SelectedPath;
                folder = dialog.SelectedPath;
            }
            await GetVersionfromFiles();
            List<Label> labels = new List<Label>()
            {
                labelCAD,
                labelStandard,
                labelMTools,
                labelExamples,
                labelAnalysis,
                labelDyna,
                labelGears,
                labelLicense,
                labelElectrical
            };
            foreach (Label label in labels)
            {
                if (labelVersion.Content.ToString().Contains(label.Content.ToString())) label.Foreground = Brushes.Green;
                else label.Foreground = Brushes.Red;
            }

        }
        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {

            Downloader.Start(this);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Версия на сайте:
            await GetVersionfromWeb();
            
        }
        private async Task GetVersionfromFiles()
        {
            string path = textBoxFolder.Text;
            labelCAD.Content = await Task.Run(() => GetMSIVersion.Get(path + @"\T-FLEX CAD 17\T-FLEX CAD 17.msi"));
            labelStandard.Content = await Task.Run(() => GetMSIVersion.Get(path + @"\Стандартные элементы 17\Стандартные элементы 17.msi"));
            labelMTools.Content = await Task.Run(() => GetMSIVersion.Get(path + @"\Станочные приспособления 17\Станочные приспособления 17.msi"));
            labelExamples.Content = await Task.Run(() => GetMSIVersion.Get(path + @"\Примеры 17\Примеры 17.msi"));
            labelAnalysis.Content = await Task.Run(() => GetMSIVersion.Get(path + @"\T-FLEX Анализ 17\T-FLEX Анализ 17.msi"));
            labelDyna.Content = await Task.Run(() => GetMSIVersion.Get(path + @"\T-FLEX Динамика 17\T-FLEX Динамика 17.msi"));
            labelGears.Content = await Task.Run(() => GetMSIVersion.Get(path + @"\T-FLEX Зубчатые передачи 17\T-FLEX Зубчатые передачи 17.msi"));
            labelLicense.Content = await Task.Run(() => GetMSIVersion.Get(path + @"\T-FLEX Лицензирование 17\T-FLEX Лицензирование 17.msi"));
            labelElectrical.Content = await Task.Run(() => GetMSIVersion.Get(path + @"\T-FLEX Электротехника 17\T-FLEX Электротехника 17.msi"));
        }
        private async Task GetVersionfromWeb()
        {
            // Xpatch //*[@id="page_maincontainer"]/div[2]/div[1]/div[1]/div[1]/text()
            // Full Xpatch /html/body/div[7]/div[2]/div[1]/div[1]/div[1]/text()
            string url = "https://www.tflex.ru/downloads/";
            string getVersion = await Task.Run(() =>
            {
                var web = new HtmlAgilityPack.HtmlWeb();
                HtmlDocument doc = web.Load(url);
                return doc.DocumentNode.SelectNodes("//*[@id=\"page_maincontainer\"]/div[2]/div[1]/div[1]/div[1]")[0].InnerText;
            });

            string pattern = @"[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+";
            string version = "";
            RegexOptions options = RegexOptions.Multiline;
            version = Regex.Match(getVersion, pattern, options).Value;
            labelVersion.Content = "Версия на сайте: " + version;
        }

        private void checkBoxElectrical_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
