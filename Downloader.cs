using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Controls;

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
                new DataModel("CAD", Paths.paths["CAD"], form.folder+@"\T-FLEX CAD 17", form.ProgressBarCAD, form.checkBoxCAD, form.labelCAD),
                new DataModel("Examples", Paths.paths["Examples"], form.folder, form.ProgressBarExamples, form.checkBoxExamples, form.labelExamples),
                new DataModel("Standard", Paths.paths["Standard"], form.folder, form.ProgressBarStandard, form.checkBoxStandard, form.labelStandard),
                new DataModel("MTools", Paths.paths["MTools"], form.folder, form.ProgressBarMtools, form.checkBoxMTools, form.labelMTools),
                new DataModel("Analysis", Paths.paths["Analysis"], form.folder, form.ProgressBarAnalysis, form.checkBoxAnalysis, form.labelAnalysis),
                new DataModel("Dynamics", Paths.paths["Dynamics"], form.folder, form.ProgressBarDyna, form.checkBoxDyna, form.labelDyna),
                new DataModel("Gears", Paths.paths["Gears"], form.folder, form.ProgressBarGears, form.checkBoxGears, form.labelGears),
                new DataModel("Electrical", Paths.paths["Electrical"], form.folder, form.ProgressBarElectrical, form.checkBoxElectrical, form.labelElectrical),
                new DataModel("License", Paths.paths["License"], form.folder, form.ProgressBarLicense, form.checkBoxLicense, form.labelLicense)
               };
        //foreach (var key in Paths.paths.Keys)
        //    files.Add(new DataModel(key, Paths.paths[key], form.folder, form.FindName("ProgressBar" + key) as ProgressBar, form.FindName("checkBox"+key) as CheckBox, form.FindName("label" + key) as Label));
        return files;
    }


    }
}
