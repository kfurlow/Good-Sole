using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace FA21_Final_Project
{
    class clsValidation
    {
        public class ValidationException : Exception
        {
            public ValidationException(string message)
                : base(message) { }
        }

        public static bool ValidateCard(string strCardNumber)
        {
            if (strCardNumber.Length > 19)
            {
                MessageBox.Show("Invalid Credit Card number.", "Too long", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;

        }

        public static bool CheckInput(string strFirst, string strMiddle, string strLast, string strCRV)
        {
            if (!Regex.Match(strFirst, "^[A-Z][a-zA-Z]*$").Success)
            {
                MessageBox.Show("Invalid first name", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Regex.Match(strLast, "^[A-Z][a-zA-Z]*$").Success)
            {
                MessageBox.Show("Invalid first name", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (strFirst.Length > 20)
            {
                MessageBox.Show("First name cannot be more than 20 characters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (strMiddle.Length > 20)
            {
                MessageBox.Show("Middle name cannot be more than 20 characters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (strLast.Length > 20)
            {
                MessageBox.Show("Last name cannot be more than 20 characters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; ;
            }

            if (strCRV.Length > 3)
            {
                MessageBox.Show("CRV cannot be longer than 3 characters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public static bool CheckInput(string strFirst, string strLast, string strCRV)
        {

            if (!Regex.Match(strFirst, "^[A-Z][a-zA-Z]*$").Success)
            {
                MessageBox.Show("Invalid first name", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Regex.Match(strLast, "^[A-Z][a-zA-Z]*$").Success)
            {
                MessageBox.Show("Invalid first name", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (strFirst.Length > 20)
            {
                MessageBox.Show("First name cannot be more than 20 characters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (strLast.Length > 20)
            {
                MessageBox.Show("Last name cannot be more than 20 characters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (strCRV.Length > 3)
            {
                MessageBox.Show("CRV cannot be longer than 3 characters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        //making sure email is valid
        public static bool IsValidEmail(string strEmail)
        {
            var address = new System.Net.Mail.MailAddress(strEmail);
            if (address.Address == strEmail)
            {
                return true;
            }
            return false;
        }

        //check if string is decimal
        public static bool CheckIfDecimal(TextBox txtDecimal)
        {
            float fltPrice;
            
            if (float.TryParse(txtDecimal.Text, out fltPrice))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Please enter a valid price", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //validates that entered data in decimal
        public static void ValidatePrice(object sender, KeyPressEventArgs e)
        {
            //allows 0 - 9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            //checks to make sure only one decimal is allowed
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        public static void ValidateNameInput(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public static void ValidatePhoneInput(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public static void ValidateCreditCardInput(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        
        public static void ValidateUsernameInput(KeyPressEventArgs e)
        {
            if (e.Handled = (e.KeyChar == (char)Keys.Space))
            {
                e.Handled = true;
            }

            if (e.Handled = (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        public static void ValidatePasswordInput(KeyPressEventArgs e)
        {
            if (e.Handled = (e.KeyChar == (char)Keys.Space))
            {
                e.Handled = true;
            }
        }

        public static void ValidateAddressInput(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
       
        public static bool CheckInput(string strFirst, string strMiddle, string strLast, string strEmail, string strPhone, string strAddress1,
            string strAddress2, string strAddress3, string strCity, string strZipcode, string strAltPhone)
        {
            if (!String.IsNullOrWhiteSpace(strEmail) && IsValidEmail(strEmail) == false)
            {
                MessageBox.Show("Email address invalid.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (strZipcode.Length == 5 && !Regex.Match(strZipcode, "^[0-9]{5}").Success)
            {
                MessageBox.Show("Invalid Zipcode", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!Regex.Match(strZipcode, "^[0-9]{5}([0-9]{4})?$").Success)
            {
                MessageBox.Show("Invalid Zipcode", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Regex.Match(strCity, @"^([a-zA-Z]+|[a-zA-Z]+\s[a-zA-Z]+)$").Success)
            {
                MessageBox.Show("Invalid City name", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Regex.Match(strFirst, "^[A-Z][a-zA-Z]*$").Success)
            {
                MessageBox.Show("Invalid first name", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Regex.Match(strLast, "^[A-Z][a-zA-Z]*$").Success)
            {
                MessageBox.Show("Invalid first name", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (strFirst.Length > 20)
            {
                MessageBox.Show("First name cannot be more than 20 characters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (strMiddle == "")
            {
                return true;
            }
            else if (strMiddle.Length > 20)
            {
                MessageBox.Show("Middle name cannot be more than 20 characters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (strLast.Length > 20)
            {
                MessageBox.Show("Last name cannot be more than 20 characters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if(strPhone.Length > 20 || strPhone.Length < 10)
            {
                MessageBox.Show("Phone number cannot be more than 20 characters or less than 10.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!String.IsNullOrWhiteSpace(strAltPhone) && (strAltPhone.Length > 20 || strAltPhone.Length < 10))
            {
                MessageBox.Show("Phone number cannot be more than 20 characters or less than 10.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (strAddress1.Length > 30)
            {
                MessageBox.Show("Address can be no longer than 30 characters", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (strAddress2.Length > 30)
            {
                MessageBox.Show("Address can be no longer than 30 characters", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (strAddress3.Length > 30)
            {
                MessageBox.Show("Address can be no longer than 30 characters", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if(strCity.Length >30)
            {
                MessageBox.Show("City name can be no longer than 30 characters", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if(strZipcode.Length > 10)
            {
                MessageBox.Show("Zipcode can be no longer than 10 characters", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
}
