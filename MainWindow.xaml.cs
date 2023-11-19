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
    }
}
