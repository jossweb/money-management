using LiveCharts;
using LiveCharts.Dtos;
using LiveCharts.Wpf;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using Rectangle = System.Windows.Shapes.Rectangle;
using Brushes = System.Windows.Media.Brushes;

namespace Money_Management
{
    /// <summary>
    /// Logique d'interaction pour PrincipalForm.xaml
    /// </summary>
    public partial class PrincipalForm : Window
    {
        public MySqlConnection connection;
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);
        public const int SM_CXSCREEN = 0;
        public PrincipalForm(User userConnected, MySqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            int largeurEcran = GetSystemMetrics(SM_CXSCREEN);
            //int hauteurEcran = GetSystemMetrics(SM_CYSCREEN);

            var lastMonthExpense = json.DeserialiseJson<Dictionary<DateTime, string>>
                (Sql.GetMoneyTransfereInBdd(connection, "money_transfer_last_month", userConnected.id));

            var grille = new Grid();
            if (largeurEcran > 2000)
            {
                this.Height = 800;
                this.Width = 1500;

                grille.Height = 800;
                grille.Width = 1500;
            }
            else
            {
                this.Height = 700;
                this.Width = 1200;
                grille.Height = 700;
                grille.Width = 1200;
            }
            grille.HorizontalAlignment = HorizontalAlignment.Right;
            grille.VerticalAlignment = VerticalAlignment.Center;

            AddEntitiesOnWindow(grille, Convert.ToInt32(this.Height), new ChartValues<double> { 1000, 1200, 1150, 1700, 1400, 1000, 1200, 1150, 400, 500, 500, 1000, 1200, 1150,
                1700, 1400, 1000, 1200, 1150, 400, -400, 500, 1000, 1200, 1150, 1700, 1606, 1000, 1200, 1150, 400, -400, 500, }, 
                new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17",
                "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30" });

            
            var rectangleValues = new List<string> { Sql.GetAccountFunds(userConnected.id, connection) + " €", 150 + ",00 €", "test" };
            grille.Children.Add(CreateEntities.CreateLabel(userConnected.name + " " + userConnected.firstName, 50, new Thickness(0, 20, 0, 0), HorizontalAlignment.Center, VerticalAlignment.Top));
            
            //grid over rectangle
            var secondGrid = CreateEntities.SetSettingsGrid(300, Convert.ToInt32(this.Height * 0.80),
                new Thickness(70, Convert.ToInt32(this.Height * 0.15), 0, 0), HorizontalAlignment.Left , VerticalAlignment.Top);
            InsertRectangleContent(secondGrid, connection, userConnected, rectangleValues, Add_Expense);
            grille.Children.Add(secondGrid);
            Content = grille;
        }
        private void Add_Expense(object sender, RoutedEventArgs e)
        {
            var newExpenseWindow = new AddExpense(connection);
            newExpenseWindow.Show();
        }
        private static void InsertRectangleContent(Grid grille, MySqlConnection connection, User userConnected, List<string> rectangleValues, RoutedEventHandler Button_Click)
        {
            Label moneyTitle = new Label();
            moneyTitle.Content = "Montant disponible sur votre compte :";
            moneyTitle.HorizontalAlignment = HorizontalAlignment.Center;
            moneyTitle.VerticalAlignment = VerticalAlignment.Top;
            moneyTitle.Margin = new Thickness(0, 0, 0, 0);
            moneyTitle.FontSize = 15;
            grille.Children.Add(moneyTitle);
            var color = new List<SolidColorBrush> { new SolidColorBrush(Colors.Black), new SolidColorBrush(Colors.Red), new SolidColorBrush(Colors.Green) };
            for (int i = 0; i < 3; i++)
            {
                Program.CreateRectangleValueLabel(rectangleValues[i], grille, color[i], 70 + (190 * i));
            }

            Button addExpense = new Button();
            addExpense.Content = "Ajouter une dépense";
            addExpense.Width = 120;
            addExpense.Height = 40;
            addExpense.Margin = new Thickness(0, 350, 0, 0);
            addExpense.HorizontalAlignment = HorizontalAlignment.Center;
            addExpense.VerticalAlignment = VerticalAlignment.Top;
            addExpense.Click += Button_Click;
            grille.Children.Add(addExpense);
        }
        private static void AddEntitiesOnWindow(Grid grille, int heightScreen, ChartValues<double> value, List<string> label)
        {
            int marginTopGraphic = 100;
            var graphic = Graphics.CreateCartesianChart(value, label, marginTopGraphic);
            grille.Children.Add(graphic);
            grille.Children.Add(Graphics.CreateCartesianChart(value, label, Convert.ToInt32(graphic.Height) + marginTopGraphic + 10)); // + 10 == space between two graphics
            Rectangle rectangle = CreateEntities.CreateRectangle(heightScreen);
            grille.Children.Add(rectangle);

            var posY = rectangle.Margin.Top;
            for (int i = 1; i < 3; i++)
            {
                var ligne = new Line();
                ligne.Stroke = Brushes.Gray;
                ligne.StrokeThickness = 3;
                ligne.X1 = rectangle.Margin.Left;
                ligne.X2 = rectangle.Margin.Left + rectangle.Width;
                ligne.Y1 = posY + (rectangle.Height / 3) * i;
                ligne.Y2 = posY + (rectangle.Height / 3) * i;
                grille.Children.Add(ligne);
            }
        }
    }
}