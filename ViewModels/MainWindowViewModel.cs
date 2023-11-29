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
using System.ComponentModel;
using Brushes = System.Windows.Media.Brushes;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;


namespace WebDownloader.ViewModels
{
    internal class MainWindowViewModel: INotifyPropertyChanged
    {
        #region Реализация интерфейса INotifyPropertyChanged
        /// <summary> Реализация интерфейса INotifyPropertyChanged </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region Заголовок окна
        private string _Title = "Web Downloader";
        /// <summary> Заголовок окна </summary>
        public string Title
        {
            get => _Title;
            set { _Title = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }
        #endregion

        #region Коллекция параметров 
        /// <summary> Коллекция параметров </summary>
        public ObservableCollection<ObjectVM> objectVMs { get; set; }
        #endregion

        #region Команды 
        #region Команда Назначить папку 
        //public ICommand PressCommand { get { return new DelegateCommand(Press); } }
        
        /// <summary> Назначаем папку для загрузки пакетов </summary>
        public ICommand SetFolderCommand { get { return new DelegateCommand(OnSetFolderCommand);} }        
        private void OnSetFolderCommand(object obj)
        {
            WinForm.FolderBrowserDialog dialog = new WinForm.FolderBrowserDialog();
            dialog.InitialDirectory = "D:\\Downloads";
            WinForm.DialogResult result = dialog.ShowDialog();
            if (result == WinForm.DialogResult.OK)
            {
                Folder = dialog.SelectedPath;                
            }
            //Проверяем версии ранее загруженных пакетов
            foreach (ObjectVM p in objectVMs)
            {
                p.Folder = Folder;
                p.FileVersion = GetMSIVersion.Get(Folder + p.RelativePath);
                if (WebVersion.Contains(p.FileVersion)) p.myColor = Brushes.Green;
                else p.myColor = Brushes.Red;
            }
            
            //Поскольку коллекция не изменилась-нужно обновить
            CollectionViewSource.GetDefaultView(objectVMs).Refresh();
        }
        #endregion
        #region Команда Скачать
        public ICommand DownloadCommand { get { return new DelegateCommand(OnDownloadCommand); } }

        private void OnDownloadCommand(object obj)
        {
            //Downloader.Start(objectVMs);
            Parallel.ForEach<ObjectVM>(objectVMs.Where(pack => pack.IsDownload == true), (file) =>
            {
                file.DwnldFile();
            });
        }
        #endregion
        #endregion
        public MainWindowViewModel()
        {
            #region Команды 
            #endregion           
            //Загружаем значение актуальной сборки с сайта
            Task.Run(() =>  GetVersionfromWeb().Wait());
            Prepdata();    
        }

        private void Prepdata()
        {  //загружаем объекты в 2 этапа - подготавливаем массив и скармливаем его в колелцию, так быстрее
            ObjectVM[] parameters =
            {
                new ObjectVM() { Name="T-FLEX CAD",
                                 Filename= Paths.paths["CAD"], 
                                 RelativePath = @"\T-FLEX CAD 17\T-FLEX CAD 17.msi",
                                 Folder = Folder + @"\T-FLEX CAD 17\" },

                new ObjectVM() { Name="Стандартные элементы 17", 
                                 Filename= Paths.paths["Standard"], 
                                 RelativePath = @"\Стандартные элементы 17\Стандартные элементы 17.msi",
                                 Folder = Folder},
                
                new ObjectVM() { Name ="Станочные приспособления 17", 
                                 Filename= Paths.paths["MTools"],
                                 RelativePath = @"\Станочные приспособления 17\Станочные приспособления 17.msi",
                                 Folder = Folder},
                
                new ObjectVM() { Name ="Примеры 17", 
                                 Filename= Paths.paths["Examples"],
                                 RelativePath = @"\Примеры 17\Примеры 17.msi",
                                 Folder = Folder},

                new ObjectVM() { Name ="T-FLEX Анализ 17", 
                                 Filename=Paths.paths["Analysis"], 
                                 RelativePath = @"\T-FLEX Анализ 17\T-FLEX Анализ 17.msi",
                                 Folder = Folder},

                new ObjectVM() { Name ="T-FLEX Динамика 17", 
                                 Filename= Paths.paths["Dynamics"], 
                                 RelativePath = @"\T-FLEX Динамика 17\T-FLEX Динамика 17.msi",
                                 Folder = Folder},

                new ObjectVM() { Name ="T-FLEX Зубчатые передачи 17", 
                                 Filename= Paths.paths["Gears"], 
                                 RelativePath = @"\T-FLEX Зубчатые передачи 17\T-FLEX Зубчатые передачи 17.msi",
                                 Folder = Folder},

                new ObjectVM() { Name ="T-FLEX Лицензирование 17", 
                                 Filename= Paths.paths["License"], 
                                 RelativePath = @"\T-FLEX Лицензирование 17\T-FLEX Лицензирование 17.msi",
                                 Folder = Folder},

                new ObjectVM() { Name ="T-FLEX Электротехника 17", 
                                 Filename= Paths.paths["Electrical"], 
                                 RelativePath = @"\T-FLEX Электротехника 17\T-FLEX Электротехника 17.msi",
                                 Folder = Folder}
            };
            objectVMs = new ObservableCollection<ObjectVM>(parameters);
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