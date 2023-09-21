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
            DataContext = this;
            var userList = json.DeserialiseJson(json.GetJsonFromFile());
            if (userList != null)
            {

                foreach (User user in userList)
                {
                    Button button = new Button();
                    button.Content = user.name.ToUpper() + " " + user.firstName;
                    button.Width = 250;
                    button.Height = 50;
                    button.Background = Brushes.White;
                    button.Click += Button_Click;
                    button.Tag = user.id;
                    button.FontSize = 15;
                    ButtonStackPanel.Children.Add(button);
                }
            }
            else
            {
                LoginForm nouvellePage = new LoginForm();
                frame.Navigate(nouvellePage);
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
