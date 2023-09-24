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
        public static bool CheckUser(string mail, string password, MySqlConnection connection)
        {

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
        public static User CreateNewUser(string mail, MySqlConnection connection)
        {
            string query = "SELECT * FROM users WHERE mail = '" + mail + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    try
                    {
                        int id = int.Parse(reader.GetString("ID"));
                        string name = reader.GetString("name");
                        string firstName = reader.GetString("firstName");
                        DateTime birthday = DateTime.Parse(reader.GetString("birthday"));
                        DateTime accountCreationDate = DateTime.Parse(reader.GetString("accountCreationDate"));

                        var user = new User(name, firstName, mail, birthday, accountCreationDate, id);
                        return user;
                    }
                    catch
                    {
                        MessageBox.Show("Erreur création de nouvel user");

                    }
                }
                return null;
            }
        }
        public static void CreateUserSql(MySqlConnection connection, User user, string HashPass)
        {
            string query = "INSERT INTO users (Name, firstName, mail, password, birthday, accountCreationDate) " +
                "VALUES ('" + user.name + "', '" + user.firstName + "', '" + user.mail + "', '" + HashPass + "', '" + user.birthday + "', '" + user.accountCreationDate + "')";
            MySqlCommand command = new MySqlCommand(query, connection);
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Créer avec succes");
            }
            else
            {
                MessageBox.Show("Erreur");
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
        public string mail;
        public DateTime birthday;
        public DateTime accountCreationDate;
        public User(string name, string firstName, string mail,DateTime birthday, DateTime accountCreationDate, int id = 0)
        {
            this.id = id;
            this.name = name;
            this.firstName = firstName;
            this.mail = mail;
            this.birthday = birthday;
            this.accountCreationDate = accountCreationDate;

        }
    }

}
