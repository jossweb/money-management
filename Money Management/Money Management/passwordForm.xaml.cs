using Azure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Money_Management
{
    /// <summary>
    /// Logique d'interaction pour passwordForm.xaml
    /// </summary>
    public partial class passwordForm : Window
    {
        private MySqlConnection connection;
        private int tag;
        public passwordForm(int TagUserSelect, List<User> users, MySqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            tag = TagUserSelect;
            User userSelect = User.CheckById(TagUserSelect, users);
            welcomeLabel.Content = "Bienvenue " + userSelect.name + " " + userSelect.firstName;
            Debug.WriteLine("info : user select : " + userSelect.mail);

            passbox.KeyUp += Enter_keyUp;
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string pass = passbox.Password;
            string query = "SELECT * FROM users WHERE ID = '" + tag + "'";
            if (User.CheckUserPass(query, pass, connection) == 1)
            {
                PrincipalForm principalForm = new PrincipalForm(User.CheckById(tag, json.DeserialiseJson<List<User>>(json.GetJsonFromFile())), connection);
                principalForm.Show();
                this.Close();
            }
            else
            {
                Debug.WriteLine("info : Password is not valid");
                Thread.Sleep(1500);
                MessageBox.Show("Mot de passe faux");
            }
        }
        private void Enter_keyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Debug.WriteLine("Enter touch was activate");
                Login_Click(sender, e);
            }
        }
    }
}
