using DvldBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DVLD
{
    public partial class ManagePeople : Form
    {
        public ManagePeople()
        {
            InitializeComponent();
            frmAdd_Update_Person.ButtonSaveClicked += GetAllPeople_EventHandler;
        }
        private void GetAllPeople_EventHandler(object sender, EventArgs e)
        {
            GetAllPeople();
        }
        public void GetAllPeople()
        {
            DVGPeopleManagemt.DataSource = clsPerson.GetAllPeopleForDataGridInPeopleManagemt();
        }
        private void ManagePeople_Load(object sender, EventArgs e)
        {
            GetAllPeople();
            txtSearch.Visible = false;
            CBFilterBy.SelectedIndex = 0;
            Records.Text = clsPerson.Records().ToString();
        }
        private void CBFilterBy_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string filterColumn;

            switch (CBFilterBy.Text)
            {
                case "PersonID": filterColumn = "PersonID"; break;
                case "National No": filterColumn = "NationalNo"; break;
                case "First Name": filterColumn = "FirstName"; break;
                case "Second Name": filterColumn = "SecondName"; break;
                case "Third Name": filterColumn = "ThirdName"; break;
                case "Last Name": filterColumn = "LastName"; break;
                case "Phone": filterColumn = "Phone"; break;
                case "Email": filterColumn = "Email"; break;
                case "NationalityCountryID": filterColumn = "NationalityCountryID"; break;
                case "Gender": filterColumn = "Gender"; break;
                default: filterColumn = null; break;  
            }

            if (filterColumn == null)
            {
                txtSearch.Visible = false;
                GetAllPeople();
                return;
            }

            txtSearch.Visible = true;
            DVGPeopleManagemt.DataSource = clsPerson.Filter(filterColumn, txtSearch.Text);
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            CBFilterBy_SelectionChangeCommitted(sender, e);
        }
        private void button2_Click(object sender, EventArgs e) 
        {

            var frm = new frmAdd_Update_Person();
                frm.ShowDialog(this);  
            
        }
        private void Delete_Click(object sender, EventArgs e)
        {
            if (DVGPeopleManagemt.SelectedRows.Count == 0)
                return;

            var row = DVGPeopleManagemt.SelectedRows[0];

            int id = Convert.ToInt32(row.Cells["PersonID"].Value);
 
                if (clsPerson.Delete(id))
                {
                    MessageBox.Show("Deleted Successfully");
                    DVGPeopleManagemt.Rows.Remove(row);
                }
                else
                {

                    MessageBox.Show("This Person Can not Be Deleted Cuz it Referenced in Another Table");
                }
        }
        private void DVGPeopleManagemt_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
           
        
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                DVGPeopleManagemt.ClearSelection();
                DVGPeopleManagemt.Rows[e.RowIndex].Selected = true;
                DVGPeopleManagemt.CurrentCell = DVGPeopleManagemt.Rows[e.RowIndex].Cells[0];
            }
        
        }
        private void AddNew_Click(object sender, EventArgs e)
        {
            using (var frm = new frmAdd_Update_Person())
                frm.ShowDialog();

            GetAllPeople();
        }
        private void Update_Click(object sender, EventArgs e)
        {
            if (DVGPeopleManagemt.SelectedRows.Count == 0) return;

            int id = (int)DVGPeopleManagemt.SelectedRows[0].Cells["PersonID"].Value;

            using (var frm = new frmAdd_Update_Person(id))
                frm.ShowDialog();

            GetAllPeople();
        }

       
    }
}
