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
            var users = new List<string> { "Jossua", "Dorian", "Jossua", "Dorian", "Jossua" };
            Style.SetStyleForm(this);
            var homePanel = CreateEntities.HomePanel(users.Count);
            this.Controls.Add(homePanel);
            var logLabel = CreateEntities.CreateLabel("Connection", new Size(205, 40), new Point(150, 20));
            homePanel.Controls.Add(logLabel);
            CreateEntities.UserButtons(homePanel, personnesButton_Click, users);

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
}
