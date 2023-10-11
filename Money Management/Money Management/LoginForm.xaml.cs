using Azure;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls.Crypto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Money_Management
{
    /// <summary>
    /// Login page for user already sign up
    /// </summary>
    public partial class LoginForm : Page
    {
        private List<User> userList = json.DeserialiseJson(json.GetJsonFromFile());
        private MySqlConnection connection;
        public LoginForm(MySqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            textBoxNomUtilisateur.KeyUp += Enter_keyUp;
            passwordBoxMotDePasse.KeyUp += Enter_keyUp;
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = textBoxNomUtilisateur.Text;
            string motDePasse = passwordBoxMotDePasse.Password;
            if ((email != null) && (motDePasse != null))
            {
                    if (Program.CheckUser("SELECT * FROM users WHERE mail = '" + email + "'", motDePasse, connection))
                    {
                        var user = Program.CreateNewUser(email, connection);

                        if (userList == null)
                        {
                            userList = new List<User>() { user };
                        }
                        else
                        {
                            userList.Add(user);
                        }
                        if (!Program.CheckUserInDbOrInJson(email, "json", connection))
                        {
                            json.SetJsonFromFile(userList);
                        }

                        MainWindow newMainWindow = new MainWindow();
                        Application.Current.MainWindow.Close();
                        Application.Current.MainWindow = newMainWindow;
                        newMainWindow.Show();
                    }
                    else
                    {
                        Debug.WriteLine("user : false, Thread Sleep 1.5 secondes");
                        Thread.Sleep(1500);
                        ErrorWindow errorWindow = new ErrorWindow("Erreur : mots de passe faux");
                        errorWindow.Show();
                    }
            }
            else
            {
                Debug.WriteLine("user : false, Thread Sleep 1.5 secondes");
                Thread.Sleep(1500);
                ErrorWindow errorWindow = new ErrorWindow("Erreur : mots de passe faux");
                errorWindow.Show();
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
            SignUp SignUpPage = new SignUp(connection);
            frame.Navigate(SignUpPage);
        }
        private void Enter_keyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Debug.WriteLine("Enter touch was activate");
                LoginButton_Click(sender, e);
            }
        }
    }
}