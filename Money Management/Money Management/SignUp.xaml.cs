using MySql.Data.MySqlClient;
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
using System.Windows.Shapes;

namespace Money_Management
{
    /// <summary>
    /// Logique d'interaction pour SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        private MySqlConnection connection;
        public string name { get; set; }
        public string firstName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string PasswordConfirmed { get; set; }
        public DateTime BirstdayDate { get; set; }
        public SignUp(MySqlConnection connection)
        {
            InitializeComponent();

            this.connection = connection;
            ResourceDictionary styleDictionary = new ResourceDictionary();
            styleDictionary.Source = new Uri("Style.xaml", UriKind.RelativeOrAbsolute);

            Grid grid = CreateEntities.SetSettingsGrid((int)this.Width, (int)this.Height, new Thickness(0, 0, 0, 0), HorizontalAlignment.Center, VerticalAlignment.Center);
            this.Content = grid;
            Program.AddComponentSignUp(grid, this, styleDictionary);
            Button buttonConnection = CreateEntities.CreateConnectionButton
                ("Connection", 140, 45, HorizontalAlignment.Center, VerticalAlignment.Top, new Thickness(0, 450, 0, 0), styleDictionary);
            buttonConnection.Click += (sender, e) => ConnectionButtonClick();
            grid.Children.Add(buttonConnection);
        }
        public void ConnectionButtonClick()
        {
            if ((name != null) && (firstName != null) && (email != null) && (password != null) && (PasswordConfirmed != null))
            {
                if ((name.Length > 50) && (firstName.Length > 50) && (email.Length > 50) && (password.Length > 50) && (PasswordConfirmed.Length > 50))
                {
                    if ((firstName.Length < 3) && (name.Length < 3) && (email.Length < 3))
                    {
                        if (email.Contains("@"))
                        {
                            if (password == PasswordConfirmed)
                            {
                                if (password.Length > 8)
                                {
                                    return true;
                                }
                                else
                                {
                                    ErrorWindow error = new ErrorWindow("ERREUR: Votre mot de passe doit contenir au moins 8 caractères");
                                    error.Show();
                                    Debug.WriteLine("ERROR : The password contains less than 8 characters");
                                }
                            }
                            else
                            {
                                ErrorWindow error = new ErrorWindow("ERREUR: Vos mots de passes ne correspondent pas !");
                                error.Show();
                                Debug.WriteLine("ERROR : Two passwords do not match");
                            }
                        }
                        else
                        {
                            ErrorWindow error = new ErrorWindow("ERREUR: Votre adresse email doit contenir un caractère '@' !");
                            error.Show();
                            Debug.WriteLine("ERROR : email does not contain '@'");
                        }
                    }
                    else
                    {
                        ErrorWindow error = new ErrorWindow("ERREUR: Les champs 'nom', 'prénom' et 'email' doivent contenir au minimum 3 caractères !");
                        error.Show();
                        Debug.WriteLine("ERROR : Last name, first name or email fields contain less than 3 characters");
                    }

                }
                else
                {
                    ErrorWindow error = new ErrorWindow("ERREUR: Les champs doivent chacun contenir moins de 50 caractères !");
                    error.Show();
                    Debug.WriteLine("ERROR: One or more fields contain more than 50 characters!");
                }     
            }
            else
            {
                ErrorWindow error = new ErrorWindow("ERREUR: Veuillez remplir tous les champs du formulaire !");
                error.Show();
                Debug.WriteLine("ERROR: Not all fields are completed!!");
            }
        }
    }
}
