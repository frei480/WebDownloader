using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebDownloader
{
    internal static class Paths
    {
        public static Dictionary<string, string> paths = new Dictionary<string, string>()
        {
            { "CAD",   @"https://www.tflex.ru/downloads/T-FLEX%20CAD%2017.msi" },
            { "Examples", @"https://www.tflex.ru/downloads/Examples%2017.zip" },
            { "Standard", @"https://www.tflex.ru/downloads/Standard%20parts%2017.zip" },
            { "MTools",  @"https://www.tflex.ru/downloads/Machine%20tools%2017.zip"},
            { "Analysis",  @"https://www.tflex.ru/downloads/T-FLEX%20Analysis%2017.zip"},
            { "Dynamics",  @"https://www.tflex.ru/downloads/T-FLEX%20Dynamics%2017.zip"},
            { "Gears",  @"https://www.tflex.ru/downloads/T-FLEX%20Gears%2017.zip"},
            { "Electrical",  @"https://www.tflex.ru/downloads/T-FLEX%20Electrical%2017.zip"},
            { "License",  @"https://www.tflex.ru/downloads/T-FLEX%2017%20Licensing.zip"}
        };
    }
}
