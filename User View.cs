//*******************************************
//*******************************************
// Programmer: Kanin Furlow
// Course: INEW 2332.{7Z1} (Final Project)
// Program Description: A foot wear application that sells a wide variety of shoes ranging
// from track & field to formal wear. 
//*******************************************
// Form Purpose: This form will be used for shopping and making purchases. It will display all 
// of the items that we sale to the user.
//*******************************************
//*******************************************
using SU21_Final_Project;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FA21_Final_Project
{
    public partial class UserView : Form
    {
        //creating database connection
        SqlConnection cs = new SqlConnection("Data Source=cstnt.tstc.edu;Initial Catalog=inew2332Fa21;Persist Security Info=True;User ID=FurlowK21Fa2332;Password=1782473");
        SqlDataAdapter da = new SqlDataAdapter();

        //declaring global variables
        public static float fltTOTAL = 0;
        public static List<int> cart = new List<int>();
        public static ViewCart vc = new ViewCart();
        public const int intQuantity = 1000;
        public static item[] items = new item[intQuantity];
        public UserView()
        {
            InitializeComponent();
        }

        //class to store item information
        public class item
        {
            public string itemDescription { get; set; }
            public string itemName { get; set; }
            public float itemPrice { get; set; }
            public int itemID { get; set; }
            public int itemInventory { get; set; }
            public Byte[] itemImage { get; set; }
            public int purchaseAmount { get; set; }

            public int intCounter = 0;

            //remove from listBox
            public void removeItem()
            {
                fltTOTAL = fltTOTAL - itemPrice;
            }

            public override string ToString()
            {
                return itemName + " " + itemPrice.ToString("C2") + " x" + purchaseAmount;
            }
        }

        //check login position title (Employee, Manager, or Customer)
        String strPositionTitle = clsSQL.newCustomer.position;
        public static int intItemID;
        private void UserView_Load(object sender, EventArgs e)
        {
            //establishes connection with database
            SqlConnection connection = cs;
            
            //check if POS system is running 
            if(ManagerView.bolPOSCustomer)//if POS checkout display manager and customer name 
            {
                lblPOSCustomer.Visible = true;
                lblPOSEmployee.Visible = true;
                lblPOSEmp.Visible = true;
                lblPOSCus.Visible = true;
                lblPOSEmployee.Text = clsSQL.newCustomer.ToString();
                lblPOSCustomer.Text = ManagerView.posCustomer.ToString();
            }
            else //if not hide controls
            {
                lblPOSCustomer.Visible = false;
                lblPOSEmployee.Visible = false;
                lblPOSEmp.Visible = false;
                lblPOSCus.Visible = false;
            }

            if (strPositionTitle == "Manager")
            {
                btnManager.Visible = true;
                btnManager.Enabled = true;
            }
            else if (strPositionTitle == "Employee")
            {
                btnEmployee.Visible = true;
                btnEmployee.Enabled = true; 
            }
            else
            {
                //disable btnManager, btnEmployee
                btnEmployee.Visible = false;
                btnEmployee.Enabled = false;
                btnManager.Visible = false;
                btnManager.Enabled = false;
            }



            try
            {
                //query to pull item data from database
                string strQuery = "SELECT * FROM FurlowK21Fa2332.Inventory";
                cs.Open();
                SqlCommand cmd = new SqlCommand(strQuery, cs);
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();

                item newItem = new item();
                newItem.itemDescription = reader["ItemDescription"].ToString();
                newItem.itemID = int.Parse(reader["InventoryID"].ToString());
                newItem.itemPrice = float.Parse(reader["RetailPrice"].ToString());
                newItem.itemName = reader["ItemName"].ToString();
                newItem.itemInventory = int.Parse(reader["Quantity"].ToString());
                Byte[] image = ((byte[])reader["ItemImage"]);

                intItemID = newItem.itemID = int.Parse(reader["InventoryID"].ToString());

                lblShoeName.Text = newItem.itemName;
                lblShoePrice.Text = newItem.itemPrice.ToString("C2");
                lblShoeQuantity.Text = newItem.itemInventory.ToString();
                lblShoeDescription.Text = newItem.itemDescription;

                if (image == null)
                {
                    picShoe.Image = null;
                }
                else
                {
                    MemoryStream ms = new MemoryStream(image);
                    picShoe.Image = Image.FromStream(ms);
                }

                cs.Close();
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error loading items from database.\n\n" + ex.ToString());
            } 
        }
        //returns back to main menu
        private void btnBackToMain_Click(object sender, EventArgs e)
        {
            frmMain main = new frmMain();
            DialogResult dlgResult = DialogResult.Yes;
            HelpFile help = new HelpFile();
            help.Close();

            if (LogonScreen.intLogin == 1)
            {
                if (UserView.cart.Count > 0)
                {
                    dlgResult = MessageBox.Show("Are you sure " + clsSQL.newCustomer.fName + "? \n" +
                        "Your cart will be cleared.", "Leaving?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                }
            
                if (dlgResult == DialogResult.Yes)
                {
                    //clear items and cart
                    UserView.cart.Clear();
                    UserView.vc.lstOrderSummary.Items.Clear();
                    UserView.vc.lblTotal.Text = "";
                    UserView.fltTOTAL = 0;

                    //set intLogin back to 0
                    LogonScreen.intLogin = 0;
                    this.Hide();
                    main.Show();
                }
            }

            if (dlgResult == DialogResult.Yes)
            {
                this.Hide();
                main.Show();

            }
        }

        private void btnManager_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManagerView manager = new ManagerView();
            manager.Show();
        }

        //display previous item 
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            //decrement itemID by 1
            intItemID--;

            //query to pull item data from database
            string strQuery = "SELECT * FROM FurlowK21Fa2332.Inventory where InventoryID = '" + intItemID + "' AND Discontinued = 0";

            //declare new item and store data from Inventory Table
            item newItem = new item();
            newItem = clsSQL.PreviousItem(strQuery, newItem);
            lblShoeName.Text = newItem.itemName;
            lblShoePrice.Text = newItem.itemPrice.ToString("C2");
            lblShoeQuantity.Text = newItem.itemInventory.ToString();
            lblShoeDescription.Text = newItem.itemDescription;
            Byte[] bytImage = newItem.itemImage;

            //check if image is null
            if (bytImage == null)
            {
                picShoe.Image = null;
            }
            else
            {
                MemoryStream ms = new MemoryStream(bytImage);
                picShoe.Image = Image.FromStream(ms);
            }
        }

        //display the next item in the database
        private void btnNext_Click(object sender, EventArgs e)
        {
            //increment itemID by 1
            intItemID++;

            //query to pull item data from database
            string strQuery = "SELECT * FROM FurlowK21Fa2332.Inventory where InventoryID = '" + intItemID + "' AND Discontinued = 0";

            //declare new item and store data from Inventory Table
            item newItem = new item();
            newItem = clsSQL.NextItem(strQuery, newItem);
            lblShoeName.Text = newItem.itemName;
            lblShoePrice.Text = newItem.itemPrice.ToString("C2");
            lblShoeQuantity.Text = newItem.itemInventory.ToString();
            lblShoeDescription.Text = newItem.itemDescription;
            Byte[] bytImage = newItem.itemImage;

            //check if image is null
            if (bytImage == null)
            {
                picShoe.Image = null;
            }
            else
            {
                MemoryStream ms = new MemoryStream(bytImage);
                picShoe.Image = Image.FromStream(ms);
            }
        }

        //stops application when user clicks 'X'
        private void UserView_FormClosing(object sender, FormClosingEventArgs e)
        {
            HelpFile help = new HelpFile();
            help.Hide();
            if (LogonScreen.intLogin == 0)
            {
                this.Hide();
                frmMain main = new frmMain();
                main.Show();
            }
            else
            {
                DialogResult dlgResult = MessageBox.Show("You are currently logged in. Continuing will sign you out\n" +
                                            "and clear cart of all items. Do you want to continue?", "Continue",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if(dlgResult == DialogResult.Yes)
                {
                    foreach (UserView.item item in vc.lstOrderSummary.Items)
                    {
                        int intQty = item.purchaseAmount;
                        string strDeduct = "UPDATE FurlowK21Fa2332.Inventory SET Quantity += '" + intQty + "' WHERE InventoryID = '" + item.itemID + "'";
                        SqlCommand sqlCmd = new SqlCommand(strDeduct, cs);
                        sqlCmd.ExecuteNonQuery();
                    }
                    LogonScreen.intLogin = 0;
                    clsSQL.newCustomer = new clsSQL.customer();

                    this.Hide();
                    frmMain main = new frmMain();
                    main.Show();
                }
                else if (dlgResult == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {

        }

        //closes application
        private void UserView_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //help menu
        private void lnkNeedHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("1. To add item to cart click button Add to Cart\n" +
                "2. To view items in cart click View Cart\n" +
                "3. Click Check Out to continue to payment information\n" +
                "4. Click Main to return to the Main Menu", "Helpful Information", MessageBoxButtons.OK);
        }

        //shows View Cart form
        private void btnViewCart_Click(object sender, EventArgs e)
        {
            vc.Show();
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            if (LogonScreen.intLogin == 0)
            {
                MessageBox.Show("Must login before completing purchase.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (!UserView.cart.Any())
            {
                MessageBox.Show("Hmm it looks like the cart is empty.\n To add items to cart" +
                    " click Add To Cart.", "Help", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                CheckOut frmCheckOut = new CheckOut();
                frmCheckOut.Show();
                this.Hide();
            }
        }
        public static int intAmount = 0;
        int intQty = 1;
        private void btnAddToCart_Click(object sender, EventArgs e)
        {

            //query to grab item data from table in database
            string strQuery = "SELECT * FROM FurlowK21Fa2332.Inventory WHERE ItemName = '" + lblShoeName.Text + "'";

            item newItem = clsSQL.GetItemInfo(strQuery);
            
            if (newItem.itemInventory == 0)
            {
                lblShoeQuantity.Text = "0";
                MessageBox.Show("This item is currently out of stock.", "Out of stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                string strDeduct = "UPDATE FurlowK21Fa2332.Inventory SET Quantity -= 1 WHERE InventoryID = '" + newItem.itemID + "'";
                clsSQL.DeductQuantity(strDeduct);

                //calculate total price
                fltTOTAL = fltTOTAL + (newItem.itemPrice * intQty);

                //display total to label on View Cart
                vc.lblTotal.Text = fltTOTAL.ToString("C2");

                items[intAmount] = newItem;
                int intTemp = intAmount, intEmpty = 0;
                bool bolCheck = false;

                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] == null)
                    {
                        break;
                    }
                    if (items[i].itemName == newItem.itemName && items[i].intCounter != 0)
                    {
                        intTemp = i;
                        items[intTemp].purchaseAmount++;
                        bolCheck = true;
                        intEmpty = i;
                        break;
                    }
                }

                //display items in listBox and add item to cart
                cart.Add(newItem.itemID);

                if (bolCheck == true)
                {
                    int temp = intEmpty;
                    vc.lstOrderSummary.Items.RemoveAt(temp);
                    vc.lstOrderSummary.Items.Insert(temp, items[intEmpty]);
                }
                else
                {
                    items[intAmount].purchaseAmount = 1;
                    items[intAmount].itemName = lblShoeName.Text;
                    vc.lstOrderSummary.Items.Add(items[intAmount]);
                    items[intAmount].intCounter = 1;
                    intAmount++;
                }
                newItem.itemInventory--;
                lblShoeQuantity.Text = newItem.itemInventory.ToString();
                bolCheck = false;
            }
        }
    }
}
