using System.Windows;

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


        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            Downloader.Start(this);
        }

    }
}
