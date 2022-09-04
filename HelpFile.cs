// Programmer: Kanin Furlow
// Course: INEW 2332.7Z1 (Final Project)
// Program Description: A foot wear application that sells a wide variety of shoes ranging
// from track & field to formal wear.
//*******************************************
// Form Purpose: This form is the HelpFile form. It will be used to help the user use the manager controls
// such as updating, adding, removing items and managers.
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

namespace FA21_Final_Project
{
    public partial class HelpFile : Form
    {
        public HelpFile()
        {
            InitializeComponent();
            lblUpdateManager.Text = "To update manager information select a manager from the Grid View.\n" +
                "Next fill out any information that needs to be modified.\n" +
                "Click 'Update' button to update manager information.";
            lblRemoveManager.Text = "To remove a manager from system select a manager from the Grid View\n" +
                "Next click 'Remove' button to delete manager\n";
            lblAddManager.Text = "To add manager to system fill out all information with '*'\n" +
                "Next click 'Add' to add manager to database.";

            lblUpdateCustomer.Text = "To update customer information select a customer from the Grid View.\n" +
                "Next fill out any information that needs to be modified.\n" +
                "Click 'Update' button to update customer information.";
            lblRemoveCustomer.Text = "To remove a customer from system select a customer from the Grid View\n" +
                "Next click 'Remove' button to delete customer\n";
            lblAddCustomer.Text = "To add customer to system fill out all information with '*'\n" +
                "Next click 'Add' to add customer to database.";

            lblUpdateItem.Text = "To update item information select an item from the Grid View.\n" +
                "Next fill out any information that needs to be modified.\n" +
                "Click 'Update Item' button to update item information.";
            lblRemoveItem.Text = "To remove an item from system select an item from the Grid View\n" +
                "Next click 'Remove' button to delete item\n";
            lblAddItem.Text = "To add item to system fill out all information\n" +
                "Next click 'Browse' to select image for item" +
                "Then click 'Add' to add the item to the database.";

            lblPOSSearch.Text = "To search for a customer for POS Check Out enter First Name, Last Name & Phone.\n" +
                "Next click 'Search' to search for customer.\n" +
                "Click the customers name in Grid View\n " +
                "Then click 'Check Out' to begin POS Check Out";
            lblPOSCheckOut.Text = "Once POS Check Out has started the Shopping Page will load and the manager\n" +
                "can begin shopping as if they where that customer. There will be labels showing which manager\n" +
                "is signed in and what customer is being used for POS Check Out";

            lblPrintSalesTotal.Text = "Select a Date.\n" +
                "'Daily' will give calculations for the selected Date.\n" +
                "'Weekly' will give calculations for the selected Date plus one week (7 days).\n" +
                "\tEx. Start Date: 12-12-01 through 12-12-08\n" +
                "'Monthly' will give calulations from selected Date plus 30 days\n" +
                "\tEx. Start Date: 12-12-01 through 12-12-31";
            lblPrintManagerInfo.Text = "To print all managers information(First, Last, Pay...) click 'Print'\n" +
                "This will open up a web browser which will give the option to print";
            lblPrintInventoryInfo.Text = "To print all items in Inventory click 'Print All'\n" +
                "To print only available items in Inventory click 'Print Available'\n" +
                "To print the items that need to be restocked click 'Print Restock'";

            lblCreateCoupon.Text = "To create a Coupon enter Coupon name\n" +
                "Next select the discount percentage\n" +
                "Then select the expiration date\n" +
                "Once everything is filled out click 'Create Coupon' to generate coupon code\n" +
                "(You can only create 5 coupons)";

        }

        private void HelpFile_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
        }
    }
}
