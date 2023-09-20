using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using System.Windows.Navigation;

namespace Money_Management
{
    class Program
    {
        public static bool CheckUser(string mail, string password)
        {
            MySqlConnection connection = new MySqlConnection("database=money management; server=localhost; user id=root;");

            try
            {
                connection.Open();
                MessageBox.Show("Connecter avec succès");

                string query = "SELECT * FROM users WHERE mail = '" + mail + "'";
                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string hashPass = reader.GetString("password");
                        if (hashPass == Hash(password))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Erreur, aucune adresse email correspondant");
                        return false;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Erreur !");
                return false;
            }
        }
        public static string Hash(string hash)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(hash);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                string hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return hashString;
            }
        }
    }
        public class User
        {
            /// <summary>
            /// Create user object
            /// </summary>
            public int id;
            public string name;
            public string firstName;
            public string keypass;
            public DateTime birthday;
            public DateTime accountCreationDate;
            public User(int id, string name, string firstName, string keypass, DateTime birthday, DateTime accountCreationDate)
            {
                this.id = id;
                this.name = name;
                this.firstName = firstName;
                this.keypass = keypass;
                this.birthday = birthday;
                this.accountCreationDate = accountCreationDate;

            }
        }
}
