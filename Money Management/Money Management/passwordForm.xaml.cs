﻿using Azure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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
    /// Logique d'interaction pour passwordForm.xaml
    /// </summary>
    public partial class passwordForm : Window
    {
        private MySqlConnection connection = new MySqlConnection("database=money management; server=localhost; user id=root;");
        private int tag;
        public passwordForm(int TagUserSelect, List<User> users)
        {
            InitializeComponent();
            tag = TagUserSelect;
            User userSelect = User.CheckById(TagUserSelect, users);
            welcomeLabel.Content = "Bienvenue " + userSelect.name + " " + userSelect.firstName;
            Debug.WriteLine("info : user select : " + userSelect.mail);

            passbox.KeyUp += Enter_keyUp;
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string pass = passbox.Password;
            string query = "SELECT * FROM users WHERE ID = '" + tag + "'";
            if (Program.CheckUser(query, pass, connection))
            {
                Debug.WriteLine("info : Password is valid");
                PrincipalForm principalForm = new PrincipalForm();
                principalForm.Show();
                this.Close();
            }
            else
            {
                Debug.WriteLine("info : Password is not valid");
                Thread.Sleep(1500);
                MessageBox.Show("Mot de passe faux");
            }
        }
        private void Enter_keyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Debug.WriteLine("Enter touch was activate");
                Login_Click(sender, e);
            }
        }
    }
}
