using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using Torpedo.Modell.Single_modell;
using System.IO;
using System.Windows;
using Newtonsoft.Json;

namespace Torpedo
{
    
    class FileWriter
    {
        static string filepath = "gyoztesek.json";
        public static List<Datas> list_adatok = new List<Datas>();
        public static void WriteToJSON(Datas adatok)
        {
            list_adatok.Add(adatok);
            string json = JsonConvert.SerializeObject(list_adatok, Formatting.Indented);
            File.WriteAllText(filepath,json);

        }
        public static void ReadFromJSON()
        {

        }

    }
}
