using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;


namespace WebDownloader
{
    /// <summary>
    /// How to use:
    /// string msiVersion = PropertyTable.Get("MyInstall.msi", "ProductVersion");
    /// </summary>      
    public class GetMSIVersion
    {
        public static string Get(string msi)
        {
            if (!File.Exists(msi)) { return "нет файла"; }
            var view = ((dynamic)Activator.CreateInstance(Type.GetTypeFromProgID("WindowsInstaller.Installer")))
                .OpenDatabase(msi, 0)
                .OpenView("SELECT Value FROM Property WHERE Property = 'ProductVersion'");
            view.Execute();
            return view.Fetch().StringData(1);
        }

            
        
    }
}
