// Programmer: Kanin Furlow
// Course: INEW 2332.7Z1 (Final Project)
// Program Description: A foot wear application that sells a wide variety of shoes ranging
// from track & field to formal wear.
//*******************************************
// Form Purpose: This is the View Cart form, it allows users to see what is in there cart, as
// well as removing selected items or clearing everything from the cart. It also displays the total
// for all the items in the cart.
//*******************************************
//*******************************************
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
    public partial class ViewCart : Form
    {
        SqlConnection cs = new SqlConnection("Data Source=cstnt.tstc.edu;Initial Catalog=inew2332Fa21;Persist Security Info=True;User ID=FurlowK21Fa2332;Password=1782473");

        public ViewCart()
        {
            InitializeComponent();
        }

        // allows customer to delete selected item from cart
        private void btnDelete_Click(object sender, EventArgs e)
        {
            UserView.item item = new UserView.item();
            item = (UserView.item)lstOrderSummary.SelectedItem;
            int index = lstOrderSummary.SelectedIndex;

            if (index >= 0)
            {
                lstOrderSummary.Items.RemoveAt(index);
                UserView.fltTOTAL -= item.itemPrice;
                item.purchaseAmount -= 1;
                lblTotal.Text = UserView.fltTOTAL.ToString("C2");

                
                string strQuery = "UPDATE FurlowK21Fa2332.Inventory set Quantity += 1 WHERE InventoryID = '" +item.itemID+ "'";
                clsSQL.UpdateQuantity(strQuery);

                UserView user = new UserView();
                user.lblShoeQuantity.Text = item.itemInventory.ToString();

                if (item.purchaseAmount < 1)
                {
                    lstOrderSummary.Items.Remove(item);
                    item.intCounter = 0;
                }
                else
                {
                    lstOrderSummary.Items.Insert(index, item);
                }
                
            }
        }

        //clears cart
        private void btnClear_Click(object sender, EventArgs e)
        {
            UserView uv = new UserView();
             
            foreach (UserView.item item in lstOrderSummary.Items)
            {
                int intQty = item.purchaseAmount;
                string strDeduct = "UPDATE FurlowK21Fa2332.Inventory SET Quantity += '" + intQty + "' WHERE InventoryID = '" + item.itemID + "'";
                clsSQL.ClearCart(strDeduct);
                item.itemInventory++;
                uv.lblShoeQuantity.Text = item.itemInventory.ToString();

            }
            lstOrderSummary.Items.Clear();
            lblTotal.Text = "";
            UserView.fltTOTAL = 0;
            UserView.cart.Clear();
            UserView.item[] newItemArr = new UserView.item[1000];
            UserView.items = newItemArr;
            UserView.intAmount = 0;
        }

        //hides form
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        //help menu for View Cart
        private void lnkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("1. To Delete item from cart click the specified item then click Delete\n" +
                   "2. To clear the cart click Clear\n" +
                   "3. To close the cart click Close\n", "Helpful Information", MessageBoxButtons.OK);

        }

        //variable to track if we are closing from the menu or 'X' (form close)
        bool bolExit = true;

        private void ViewCart_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bolExit)
            {
                //Based on the value in bolExit we cancel the FormClosing Event
                e.Cancel = true; //Cancel the FormClosing event
                //Now give the user some feedback on what they should do instead of clicking the 'X'
                MessageBox.Show("Sorry, you must choose 'Close' to close the cart"
                           , "Please don't click the 'X' to close this screen"
                           , MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
    }
}
