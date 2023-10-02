using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Money_Management.Program;

namespace Money_Management
{
    /// <summary>
    /// Logique d'interaction pour SignUp.xaml
    /// </summary>
    public partial class SignUp : Page
    {
        public MySqlConnection connection = new MySqlConnection("database=money management; server=localhost; user id=root;");

        public SignUp()
        {
            InitializeComponent();
            connection.Open();
            DatePickerBirthday.SelectedDate = DateTime.Now;
            textBoxname.KeyUp += Enter_keyUp;
            textBoxfirstName.KeyUp += Enter_keyUp;
            textBoxemail.KeyUp += Enter_keyUp;
            PasswordBoxpasswordValidation.KeyUp += Enter_keyUp;
            PasswordBoxPassword.KeyUp += Enter_keyUp;


        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var textBoxs = new List<TextBox> { textBoxname, textBoxfirstName, textBoxemail };
            var passBoxs = new List<PasswordBox> { PasswordBoxPassword, PasswordBoxpasswordValidation };

            if (!string.IsNullOrWhiteSpace(textBoxname.Text) && !string.IsNullOrWhiteSpace(textBoxfirstName.Text) &&
                !string.IsNullOrWhiteSpace(textBoxemail.Text) && !string.IsNullOrWhiteSpace(PasswordBoxPassword.Password) &&
                !string.IsNullOrWhiteSpace(PasswordBoxpasswordValidation.Password)) 
            {
                string name = textBoxname.Text;
                string firstName = textBoxfirstName.Text;
                string email = textBoxemail.Text;
                string pass = PasswordBoxPassword.Password;
                string confirmPass = PasswordBoxpasswordValidation.Password;

                if ((name.Length < 50) && (firstName.Length < 50) &&
                    (email.Length < 50) && (pass.Length < 50) &&
                    (confirmPass.Length < 50))
                {
                    if ((name.Contains(" ")) && (firstName.Contains(" ")) &&
                    (email.Contains(" ") && (pass.Contains(" ")) &&
                    (confirmPass.Contains(" "))))
                    {
                        try
                        {

                            DateTime? birthday = DatePickerBirthday.SelectedDate;
                            DateTime selectedDate = birthday.Value;
                            if (email.Contains("@"))
                            {
                                if (pass == confirmPass)
                                {
                                    if (!Program.CheckUserInDbOrInJson(email, "DataBase", connection))
                                    {
                                        User newUser = new User(name, firstName, email, selectedDate, DateTime.Now);
                                        Program.CreateUserSql(connection, newUser, pass);
                                    }
                                    else
                                    {
                                        ErrorWindow errorWindow = new ErrorWindow("Erreur : Email déjà utilisé");
                                        errorWindow.Show();
                                    }
                                }
                                else
                                {
                                    ErrorWindow errorWindow = new ErrorWindow("Erreur : Les mots de passe ne sont pas identiques");
                                    errorWindow.Show();
                                }
                            }
                            else
                            {
                                ErrorWindow errorWindow = new ErrorWindow("Erreur : Veuillez entrer une véritable adresse email");
                                errorWindow.Show();
                            }
                        }
                        catch (Exception ex)
                        {
                            Program.RemoveText(textBoxs, passBoxs);
                            Debug.WriteLine("Text Field reset");
                            Debug.WriteLine("Error" + ex);
                            ErrorWindow errorWindow = new ErrorWindow("Erreur : Connection au serveur échoué ...");
                            errorWindow.Show();
                        }
                    }
                    else
                    {
                        Program.RemoveText(textBoxs, passBoxs);
                        Debug.WriteLine("Error : field text can't contain text");
                        ErrorWindow errorWindow = new ErrorWindow("Erreur : Les champs ne peuvent pas contenir d'espace");
                        errorWindow.Show();
                    }
                }
                else
                {
                    Program.RemoveText(textBoxs, passBoxs);
                    Debug.WriteLine("Error : User to enter more than 50 character");
                    ErrorWindow errorWindow = new ErrorWindow("Erreur : Veuillez ne pas dépasser 50 caractères par champs");
                    errorWindow.Show();
                }

            }
            else
            {
                Program.RemoveText(textBoxs, passBoxs);
                Debug.WriteLine("Error : All texts field was not completed");
                ErrorWindow errorWindow = new ErrorWindow("Erreur : Veuillez remplir tous les champs");
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
