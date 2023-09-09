using System.Drawing.Drawing2D;
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
            this.Controls.Add(homePanel);
            //

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }
    }
}