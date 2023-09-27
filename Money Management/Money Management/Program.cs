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
using System.Windows.Controls;
using System.Diagnostics.Tracing;
using System.Windows.Media;
using System.Windows.Media.Media3D;


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
        public static void CreateUserSql(MySqlConnection connection, User user, string pass)
        {
            string query = "INSERT INTO users (Name, firstName, mail, password, birthday, accountCreationDate) " +
                "VALUES ('" + user.name + "', '" + user.firstName + "', '" + user.mail + "', '" + Hash(pass) + "', '" + user.birthday.ToString("yyyy-MM-dd") + "', '" + user.accountCreationDate.ToString("yyyy-MM-dd") + "')";
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
        public static void CreateUserButton(List<User> users, StackPanel UserButtonPanel, RoutedEventHandler Button_Click, Brush buttonBackground)
        {
            foreach (User user in users)
            {
                Button button = new Button();
                button.Name = "UserButton";
                button.Content = user.name.ToUpper() + " " + user.firstName;
                button.Width = 250;
                button.Height = 50;
                button.Background = buttonBackground;
                button.Click += Button_Click;
                button.Tag = user.id;
                button.FontSize = 15;
                button.Margin = new Thickness(0, 5, 0, 5);
                UserButtonPanel.Children.Add(button);
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
        public static User CheckById(int id, List<User> users)
        {
            foreach(User user in users)
            {
                if (user.id == id)
                {
                    return user;
                }
            }
            ErrorWindow errorWindow = new ErrorWindow("Erreur : Impossible de retrouver l'utilisateur");
            errorWindow.Show();
            DateTime dateOnly = new DateTime(2023, 1, 1);
            return new User("XXXXXX", "XXXXXX", "XXXXXX@XXX.XXX", dateOnly, dateOnly);
        }
    }

}
