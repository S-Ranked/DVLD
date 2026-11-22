using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Subscribe to the custom control's event
            UserControl1.ButtonClicked += MyCustomControl_ButtonClicked;
        }

        private void MyCustomControl_ButtonClicked(object sender, EventArgs e)
        {
            MessageBox.Show("The button inside the control was clicked!");
        }
    }
}
