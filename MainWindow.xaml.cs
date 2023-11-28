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
using WebDownloader.ViewModels;

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
            //await GetVersionfromFiles();
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


    }
}
