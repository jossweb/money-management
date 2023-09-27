using System;
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
        public MainWindow()
        {
            InitializeComponent();
            //Get users list from json File
            var userList = json.DeserialiseJson(json.GetJsonFromFile());
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
            passwordForm checkPasswordWindow = new passwordForm();
            checkPasswordWindow.Show();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //Redirect to login page
            LoginForm logInPage = new LoginForm();
            frame.Navigate(logInPage);
        }
    }
}
