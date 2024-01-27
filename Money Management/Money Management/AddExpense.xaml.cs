using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Money_Management
{
    /// <summary>
    /// Logique d'interaction pour AddExpense.xaml
    /// </summary>
    public partial class AddExpense : Window
    {
        public AddExpense(MySqlConnection connection)
        {
            InitializeComponent();

            Grid grid = CreateEntities.SetSettingsGrid(Convert.ToInt32(this.Width), Convert.ToInt32(this.Height),
                new Thickness(0, 0, 0, 0), HorizontalAlignment.Center, VerticalAlignment.Center);
            this.Content = grid;
            Program.AddEllipses(2, grid);
            AddContent(grid);
        }
        private static void AddContent(Grid grid)
        {
            ResourceDictionary styleDictionary = new ResourceDictionary();
            styleDictionary.Source = new Uri("Style.xaml", UriKind.RelativeOrAbsolute);
            Label title = CreateEntities.CreateLabel("Ajouter une dépense", 35, new Thickness(0, 15, 0, 0),
                HorizontalAlignment.Center, VerticalAlignment.Top);
            grid.Children.Add(title);
            var datePicker = CreateEntities.CreateDatePicker(160, 40, HorizontalAlignment.Center, VerticalAlignment.Center, new Thickness(0, 0, 0, 0), styleDictionary);
            grid.Children.Add(datePicker);
        }
    }
}