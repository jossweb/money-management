using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Money_Management
{
    /// <summary>
    /// Login page for user already sign up
    /// </summary>
    public partial class LoginForm : Page
    {
        public MySqlConnection connection = new MySqlConnection("database=money management; server=localhost; user id=root;");
        public List<User> userList = json.DeserialiseJson(json.GetJsonFromFile());
        public LoginForm()
        {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = textBoxNomUtilisateur.Text;
            string motDePasse = passwordBoxMotDePasse.Password;
            if ((email != null) && (motDePasse != null))
            {
                if (Program.CheckUser(email, motDePasse, connection))
                {
                    MessageBox.Show("Connecté avec succès");
                    var user = Program.CreateNewUser(email, connection);
                    if (user != null)
                    {
                        if (userList == null)
                        {
                            userList = new List<User>() { user };
                        }
                        else 
                        {
                            userList.Add(user);
                        }  
                        json.SetJsonFromFile(userList);
                        MainWindow newMainWindow = new MainWindow();
                        Application.Current.MainWindow.Close();
                        Application.Current.MainWindow = newMainWindow;
                        newMainWindow.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Erreur");
                }

            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs");
            }
        }
        private void BackHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newMainWindow = new MainWindow();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = newMainWindow;
            newMainWindow.Show();
        }
        private void SignUp_Button(object sender, RoutedEventArgs e)
        {
            SignUp SignUpPage = new SignUp();
            frame.Navigate(SignUpPage);
        }

    }
}
