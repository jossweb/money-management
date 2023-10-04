using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Money_Management
{
    /// <summary>
    /// Logique d'interaction pour PrincipalForm.xaml
    /// </summary>
    public partial class PrincipalForm : Window
    {
        public PrincipalForm()
        {
            InitializeComponent();
            var valeurs = new ChartValues<double> { 1000, 1200, 1150, 1700, 1400, 1000, 1200, 1150, 1700, 1400 };
            var étiquettes = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

            var grille = new Grid();
            grille.Height = 800;
            grille.Width = 1500;
            grille.HorizontalAlignment = HorizontalAlignment.Center;
            grille.VerticalAlignment = VerticalAlignment.Center;


            grille.Children.Add(graphics.CartesianGraphique(valeurs, étiquettes));

            Content = grille;

            Label label = new Label();
            label.Content = "Hello World";
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Top;
            grille.Children.Add(label);



            var données = new ChartValues<int> { 80 };


            // Création du graphique circulaire
            var graphique = new PieChart
            {
                Series = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "Catégorie A",
                        Values = données,
                        DataLabels = true,
                        Fill = new SolidColorBrush(Colors.Red)
                    },
                    new PieSeries
                    {
                        Title = "Catégorie B",
                        Values = données,
                        DataLabels = true,
                        Fill = new SolidColorBrush(Colors.Blue)
                    },
                    new PieSeries
                    {
                        Title = "Catégorie C",
                        Values = données,
                        DataLabels = true,
                        Fill = new SolidColorBrush(Colors.Green)
                    }
                },

            };
            graphique.Width = 400;
            graphique.Height = 300;

            //Content = graphique;


            grille.Children.Add(graphique);


        }
    }
}