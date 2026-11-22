using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DvldDataAccessLayer;

namespace DvldBusinessLayer
{
    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int PersonID { set; get; }
        public string NationalNo { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
        public DateTime DateOfBirth { set; get; }
        public int Gender { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public int NationalityCountryID { set; get; }
        private string _ImagePath;
        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
        public clsPerson()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Gender = -1;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";

            Mode = enMode.AddNew;
        }
        private clsPerson(int PersonID,string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, int Gender, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
            Mode = enMode.Update;
        }
        public static clsPerson FindPersonByID(int ID)
            {
                string nationalNo = "";
                string firstName = "";
                string secondName = "";
                string thirdName = "";
                string lastName = "";
                DateTime dateOfBirth = DateTime.Now;
                int gender = -1;
                string address = "";
                string phone = "";
                string email = "";
                int nationalityCountryID = -1;
                string imagePath = "";

                if (clsPersonData.GetPersonByID(ID, ref nationalNo, ref firstName, ref secondName, ref thirdName, ref lastName, ref dateOfBirth, ref gender, ref address, ref phone, ref email, ref nationalityCountryID, ref imagePath))
                {
                    return new clsPerson(ID, nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, nationalityCountryID, imagePath);
                }
                else
                {
                    return null;
                }
            }
        public static int Login(string username, string password)
        {
            return clsPersonData.GetValidUserID(username, password);
        }
        public static DataTable GetAllPeopleForDataGridInPeopleManagemt()
        {
            return clsPersonData.GetAllPeople();
        }
        public static DataTable Filter(string ColumnName, string filterBy)
        {
           return clsPersonData.FilterPeople(ColumnName, filterBy);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {

                        Mode = enMode.Update;
                        return true;
                     
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdatePerson();

                default:
                    return false;
            }
        }
        public static bool IsNationalNoExists(string nationalNo, int ExcludePersonID)
        {
            return clsPersonData.IsNationalNoExists(nationalNo, ExcludePersonID);
        }
        private bool _AddNewPerson()
        {
            if (IsNationalNoExists(this.NationalNo,this.PersonID) ||
                string.IsNullOrWhiteSpace(this.FirstName) ||
                string.IsNullOrWhiteSpace(this.SecondName) ||
                string.IsNullOrWhiteSpace(this.LastName) ||
                string.IsNullOrWhiteSpace(this.NationalNo) ||
                string.IsNullOrWhiteSpace(this.Address) ||
                string.IsNullOrWhiteSpace(this.Phone)
                )
            {
                return false;
            }

            this.PersonID = clsPersonData.AddNewPerson(
                this.NationalNo,
                this.FirstName,
                this.SecondName,
                this.ThirdName,
                this.LastName,
                this.DateOfBirth,
                this.Gender,
                this.Address,
                this.Phone,
                this.Email,
                this.NationalityCountryID,
                this.ImagePath);
            return (this.PersonID != -1);
        }
        private bool _UpdatePerson()
        {
            if (IsNationalNoExists(this.NationalNo, this.PersonID) ||
                string.IsNullOrWhiteSpace(this.FirstName) ||
                string.IsNullOrWhiteSpace(this.SecondName) ||
                string.IsNullOrWhiteSpace(this.LastName) ||
                string.IsNullOrWhiteSpace(this.NationalNo) ||
                string.IsNullOrWhiteSpace(this.Address) ||
                string.IsNullOrWhiteSpace(this.Phone)
                )
            {
                return false;
            }

            return clsPersonData.UpdatePerson(
                this.PersonID,
                this.NationalNo,
                this.FirstName,
                this.SecondName,
                this.ThirdName,
                this.LastName,
                this.DateOfBirth,
                this.Gender,
                this.Address,
                this.Phone,
                this.Email,
                this.NationalityCountryID,
                this.ImagePath
            );
        }
        public static DataTable GetAllCountries()
        {
            return clsPersonData.GetAllCountries();

        }
        public static int Records()
        {
           return clsPersonData.CountRecords();
        }
        private static bool IsPersonReferenced(int PersonID)
        {
            if (clsPersonData.IsPersonExistsInApplications(PersonID)) return true;

            if (clsPersonData.IsPersonExistsInUsers(PersonID)) return true;

            if (clsPersonData.IsPersonExistsInDrivers(PersonID)) return true;

            return false;
        }
        public static bool Delete(int PersonID)
        {
            if (IsPersonReferenced(PersonID))
            {
                return false;
            }
            else
            {
                return clsPersonData.DeletePerson(PersonID);
            }
        }
        public static bool IsNumber(char C)
        {
            for (int i = 48; i <= 57; i++)
            {
                if (i == C) 
                {
                    return true;
                }
            }
            return false;
        }
        public static bool IsNumber(string text)
        {
            foreach (char c in text)
            {
                if (!IsNumber(c))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool IspersonExists(int PersonID)
        {
            return clsPersonData.IsPersonExists(PersonID);
        }
        public static int GetDefaultCountry(string CountryName)
        {
            return clsPersonData.GetDefaultCountry( CountryName);
        }


    }
}
