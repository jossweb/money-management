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
            var valeurs = new ChartValues<double> { 3, 6, 8, 12, 7 };
            var étiquettes = new List<string> { "A", "B", "C", "D", "E" };

            // Création du graphique
            var graphique = new CartesianChart();

            var axeX = new AxesCollection();
            axeX.Add(new Axis
            {
                Title = "Étiquettes",
                Labels = étiquettes
            });

            graphique.AxisX = axeX;

            graphique.Series = new SeriesCollection
            {
                new ColumnSeries // Utilisation de ColumnSeries pour un graphique à barres verticales
                {
                    Title = "Valeurs",
                    Values = valeurs,
                    DataLabels = true,
                    Fill = new SolidColorBrush(Colors.Red) // Changement de la couleur des barres
                }
            };

            // Création d'une grille pour centrer le graphique
            var grille = new Grid();
            grille.HorizontalAlignment = HorizontalAlignment.Center;
            grille.VerticalAlignment = VerticalAlignment.Center;

            // Ajout du graphique à la grille
            grille.Children.Add(graphique);

            // Ajout de la grille à la fenêtre
            Content = grille;

            // Définir la taille du graphique
            graphique.Width = 400; // Ajustez la taille selon vos besoins
            graphique.Height = 300; // Ajustez la taille selon vos besoins
        }
    }
}