using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DvldBusinessLayer;
namespace DVLD
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        
        
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (clsPerson.Login(txtUserName.Text, txtPassword.Text) != -1)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("User Name Or Password are Incorect");
            }
        }
    }
}
