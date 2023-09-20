using System;
using System.Windows;
using System.Windows.Controls;
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
            DataContext = this;
            var userList = json.DeserialiseJson(json.GetJsonFromFile());
            if (userList == null)
            {
                LoginForm nouvellePage = new LoginForm();
                frame.Navigate(nouvellePage);
            }
            foreach (User user in userList)
            {
                Button button = new Button();
                button.Content = user.name;
                button.Click += Button_Click;
                button.Tag = user.id;
                ButtonStackPanel.Children.Add(button);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            LoginForm nouvellePage = new LoginForm();
            frame.Navigate(nouvellePage);
        }
    }
}
