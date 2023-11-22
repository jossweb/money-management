using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Security.Cryptography;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using LiveCharts.Wpf;
using LiveCharts;
using System.Windows.Shapes;
using Rectangle = System.Windows.Shapes.Rectangle;
using Brushes = System.Windows.Media.Brushes;
using Color = System.Windows.Media.Color;
using Brush = System.Windows.Media.Brush;
using addExpense = Money_Management.AddExpense;
using HAlignment = System.Windows.HorizontalAlignment;
using VAlignement = System.Windows.VerticalAlignment;
using System.Windows.Controls.Primitives;
using System.Security.Principal;

namespace Money_Management
{
    class Program
    {
        public static void AddComponentSignUp(Grid grid, SignUp window)
        {
            ResourceDictionary styleDictionary = new ResourceDictionary();
            styleDictionary.Source = new Uri("Style.xaml", UriKind.RelativeOrAbsolute);
            const int WIDTHELEMENTS = 340;
            const int HEIGHTTEXTBLOCK = 20;
            const int HEIGHTTEXTBOX = 40;
            List<string> textBlockText = new List<string> { "Nom :", "Prénom :", "Email", "Mot de passe :", "Confirmer le mot de passe :" };
            List<string> NametextBox = new List<string> { "textBoxname", "textBoxfirstName", "textBoxemail" };

            AddEllipses(1, grid);
            Label title = CreateEntities.CreateLabel("Créer votre compte", 35, new Thickness(0, 5, 0, 0),
                HorizontalAlignment.Center, VerticalAlignment.Top);
            grid.Children.Add(title);

            int marginTop = 60;
            for (int i = 0; i < 3; i++)
            {
                TextBlock textBlock = CreateEntities.CreateTextBlock
                    (textBlockText[i], Brushes.Black, WIDTHELEMENTS, HEIGHTTEXTBLOCK, HAlignment.Center, VAlignement.Top, new Thickness(0, marginTop, 0, 0));
                marginTop += HEIGHTTEXTBLOCK;
                grid.Children.Add(textBlock);

                TextBox textBox = CreateEntities.CreateTextBox
                    (NametextBox[i], WIDTHELEMENTS, HEIGHTTEXTBOX, 22, HAlignment.Center, VAlignement.Top, new Thickness(0, marginTop, 0, 0), window, styleDictionary);
                grid.Children.Add(textBox);

                switch (NametextBox[i])
                {
                    case "textBoxname":
                        window.Nom = textBox.Text;
                        break;
                    case "textBoxfirstName":
                        window.Prenom = textBox.Text;
                        break;
                    case "textBoxemail":
                        window.Email = textBox.Text;
                        break;
                }

                marginTop += HEIGHTTEXTBOX;
            }

            for (int i = 0; i < 2; i++)
            {
                TextBlock textBlock = CreateEntities.CreateTextBlock
                    (textBlockText[i + 3], Brushes.Black, WIDTHELEMENTS, HEIGHTTEXTBLOCK, HAlignment.Center, VAlignement.Top, new Thickness(0, marginTop, 0, 0));
                marginTop += HEIGHTTEXTBLOCK;
                grid.Children.Add(textBlock);
                PasswordBox passBox = CreateEntities.CreatePasswordBox
                    (NametextBox[i], WIDTHELEMENTS, HEIGHTTEXTBOX, 22, HAlignment.Center, VAlignement.Top, new Thickness(0, marginTop, 0, 0), window, styleDictionary);
                grid.Children.Add(passBox);
                marginTop += HEIGHTTEXTBOX;
            }
            DatePicker datePicker = CreateEntities.CreateDatePicker
                (150, 40, HAlignment.Center, VAlignement.Top, new Thickness(0, marginTop + HEIGHTTEXTBLOCK, 0, 0), styleDictionary);
            grid.Children.Add (datePicker);
            window.DateDeNaissance = datePicker.SelectedDate ?? DateTime.Now;
            marginTop += 2 * HEIGHTTEXTBOX;

            Button buttonConnection = CreateEntities.CreateConnectionButton
                ("Connection", 120, 45, HAlignment.Center, VAlignement.Top, new Thickness(0, marginTop, 0, 0), styleDictionary);
            grid.Children.Add(buttonConnection);
        }


