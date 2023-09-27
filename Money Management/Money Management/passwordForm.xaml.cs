using Azure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private MySqlConnection connection = new MySqlConnection("database=money management; server=localhost; user id=root;");
        private int tag;
        public passwordForm(int TagUserSelect, List<User> users)
        {
            InitializeComponent();
            tag = TagUserSelect;
            User userSelect = User.CheckById(TagUserSelect, users);
            welcomeLabel.Content = "Bienvenue " + userSelect.name + " " + userSelect.firstName; 
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {

            string pass = passbox.Password;
            string query = "SELECT * FROM users WHERE ID = '" + tag + "'";
            if (Program.CheckUser(query, pass, connection))
            {
                MessageBox.Show("Mot de passe vrai");
            }
            else
            {
                MessageBox.Show("Mot de passe faux");
            }
        }
    }
}
