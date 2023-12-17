using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MySql.Data.MySqlClient;

namespace Money_Management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MySqlConnection connection = new MySqlConnection("database=money_management; server=localhost; user id=root;");
        private List<User> userList = json.DeserialiseJson<List<User>>(json.GetJsonFromFile());
        private StackPanel StackPanel;
    public MainWindow()
        {
            InitializeComponent();
            //Get users list from json File
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error : " + ex);
                ErrorWindow connectionError = new ErrorWindow("Erreur: impossible de vous connecter au serveur sql");
                connectionError.Show();
                this.Close();
            }
            ResourceDictionary styleDictionary = new ResourceDictionary();
            styleDictionary.Source = new Uri("Style.xaml", UriKind.RelativeOrAbsolute);
            Grid grid = CreateEntities.SetSettingsGrid((int)this.Width, (int)this.Height, new Thickness(0, 0, 0, 0), HorizontalAlignment.Center, VerticalAlignment.Center);
            this.Content = grid;
            
            if (userList != null)
            {
                //create a button per user
                AddComponents(grid, styleDictionary);
            }
            else
            {
                //if there is no user in the json, then the application opens on the login page
                Login nouvellePage = new Login(connection);
                nouvellePage.Show();
                this.Close();
            }
        }
        private void AddComponents(Grid grid, ResourceDictionary styleDictionary)
        {
            Program.AddEllipses(1, grid);

            Label title = CreateEntities.CreateLabel("Bienvenue", 35, new Thickness(0, 15, 0, 0),
                HorizontalAlignment.Center, VerticalAlignment.Top);
            grid.Children.Add(title);
            StackPanel = CreateEntities.SetSettingsPanel((int)this.Width, (int)this.Height - 90, new Thickness(0, 90, 0, 0), HorizontalAlignment.Center, VerticalAlignment.Center);
            grid.Children.Add(StackPanel);
            Program.CreateUserButton(userList, StackPanel, Button_Click, Brushes.White, this);

            //button for navigate to Login Page
            Button buttonAddUser = CreateEntities.CreateNavigateButton
                ("+", 40, 40, HorizontalAlignment.Right, VerticalAlignment.Top, new Thickness(0, 30, 30, 0), styleDictionary);
            buttonAddUser.Click += ButtonAddUser_Click;
            grid.Children.Add(buttonAddUser);

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            int userId = (int)clickedButton.Tag;
            if (!User.CheckUserInDbOrInJson(User.CheckById(userId, userList).mail, "DataBase", connection))
            {
                passwordForm checkPasswordWindow = new passwordForm(userId, userList, connection);
                checkPasswordWindow.Show();
                Close();
            }
            else
            {
                userList.Remove(User.CheckById(userId, userList));
                json.SetJsonFromFile(userList);
                ErrorWindow Error = new ErrorWindow("Utilisateur introuvable dans la base de donnée !");
                Error.Show();
                //refresh Main page
                MainWindow restartMain = new MainWindow();
                restartMain.Show();
                this.Close();
            }
        }
        private void ButtonAddUser_Click(object sender, RoutedEventArgs e)
        {
            //Redirect to login page
            Login logInPage = new Login(connection);
            logInPage.Show();
            this.Close();
        }
    }
}
