using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;

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

            var axeY = new AxesCollection();
            axeY.Add(new Axis
            {
                Title = "Étiquettes",
                Labels = étiquettes
            });

            graphique.AxisY = axeY;

            graphique.Series = new SeriesCollection
            {
                new RowSeries
                {
                    Title = "Valeurs",
                    Values = valeurs,
                    DataLabels = true
                }
            };

            // Ajout du graphique à la fenêtre
            Content = graphique;
        }
    }
}
