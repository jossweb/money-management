﻿using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
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
            grille.Height = 900;
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

            var rectangle = new Rectangle();
            rectangle.Width = 1200;
            rectangle.Height = 250;
            rectangle.HorizontalAlignment = HorizontalAlignment.Center;
            rectangle.VerticalAlignment = VerticalAlignment.Top;
            rectangle.Stroke = Brushes.Black;
            rectangle.StrokeThickness = 3;
            rectangle.Margin = new Thickness(0, 110, 0, 0);
            rectangle.RadiusX = 10;
            rectangle.RadiusY = 10;
            grille.Children.Add(rectangle);

            var nombreDeDivisions = 3;
            var largeurDivision = rectangle.Width / nombreDeDivisions;

            var posY = rectangle.Margin.Top;

            for (int i = 1; i < nombreDeDivisions; i++)
            {
                var ligne = new Line();
                ligne.Stroke = Brushes.Black;
                ligne.StrokeThickness = 3;
                ligne.X1 = i * largeurDivision + (grille.Width - rectangle.Width) / 2;
                ligne.X2 = i * largeurDivision + (grille.Width - rectangle.Width) / 2;
                ligne.Y1 = posY;
                ligne.Y2 = posY + rectangle.Height;
                grille.Children.Add(ligne);
            }

            Content = grille;


        }
    }
}