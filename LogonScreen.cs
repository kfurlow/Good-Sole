//*******************************************
//*******************************************
// Programmer: Kanin Furlow
// Course: INEW 2332.{7Z1} (Final Project)
// Program Description: A foot wear application that sells a wide variety of shoes ranging
// from track & field to formal wear. 
//*******************************************
// Form Purpose: This form will be used for logging in or creating an account for new users.
//*******************************************
//*******************************************

using SU21_Final_Project;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FA21_Final_Project
{
    public partial class LogonScreen : Form
    {
        public static int intLogin = 0;

        public LogonScreen()
        {
            InitializeComponent();
        }

        //opens Create Account form to allow user to create account
        private void btnCreate_Click(object sender, EventArgs e)
        {
            this.Hide();
            Create_Account ca = new Create_Account();
            ca.Show();
        }

        //username will not allow spaces
        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateUsernameInput(e);
        }

        //password will not allow spaces
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePasswordInput(e);
        }

        //exception class to display error message for incorrect format of new password
        public class ValidationException : Exception
        {
            public ValidationException(string message)
                : base(message) { }
        }

        //checks to make sure username and password are valid and in database
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //check to make sure username and password field has info
                //check to see if security questions have been answered
                if (String.IsNullOrWhiteSpace(txtUsername.Text) || String.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Please enter Username or Password.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //string to hold username and password
                    string strUsername = txtUsername.Text;
                    string strPassword = txtPassword.Text;
                    string strUserLow = strUsername.ToLower();


                    //query to select all matching data from database
                    string strSelectAllQuery = "SELECT * FROM FurlowK21Fa2332.Logon WHERE LogonName = '" + strUserLow + "' and Password = '" + strPassword + "' " +
                        "COLLATE SQL_Latin1_General_CP1_CS_AS";

                    if (ManagerView.bolPOSCustomer)
                    {
                        ManagerView.customer emptyCustomer = new ManagerView.customer();
                        ManagerView.posCustomer = emptyCustomer;
                        ManagerView.bolPOSCustomer = false;
                    }

                    if (clsSQL.Login(strSelectAllQuery))
                    {
                        //if checked in user is manager open Manager Form
                        if(clsSQL.newCustomer.position == "Manager")
                        {
                            this.Hide();
                            ManagerView management = new ManagerView();
                            management.Show();
                            intLogin = 1;
                        }
                        else //else login as user
                        {
                            intLogin = 1;
                            this.Hide();
                            UserView uv = new UserView();
                            uv.Show();
                        }
                    }

                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //opens form to reset password
        private void lnkPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            ResetPassword reset = new ResetPassword();
            reset.Show();
        }

        //displays help message to user
        private void lnkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("1. Enter Username and Password for existing customers then click Login\n" +
                "2. If new customer click Create to create a new account\n" +
                "3. If you have forgotten your password click Forgot Password? to create a new password\n" +
                "4. To view our inventory click View Inventory\n" +
                "5. Click Main to return to the Main Menu", "Helpful Information", MessageBoxButtons.OK);
        }

        //returns user to main menu
        private void btnMain_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMain main = new frmMain();
            main.ShowDialog();
        }

        //shows or hides password text
        private void rdoShow_Click(object sender, EventArgs e)
        {
            if (rdoShow.Checked && txtPassword.PasswordChar == '*')
            {
                txtPassword.PasswordChar = '\0';
            }
            else if (rdoShow.Checked)
            {
                txtPassword.PasswordChar = '*';
            }
        }

        //stops application when 'X' is clicked on form
        private void LogonScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMain main = new frmMain();
            this.Hide();
            main.Show();
        }

        private void lnkViewInventory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UserView user = new UserView();
            user.Show();
            this.Hide();
        }
    }
}
