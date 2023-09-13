using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;

namespace Money_Management
{
    public partial class Form1 : Form
    {
        private TextBox textDisplay;
        public List<User> usersData = json.DeserialiseJson(json.GetJsonFromFile());
        public Form1()
        {
            InitializeComponent();
            var users = new List<string> { "Jossua", "Dorian", "Jossua", "Dorian", "Jossua" };
            Style.SetStyleForm(this);
            var homePanel = CreateEntities.HomePanel(users.Count);
            this.Controls.Add(homePanel);
            var logLabel = CreateEntities.CreateLabel("Connection", new Size(205, 40), new Point(150, 20));
            homePanel.Controls.Add(logLabel);
            CreateEntities.UserButtons(homePanel, personnesButton_Click, usersData);
            //CreateEntities.UserButtons(homePanel, addButton_Click);
            Style.Shapes(homePanel);
            //button +
            var addbutton = CreateEntities.AddButton();
            addbutton.Click += new EventHandler(addButton_Click);
            homePanel.Controls.Add(addbutton);

        }

        private void personnesButton_Click(object sender, EventArgs e)
        {
            
        }
        public void addButton_Click(object sender, EventArgs e)
        {
            User user = new User(1, "Jossua", "pass");
            /*if (usersData.Count != null)
            {
                
                usersData.Add(user);
                json.SetJsonFromFile(usersData);
            }
            else
            {*/
                var test = new List<User> { user };
                json.SetJsonFromFile(test);
            //}
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }
    }
}
