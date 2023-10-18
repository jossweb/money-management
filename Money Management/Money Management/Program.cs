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
using LiveCharts.Wpf;
using LiveCharts;
using LiveCharts.Definitions.Charts;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using LiveCharts.Dtos;
using System.Windows.Shapes;
using static System.Net.WebRequestMethods;

namespace Money_Management
{
    class Program
    {
        public static Dictionary<DateTime, string> GetAccountHistory(string duration, MySqlConnection connection, int userId)
        {
            return json.DeserialiseJson<Dictionary<DateTime, string>>(Sql.GetMoneyTransfereInBdd(connection, duration, userId));
            
        }
        public static Rectangle CreateRectangle()
        {
            var rectangle = new Rectangle();
            rectangle.Width = 300;
            rectangle.Height = 600;
            rectangle.HorizontalAlignment = HorizontalAlignment.Left;
            rectangle.VerticalAlignment = VerticalAlignment.Top;
            rectangle.Stroke = Brushes.Gray;
            rectangle.StrokeThickness = 3;
            rectangle.Margin = new Thickness(70, 110, 0, 0);
            rectangle.RadiusX = 10;
            rectangle.RadiusY = 10;
            Color backgroundColor = Color.FromArgb(80, 180, 180, 180);
            rectangle.Fill = new SolidColorBrush(backgroundColor);
            return rectangle;
        }
        public static bool CheckUser(string query, string password, MySqlConnection connection)
        {
            try
            {

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
                            Debug.WriteLine("invalid password");
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
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
                    string query = "SELECT COUNT(*) FROM users WHERE mail = @Email";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Email", email);

                    int count = Convert.ToInt32(command.ExecuteScalar());
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
                    Debug.WriteLine("Error : " + ex);
                    ErrorWindow error = new ErrorWindow("Inpossible de se connecter à la base de donnée ! \n " +
                        "Veuillez vérifier l'état du réseau et réessayer");
                    error.Show();
                    return false;
                }
            }
            else if(storage == "json")
            {
                List<User> userList = json.DeserialiseJson<List<User>>(json.GetJsonFromFile());
                
                if (userList != null)
                {
                    foreach (User user in userList)
                    {
                        if (user.mail == email)
                        {
                            ErrorWindow errorWindow = new ErrorWindow("Erreur : Utilisateur déjà enregistré !");
                            errorWindow.Show();
                            Debug.WriteLine("Error : Already existing email");
                            return true;
                        }
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
                
                
                MainWindow main = new MainWindow();
                main.Show();
                ErrorWindow error = new ErrorWindow("Inscrit avec success !");
                error.Show();
                System.Windows.Application.Current.MainWindow.Close();
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
        public static void InsertLabel(Grid grille, MySqlConnection connection, User userConnected)
        {
            Label moneyTitle = new Label();
            moneyTitle.Content = "Montant disponible sur votre compte :";
            moneyTitle.HorizontalAlignment = HorizontalAlignment.Center;
            moneyTitle.VerticalAlignment = VerticalAlignment.Top;
            moneyTitle.Margin = new Thickness(0, 0, 0, 0);
            moneyTitle.FontSize = 15;
            grille.Children.Add(moneyTitle);

            Label money = new Label();
            money.Content = Sql.GetAccountFunds(userConnected.id, connection) + " €";
            money.HorizontalAlignment = HorizontalAlignment.Center;
            money.VerticalAlignment = VerticalAlignment.Top;
            money.Margin = new Thickness(0, 70, 0, 0);
            money.FontSize = 50;
            grille.Children.Add(money);
        }
        public static void AddGraphicsOnWindow(Grid grille, ChartValues<double> value, List<string> label)
        {
            int marginTopGraphic = 100;
            var graphic = graphics.CreateCartesianChart(value, label, marginTopGraphic);
            grille.Children.Add(graphic);
            grille.Children.Add(graphics.CreateCartesianChart(value, label, Convert.ToInt32(graphic.Height) + marginTopGraphic + 10)); // + 10 == space between two graphics
            Rectangle rectangle = Program.CreateRectangle();

            grille.Children.Add(rectangle);

            var nombreDeDivisions = 3;

            var posY = rectangle.Margin.Top;
            for (int i = 1; i < nombreDeDivisions; i++)
            {
                var ligne = new Line();
                ligne.Stroke = Brushes.Gray;
                ligne.StrokeThickness = 3;
                ligne.X1 = rectangle.Margin.Left;
                ligne.X2 = rectangle.Margin.Left + rectangle.Width;
                ligne.Y1 = posY + (rectangle.Height / 3) * i;
                ligne.Y2 = posY + (rectangle.Height / 3) * i;
                grille.Children.Add(ligne);
            }
        }
        public static void SetSettingsGrid(Grid secondGrid, int width, int height, Thickness margins)
        {
            secondGrid.Width = width;
            secondGrid.Height = height;
            secondGrid.Margin = margins;
            secondGrid.HorizontalAlignment = HorizontalAlignment.Left;
            secondGrid.VerticalAlignment = VerticalAlignment.Top;
        }
        public static void CreateWindowNameTitle(Grid grille, User userConnected)
        {
            Label label = new Label();
            label.Content = userConnected.name + " " + userConnected.firstName;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Top;
            label.Margin = new Thickness(0, 20, 0, 0);
            label.FontSize = 50;
            grille.Children.Add(label);
        }
    }
    public class graphics
    {
        public static CartesianChart CartesianGraphique(ChartValues<double> value, List<string> label)
        {
            var graphic = new CartesianChart();

            var axeX = new AxesCollection();
            axeX.Add(new Axis
            {
                Title = "label",
                Labels = label
            });

            graphic.AxisX = axeX;

            graphic.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "value",
                    Values = value,
                    DataLabels = true,
                    Fill = new SolidColorBrush(Colors.Red)
                }
            };

            graphic.Width = 500;
            graphic.Height = 300;
            graphic.Margin = new Thickness(0, 450, 950, 0);

            return graphic;
        }
        public static CartesianChart CreateCartesianChart(ChartValues<double> Value, List<string> name, int marginTop)
        {
            var graphique = new CartesianChart();

            var axeX = new AxesCollection();
            axeX.Add(new Axis
            {
                Title = "Solde sur les 30 derniers jours",
                Labels = name
            });

            graphique.AxisX = axeX;

            graphique.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "euros",
                    Values = Value ,
                    Stroke = new SolidColorBrush(Colors.Gray)
                }
            };
            graphique.HorizontalAlignment = HorizontalAlignment.Right;
            graphique.VerticalAlignment = VerticalAlignment.Top;
            graphique.Width = 550;
            graphique.Height = 230;
            graphique.Margin = new Thickness(0, marginTop, 70, 0);
            return graphique;
        }
        public static PieChart CreatePieChart(ChartValues<int> data)
        {
            var pieChart = new PieChart
            {
                Series = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "Catégorie A",
                        Values = data,
                        DataLabels = true,
                        Fill = new SolidColorBrush(Colors.Red)
                    },
                    new PieSeries
                    {
                        Title = "Catégorie B",
                        Values = data,
                        DataLabels = true,
                        Fill = new SolidColorBrush(Colors.Blue)
                    },
                    new PieSeries
                    {
                        Title = "Catégorie C",
                        Values = data,
                        DataLabels = true,
                        Fill = new SolidColorBrush(Colors.Green)
                    }
                },

            };
            pieChart.Width = 300;
            pieChart.Height = 300;
            pieChart.Margin = new Thickness(0, 400, 0, 0);

            return pieChart;
        }
    }
    public class Sql
    {
        public static float GetAccountFunds(int id, MySqlConnection connection)
        {
            string query = "SELECT money_on_account FROM data_money WHERE ID_user = '" + id + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    try
                    {
                        float money_in_eu = float.Parse(reader.GetString("money_on_account"));
                        return money_in_eu;
                    }
                    catch (Exception ex) 
                    { 
                        ErrorWindow error = new ErrorWindow("Erreur : Erreur récupération données");
                        error.Show();
                        Debug.WriteLine(ex.Message);
                        System.Windows.Application.Current.MainWindow.Close();
                        return 0;
                    }
                }
                else
                {
                    ErrorWindow error = new ErrorWindow("Erreur : Erreur de connection de base de donnée");
                    error.Show();
                    Debug.WriteLine("Error : connection to sql data base is impossible");
                    System.Windows.Application.Current.MainWindow.Close();
                    return 0;
                }
            }
        }
        public static string GetMoneyTransfereInBdd(MySqlConnection connection, string type, int id)
        {
            string query = "SELECT " + type + " FROM data_money WHERE ID_user = '" + id + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    try
                    {
                        string money_in_eu = reader.GetString(type);
                        return money_in_eu;
                    }
                    catch (Exception ex)
                    {
                        ErrorWindow error = new ErrorWindow("Erreur : Erreur récupération données");
                        error.Show();
                        Debug.WriteLine(ex.Message);
                        System.Windows.Application.Current.MainWindow.Close();
                        return null;
                    }
                }
                else
                {
                    ErrorWindow error = new ErrorWindow("Erreur : Erreur de connection de base de donnée");
                    error.Show();
                    Debug.WriteLine("Error : connection to sql data base is impossible");
                    System.Windows.Application.Current.MainWindow.Close();
                    return null;
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
        public User(string name, string firstName, string mail, DateTime birthday, DateTime accountCreationDate, int id = 0)
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
            foreach (User user in users)
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