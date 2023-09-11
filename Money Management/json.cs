using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Money_Management
{
    internal class json
    {
        public static string GetJsonFromFile()
        {
            string json = "";
            string file = "Data";
            string filePath = "Data/dataUser.json";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(file);
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.Write(json);
                }
            }
            return File.ReadAllText(filePath);
        }
        public static void SetJsonFromFile(List<User> usersData)
        {
            string filePath = "Data/dataUser.json";
            string json = JsonConvert.SerializeObject(usersData);

            if (File.Exists(filePath))
            {
                File.WriteAllText(filePath, json);
            }
            else
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.Write(json);
                }
            }
        }
    }
}
