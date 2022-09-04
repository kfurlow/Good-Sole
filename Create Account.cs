//*******************************************
//*******************************************
// Programmer: Kanin Furlow
// Course: INEW 2332.{7Z1} (Final Project)
// Program Description: A foot wear application that sells a wide variety of shoes ranging
// from track & field to formal wear. 
//*******************************************
// Form Purpose: This form will be used for creating accounts for new users
//*******************************************
//*******************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using SU21_Final_Project;

namespace FA21_Final_Project
{
    public partial class Create_Account : Form
    {
        //creating database connection
        SqlConnection cs = new SqlConnection("Data Source=cstnt.tstc.edu;Initial Catalog=inew2332fa21;Persist Security Info=True;User ID=FurlowK21Fa2332;Password=1782473");
        SqlDataAdapter da = new SqlDataAdapter();

        public Create_Account()
        {
            InitializeComponent();
        }
        public class ValidationException : Exception
        {
            public ValidationException(string message)
                : base(message) { }
        }


        //only allows letters for first name field
        private void txtFirstName_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateNameInput(e);
        }

        //only allows letter for middle name field
        private void txtMiddleName_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateNameInput(e);
        }

        //only allows letters for last name field
        private void txtLastName_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateNameInput(e);
        }

        //only allows letters for city field
        private void txtCity_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateNameInput(e);
        }

        //only allows numbers for phone # field
        private void txtPhoneNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePhoneInput(e);
        }

        //only allows numbers for alt phone field
        private void txtAltPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePhoneInput(e);
        }

        //doesn't allow spaces in username field
        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateUsernameInput(e);
        }

        //doesn't allow spaces in password field
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePasswordInput(e);
        }

        //creates user account and adds data to database
        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtFirstName.Text) || String.IsNullOrEmpty(txtLastName.Text) || String.IsNullOrEmpty(txtPhoneNum.Text) || String.IsNullOrEmpty(txtAddress1.Text)
                    || String.IsNullOrEmpty(cboState.Text) || String.IsNullOrEmpty(txtCity.Text) || String.IsNullOrEmpty(txtZipcode.Text))
                {
                    MessageBox.Show("Please fill out all infomation with '*'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (String.IsNullOrWhiteSpace(txtAnswer1.Text) || String.IsNullOrWhiteSpace(txtAnswer2.Text) || String.IsNullOrWhiteSpace(txtAnswer3.Text))
                {
                    MessageBox.Show("Please answer security questions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if(String.IsNullOrEmpty(cboSecurity1.Text) || String.IsNullOrEmpty(cboSecurity2.Text) || String.IsNullOrEmpty(cboSecurity3.Text))
                {
                    MessageBox.Show("Please select security questions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //capturing username and password
                    String strUsername, strPassword;
                    strPassword = txtPassword.Text;
                    strUsername = txtUsername.Text;

                    if (clsLogon.ValidatePassword(strPassword) && clsLogon.UniqueUsername(strUsername) && clsLogon.ValidateUsername(strUsername))
                    {
                        //string to store email
                        string strEmail = txtEmail.Text;

                        string strPhone = txtPhoneNum.Text;
                        strPhone = strPhone.Replace("-", string.Empty);
                        string strPhoneAlt = txtAltPhone.Text;
                        strPhoneAlt = strPhoneAlt.Replace("-", string.Empty);
                        string strZipcode = txtZipcode.Text;
                        strZipcode = strZipcode.Replace("-", string.Empty);
                        strZipcode = strZipcode.Replace("_", string.Empty);


                        //validate all user input
                        if (clsValidation.CheckInput(txtFirstName.Text, txtMiddleName.Text, txtLastName.Text, txtEmail.Text, strPhone, txtAddress1.Text, txtAddress2.Text, txtAddress3.Text,
                            txtCity.Text, strZipcode, strPhoneAlt))
                        {
                            //capturing and inserting customer account information into database
                            clsSQL.InsertData(cboTitle.Text, txtFirstName.Text, txtMiddleName.Text, txtLastName.Text, cboSuffix.Text, txtAddress1.Text, txtAddress2.Text, txtAddress3.Text,
                                txtCity.Text, cboState.Text, strZipcode, txtEmail.Text, strPhone, strPhoneAlt);


                            //string to store answers and set all input to lower case
                            string strAnswer1, strAnswer2, strAnswer3, strAnswerLow1, strAnswerLow2, strAnswerLow3, strUsernameLow;
                            strAnswer1 = txtAnswer1.Text;
                            strAnswer2 = txtAnswer2.Text;
                            strAnswer3 = txtAnswer3.Text;
                            strAnswerLow1 = strAnswer1.ToLower();
                            strAnswerLow2 = strAnswer2.ToLower();
                            strAnswerLow3 = strAnswer3.ToLower();
                            strUsernameLow = strUsername.ToLower();
                            string strPosition = "Customer";
                            if (clsSQL.InsertLoginInfo(strUsernameLow, strPassword, cboSecurity1.Text, cboSecurity2.Text, cboSecurity3.Text, strAnswerLow1, strAnswerLow2, strAnswerLow3, strPosition))
                            {
                                MessageBox.Show("Account created... Returning to Login screen.", "Success!", MessageBoxButtons.OK);
                                this.Hide();
                                LogonScreen logon = new LogonScreen();
                                logon.Show();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //message box to give user assistance during account creation
        private void lnkNeedHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("1. Fill in all information with *\n" +
                "2. Make sure entered information is correct\n" +
                "3. Click Create Account", "Helpful Information", MessageBoxButtons.OK);
        }

        //returns user back to login form
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogonScreen login = new LogonScreen();
            login.Show();
        }

        //only allows numbers in zipcode field
        private void txtZipcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePhoneInput(e);
        }

        
        //validate input for address 1
        private void txtAddress1_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateAddressInput(e);
        }

        //validate input for address 2
        private void txtAddress2_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateAddressInput(e);
        }

        //validate input for address 1
        private void txtAddress3_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateAddressInput(e);
        }



        //shows or hides password text
        private void rdoShow_Click(object sender, EventArgs e)
        {
            if(rdoShow.Checked  && txtPassword.PasswordChar == '*')
            {
                txtPassword.PasswordChar = '\0';
            }
            else if(rdoShow.Checked)
            {
                txtPassword.PasswordChar = '*';
            }
        }

        //stops application when user clicks 'X'
        private void Create_Account_FormClosing(object sender, FormClosingEventArgs e)
        {
            LogonScreen logon = new LogonScreen();
            logon.Show();
            this.Hide();
        }

        private void rdoShow_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
