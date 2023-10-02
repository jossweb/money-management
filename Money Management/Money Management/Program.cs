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
using Azure;
using System.Net.Mail;
using System.Diagnostics;
using MySqlX.XDevAPI.CRUD;

namespace Money_Management
{
    class Program
    {
        public static bool CheckUser(string query, string password, MySqlConnection connection)
        {
            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string hashPass = reader.GetString("password");
                        if (hashPass == Hash(password))
                        {
                            connection.Close();
                            return true;
                        }
                        else
                        {
                            connection.Close();
                            Debug.WriteLine("invalid password");
                            return false;
                        }
                    }
                    else
                    {
                        connection.Close();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                Debug.WriteLine("Error : " + ex);
                ErrorWindow error = new ErrorWindow("Inpossible de se connecter à la base de donnée ! \n Veuillez vérifier l'état du réseau et réessayer");
                error.Show();
                return false;
            }
        }
        public static bool CheckUserInDbOrInJson(string email, string storage, MySqlConnection connection)
        {
            if (storage == "DataBase")
            {
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM users WHERE mail = @Email";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Email", email);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                    if (count > 0)
                    {
                        
                        Debug.WriteLine("Already existing email");
                        return false;

                    }
                    else
                    {
                        Debug.WriteLine("Unused email");
                        return true;
                    }

                }
                catch (Exception ex)
                {
                    connection.Close();
                    Debug.WriteLine("Error : " + ex);
                    ErrorWindow error = new ErrorWindow("Inpossible de se connecter à la base de donnée ! \n " +
                        "Veuillez vérifier l'état du réseau et réessayer");
                    error.Show();
                    return false;
                }
            }
            else if(storage == "json")
            {
                List<User> userList = json.DeserialiseJson(json.GetJsonFromFile());
                
                foreach(User user in userList)
                {
                    if (user.mail == email)
                    {
                        ErrorWindow errorWindow = new ErrorWindow("Erreur : Utilisateur déjà enregistré !");
                        errorWindow.Show();
                        Debug.WriteLine("Error : Already existing email");
                        return true;
                    }
                }
                Debug.WriteLine("Success : Unused email");
                return false;
            }
            else 
            {
                Debug.WriteLine("Error : storage value is not valid");
                return true; 
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
            connection.Open();
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
                    catch (Exception ex)
                    {
                        ErrorWindow error = new ErrorWindow("Erreur de création de l'utilisateur");
                        error.Show();
                        Debug.WriteLine("Error : " + ex);
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
        public static void RemoveText(List<TextBox> TextBoxList, List<PasswordBox> PassBoxList)
        {
            if (TextBoxList != null)
            {
                foreach (TextBox textBox in TextBoxList)
                {
                    textBox.Text = "";
                }
            }
            if (PassBoxList != null)
            {
                foreach (PasswordBox passwordBox in PassBoxList)
                {
                    passwordBox.Password = "Password";
                }
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