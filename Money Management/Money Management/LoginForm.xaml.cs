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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Money_Management
{
    /// <summary>
    /// Logique d'interaction pour LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Page
    {

        public LoginForm()
        {
            InitializeComponent();
            string email = textBoxNomUtilisateur.Text;
            string motDePasse = passwordBoxMotDePasse.Password;
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
