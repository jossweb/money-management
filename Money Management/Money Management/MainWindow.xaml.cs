﻿using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Money_Management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            var userList = json.GetJsonFromFile();
            DataContext = this;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection("database=money management; server=localhost; user id=root;");

            try
            {
                connection.Open();
                MessageBox.Show("Connecter avec succès");

                string query = "SELECT name FROM users WHERE id = 1";
                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string nom = reader.GetString("name");
                        MessageBox.Show("Nom de l'ID 1 : " + nom);
                    }
                    else
                    {
                        Console.WriteLine("Aucun enregistrement trouvé avec l'ID 1");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Erreur !");
            }
        }
    }
}
