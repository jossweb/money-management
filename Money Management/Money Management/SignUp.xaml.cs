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

        private static TextBox textBoxName;
        private static TextBox textBoxFirstName;
        private static TextBox textBoxEmail;
        private static PasswordBox passwordBox1;
        private static PasswordBox passwordBox2;
        private static DatePicker datePicker;
        public SignUp(MySqlConnection connection)
        {
            InitializeComponent();
            textBoxName = new TextBox();
            textBoxFirstName = new TextBox();
            textBoxEmail = new TextBox();
            passwordBox1 = new PasswordBox();
            passwordBox2 = new PasswordBox();

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
            List<string> textBlockText = new List<string> { "Nom :", "Prénom :", "Email", "Mot de passe :", "Confirmer le mot de passe :" };
            List<string> NametextBox = new List<string> { "textBoxname", "textBoxfirstName", "textBoxemail" };

            Program.AddEllipses(1, grid);

            Label title = CreateEntities.CreateLabel("Créer votre compte", 35, new Thickness(0, 5, 0, 0),
                HorizontalAlignment.Center, VerticalAlignment.Top);
            grid.Children.Add(title);

            int marginTop = 60;
            for (int i = 0; i < 3; i++)
            {
                TextBlock textBlock = CreateEntities.CreateTextBlock
                    (textBlockText[i], Brushes.Black, WIDTHELEMENTS, HEIGHTTEXTBLOCK, HorizontalAlignment.Center, VerticalAlignment.Top, new Thickness(0, marginTop, 0, 0));
                marginTop += HEIGHTTEXTBLOCK;
                grid.Children.Add(textBlock);

                TextBox textBox = CreateEntities.CreateTextBox
                    (NametextBox[i], WIDTHELEMENTS, HEIGHTTEXTBOX, 22, HorizontalAlignment.Center, VerticalAlignment.Top, new Thickness(0, marginTop, 0, 0), window, styleDictionary);

                switch (i)
                {
                    case 0:
                        textBoxName = textBox;
                        break;
                    case 1:
                        textBoxFirstName = textBox;
                        break;
                    case 2:
                        textBoxEmail = textBox;
                        break;
                }

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

                switch (i)
                {
                    case 0:
                        passwordBox1 = passBox;
                        break;
                    case 1:
                        passwordBox2 = passBox;
                        break;
                }

                grid.Children.Add(passBox);
                marginTop += HEIGHTTEXTBOX;
            }
            DatePicker datePickerBirstday = CreateEntities.CreateDatePicker
                (150, 40, HorizontalAlignment.Center, VerticalAlignment.Top, new Thickness(0, marginTop + HEIGHTTEXTBLOCK, 0, 0), styleDictionary);
            grid.Children.Add(datePickerBirstday);
            datePicker = datePickerBirstday;
        }
        private void ConnectionButtonClick()
        {
            string name = textBoxName.Text;
            string firstName = textBoxFirstName.Text;
            string email = textBoxEmail.Text;
            string password = passwordBox1.Password;
            string passwordCheck = passwordBox2.Password;
            DateTime? datePickercontain = datePicker.SelectedDate;
            DateTime birstday = datePicker.SelectedDate ?? DateTime.Today;

            if (Program.CheckSignIn(name, firstName, email, password, passwordCheck));
            {
                if (Sql.EmailTestInSql(connection, textBoxEmail.ToString()))
                {
                    User newUser = new User(name, firstName, email, birstday, birstday);
                    Sql.CreateUserInSql(connection, newUser, password);
                }
                else
                {
                    Program.ShowError("Erreur : Cette adresse Email est déjà utilisé. Veuillez réessayer avec une autre adresse ou vous connecter", 
                        "Error : Email already used ");
                }
            }
        }
    }
}
