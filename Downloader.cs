using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace WebDownloader
{
    public static class Downloader
    {

        public  static void Start(string folder, MainWindow form)
        {
            List<DataModel> files=PrepData(folder, form);

            Parallel.ForEach<DataModel> (files, (file) =>
            {
                file.DwnldFile();
            });
        }
        static List<DataModel> PrepData(string folder, MainWindow form)
        {
            List<DataModel> files = new List<DataModel>() {
                new DataModel("CAD", Paths.paths["CAD"], folder + @"\T-FLEX CAD 17", form.ProgressBarCAD, form.checkBoxCAD),                
                new DataModel("Examples", Paths.paths["Examples"], folder + @"\Примеры 17",form.ProgressBarExamples, form.checkBoxExamples),
                new DataModel("Standard", Paths.paths["Standard"], folder + @"\Стандартные элементы 17",form.ProgressBarStandard, form.checkBoxStandard),
                new DataModel("MTools", Paths.paths["MTools"], folder + @"\Станочные приспособления 17",form.ProgressBarMtools, form.checkBoxMTools),
                new DataModel("Analysis", Paths.paths["Analysis"], folder + @"\T-FLEX Анализ 17",form.ProgressBarAnalysis, form.checkBoxAnalysis),
                new DataModel("Dynamics", Paths.paths["Dynamics"], folder + @"\T-FLEX Динамика 17",form.ProgressBarDyna, form.checkBoxDyna),
                new DataModel("Gears", Paths.paths["Gears"], folder + @"\T-FLEX Зубчатые передачи 17",form.ProgressBarGears, form.checkBoxGears),
                new DataModel("Electrical", Paths.paths["Electrical"], folder + @"\T-FLEX Электротехника 17",form.ProgressBarElectrical, form.checkBoxElectrical),
                new DataModel("License", Paths.paths["License"], folder + @"\T-FLEX Лицензирование 17",form.ProgressBarLicense, form.checkBoxLicense)
               };
            return files;
        }
        

    }
}
