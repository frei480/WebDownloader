using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WinForm = System.Windows.Forms;
using WebDownloader.Commands;
using WebDownloader.ViewModels.Base;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;

namespace WebDownloader.ViewModels
{
    internal class MainWindowViewModel:ViewModel
    {
        #region Заголовок окна
        private string _Title = "Web Downloader";
        /// <summary> Заголовок окна </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        } 
        #endregion
         
        #region Версия на сайте 
        private string _WebVersion = "Версия на сайте: ?";
        /// <summary> Загружаем с сайта актуальную версию </summary>
        public  string WebVersion
        {
            get => _WebVersion;
        
            set => Set(ref _WebVersion, value);
        }
        #endregion

        #region Папка для скачивания 
        private string _Folder = "";
        /// <summary> Путь для загрузки файлов </summary>
        public string Folder
        {
            get => _Folder;

            set => Set(ref _Folder, value);
        }
        #endregion

        #region версия CAD 
        private string _labelCAD = "";
        public string labelCAD
        {
            get => _labelCAD;

            set => Set(ref _labelCAD, value);
        }
        #endregion
        public ObjectVM CAD { get; set; }

        #region версия Standard Parts 
        private string _labelStandard = "";
        public string labelStandard
        {
            get => _labelStandard;

            set => Set(ref _labelStandard, value);
        }
        #endregion

        #region версия Machine Tools
        private string _labelMTools = "";
        public string labelMTools
        {
            get => _labelMTools;

            set => Set(ref _labelMTools, value);
        }
        #endregion

        #region версия Examples
        private string _labelExamples = "";
        public string labelExamples
        {
            get => _labelExamples;

            set => Set(ref _labelExamples, value);
        }
        #endregion
        #region версия Analysis
        private string _labelAnalysis = "";
        public string labelAnalysis
        {
            get => _labelAnalysis;

            set => Set(ref _labelAnalysis, value);
        }
        #endregion
        #region версия Dynamics
        private string _labelDynamics = "";
        public string labelDynamics
        {
            get => _labelDynamics;

            set => Set(ref _labelDynamics, value);
        }
        #endregion
        #region версия Gears
        private string _labelGears = "";
        public string labelGears
        {
            get => _labelGears;

            set => Set(ref _labelGears, value);
        }
        #endregion
        #region версия License
        private string _labelLicense = "";
        public string labelLicense
        {
            get => _labelLicense;

            set => Set(ref _labelLicense, value);
        }
        #endregion
        #region версия Electrical
        private string _labelElectrical = "";
        public string labelElectrical
        {
            get => _labelElectrical;

            set => Set(ref _labelElectrical, value);
        }
        #endregion

        #region Команды 
        #region Команда Назначить папку 
        public ICommand SetFolderCommand { get; set; }
        private async void OnSetFolderCommandExecuted(object p)
        {
            WinForm.FolderBrowserDialog dialog = new WinForm.FolderBrowserDialog();
            dialog.InitialDirectory = "D:\\Downloads";
            WinForm.DialogResult result = dialog.ShowDialog();
            
            if (result == WinForm.DialogResult.OK)
            {
                Folder = dialog.SelectedPath;
                OnPropertyChanged(nameof(Folder));
            }
            CAD.labelText = "test";
            await GetVersionfromFiles();
           /*
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
            } */
        }
        private bool CanSetFolderCommandExecute(object p) => true;
        
        #endregion
        #endregion
        public MainWindowViewModel()
        {
            #region Команды 
            SetFolderCommand = new LambdaCommand(OnSetFolderCommandExecuted, CanSetFolderCommandExecute);
            #endregion
            ObjectVM CAD = new ObjectVM();
            Task.Run(() =>  GetVersionfromWeb().Wait());          
      
            
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
            WebVersion = "Версия на сайте: " + version;         
            OnPropertyChanged(nameof(WebVersion));
        }
        private async Task GetVersionfromFiles()
        {
            string path = Folder;
            CAD.labelText = await Task.Run(() => GetMSIVersion.Get(path + @"\T-FLEX CAD 17\T-FLEX CAD 17.msi"));
            OnPropertyChanged(nameof(CAD.labelText));
            labelStandard = await Task.Run(() => GetMSIVersion.Get(path + @"\Стандартные элементы 17\Стандартные элементы 17.msi"));
            OnPropertyChanged(nameof(labelStandard));
            labelMTools = await Task.Run(() => GetMSIVersion.Get(path + @"\Станочные приспособления 17\Станочные приспособления 17.msi"));
            OnPropertyChanged(nameof(labelMTools));
            labelExamples = await Task.Run(() => GetMSIVersion.Get(path + @"\Примеры 17\Примеры 17.msi"));
            OnPropertyChanged(nameof(labelExamples));
            labelAnalysis = await Task.Run(() => GetMSIVersion.Get(path + @"\T-FLEX Анализ 17\T-FLEX Анализ 17.msi"));
            OnPropertyChanged(nameof(labelAnalysis));
            labelDynamics = await Task.Run(() => GetMSIVersion.Get(path + @"\T-FLEX Динамика 17\T-FLEX Динамика 17.msi"));
            OnPropertyChanged(nameof(labelDynamics));
            labelGears = await Task.Run(() => GetMSIVersion.Get(path + @"\T-FLEX Зубчатые передачи 17\T-FLEX Зубчатые передачи 17.msi"));
            OnPropertyChanged(nameof(labelGears));
            labelLicense = await Task.Run(() => GetMSIVersion.Get(path + @"\T-FLEX Лицензирование 17\T-FLEX Лицензирование 17.msi"));
            OnPropertyChanged(nameof(labelLicense));
            labelElectrical = await Task.Run(() => GetMSIVersion.Get(path + @"\T-FLEX Электротехника 17\T-FLEX Электротехника 17.msi"));
            OnPropertyChanged(nameof(labelElectrical));
        }
    }
    internal class ObjectVM : ViewModel
    {
        #region версия  
        private string _labelText = "";
        public string labelText
        {
            get => _labelText;

            set => Set(ref _labelText, value);
        }
        #endregion
        public ObjectVM()
        {            
            this.labelText= string.Empty;
        }
    }
}