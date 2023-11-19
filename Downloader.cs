using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebDownloader
{
    public static class Downloader
    {

        public static void Start(string folder, MainWindow form)
        {
            List<DataModel> files = PrepData(folder, form);
            Parallel.ForEach<DataModel>(files, (file) => file.DwnldFile());
        }
        static List<DataModel> PrepData(string folder, MainWindow form)
        {
            List<DataModel> files = new List<DataModel>() {
                new DataModel("CAD",        Paths.paths["CAD"],        folder, form.ProgressBarCAD,         form.checkBoxCAD.IsChecked??false),
                new DataModel("Examples",   Paths.paths["Examples"],   folder, form.ProgressBarExamples,    form.checkBoxExamples.IsChecked??false),
                new DataModel("Standard",   Paths.paths["Standard"],   folder, form.ProgressBarStandard,    form.checkBoxStandard.IsChecked??false),
                new DataModel("MTools",     Paths.paths["MTools"],     folder, form.ProgressBarMtools,      form.checkBoxMTools.IsChecked ?? false),
                new DataModel("Analysis",   Paths.paths["Analysis"],   folder, form.ProgressBarAnalysis,    form.checkBoxAnalysis.IsChecked ?? false),
                new DataModel("Dynamics",   Paths.paths["Dynamics"],   folder, form.ProgressBarDyna,        form.checkBoxDyna.IsChecked ?? false),
                new DataModel("Gears",      Paths.paths["Gears"],      folder, form.ProgressBarGears,       form.checkBoxGears.IsChecked ?? false),
                new DataModel("Electrical", Paths.paths["Electrical"], folder, form.ProgressBarElectrical,  form.checkBoxElectrical.IsChecked ?? false),
                new DataModel("License",    Paths.paths["License"],    folder, form.ProgressBarLicense,     form.checkBoxLicense.IsChecked ?? false)
               };
            return files;
        }


    }
}
