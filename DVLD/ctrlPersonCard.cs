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
namespace DVLD
    {
        public partial class ctrlPersonCard : UserControl
        {
        private Image _customImage;
        public ctrlPersonCard()
        {
            InitializeComponent();
        }
        public void LoadPersonData(clsPerson _person)
        {
            textNational.Text = _person.NationalNo;
            textFirst.Text = _person.FirstName;
            textSecond.Text = _person.SecondName;
            textThird.Text = _person.ThirdName;
            textLast.Text = _person.LastName;
            DateOfBirth.Value = _person.DateOfBirth;

            // Set radio button based on gender (0=Male, 1=Female)
            if (_person.Gender == 0)
                RbtnMale.Checked = true;
            else
                RbtnFemale.Checked = true;

            txtAddress.Text = _person.Address;
            textPhone.Text = _person.Phone;
            textEmail.Text = _person.Email;

            // Load image if exists
            if (!string.IsNullOrEmpty(_person.ImagePath))
                pictureBox1.ImageLocation = _person.ImagePath;
        }
        public void FillPersonObject(clsPerson _person)
        {
            _person.NationalNo = textNational.Text;
            _person.FirstName = textFirst.Text;
            _person.SecondName = textSecond.Text;
            _person.ThirdName = _person.ThirdName;
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

           
            if (_customImage != null)
                _person.ImagePath = pictureBox1.ImageLocation;
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

            FirstNameValidating?.Invoke(this, e);
        }
        public event CancelEventHandler FirstNameValidating;
        private void OnNationalNoValidating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textNational.Text))
            {
                errorProvider1.SetError(textNational, "National Number is required");
                e.Cancel = true;
            }

            NationalNoValidating?.Invoke(this, e);
        }
        public event CancelEventHandler NationalNoValidating;
        private void OnSecondNameValidating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textSecond.Text))
            {
                errorProvider1.SetError(textSecond, "Second Name should have a value");
                e.Cancel = true;
            }

            SecondNameValidating?.Invoke(this, e);
        }
        public event CancelEventHandler SecondNameValidating;
        private void OnLastNameValidating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textLast.Text))
            {
                errorProvider1.SetError(textLast, "Last Name should have a value");
                e.Cancel = true;
            }

            LastNameValidating?.Invoke(this, e);
        }
        public event CancelEventHandler LastNameValidating;
        private void OnPhoneValidating(object sender, CancelEventArgs e)
        {
            if (!clsPerson.IsNumber(textPhone.Text))
            {
                errorProvider1.SetError(textPhone, "This phone number has Letters");
                e.Cancel = true;
            }

            PhoneValidating?.Invoke(this, e);
        }
        public event CancelEventHandler PhoneValidating;
        private void OnbtnSave_Click(object sender, EventArgs e)
        {
            PersonSaved?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler PersonSaved;
        private void OnbtnClose_Click(object sender, EventArgs e)
        {
            btnCloseClicked?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler btnCloseClicked;
        private void OnlblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkClicked?.Invoke(this, LinkLabelLinkClickedEventArgs.Empty);
        }
        public event EventHandler LinkClicked;
        private void OnDateOfBirth_DropDown(object sender, EventArgs e)
        {
            DateOfBirthClicked?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler DateOfBirthClicked;
        private void OnRbtnMale_CheckedChanged(object sender, EventArgs e)
        {
            RbtnMaleCheckedChanged?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler RbtnMaleCheckedChanged;
        private void OnlblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetImageLinkClicked?.Invoke(this, LinkLabelLinkClickedEventArgs.Empty);
        }
        public event EventHandler SetImageLinkClicked;
        private bool ValidateAll()
        {
            bool isValid = true;

            // Validate National Number
            if (string.IsNullOrWhiteSpace(textNational.Text))
            {
                errorProvider1.SetError(textNational, "National Number is required");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(textNational, "");
            }

            // Validate First Name
            if (string.IsNullOrWhiteSpace(textFirst.Text))
            {
                errorProvider1.SetError(textFirst, "First Name should have a value");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(textFirst, "");
            }

            // Validate Second Name
            if (string.IsNullOrWhiteSpace(textSecond.Text))
            {
                errorProvider1.SetError(textSecond, "Second Name should have a value");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(textSecond, "");
            }

            // Validate Last Name
            if (string.IsNullOrWhiteSpace(textLast.Text))
            {
                errorProvider1.SetError(textLast, "Last Name should have a value");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(textLast, "");
            }

            // Validate Phone (must be numeric)
            if (!clsPerson.IsNumber(textPhone.Text))
            {
                errorProvider1.SetError(textPhone, "This phone number has Letters");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(textPhone, "");
            }

            return isValid;
        }

        
    }
}
