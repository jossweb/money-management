using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
        private void AddContent(Grid grid)
        {
            ResourceDictionary styleDictionary = new ResourceDictionary();
            styleDictionary.Source = new Uri("Style.xaml", UriKind.RelativeOrAbsolute);
            Label title = CreateEntities.CreateLabel("Ajouter une dépense", 35, new Thickness(0, 15, 0, 0),
                HorizontalAlignment.Center, VerticalAlignment.Top);

            //create entities of page
            var textBlockNameExpense = CreateEntities.CreateTextBlock("Nom de la dépense", Brushes.Black, 220, 20, HorizontalAlignment.Center, VerticalAlignment.Top, new Thickness(-40,80,0,0));
            var textBoxNameExpense = CreateEntities.CreateTextBox("NameExpense", 220, 35, 25, HorizontalAlignment.Center, VerticalAlignment.Top, new Thickness(0, 100, 0, 0), styleDictionary);
            var textBoxValueExpense = CreateEntities.CreateTextBox("ValueExpense", 220, 35, 25, HorizontalAlignment.Center, VerticalAlignment.Top, new Thickness(0, 200, 0, 0), styleDictionary);
            var datePicker = CreateEntities.CreateDatePicker(160, 40, HorizontalAlignment.Center, VerticalAlignment.Center, new Thickness(0, 0, 0, 0), styleDictionary);

            textBoxValueExpense.PreviewTextInput += TextBoxNumbers_PreviewTextInput;

            //add entities on grid
            grid.Children.Add(title);
            grid.Children.Add(textBoxNameExpense);
            grid.Children.Add(textBlockNameExpense);
            grid.Children.Add(textBoxValueExpense);
            grid.Children.Add(datePicker);
        }
        private void TextBoxNumbers_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c) && c != '.' && c != ',')
                {
                    e.Handled = true;
                }
            }
        }
    }
}