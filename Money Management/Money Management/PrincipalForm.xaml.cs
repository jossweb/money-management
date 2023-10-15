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
        public const int SM_CYSCREEN = 1;
        public PrincipalForm(User userConnected, MySqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            int largeurEcran = GetSystemMetrics(SM_CXSCREEN);
            //int hauteurEcran = GetSystemMetrics(SM_CYSCREEN);

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

            Program.AddGraphicsOnWindow(grille, new ChartValues<double> { 1000, 1200, 1150, 1700, 1400, 1000, 1200, 1150, 400, -400, 500, 1000, 1200, 1150,
                1700, 1400, 1000, 1200, 1150, 400, -400, 500, 1000, 1200, 1150, 1700, 1606, 1000, 1200, 1150, 400, -400, 500, }, 
                new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17",
                "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30" });

            Program.CreateWindowNameTitle(grille, userConnected);
            Content = grille;
            Program.InsertLabel(grille, connection, userConnected);
        }
    }
}