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

        public  static void Start(string folder)
        {
            List<DataModel> files=PrepData(folder);

            Parallel.ForEach<DataModel> (files, (file) =>
            {
                file.DwnldFile();
            });
        }
        static List<DataModel> PrepData(string folder)
        {
            List<DataModel> files = new List<DataModel>() {
                new DataModel("CAD", Paths.paths["CAD"], folder + @"\T-FLEX CAD 17"),
                new DataModel("CAD", Paths.paths["CAD"], folder+ @"\T-FLEX CAD 17"),
                new DataModel("Examples", Paths.paths["Examples"], folder + @"\Примеры 17"),
                new DataModel("Standard", Paths.paths["Standard"], folder + @"\Стандартные элементы 17"),
                new DataModel("MTools", Paths.paths["MTools"], folder + @"\Станочные приспособления 17"),
                new DataModel("Analysis", Paths.paths["Analysis"], folder + @"\T-FLEX Анализ 17"),
                new DataModel("Dynamics", Paths.paths["Dynamics"], folder + @"\T-FLEX Динамика 17"),
                new DataModel("Gears", Paths.paths["Gears"], folder + @"\T-FLEX Зубчатые передачи 17"),
                new DataModel("Electrical", Paths.paths["Electrical"], folder + @"\T-FLEX Электротехника 17"),
                new DataModel("License", Paths.paths["License"], folder + @"\T-FLEX Лицензирование 17")
               };
            return files;
        }
        

    }
}
