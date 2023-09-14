using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Money_Management
{
    public partial class NewUserForm : Form
    {
        public NewUserForm()
        {
            InitializeComponent();
            Style.SetStyleForm(this);
            Style.Shapes(null, this);
        }
    }
}
