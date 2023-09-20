using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
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
                if (Program.CheckUser(email, motDePasse))
                {
                    MessageBox.Show("Connecté avec succès");
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
    }
}
