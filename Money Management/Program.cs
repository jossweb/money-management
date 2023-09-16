using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
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
            /// <summary>
            /// Create home page panel (setting)
            /// </summary>
            /// <param name="u">user number found in the database to activate or note Scroll</param>
            /// <returns>Home Page Panel</returns>
            
            Panel homePanel = new Panel();
            

            homePanel.Size = new Size(600, 500);/*régler taille !!!!*/
            homePanel.Location = new Point(0, 0);
            homePanel.BackColor = Color.WhiteSmoke;

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
        public static void UserButtons(Panel panel, EventHandler personnesButton_Click, List<User> users)
        {
            /// <summary>
            /// Create one button for each user detect in json data base
            /// </summary>
            /// <param name="panel">Home panel</param>
            /// <param name="personnesButton_Click">EventHandler of click on user button</param>
            /// <param name="users">All user recovered in json</param>

            int interval = 100;

            // initialize users list if it's null
            if (users == null)
            {
                users = new List<User>();

            }

            if (users.Count > 0)
            {
                //create 1 button for each users
                foreach (var user in users)
                {
                var buttonPersonne = new Button();
                buttonPersonne.Text = user.name.ToUpper() + " " + user.firstName;
                Style.UserButtonStyle(buttonPersonne, new Size(350, 60), new Point(135, interval), Color.AntiqueWhite); 
                buttonPersonne.Click += new EventHandler(personnesButton_Click);
                panel.Controls.Add(buttonPersonne);
                interval += 90;
                }
            }
            else
            {
                //Warning Message print if there are not user in app data
                var warningLabel = CreateLabel("Aucun utilisateurs trouvé", new Size(205, 40), new Point(150, 20));
                panel.Controls.Add(warningLabel);
            }
        }
        public static void AddShapes(string path, Size size, Point point, Panel panel = null, Form form = null)
        {
            ///<summary>
            /// add shape for home decoration
            /// </summary>
            /// <param name="panel">Home panel</param>
            /// <param name="path">File path of image</param>
            /// <param name="size">size</param>
            /// <param name="point">Location of image on form</param>
            /// <return>1 decoration for background (picture box)</return>

            PictureBox pictureBox1 = new PictureBox();
            pictureBox1.Image = Image.FromFile(path);
            pictureBox1.Size = size;
            pictureBox1.Location = point;
            pictureBox1.SendToBack();
            if (form == null)
            {
                panel.Controls.Add(pictureBox1);
            }
            else
            {
                form.Controls.Add(pictureBox1);
            }
        }
        public static Label CreateLabel(string text, Size size, Point point)
        {
            ///<summary>
            /// Create new label
            /// </summary>
            /// <param name="text">label's text</param>
            /// <param name="size">size</param>
            /// <param name="point">location</param>
            /// <return>label</return>
            var label = new Label();
            label.Text = text;
            label.Size = size;
            label.Location = point;
            label.Font = new Font(label.Font.FontFamily, 22, FontStyle.Italic);
            label.BackColor = Color.Transparent;
            label.TextAlign = ContentAlignment.MiddleCenter;
            return label;
        }
        public static Button AddButton()
        {
            ///<summary>
            /// Create add button
            /// </summary>
            /// <return>add button</return>
            var addbutton = new Button();
            addbutton.Text = ("+");
            addbutton.TextAlign = ContentAlignment.MiddleCenter;
            addbutton.Font = new(addbutton.Font.FontFamily, 15, FontStyle.Bold);
            addbutton.Size = new Size(50, 50);
            addbutton.Location = new Point(440, 10);
            addbutton.BringToFront();
            addbutton.BackColor = Color.PaleVioletRed;
            return addbutton;
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
        public static void UserButtonStyle(Button button, Size size, Point point, Color color)
        {
            button.Size = size;
            button.Location = point;
            button.BringToFront();
            button.BackColor = color;
        }
        public static void Shapes(Panel panel = null, Form form = null)
        {
            if ((form == null)||(panel == null))
            {
                var shapesList = new List<ImageBack>
                {
                    new ImageBack("backgroundAbstractShapes/Shapes1.png", new Size(200, 200), new Point(0, 250)),
                    new ImageBack("backgroundAbstractShapes/Shapes2.png", new Size(200, 200), new Point(0, 0)),
                    new ImageBack("backgroundAbstractShapes/Shapes3.png", new Size(250, 250), new Point(300, 200)),
                    new ImageBack("backgroundAbstractShapes/Shapes4.png", new Size(200, 200), new Point(550, 620)),
                    new ImageBack("backgroundAbstractShapes/Shapes5.png", new Size(400, 300), new Point(0, 500))
                };
                foreach (var shape in shapesList)
                {
                    if (panel != null)
                    {
                        CreateEntities.AddShapes(shape.path, shape.size, shape.point, panel);
                    }
                    else
                    {
                        CreateEntities.AddShapes(shape.path, shape.size, shape.point, null, form);
                    }
                    
                }
            }
            else
            {
                Console.WriteLine("Error ! Panel and form was null. App can't show background Shapes");
            }
        }
    }
    public class ImageBack
    {
        /// <summary>
        /// create Image background object
        /// </summary>
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
        /// <summary>
        /// Create user object
        /// </summary>
        public int id;
        public string name;
        public string firstName;
        public string keypass;
        public User(int id, string name, string firstName, string keypass)
        {
            this.id = id;
            this.name = name;
            this.firstName = firstName;
            this.keypass = keypass;
        }
    }
}
