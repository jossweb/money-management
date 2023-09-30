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
        private MySqlConnection connection = new MySqlConnection("database=money management; server=localhost; user id=root;");
        private List<User> userList = json.DeserialiseJson(json.GetJsonFromFile());
    public MainWindow()
        {
            InitializeComponent();
            //Get users list from json File

            if (userList != null)
            {
                //create a button per user
                Program.CreateUserButton(userList, ButtonStackPanel, Button_Click, Brushes.White);
            }
            else
            {
                //if there is no user in the json, then the application opens on the login page
                LoginForm nouvellePage = new LoginForm();
                frame.Navigate(nouvellePage);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            int userId = (int)clickedButton.Tag;
            if (!Program.CheckUserInDbOrInJson(User.CheckById(userId, userList).mail, "DataBase", connection))
            {
                passwordForm checkPasswordWindow = new passwordForm(userId, userList);
                checkPasswordWindow.Show();
                this.Close();
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
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //Redirect to login page
            LoginForm logInPage = new LoginForm();
            frame.Navigate(logInPage);
        }
    }
}
