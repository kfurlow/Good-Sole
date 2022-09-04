//*******************************************
//*******************************************
// Programmer: Kanin Furlow
// Course: INEW 2332.{7Z1} (Final Project)
// Program Description: A foot wear application that sells a wide variety of shoes ranging
// from track & field to formal wear. 
//*******************************************
// Form Purpose: This will be the main form allowing the customer to move to the login/ create account form
// or allowing the user to view the inventory.
//*******************************************
//*******************************************


using FA21_Final_Project;
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


namespace SU21_Final_Project
{
    public partial class frmMain : Form
    {
        //creating database connection
        SqlConnection cs = new SqlConnection("Data Source=cstnt.tstc.edu;Initial Catalog=inew2332Fa21;Persist Security Info=True;User ID=FurlowK21Fa2332;Password=1782473");
        ViewCart vc = new ViewCart();
        public frmMain()
        {
            InitializeComponent();
        }

        //displays help message to user
        private void lnkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("1. Click Login to Create Account or Login\n" +
                   "2. To view our inventory click View Inventory", "Helpful Information");
        }

        //opens login form
        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            LogonScreen logon = new LogonScreen();
            logon.Show();
        }

        //opens inventory for viewing only
        private void lnkViewInventory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            UserView uv = new UserView();
            uv.Show();
        }

        //closes the application when user clicks 'X'
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgResult == DialogResult.Yes)
            {
                cs.Open();
                for(int i = 0; i <= UserView.items.Length; i++)
                {
                    if (UserView.items[i] == null)
                    {
                        break;
                    }
                    int intQty = UserView.items[i].purchaseAmount;
                    string strDeduct = "UPDATE FurlowK21Fa2332.Inventory SET Quantity += '" + intQty + "' WHERE InventoryID = '" + UserView.items[i].itemID + "'";
                    SqlCommand sqlCmd = new SqlCommand(strDeduct, cs);
                    sqlCmd.ExecuteNonQuery();
                }
                cs.Close();
                Application.ExitThread();
            }
            else if (dlgResult == DialogResult.No)
                e.Cancel = true;
        }
    }
}