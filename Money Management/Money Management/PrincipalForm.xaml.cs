using LiveCharts;
using LiveCharts.Dtos;
using LiveCharts.Wpf;
using Microsoft.IdentityModel.Tokens;
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
            grille.Width = 1700;
            grille.HorizontalAlignment = HorizontalAlignment.Right;
            grille.VerticalAlignment = VerticalAlignment.Center;


            //grille.Children.Add(graphics.CartesianGraphique(valeurs, étiquettes));

            

            Label label = new Label();
            label.Content = userConnected.name + " " + userConnected.firstName;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Top;
            label.Margin = new Thickness(0, 20, 0, 0);
            label.FontSize = 50;
            grille.Children.Add(label);

            int marginTopGraphic = 100;
            var graphic = graphics.CreateCartesianChart(valeurs, étiquettes, marginTopGraphic);
            grille.Children.Add(graphic);
            grille.Children.Add(graphics.CreateCartesianChart(valeurs, étiquettes, Convert.ToInt32(graphic.Height) + marginTopGraphic + 10)); // + 10 == little space between two graphics
            Rectangle rectangle = Program.CreateRectangle();

            grille.Children.Add(rectangle);

            var nombreDeDivisions = 3;

            var posY = rectangle.Margin.Top;

            for (int i = 1; i < nombreDeDivisions; i++)
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

            Content = grille;


        }
    }
}