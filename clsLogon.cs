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
    class clsLogon
    {
        public class ValidationException : Exception
        {
            public ValidationException(string message)
                : base(message) { }
        }

        //checks to make sure username is unique
        public static bool UniqueUsername(string strUsername)
        {
            bool isUnique = true;
            try
            {
                isUnique = clsSQL.UniqueUsername(strUsername, isUnique);
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return isUnique;
        }

        //checks username for validation
        public static bool ValidateUsername(string username)
        {
            int minimum = 8, maximum = 20;

            if (username.Length > maximum)
            {
                MessageBox.Show("Username cannot be more thab 20 characters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (username.Length < minimum)
            {
                MessageBox.Show("Username cannot be less than 8 characters", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (Char.IsDigit(username[0]))
            {
                MessageBox.Show("Username cannot begin with numbers.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (username.Contains(" "))
            {
                MessageBox.Show("Username cannot contain spaces.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string strSpecial = "!@#$%^&*()";
            char[] chSpecial = strSpecial.ToCharArray();

            foreach (char ch in chSpecial)
            {
                if (username.Contains(ch))
                {
                    MessageBox.Show("Username cannot contain any Special Characters", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        //checks password for validation
        public static bool ValidatePassword(string password)
        {
            int minimum = 8, maximum = 20, complex = 0 ;
            string strSpecial = "!@#$%^&*()";
            char[] chSpecial = strSpecial.ToCharArray();

            if (password.Contains(" "))
            {
                MessageBox.Show("Password cannot contain spaces.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (password.Length < minimum || password.Length > maximum)
            {
                MessageBox.Show("Password must be at least 8 characters or greater than 20 characters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            if (password.Any(char.IsLower))
            {
                complex++;
            }

            if (password.Any(char.IsUpper))
            {
                complex++;
            }

            if (password.Any(char.IsDigit))
            {
                complex++;
            }

            foreach (char ch in chSpecial)
            {
                if (password.Contains(ch))
                {
                    complex++;
                    break;
                }
            }

            if (complex >= 3)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Password complexity does not meet requirements.\n" +
                        "Password must contain at least 3 requirements\n" +
                        "1. Capitol or lower case letter\n" +
                        "2. Number\n" +
                        "3. Special character !@#$%^&*()", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
    }
}
