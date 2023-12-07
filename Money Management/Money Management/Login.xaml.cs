using MySql.Data.MySqlClient;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private static TextBox emailTextBox;
        private static PasswordBox passwordTextBox;

        public MySqlConnection connection;
        public Login(MySqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            ResourceDictionary styleDictionary = new ResourceDictionary();
            styleDictionary.Source = new Uri("Style.xaml", UriKind.RelativeOrAbsolute);
            Grid grid = CreateEntities.SetSettingsGrid((int)this.Width, (int)this.Height, new Thickness(0, 0, 0, 0), HorizontalAlignment.Center, VerticalAlignment.Center);
            this.Content = grid;
            AddComponents(grid, styleDictionary);



            Button buttonConnection = CreateEntities.CreateConnectionButton
                ("Connection", 140, 45, HorizontalAlignment.Center, VerticalAlignment.Top, new Thickness(0, 280, 0, 0), styleDictionary);
            buttonConnection.Click += (sender, e) => ConnectionButtonClick();
            grid.Children.Add(buttonConnection);

            Button buttonSignUp = CreateEntities.CreateButtonWithoutEffects
                ("Créer mon compte", 140, 45, HorizontalAlignment.Center, VerticalAlignment.Top, new Thickness(0, 380, 0, 0), styleDictionary);
            buttonSignUp.Click += (sender, e) => RedirectSignUpButtonClick();
            grid.Children.Add(buttonSignUp);


        }
        private static void AddComponents(Grid grid, ResourceDictionary styleDictionary)
        {
            const int WIDTHELEMENTS = 340;
            const int HEIGHTTEXTBLOCK = 20;
            const int HEIGHTTEXTBOX = 40;
            const int HEIGHTTEXTBLOCKADDMARGIN = 60; 

            Program.AddEllipses(1, grid);

            Label title = CreateEntities.CreateLabel("Connection", 35, new Thickness(0, 15, 0, 0),
                HorizontalAlignment.Center, VerticalAlignment.Top);
            grid.Children.Add(title);

            int marginTop = 100;

            TextBlock emailTextBlock = CreateEntities.CreateTextBlock
                  ("Email : ", Brushes.Black, WIDTHELEMENTS, HEIGHTTEXTBLOCK, HorizontalAlignment.Center, VerticalAlignment.Top, new Thickness(0, marginTop, 0, 0));
            marginTop += HEIGHTTEXTBLOCK;
            grid.Children.Add(emailTextBlock);

            TextBox textBox = CreateEntities.CreateTextBox
                    ("emailTextBox", WIDTHELEMENTS, HEIGHTTEXTBOX, 22, HorizontalAlignment.Center, VerticalAlignment.Top, new Thickness(0, marginTop, 0, 0), styleDictionary);
            grid.Children.Add(textBox);
            emailTextBox = textBox;
            marginTop += HEIGHTTEXTBLOCKADDMARGIN;

            TextBlock passwordTextBlock = CreateEntities.CreateTextBlock
                ("Mot de passe : ", Brushes.Black, WIDTHELEMENTS, HEIGHTTEXTBLOCK, HorizontalAlignment.Center, VerticalAlignment.Top, new Thickness(0, marginTop, 0, 0));
            marginTop += HEIGHTTEXTBLOCK;
            grid.Children.Add(passwordTextBlock);

            PasswordBox passwordBox = CreateEntities.CreatePasswordBox
                    ("emailTextBox", WIDTHELEMENTS, HEIGHTTEXTBOX, 22, HorizontalAlignment.Center, VerticalAlignment.Top, new Thickness(0, marginTop, 0, 0), styleDictionary);
            grid.Children.Add(passwordBox);
            passwordTextBox = passwordBox;
            marginTop += HEIGHTTEXTBLOCKADDMARGIN;


        }
        private static void ConnectionButtonClick()
        {

        }
        private static void RedirectSignUpButtonClick()
        {

        }
    }
}
