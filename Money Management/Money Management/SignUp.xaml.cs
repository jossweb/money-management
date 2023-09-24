using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
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
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if ((textBoxname.Text != null) & (textBoxfirstName.Text != null) & (textBoxemail.Text != null) & (PasswordBoxPassword.Password != null) & (PasswordBoxpasswordValidation.Password != null))
            {
                try
                {
                    string name = textBoxname.Text;
                    string firstName = textBoxfirstName.Text;
                    string email = textBoxemail.Text;
                    string pass = PasswordBoxPassword.Password;
                    string confirmPass = PasswordBoxpasswordValidation.Password;
                    DateTime? birthday = DatePickerBirthday.SelectedDate;
                    DateTime selectedDate = birthday.Value;
                    if (pass == confirmPass)
                    {
                        User newUser = new User(name, firstName, email, selectedDate, DateTime.Now);
                        Program.CreateUserSql(connection, newUser, pass);
                    }
                    else
                    {
                        ErrorWindow errorWindow = new ErrorWindow("Erreur : Les mots de passe ne sont pas identiques");
                        errorWindow.Show();
                    }
                }
                catch(Exception ex) 
                {
                    MessageBox.Show("Erreur" + ex);
                }

            }
        }

        private void BackHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newMainWindow = new MainWindow();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = newMainWindow;
            newMainWindow.Show();
        }
    }
}
