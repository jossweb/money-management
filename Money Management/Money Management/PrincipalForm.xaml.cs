using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static System.Net.WebRequestMethods;

namespace Money_Management
{
    /// <summary>
    /// Logique d'interaction pour PrincipalForm.xaml
    /// </summary>
    public partial class PrincipalForm : Window
    {
        public PrincipalForm(User userConnected)
        {
            InitializeComponent();
            var valeurs = new ChartValues<double> { 1000, 1200, 1150, 1700, 1400, 1000, 1200, 1150, 400, -400, 500, 1000, 1200, 1150, 
                1700, 1400, 1000, 1200, 1150, 400, -400, 500, 1000, 1200, 1150, 1700, 1606, 1000, 1200, 1150, 400, -400, 500, };
            var étiquettes = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17",
                "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30" };

            var grille = new Grid();
            grille.Height = 800;
            grille.Width = 1500;
            grille.HorizontalAlignment = HorizontalAlignment.Center;
            grille.VerticalAlignment = VerticalAlignment.Center;


            //grille.Children.Add(graphics.CartesianGraphique(valeurs, étiquettes));

            

            Label label = new Label();
            label.Content = userConnected.name + " " + userConnected.firstName;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Top;
            label.FontSize = 50;
            grille.Children.Add(label);

            grille.Children.Add(graphics.CreateCartesianChart(valeurs, étiquettes));

            //grille.Children.Add(graphics.CreatePieChart(new ChartValues<int> { 80 }));

            Content = grille;


        }
    }
}