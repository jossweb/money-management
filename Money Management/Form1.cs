using System.Drawing.Drawing2D;

namespace Money_Management
{
    public partial class Form1 : Form
    {

        private TextBox textDisplay;
        public Form1()
        {
            InitializeComponent();
            Style.SetStyleForm(this);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }
    }
}