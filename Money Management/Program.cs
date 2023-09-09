using Microsoft.VisualBasic;
using System.Windows.Forms;

namespace Money_Management
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
    static class CreateEntities
    {
        public static Panel HomePanel()
        {
            Panel homePanel = new Panel();
            homePanel.Size = new Size(500, 600);
            homePanel.Location = new Point(0, 0);
            return homePanel;
        }
        public static void UserButtons(Panel panel, EventHandler personnesButton_Click)
        {
            var users = new List<string> {"Jossua", "Dorian" };
            int interval = 100;

            if (users.Count > 0 )
            {
                foreach(var user in users)
                {
                    var buttonPersonne = new Button();
                    buttonPersonne.Text = (user); 
                    Style.UserButtonStyle(buttonPersonne, interval);
                    buttonPersonne.Click += new EventHandler(personnesButton_Click);
                    panel.Controls.Add(buttonPersonne);
                    interval += 90;
                }
            }
            else
            {

            }
        }
    }
    static class Style
    {
        public static void SetStyleForm(Form form)
        {
            form.BackColor = Color.White;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Text = "Money Management";
            form.ClientSize = new Size(600, 500);
        }
        public static void UserButtonStyle(Button button, int interval)
        {
            button.Size = new Size(350, 60);
            button.Location = new Point(75, interval);
            button.BackColor = Color.Gray;
        }
    }
}