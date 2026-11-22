using DVLD.Properties;
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
using DVLD;
using System.IO;
namespace DVLD
{
    public partial class frmAdd_Update_Person : Form
    {
        private string _NewPath;
        private string _PeopleImagesPath = "C:\\DVLD-People-Image";
        private clsPerson _person;
        public frmAdd_Update_Person()
        {
            InitializeComponent();
            _person = new clsPerson();
        }
        public frmAdd_Update_Person(int personID)
        {
            InitializeComponent();
            _person = clsPerson.FindPersonByID(personID);
            textNational.Text = _person.NationalNo;
            textFirst.Text = _person.FirstName;
            textSecond.Text = _person.SecondName;
            textThird.Text = _person.ThirdName;
            textLast.Text = _person.LastName;
            DateOfBirth.Value = _person.DateOfBirth;
            if (_person.Gender == 0)
                RbtnMale.Checked = true;
            else
                RbtnFemale.Checked = true;
            txtAddress.Text = _person.Address;
            textPhone.Text = _person.Phone;
            textEmail.Text = _person.Email;
             pictureBox1.ImageLocation=_person.ImagePath;


        }

        public static event EventHandler ButtonSaveClicked;
        private void btnSave_Click(object sender, EventArgs e)
        {
            _person.NationalNo = textNational.Text;
            _person.FirstName = textFirst.Text;
            _person.SecondName = textSecond.Text;
            _person.ThirdName = textThird.Text;
            _person.LastName = textLast.Text;
            _person.DateOfBirth = DateOfBirth.Value;
            if (RbtnMale.Checked)
                _person.Gender = 0;
            else
                _person.Gender = 1;
            _person.Address = txtAddress.Text;
            _person.Phone = textPhone.Text;
            _person.Email = textEmail.Text;
            _person.NationalityCountryID = (int)cbCountry.SelectedValue;
            _person.ImagePath = _NewPath;
            if (_person.Save())
               
            MessageBox.Show($"ImagePath being saved: '{_person.ImagePath}'");

            else
                MessageBox.Show("National number already exists.");

            ButtonSaveClicked?.Invoke(this, EventArgs.Empty);
        }
        private void AddEditPerson_Load(object sender, EventArgs e)
        {
            cbCountry.DataSource = clsPerson.GetAllCountries();
            cbCountry.DisplayMember = "CountryName";
            cbCountry.ValueMember = "CountryID";
            if (_person.PersonID == -1)                   
                cbCountry.SelectedValue = clsPerson.GetDefaultCountry("Jordan"); 
            else                                          
                cbCountry.SelectedValue = _person.NationalityCountryID;
            lblRemove.Visible = false;
            DateOfBirth.MaxDate = DateTime.Today.AddYears(-18);
            DateOfBirth.MinDate = DateTime.Today.AddYears(-80);
        }
        private void RbtnMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pictureBox1 == null)
            {
                pictureBox1.Image = RbtnMale.Checked ? Properties.Resources.Male : Properties.Resources.Female;
            }
        }
        private void RbtnFemale_CheckedChanged(object sender, EventArgs e)
        {
           
            if (RbtnFemale.Checked)
                _person.Gender = 1;  
            else
                _person.Gender = 0;  

            pictureBox1.Image = RbtnMale.Checked ? Properties.Resources.Male : Properties.Resources.Female;
        }
        private void lblSetImage_LinkClicked(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Set Image";
            openFileDialog1.Filter = "Image Files (*.png;*.jpg;*.gif;*.bmp)|*.png;*.jpg;*.gif;*.bmp|All Files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                Directory.CreateDirectory(_PeopleImagesPath);
                string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(openFileDialog1.FileName);
                string newPath = Path.Combine(_PeopleImagesPath, newFileName);
                File.Copy(openFileDialog1.FileName, Path.Combine(_PeopleImagesPath, newFileName));
                pictureBox1.Image?.Dispose();
                pictureBox1.Image = Image.FromFile(newPath);
                _NewPath = newPath;
                lblRemove.Visible = true;
            }
        }
        private void lblRemove_LinkClicked(object sender, EventArgs e)
        {
            pictureBox1?.Dispose();
            pictureBox1 = null;


            if (RbtnMale.Checked)
                _person.Gender = 0;
            else
                _person.Gender = 1;

            pictureBox1.Image = RbtnMale.Checked ? Properties.Resources.Male : Properties.Resources.Female;

            lblRemove.Visible = false;
        }

        private void OnNationalNoValidating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textNational.Text) || clsPerson.IsNationalNoExists(textNational.Text, _person.PersonID))
            {
                errorProvider1.SetError(textNational, "National Number is required");
                e.Cancel = true;
            }


        }
        private void OnFirstNameValidating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textFirst.Text))
            {
                errorProvider1.SetError(textFirst, "First Name should have a value");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(textFirst, "");
            }

            
        }
        private void OnSecondNameValidating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textSecond.Text))
            {
                errorProvider1.SetError(textSecond, "Second Name should have a value");
                e.Cancel = true;
            }

            
        }
        private void OnLastNameValidating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textLast.Text))
            {
                errorProvider1.SetError(textLast, "Last Name should have a value");
                e.Cancel = true;
            }

            
        }
        private void OnPhoneValidating(object sender, CancelEventArgs e)
        {
            if (!clsPerson.IsNumber(textPhone.Text))
            {
                errorProvider1.SetError(textPhone, "This phone number has Letters");
                e.Cancel = true;
            }

        }

        
    }
}
