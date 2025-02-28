﻿using System;
using System.Collections.Generic;
using System.IO;
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
            string filePath = "Data/UsersData.json";

            if (!Directory.Exists(file))
            {
                Directory.CreateDirectory(file);
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.Write(json);
                }
            }
            return File.ReadAllText("Data/UsersData.json");
        }
        public static void SetJsonFromFile(List<User> usersData)
        {
            string filePath = "Data/UsersData.json";
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
        public static T DeserialiseJson<T>(string json)
        {
            if (json == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}