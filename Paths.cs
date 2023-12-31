﻿using System.Collections.Generic;

namespace WebDownloader
{
    internal static class Paths
    {
        /// <summary>
        /// Страница загрузки
        /// </summary>
        public static string url = "https://www.tflex.ru/downloads/";
        
        /// <summary> URL адрес пакета </summary>
        public static Dictionary<string, string> paths = new Dictionary<string, string>()
        {
            { "CAD",   @"T-FLEX%20CAD%2017.msi" },
            { "Standard", @"Standard%20parts%2017.zip" },
            { "MTools",  @"Machine%20tools%2017.zip"},
            { "Examples", @"Examples%2017.zip" },
            { "Analysis",  @"T-FLEX%20Analysis%2017.zip"},
            { "Dynamics",  @"T-FLEX%20Dynamics%2017.zip"},
            { "Gears",  @"T-FLEX%20Gears%2017.zip"},
            { "Electrical",  @"T-FLEX%20Electrical%2017.zip"},
            { "License",  @"T-FLEX%2017%20Licensing.zip"}
        };
    }
}
