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
        public string passwordConfirmed { get; set; }
        public DateTime birstdayDate { get; set; }
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
            if (Program.CheckSignInConnections(name, firstName, email, password, passwordConfirmed))
            {
                Debug.Write("Success");
            }

        }
    }
}
