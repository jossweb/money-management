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
        public static Panel HomePanel(int u)
        {
            Panel homePanel = new Panel();
            homePanel.Size = new Size(500, 600);
            homePanel.Location = new Point(0, 0);

            homePanel.HorizontalScroll.Visible = false;
            homePanel.VerticalScroll.Visible = false;
            if (u > 5)
            {
                homePanel.AutoScroll = true;
            }
            else
            {
                homePanel.AutoScroll = false;
            }

            return homePanel;
        }
        public static void UserButtons(Panel panel, EventHandler personnesButton_Click, List<string> users)
        {

            int interval = 100;

            if (users.Count > 0)
            {
                foreach (var user in users)
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
        public static void AddShapes(Panel panel, string path, Size size, Point point)
        {
            PictureBox pictureBox1 = new PictureBox();
            pictureBox1.Image = Image.FromFile(path);
            pictureBox1.Size = size;
            pictureBox1.Location = point;
            pictureBox1.SendToBack();
            panel.Controls.Add(pictureBox1);


        }
        public static Label CreateLabel(string text, Size size, Point point)
        {
            var label = new Label();
            label.Text = text;
            label.Size = size;
            label.Location = point;
            label.Font = new Font(label.Font.FontFamily, 22, FontStyle.Italic);
            label.BackColor = Color.Transparent;
            label.TextAlign = ContentAlignment.MiddleCenter;
            return label;
        }
    }
    static class Style
    {
        public static void SetStyleForm(Form form)
        {
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Text = "Money Management";
            form.ClientSize = new Size(600, 500);
        }
        public static void UserButtonStyle(Button button, int interval)
        {
            button.Size = new Size(350, 60);
            button.Location = new Point(75, interval);
            button.BringToFront();
            button.BackColor = Color.Gray;
        }
        public static void Shapes(Panel panel)
        {
            var shapesList = new List<ImageBack>
            {
                new ImageBack("backgroundAbstractShapes/Shapes1.png", new Size(200, 200), new Point(0, 150)),
                new ImageBack("backgroundAbstractShapes/Shapes2.png", new Size(200, 200), new Point(300, 0)),
                new ImageBack("backgroundAbstractShapes/Shapes3.png", new Size(250, 250), new Point(250, 200)),
                new ImageBack("backgroundAbstractShapes/Shapes4.png", new Size(200, 200), new Point(400, 500)),
                new ImageBack("backgroundAbstractShapes/Shapes5.png", new Size(400, 300), new Point(0, 400))
            };
            foreach (var shape in shapesList)
            {
                CreateEntities.AddShapes(panel, shape.path, shape.size, shape.point);
            }
        }
    }
    public class ImageBack
    {
        public string path;
        public Size size;
        public Point point;

        public ImageBack(string path, Size size, Point point)
        {
            this.path = path;
            this.size = size;
            this.point = point;
        }
    }
    public class User
    {
        public int id;
        public string name;
        public string keypass;
        public User(int id, string name, string keypass)
        {
            this.id = id;
            this.name = name;
            this.keypass = keypass;
        }
    }
}
