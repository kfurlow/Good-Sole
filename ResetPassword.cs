//*******************************************
//*******************************************
// Programmer: Kanin Furlow
// Course: INEW 2332.{7Z1} (Final Project)
// Program Description: A foot wear application that sells a wide variety of shoes ranging
// from track & field to formal wear. 
//*******************************************
// Form Purpose: This form is for resetting the users password. It requires the users username 
// which will then be checked with the usernames in the database. Then the security questions
// will be loaded requiring the answers for the password to be reset.
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
    public partial class ResetPassword : Form
    {
        //variables to store PersonLogin information
        public static string strPassword, strUsername, strSecurity1, strSecurity2, strSecurity3,
            strAnswer1, strAnswer2, strAnswer3;

        //establishes connection will database
        SqlConnection cs = new SqlConnection("Data Source=cstnt.tstc.edu;Initial Catalog=inew2332fa21;Persist Security Info=True;User ID=FurlowK21Fa2332;Password=1782473");

        //returns user to main menu
        private void btnMain_Click(object sender, EventArgs e)
        {
            LogonScreen logon = new LogonScreen();
            this.Hide();
            logon.Show();
        }



        private void chkShowTwo_Click(object sender, EventArgs e)
        {
            if (chkShowTwo.Checked && txtConfirmPass.PasswordChar == '*')
            {
                txtConfirmPass.PasswordChar = '\0';
            }
            else if (!chkShowTwo.Checked)
            {
                txtConfirmPass.PasswordChar = '*';
            }
        }

        private void chkShow_Click(object sender, EventArgs e)
        {
            if (chkShow.Checked && txtNewPassword.PasswordChar == '*')
            {
                txtNewPassword.PasswordChar = '\0';
            }
            else if (!chkShow.Checked)
            {
                txtNewPassword.PasswordChar = '*';
            }
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateUsernameInput(e);
        }

        //stops applications when user clicks 'X'
        private void ResetPassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            LogonScreen logonScreen = new LogonScreen();
            this.Hide();
            logonScreen.Show();
        }

        //display help message
        private void lnkNeedHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("1. Enter Username then click Enter.\n" +
                "2. Answer the following Security Questions (not case sensitive).\n" +
                "3. Enter and confirm New Password.\n" +
                "4. Click Reset Password.", "Help", MessageBoxButtons.OK);
        }


        //checks to make sure user answers are correct
        private void btnResetPass_Click(object sender, EventArgs e)
        {
            bool bEmpty = false;

            if(String.IsNullOrWhiteSpace(txtUsername.Text))
            {
                bEmpty = true;
                MessageBox.Show("Please enter Username", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(String.IsNullOrWhiteSpace(txtAnswerOne.Text) || String.IsNullOrWhiteSpace(txtAnswerTwo.Text) || String.IsNullOrWhiteSpace(txtAnswerThree.Text))
            {
                bEmpty = true;
                MessageBox.Show("Please answer security questions.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(String.IsNullOrWhiteSpace(txtNewPassword.Text) || String.IsNullOrWhiteSpace(txtConfirmPass.Text))
            {
                bEmpty = true;
                MessageBox.Show("Please enter and confirm new password.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string strUserAnswer1, strUserAnswer2, strUserAnswer3, strUserAnswerLow1, strUserAnswerLow2, strUserAnswerLow3;
                strUserAnswer1 = txtAnswerOne.Text;
                strUserAnswer2 = txtAnswerTwo.Text;
                strUserAnswer3 = txtAnswerThree.Text;
                strUserAnswerLow1 = strUserAnswer1.ToLower();
                strUserAnswerLow2 = strUserAnswer2.ToLower();
                strUserAnswerLow3 = strUserAnswer3.ToLower();

                if (strUserAnswerLow1 == strAnswer1 && strUserAnswerLow2 == strAnswer2 && strUserAnswerLow3 == strAnswer3)
                {
                    //variables to store new password
                    string strNewPassword, strConfirmNewPassword;

                    //capture new password input
                    strNewPassword = txtNewPassword.Text;
                    strConfirmNewPassword = txtConfirmPass.Text;

                    //check to make sure passwords are the same
                    if (strNewPassword != strConfirmNewPassword)
                    {
                        MessageBox.Show("Passwords do not match, please try again.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (strNewPassword == strConfirmNewPassword)
                    {
                        //validate new passwords
                        if(clsLogon.ValidatePassword(strNewPassword))
                        {
                            try
                            {
                                clsSQL.ResetPassword(strUsername, strNewPassword);

                                MessageBox.Show("Password reset.", "Reset", MessageBoxButtons.OK);

                                //return to login screen
                                this.Hide();
                                frmMain main = new frmMain();
                                main.Show();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
                else
                {
                    if (bEmpty == false)
                    {
                        MessageBox.Show("Answers do not match, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            } 
        }

        //doesn't allow spaces in confirm pass field
        private void txtConfirmPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePasswordInput(e);
        }

        //doesn't allow spaces in new password field
        private void txtNewPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePasswordInput(e);
        }

        public ResetPassword()
        {
            InitializeComponent();
        }

        //checks to make sure username is in database
        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                if(String.IsNullOrEmpty(txtUsername.Text))
                {
                    MessageBox.Show("Please enter Username", "Check Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //stores username in variable
                    strUsername = txtUsername.Text;

                    clsLogon.ValidateUsername(strUsername);

                    //query to select all matching data from database
                    string strSelectAllQuery = "SELECT * FROM FurlowK21Fa2332.Logon WHERE LogonName = '" + txtUsername.Text + "' ";

                    SqlCommand cmd = new SqlCommand(strSelectAllQuery, cs);

                    //create SQLdatareader
                    SqlDataReader reader;

                    //open connection
                    cs.Open();

                    //pulls results of cmd command
                    reader = cmd.ExecuteReader();
                    reader.Read();

                    if (txtUsername.Text == reader["LogonName"].ToString())
                    {
                        //allow grpResetPass to be edited
                        grpSecurity.Enabled = true;
                        btnResetPass.Enabled = true;
                        grpResetPass.Enabled = true;

                        //stores pulled data into variables
                        strPassword = reader["Password"].ToString();
                        strSecurity1 = reader["FirstChallengeQuestion"].ToString();
                        strSecurity2 = reader["SecondChallengeQuestion"].ToString();
                        strSecurity3 = reader["ThirdChallengeQuestion"].ToString();
                        strAnswer1 = reader["FirstChallengeAnswer"].ToString();
                        strAnswer2 = reader["SecondChallengeAnswer"].ToString();
                        strAnswer3 = reader["ThirdChallengeAnswer"].ToString();

                        //displays security questions into labels
                        lblSecurityOne.Text = strSecurity1;
                        lblSecurityTwo.Text = strSecurity2;
                        lblSecurityThree.Text = strSecurity3;
                        cs.Close();
                    }
                    else
                    {
                        MessageBox.Show("Username not correct, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //exception class to display error message for incorrect format of new password
        public class ValidationException : Exception
        {
            public ValidationException(string message)
                : base(message) { }
        }

      
    }
}
