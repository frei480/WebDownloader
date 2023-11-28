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
using System.Windows.Forms;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Media;
using System.Drawing;
using System.Diagnostics;
using Brushes = System.Windows.Media.Brushes;

namespace WebDownloader.ViewModels
{
    internal class MainWindowViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        #region Заголовок окна
        private string _Title = "Web Downloader";
        /// <summary> Заголовок окна </summary>
        public string Title
        {
            get => _Title;
            set { _Title = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Title)));
            }
        } 
        #endregion
         
        #region Версия на сайте 
        private string _WebVersion = "Версия на сайте: ?";
        /// <summary> Загружаем с сайта актуальную версию </summary>
        public  string WebVersion
        {
            get => _WebVersion;
            set { 
                _WebVersion = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(WebVersion)));
            }
        }
        #endregion

        #region Папка для скачивания 
        private string _Folder = "";
        /// <summary> Путь для загрузки файлов </summary>
        public string Folder
        {
            get => _Folder;
            set
            {
                _Folder = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Folder)));
            }
        }
        #endregion

        #region версия CAD 
        public ObjectVM CAD { get; set; } = new ObjectVM();
        #endregion

        #region версия Standard Parts 
        public ObjectVM Standard { get; set; } = new ObjectVM();
        #endregion
        #region версия Machine Tools
        public ObjectVM MTools { get; set; } = new ObjectVM();               
        #endregion
        #region версия Examples
        private string _labelExamples = "";
        public ObjectVM Examples { get; set; } = new ObjectVM();
        #endregion
        #region версия Analysis
        public ObjectVM Analysis { get; set; } = new ObjectVM();
        #endregion
        #region версия Dynamics
        public ObjectVM Dynamics { get; set; } = new ObjectVM();
        #endregion
        #region версия Gears
        public ObjectVM Gears { get; set; } = new ObjectVM();
        #endregion
        #region версия License
        public ObjectVM License { get; set; } = new ObjectVM();
        #endregion
        #region версия Electrical
        public ObjectVM Electrical { get; set; } = new ObjectVM();
        #endregion

        #region Команды 
        #region Команда Назначить папку 
        //public ICommand PressCommand { get { return new DelegateCommand(Press); } }
        public ICommand SetFolderCommand { get { return new DelegateCommand(OnSetFolderCommand);} }        
        private async void OnSetFolderCommand(object p)
        {
            WinForm.FolderBrowserDialog dialog = new WinForm.FolderBrowserDialog();
            dialog.InitialDirectory = "D:\\Downloads";
            WinForm.DialogResult result = dialog.ShowDialog();
            
            if (result == WinForm.DialogResult.OK)
            {
                Folder = dialog.SelectedPath;                
            }
            
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
            
            #endregion

         //   ObjectVM CAD = new ObjectVM();

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
                HtmlAgilityPack.HtmlDocument doc = web.Load(url);
                return doc.DocumentNode.SelectNodes("//*[@id=\"page_maincontainer\"]/div[2]/div[1]/div[1]/div[1]")[0].InnerText;
            });
            string pattern = @"[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+";
            string version = "";
            RegexOptions options = RegexOptions.Multiline;
            version = Regex.Match(getVersion, pattern, options).Value;
            WebVersion = "Версия на сайте: " + version;         
        }
        private async Task GetVersionfromFiles()
        {
            string path = Folder;
            CAD.FileVersion = await Task.Run(() => GetMSIVersion.Get(path + @"\T-FLEX CAD 17\T-FLEX CAD 17.msi"));
            Standard.FileVersion = await Task.Run(() => GetMSIVersion.Get(path + @"\Стандартные элементы 17\Стандартные элементы 17.msi"));
            MTools.FileVersion = await Task.Run(() => GetMSIVersion.Get(path + @"\Станочные приспособления 17\Станочные приспособления 17.msi"));
            Examples.FileVersion = await Task.Run(() => GetMSIVersion.Get(path + @"\Примеры 17\Примеры 17.msi"));
            Analysis.FileVersion = await Task.Run(() => GetMSIVersion.Get(path + @"\T-FLEX Анализ 17\T-FLEX Анализ 17.msi"));
            Dynamics.FileVersion = await Task.Run(() => GetMSIVersion.Get(path + @"\T-FLEX Динамика 17\T-FLEX Динамика 17.msi"));
            Gears.FileVersion = await Task.Run(() => GetMSIVersion.Get(path + @"\T-FLEX Зубчатые передачи 17\T-FLEX Зубчатые передачи 17.msi"));
            License.FileVersion = await Task.Run(() => GetMSIVersion.Get(path + @"\T-FLEX Лицензирование 17\T-FLEX Лицензирование 17.msi"));
            Electrical.FileVersion = await Task.Run(() => GetMSIVersion.Get(path + @"\T-FLEX Электротехника 17\T-FLEX Электротехника 17.msi"));
        }
    }
    internal class ObjectVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        #region версия  
        private string _FileVersion = "";
        public string FileVersion
        {
            get => _FileVersion;
            set
            { 
                _FileVersion = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(FileVersion)));
            }
        }
        #endregion
        #region цвет  
        SolidColorBrush _Color = Brushes.Black;
        public SolidColorBrush myColor
        {
            get => _Color;
            set
            {
                _Color = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(myColor)));
            }
        }
        #endregion
    }
    public class DelegateCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExceute;
        event EventHandler? ICommand.CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public DelegateCommand(Action<object> execute, Func<object, bool> canExceute = null)
        {
            this.execute = execute;
            this.canExceute = canExceute;
        }
        bool ICommand.CanExecute(object? parameter)
        {
            return this.canExceute == null || this.canExceute(parameter);
        }

        void ICommand.Execute(object? parameter)
        {
            this.execute(parameter);
        }
    }
}