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

        private TextBox textBoxName;
        private TextBox textBoxFirstName;
        private TextBox textBoxEmail;
        private PasswordBox passwordBox1;
        private PasswordBox passwordBox2;
        public SignUp(MySqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            ResourceDictionary styleDictionary = new ResourceDictionary();
            styleDictionary.Source = new Uri("Style.xaml", UriKind.RelativeOrAbsolute);
            Grid grid = CreateEntities.SetSettingsGrid((int)this.Width, (int)this.Height, new Thickness(0, 0, 0, 0), HorizontalAlignment.Center, VerticalAlignment.Center);
            this.Content = grid;
            AddComponents(grid, this, styleDictionary);
            Button buttonConnection = CreateEntities.CreateConnectionButton
                ("Connection", 140, 45, HorizontalAlignment.Center, VerticalAlignment.Top, new Thickness(0, 450, 0, 0), styleDictionary);
            buttonConnection.Click += (sender, e) => ConnectionButtonClick();
            grid.Children.Add(buttonConnection);
        }
        private static void AddComponents(Grid grid, SignUp window, ResourceDictionary styleDictionary)
        {
            const int WIDTHELEMENTS = 340;
            const int HEIGHTTEXTBLOCK = 20;
            const int HEIGHTTEXTBOX = 40;
            int marginTop = 60;
            List<string> textBlockText = new List<string> { "Nom :", "Prénom :", "Email", "Mot de passe :", "Confirmer le mot de passe :" };
            List<string> NametextBox = new List<string> { "textBoxname", "textBoxfirstName", "textBoxemail" };

            //This one add Ellipse on the background of the page
            Program.AddEllipses(1, grid);

            //Add Label Title
            Label title = CreateEntities.CreateLabel("Créer votre compte", 35, new Thickness(0, 5, 0, 0),
                HorizontalAlignment.Center, VerticalAlignment.Top);
            grid.Children.Add(title);

            //textblock creation loop
            for (int i = 0; i < 3; i++)
            {
                TextBlock textBlock = CreateEntities.CreateTextBlock
                    (textBlockText[i], Brushes.Black, WIDTHELEMENTS, HEIGHTTEXTBLOCK, HorizontalAlignment.Center, VerticalAlignment.Top, new Thickness(0, marginTop, 0, 0));
                marginTop += HEIGHTTEXTBLOCK;
                grid.Children.Add(textBlock);

                TextBox textBox = CreateEntities.CreateTextBox
                    (NametextBox[i], WIDTHELEMENTS, HEIGHTTEXTBOX, 22, HorizontalAlignment.Center, VerticalAlignment.Top, new Thickness(0, marginTop, 0, 0), window, styleDictionary);
                grid.Children.Add(textBox);

                marginTop += HEIGHTTEXTBOX;
            }

            for (int i = 0; i < 2; i++)
            {
                TextBlock textBlock = CreateEntities.CreateTextBlock
                    (textBlockText[i + 3], Brushes.Black, WIDTHELEMENTS, HEIGHTTEXTBLOCK, HorizontalAlignment.Center, VerticalAlignment.Top, new Thickness(0, marginTop, 0, 0));
                marginTop += HEIGHTTEXTBLOCK;
                grid.Children.Add(textBlock);
                PasswordBox passBox = CreateEntities.CreatePasswordBox
                    (NametextBox[i], WIDTHELEMENTS, HEIGHTTEXTBOX, 22, HorizontalAlignment.Center, VerticalAlignment.Top, new Thickness(0, marginTop, 0, 0), window, styleDictionary);
                grid.Children.Add(passBox);
                marginTop += HEIGHTTEXTBOX;
            }
            DatePicker datePicker = CreateEntities.CreateDatePicker
                (150, 40, HorizontalAlignment.Center, VerticalAlignment.Top, new Thickness(0, marginTop + HEIGHTTEXTBLOCK, 0, 0), styleDictionary);
            grid.Children.Add(datePicker);
        }
        public void ConnectionButtonClick()
        {

        }
    }
}
