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
using static System.Net.WebRequestMethods;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Google.Protobuf.WellKnownTypes;
using System.Drawing;

namespace Money_Management
{
    /// <summary>
    /// Logique d'interaction pour PrincipalForm.xaml
    /// </summary>
    public partial class PrincipalForm : Window
    {
        MySqlConnection connection;
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);
        public const int SM_CXSCREEN = 0;
        //public const int SM_CYSCREEN = 1;
        public PrincipalForm(User userConnected, MySqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            int largeurEcran = GetSystemMetrics(SM_CXSCREEN);
            //int hauteurEcran = GetSystemMetrics(SM_CYSCREEN);

            //var lastMonthExpense = json.DeserialiseJson<Dictionary<DateTime, string>>
            //(Sql.GetMoneyTransfereInBdd(connection, "money_transfer_last_month", userConnected.id));

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

            Program.AddEntitiesOnWindow(grille, Convert.ToInt32(this.Height), new ChartValues<double> { 1000, 1200, 1150, 1700, 1400, 1000, 1200, 1150, 400, 500, 500, 1000, 1200, 1150,
                1700, 1400, 1000, 1200, 1150, 400, -400, 500, 1000, 1200, 1150, 1700, 1606, 1000, 1200, 1150, 400, -400, 500, }, 
                new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17",
                "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30" });

            
            var rectangleValues = new List<string> { Sql.GetAccountFunds(userConnected.id, connection) + " €", 150 + ",00 €", "test" };
            Program.CreateWindowNameTitle(grille, userConnected);
            
            var secondGrid = new Grid(); //This grid is over the rectangle
            Program.SetSettingsGrid(secondGrid, 300, 600, new Thickness(70, 110, 0, 0));
            Program.InsertRectangleContent(secondGrid, connection, userConnected, rectangleValues);
            grille.Children.Add(secondGrid);
            Content = grille;
        }
    }
}