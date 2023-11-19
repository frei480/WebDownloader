using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace WebDownloader
{
    public static class Downloader
    {

        public static void Start(MainWindow form)
        {
            List<DataModel> files = PrepData(form);
            
            //загрузка только тех у кого чекбокс отмечен
            Parallel.ForEach<DataModel>(files.Where(pack => pack.checkBoxCheked==true), (file) =>
            {
                file.DwnldFile();
             });
            
        }
        static List<DataModel> PrepData(MainWindow form)
        {
            List<DataModel> files = new List<DataModel>() {
                new DataModel("CAD",        Paths.paths["CAD"],        form.folder, form.ProgressBarCAD,         form.checkBoxCAD),
                new DataModel("Examples",   Paths.paths["Examples"],   form.folder, form.ProgressBarExamples,    form.checkBoxExamples),
                new DataModel("Standard",   Paths.paths["Standard"],   form.folder, form.ProgressBarStandard,    form.checkBoxStandard),
                new DataModel("MTools",     Paths.paths["MTools"],     form.folder, form.ProgressBarMtools,      form.checkBoxMTools),
                new DataModel("Analysis",   Paths.paths["Analysis"],   form.folder, form.ProgressBarAnalysis,    form.checkBoxAnalysis),
                new DataModel("Dynamics",   Paths.paths["Dynamics"],   form.folder, form.ProgressBarDyna,        form.checkBoxDyna),
                new DataModel("Gears",      Paths.paths["Gears"],      form.folder, form.ProgressBarGears,       form.checkBoxGears),
                new DataModel("Electrical", Paths.paths["Electrical"], form.folder, form.ProgressBarElectrical,  form.checkBoxElectrical),
                new DataModel("License",    Paths.paths["License"],    form.folder, form.ProgressBarLicense,     form.checkBoxLicense)
               };
            return files;
        }


    }
}
