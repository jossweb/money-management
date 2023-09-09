using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Money_Management
{
    public partial class Form1 : Form
    {
        private TextBox textDisplay;

        public Form1()
        {
            InitializeComponent();
            Style.SetStyleForm(this);
            var homePanel = CreateEntities.HomePanel();
            this.Controls.Add(homePanel); // Ajoutez le panel principal au formulaire

            // Créez d'abord les boutons utilisateur
            CreateEntities.UserButtons(homePanel, personnesButton_Click);

            Style.Shapes(homePanel);
        }

        private void personnesButton_Click(object sender, EventArgs e)
        {
            // Code pour gérer le clic sur les boutons utilisateur
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }
    }

    // ... Votre code pour Application, CreateEntities et Style reste inchangé ...
}
