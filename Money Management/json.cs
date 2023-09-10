using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Management
{
    internal class json
    {
        public static string GetJsonFromFile()
        {
            string json = "";
            string file = "Data";
            string filePath = "Data/personneList.json";

            if (!Directory.Exists(file))
            {
                Directory.CreateDirectory(file);
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.Write(json);
                }
            }
            return File.ReadAllText("Data/personneList.json");
        }
    }

}