        public static void AddEllipses(int PageType, Grid grid)
        {
            if (PageType == 1)  //format of page is 600 x 500
            {
                List<Brush> colors = new List<Brush> ();
                colors.Add (Brushes.LightBlue);
                colors.Add (Brushes.LightGreen);
                colors.Add (Brushes.PaleVioletRed);
                colors.Add (Brushes.Coral);
                colors.Add (Brushes.CadetBlue);
                colors.Add (Brushes.Purple);

                List<Thickness> ellipsesMargin = new List<Thickness>();
                ellipsesMargin.Add (new Thickness(-115, -69, 331, 469));
                ellipsesMargin.Add(new Thickness (406, 200, -80, 250 ));
                ellipsesMargin.Add(new Thickness (299, 395, 110, 136));
                ellipsesMargin.Add(new Thickness (34, 260, 385, 272 ));
                ellipsesMargin.Add(new Thickness (34, 516, 385, 25 ));
                ellipsesMargin.Add(new Thickness (350, -60, 0, 500 ));
                ellipsesMargin.Add(new Thickness ( 369, 620, -26, -0 ));


                for (int i = 0; i < ellipsesMargin.Count - 1; i++)
                {
                    Ellipse ellipse = new Ellipse();
                    ellipse.Margin = ellipsesMargin[i];
                    ellipse.Fill = colors[i];
                    grid.Children.Add(ellipse);
                }

            }
        }
        public static void AddContent(Grid grid)
        {
            ResourceDictionary styleDictionary = new ResourceDictionary();
            styleDictionary.Source = new Uri("Style.xaml", UriKind.RelativeOrAbsolute);
            Label title = CreateEntities.CreateLabel("Ajouter une dépense", 35, new Thickness(0, 5, 0, 0), 
                HorizontalAlignment.Center, VerticalAlignment.Top);
            grid.Children.Add(title);
            var datePicker = CreateEntities.CreateDatePicker(160, 40, HAlignment.Center, VAlignement.Center, new Thickness(0, 0, 0, 0), styleDictionary);
            grid.Children.Add(datePicker);
        }
        public static Dictionary<DateTime, string> GetAccountHistory(string duration, MySqlConnection connection, int userId)
        {
            return json.DeserialiseJson<Dictionary<DateTime, string>>(Sql.GetMoneyTransfereInBdd(connection, duration, userId));
        }
        public static Dictionary<DateTime, string> AddNewExpense(Dictionary<DateTime, string> currentExpense, DateTime DateExpense, string titleExpense)
        {
            if (currentExpense == null)
            {
                var initializeDictionary = new Dictionary<DateTime, string>
                {
                    { DateExpense, titleExpense }
                };
                return initializeDictionary;

            }
            else
            {
                currentExpense.Add(DateExpense, titleExpense);
                return currentExpense;
            }
        }
        public static Rectangle CreateRectangle(int height)
        {
            var rectangle = new Rectangle();
            rectangle.Width = 300;
            rectangle.Height = height * 0.80;
            rectangle.HorizontalAlignment = HorizontalAlignment.Left;
            rectangle.VerticalAlignment = VerticalAlignment.Top;
            rectangle.Stroke = Brushes.Gray;
            rectangle.StrokeThickness = 3;
            rectangle.Margin = new Thickness(70, height * 0.15, 0, 0);
            rectangle.RadiusX = 10;
            rectangle.RadiusY = 10;
            Color backgroundColor = Color.FromArgb(80, 180, 180, 180);
            rectangle.Fill = new SolidColorBrush(backgroundColor);
            return rectangle;
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
        public static void CreateUserButton(List<User> users, StackPanel UserButtonPanel, RoutedEventHandler Button_Click, Brush buttonBackground, MainWindow window)
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
                button.BorderBrush = Brushes.Black;
                button.BorderThickness = new Thickness(1.5);
                button.Margin = new Thickness(0, 5, 0, 5);
                button.Style = (Style)window.Resources["UsersButtonStyle"];
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
                    passwordBox.Password = "";
                }
            }
        }
        public static void CreateRectangleValueLabel(string content, Grid grille, SolidColorBrush color, int marginTop)
        {
            Label money = new Label();
            money.Content = content;
            money.HorizontalAlignment = HorizontalAlignment.Center;
            money.VerticalAlignment = VerticalAlignment.Top;
            money.Margin = new Thickness(0, marginTop, 0, 0);
            money.FontSize = 50;
            money.Foreground = color;
            grille.Children.Add(money);
        }
        public static void InsertRectangleContent(Grid grille, MySqlConnection connection, User userConnected, List<string> rectangleValues, RoutedEventHandler Button_Click)
        {
            Label moneyTitle = new Label();
            moneyTitle.Content = "Montant disponible sur votre compte :";
            moneyTitle.HorizontalAlignment = HorizontalAlignment.Center;
            moneyTitle.VerticalAlignment = VerticalAlignment.Top;
            moneyTitle.Margin = new Thickness(0, 0, 0, 0);
            moneyTitle.FontSize = 15;
            grille.Children.Add(moneyTitle);
            var color = new List<SolidColorBrush> {new SolidColorBrush(Colors.Black), new SolidColorBrush(Colors.Red), new SolidColorBrush(Colors.Green)};
            for (int i = 0; i < 3; i++)
            {
                CreateRectangleValueLabel(rectangleValues[i], grille, color[i], 70 + (190 * i));
            }

            Button addExpense = new Button();
            addExpense.Content = "Ajouter une dépense";
            addExpense.Width = 120;
            addExpense.Height = 40;
            addExpense.Margin = new Thickness(0, 350, 0, 0);
            addExpense.HorizontalAlignment = HorizontalAlignment.Center;
            addExpense.VerticalAlignment = VerticalAlignment.Top;
            addExpense.Click += Button_Click;
            grille.Children.Add(addExpense);
        }
        public static void AddEntitiesOnWindow(Grid grille, int heightScreen, ChartValues<double> value, List<string> label)
        {
            int marginTopGraphic = 100;
            var graphic = graphics.CreateCartesianChart(value, label, marginTopGraphic);
            grille.Children.Add(graphic);
            grille.Children.Add(graphics.CreateCartesianChart(value, label, Convert.ToInt32(graphic.Height) + marginTopGraphic + 10)); // + 10 == space between two graphics
            Rectangle rectangle = Program.CreateRectangle(heightScreen);
            grille.Children.Add(rectangle);

            var posY = rectangle.Margin.Top;
            for (int i = 1; i < 3; i++)
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
    }
    public class CreateEntities
    {
        public static Label CreateLabel(string content, int fontSize, Thickness margins, HAlignment horizontalAlignment, VAlignement VerticalAlignement)
        {
            Label label = new Label();
            label.Foreground = Brushes.Black;
            label.Content = content;
            label.HorizontalAlignment = horizontalAlignment;
            label.VerticalAlignment = VerticalAlignement;
            label.Margin = margins;
            label.FontSize = fontSize;
            return label;
        }
        public static Grid SetSettingsGrid(int width, int height, Thickness margins, HAlignment hAlignment, VAlignement vAlignment)
        {
            Grid grid = new Grid();
            grid.Width = width;
            grid.Height = height;
            grid.Margin = margins;
            grid.HorizontalAlignment = hAlignment;
            grid.VerticalAlignment = vAlignment;
            return grid;
        }
        public static DatePicker CreateDatePicker(int width, int height, HAlignment hAlignment, VAlignement vAlignment, Thickness margins, ResourceDictionary styleDictionary)
        {
            DatePicker datePicker = new DatePicker();
            datePicker.Width = width;
            datePicker.Height = height;
            datePicker.HorizontalAlignment = hAlignment;
            datePicker.VerticalAlignment = vAlignment;
            datePicker.Margin = margins;

            // Appliquer le style du DatePicker
            Style textBoxStyle = (Style)styleDictionary["DatePickerStyle"];
            datePicker.Style = textBoxStyle;

            // Appliquer le style du Calendar
            Style calendarStyle = (Style)styleDictionary["StyleCalendar"];
            datePicker.CalendarStyle = calendarStyle;

            return datePicker;
        }
        public static Button CreateConnectionButton(string content ,int width, int height, HAlignment hAlignment, VAlignement vAlignment, Thickness margins, ResourceDictionary styleDictionary)
        {
            Button button = new Button();
            button.Content = content;
            button.Width = width;
            button.Height = height;
            button.HorizontalAlignment = hAlignment;
            button.VerticalAlignment = vAlignment;
            button.Margin = margins;
            Style buttonStyle = (Style)styleDictionary["ConnectionButtonStyle"];
            button.Style = buttonStyle;

            return button;
        }
        public static TextBox CreateTextBox(string name, int width, int height, int FontSize, HAlignment hAlignment, VAlignement vAlignment, Thickness margins, SignUp window, ResourceDictionary styleDictionary)
        {
            TextBox textBox = new TextBox();
            textBox.Name = name;
            textBox.Width = width;
            textBox.Height = height;
            textBox.HorizontalAlignment = hAlignment;
            textBox.VerticalAlignment = vAlignment;
            textBox.Margin = margins;
            textBox.FontSize = FontSize;
            Style textBoxStyle = (Style)styleDictionary["TextBoxStyle"];
            textBox.Style = textBoxStyle;

            return textBox;
        }
        public static TextBlock CreateTextBlock(string content, SolidColorBrush foregroundColor, int width, int height, HAlignment hAlignment, VAlignement vAlignment, Thickness margins)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = content;
            textBlock.Foreground = foregroundColor;
            textBlock.Width = width;
            textBlock.Height = height;
            textBlock.HorizontalAlignment = hAlignment;
            textBlock.VerticalAlignment = vAlignment;
            textBlock.Margin = margins;
            return textBlock;
        }
        public static PasswordBox CreatePasswordBox(string name, int width, int height, int FontSize, HAlignment hAlignment, VAlignement vAlignment, Thickness margins, SignUp window, ResourceDictionary styleDictionary)
        {
            PasswordBox passwordBox = new PasswordBox();
            passwordBox.Name = name;
            passwordBox.Width = width;
            passwordBox.Height = height;
            passwordBox.HorizontalAlignment = hAlignment;
            passwordBox.VerticalAlignment = vAlignment;
            passwordBox.Margin = margins;
            passwordBox.FontSize = FontSize;
            Style passwordBoxStyle = (Style)styleDictionary["PasswordBoxStyle"];
            passwordBox.Style = passwordBoxStyle;

            return passwordBox;
        }
    }
    public class graphics
    {
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
        public static void CreateUserInSql(MySqlConnection connection, User user, string pass)
        {
            string query = "INSERT INTO users (Name, firstName, mail, password, birthday, accountCreationDate) " +
                "VALUES ('" + user.name + "', '" + user.firstName + "', '" + user.mail + "', '" + Program.Hash(pass) + "', '" + user.birthday.ToString("yyyy-MM-dd") + "', '" + user.accountCreationDate.ToString("yyyy-MM-dd") + "')";
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
        public static bool CheckUserPass(string query, string password, MySqlConnection connection)
        {
            try
            {

                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string hashPass = reader.GetString("password");
                        if (hashPass == Program.Hash(password))
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
            else if (storage == "json")
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
    }

}