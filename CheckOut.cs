// Programmer: Kanin Furlow
// Course: INEW 2332.7Z1 (Final Project)
// Program Description: A foot wear application that sells a wide variety of shoes ranging
// from track & field to formal wear.
//*******************************************
// Form Purpose: This form is for checking out. The user can see what's in their cart as well as add more items
// or cancel the order all together. The total is displayed along with any other fees that are required. The
// user will also select their form of paying and has the option between two different shipping times.
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
using static FA21_Final_Project.UserView;

namespace FA21_Final_Project
{
    public partial class CheckOut : Form
    {
        //creating database connection
        SqlConnection cs = new SqlConnection("Data Source=cstnt.tstc.edu;Initial Catalog=inew2332fa21;Persist Security Info=True;User ID=FurlowK21Fa2332;Password=1782473");

        //variable to store delivery fee
        public static float deliveryFee = 9.99f;
        public static float total;

        static string strPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

        //variable to store shipping type
        public static string strShipping;

        //variable to store date
        DateTime date = DateTime.Now;

        //varible to store ordernum
        string strOrderNumber;

        //create variable for total calculation
        float tax = 0.0825f;

        public CheckOut()
        {
            InitializeComponent();
            if(ManagerView.bolPOSCustomer)
            {
                lblCustomerName.Text = ManagerView.posCustomer.ToString();
            }
            else
            {
                lblCustomerName.Text = clsSQL.newCustomer.ToString();
            }
        }

        //stringbuilder to create invoice for purchase
        private StringBuilder CreateInvoice()
        {
            StringBuilder html = new StringBuilder();
            StringBuilder css = new StringBuilder();

            //creating database connection
            SqlConnection cs = new SqlConnection("Data Source=cstnt.tstc.edu;Initial Catalog=inew2332fa21;Persist Security Info=True;User ID=FurlowK21Fa2332;Password=1782473");
            SqlDataAdapter da = new SqlDataAdapter();
            

            css.Append("<style>");
            css.Append("table {width: 800px;}");
            css.Append("th {text-align:left;}");
            css.Append("table, th, td{border: 1px solid #000;border-collapse: collapse;}");
            css.Append("caption{font:bold;}");
            css.Append("h1 {font:bold 100% Georgia; letter-spacing:0.5em;text-align: center; text-transform: uppercase;}");
            css.Append("</style>");

            //grab orderID from database
            try
            {
                int intOrderNum;
                string strQuery = "SELECT TOP 1 * FROM FurlowK21Fa2332.OrderTable ORDER BY OrderID DESC";
                intOrderNum = clsSQL.GetOrderID(strQuery);
                strOrderNumber = intOrderNum.ToString();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Error connecting to database.\n\n" + e.ToString());
            }

            html.Append("<html>");
            html.Append($"<head>{css}<title>{"Good Sole"}</title></head>");
            html.Append("<body>");
            html.Append($"<h1>{"Good Sole"}</h1>");
            html.Append("<br>");
            html.Append("<p>Purchase Date: " + date.ToString() + "</p>");
            if(ManagerView.bolPOSCustomer)
            {
                html.Append("<p>Employee: " + clsSQL.newCustomer.ToString() + "</p>");
            }
            string strCustFirstName, strCustPhone, strCustTitle, strCustLastName, strCustMiddleName, strCustSuffix;
            if (ManagerView.bolPOSCustomer)
            {
                strCustFirstName = ManagerView.posCustomer.fName;
                strCustMiddleName = ManagerView.posCustomer.mName;
                strCustLastName = ManagerView.posCustomer.lName;
                strCustTitle = ManagerView.posCustomer.title;
                strCustPhone = ManagerView.posCustomer.phone;
                strCustSuffix = ManagerView.posCustomer.suffix;
            }
            else
            {
                strCustFirstName = clsSQL.newCustomer.fName;
                strCustMiddleName = clsSQL.newCustomer.mName;
                strCustLastName = clsSQL.newCustomer.lName;
                strCustTitle = clsSQL.newCustomer.title;
                strCustPhone = clsSQL.newCustomer.phone;
                strCustSuffix = clsSQL.newCustomer.suffix;
            }
            html.Append("<p>Phone #: " + strCustPhone + "</p>");
            html.Append("<p>Receipt #: " + strOrderNumber + "</p>");
            html.Append("<br>");

            html.Append("<table>");
            html.Append("<caption><h3>Customer Information</caption></h3>");
            html.Append("<tr><th>Title</th><th>First Name</th><th>Middle Name</th><th>Last Name</th><th>Suffix</th></tr>");
            html.Append("<tr><td>" + strCustTitle + "</td>" +
                            "<td>" + strCustFirstName + "</td>" +
                            "<td>" + strCustMiddleName + "</td>" +
                            "<td>" + strCustLastName + "</td>" +
                            "<td>" + strCustSuffix + "</td></tr>");
            html.Append("</table>");

            html.Append("<br>");
            html.Append("<br>");

            

            html.Append("<table>");
            html.Append("<caption><h3> Item Information</caption></h3>");
            html.Append("<tr><th>Product Name</th><th>Quantity</th><th>Price</th></tr>");

            
            string strName;
            float fltQueryTotal = 0;
            string strQuantity;
            float cost;
            int intTotalAmount;

            try
            {

                for (int i = 0; i < UserView.intQuantity; i++)
                {
                    if(UserView.items[i] == null)
                    {
                        //do nothing
                    }
                    else
                    {
                        
                        strQuantity = UserView.items[i].purchaseAmount.ToString();
                        intTotalAmount = UserView.items[i].purchaseAmount;

                        if(intTotalAmount != 0)
                        {
                            strName = UserView.items[i].itemName;
                            cost = UserView.items[i].itemPrice;
                            cost = cost * intTotalAmount;
                            if (UserView.items[i].intCounter == 0)
                            {
                                //do nothing
                            }
                            else
                            {
                                html.Append("<tr><td>" + strName + "</td>");
                                html.Append("<td>" + strQuantity + "</td>");
                                html.Append("<td>" + cost.ToString("C2") + "</td></tr>");
                                UserView.items[i].intCounter = 1;
                                tax *= cost;
                                fltQueryTotal = tax + cost;

                                int intOrderID = int.Parse(strOrderNumber);
                                float fltTotalTemp = total;
                                if(bolCouponUsed)
                                {
                                    fltTotalTemp = fltTotal;
                                }
                                if(ManagerView.bolPOSCustomer)
                                {
                                    string strOrderDetail = "insert into FurlowK21Fa2332.OrderDetail values('" + intOrderID + "', '" + UserView.items[i].itemID + "', '" + ManagerView.posCustomer.id + "', '" + UserView.items[i].purchaseAmount + "', '" + fltQueryTotal + "', '" + fltTotalTemp + "')";
                                    clsSQL.CreateOrderDetail(strOrderDetail);
                                }
                                else
                                {
                                    string strOrderDetail = "insert into FurlowK21Fa2332.OrderDetail values('" + intOrderID + "', '" + UserView.items[i].itemID + "', '" + clsSQL.newCustomer.id + "', '" + UserView.items[i].purchaseAmount + "', '" + fltQueryTotal + "', '" + fltTotalTemp + "')";
                                    clsSQL.CreateOrderDetail(strOrderDetail);
                                }
                                fltQueryTotal = 0.00f;
                                tax = 0.0825f;
                            }
                        }
                        
                       
                    } 
                }
            }
            catch(SqlException test)
            {
                MessageBox.Show("Error connecting to database.\n\n" + test.ToString());
            }



            html.Append("</table>");

            html.Append("<br>");

            float delivery;
            if (strShipping == "STANDARD")
            {
                delivery = 9.99f;
            }
            else
            {
                delivery = 15.99f;
            }

            if (bolCouponUsed == true)
            {
                lblTotal.Text = fltTotal.ToString("C2");
            }


            html.Append("<table>");
            html.Append("<caption><h3>Purchase Information</caption></h3>");
            html.Append("<tr><th>Sub Total</th><th>Taxes at 8.25%</th><th>Shipping</th><th>Discount</th><th>Total Price</th></tr>");
            html.Append("<tr><td>" + lblSubTotal.Text + "</td>" +
                            "<td>" + lblTax.Text + "</td>" +
                            "<td>" + delivery.ToString("C2") + "</td>" +
                            "<td>" + fltAmountDiscount.ToString("C2") + "</td>" +
                            "<td>" + lblTotal.Text + "</td></tr>");
            html.Append("</table>");

            html.Append("<br>");

            html.Append("</body></html>");


            return html;
        }

        //cancels order, clears cart and resets total to 0
        private void btnCancel_Click(object sender, EventArgs e)
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
            //clear items and cart
            UserView.cart.Clear();
            UserView.vc.lstOrderSummary.Items.Clear();
            UserView.vc.lblTotal.Text = "";
            UserView.fltTOTAL = 0;
            lblDiscount.Text = "";
            lblDiscountAmount.Text = "";
            txtCoupon.Text = "";
            bolCouponUsed = false;
            intCount = 0;
            this.Hide();
            UserView user = new UserView();
            user.Show();
        }

        //takes user back to user view to continue shopping
        private void btnAddItems_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserView user = new UserView();
            user.Show();
        }

        //only allows letters for first name
        private void txtFirstName_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateNameInput(e);
        }

        //only allows letters for middle name
        private void txtMiddleName_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateNameInput(e);
        }

        //only allows letters for last name
        private void txtLastName_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateNameInput(e);
        }

        //only allows numbers for crv
        private void txtCRV_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePhoneInput(e);
        }
        
        //declare temp total for calculations
        float fltTempTotal;

        private void CheckOut_Load(object sender, EventArgs e)
        {
            //display fees in labels
            lblSubTotal.Text = UserView.fltTOTAL.ToString("C2");
            lblDeliveryFee.Text = deliveryFee.ToString("C2");

            //calculate tax and display 
            tax = (tax * UserView.fltTOTAL);
            lblTax.Text = tax.ToString("C2");

            //calculate balance and display it in label
            fltTempTotal = total;
            total = UserView.fltTOTAL + (total * tax) + deliveryFee;
            lblTotal.Text = total.ToString("C2");

            //display items in cart
            foreach (UserView.item i in UserView.vc.lstOrderSummary.Items)
            {
                lstOrderSummary.Items.Add(i);
            }
            tax = 0.0825f;
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            try
              {
                if (!UserView.cart.Any())
                {
                    MessageBox.Show("Hmm it looks like the cart is empty.\n" +
                        "Click Add Items to continue shopping.", "Help", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                //checks user input
                if (String.IsNullOrWhiteSpace(txtMiddleName.Text))
                {
                    clsValidation.CheckInput(txtFirstName.Text, txtLastName.Text, txtCRV.Text);
                }
                else
                {
                    clsValidation.CheckInput(txtFirstName.Text, txtMiddleName.Text, txtLastName.Text, txtCRV.Text);
                }

                //checks to make sure credit card input is valide
                if (clsValidation.ValidateCard(txtCreditCard.Text))
                {

                    //capture current date
                    DateTime orderDate = DateTime.Now;
                    string strMonth = cboMonth.Text;
                    string strYear = cboYear.Text;
                    string strFull = strMonth + "/" + strYear;

                    DateTime.TryParse(strFull, out DateTime date);

                    //capture MM and yyyy
                    int intYear = orderDate.Year;
                    int intMonth = orderDate.Month;

                    //capture MM and yyyy from user date
                    int intUserYear = date.Year;
                    int intUserMonth = date.Month;

                    //make sure date is valid
                    if ((intUserYear >= intYear))
                    {
                        if (intUserYear == intYear)
                        {
                            if (intUserMonth < intMonth)
                            {
                                MessageBox.Show("Please select valid Month/Year.", "Invalide Date", MessageBoxButtons.OK);
                            }
                            else
                            {
                                SqlDataAdapter da = new SqlDataAdapter();
                                //cs.Open();
                                string payment = "";
                                int intCustomerID;

                                if (rdoVisa.Checked)
                                {
                                    payment = "VISA";
                                }
                                else if (rdoMastercard.Checked)
                                {
                                    payment = "MASTER CARD";
                                }

                                if (rdoStandard.Checked)
                                {
                                    strShipping = "STANDARD";
                                }
                                else
                                {
                                    strShipping = "EXPRESS";
                                }

                                string strQuery = "";
                                int intPOSCustomer;
                                if (ManagerView.bolPOSCustomer)
                                {
                                    intPOSCustomer = ManagerView.posCustomer.id;
                                    //query to store payment info into database
                                    strQuery = "insert into FurlowK21Fa2332.OrderTable values('" + intPOSCustomer + "', '" + orderDate + "', " +
                                        "'" + strShipping + "', '" + payment + "', '" + ManagerView.posCustomer.address + "', '" + ManagerView.posCustomer.address2 + "', '" + ManagerView.posCustomer.address3 + "'," +
                                        "'" + ManagerView.posCustomer.city + "', '" + ManagerView.posCustomer.state + "', '" + ManagerView.posCustomer.zipcode + "')";
                                    //run query to store data in database
                                    clsSQL.InsertOrder(strQuery);
                                }
                                else
                                {
                                    //stores customer id
                                    intCustomerID = clsSQL.newCustomer.id;
                                    //query to store payment info into database
                                    strQuery = "insert into FurlowK21Fa2332.OrderTable values('" + intCustomerID + "', '" + orderDate + "', " +
                                        "'" + strShipping + "', '" + payment + "', '" + clsSQL.newCustomer.address + "', '" + clsSQL.newCustomer.address2 + "', '" + clsSQL.newCustomer.address3 + "'," +
                                        "'" + clsSQL.newCustomer.city + "', '" + clsSQL.newCustomer.state + "', '" + clsSQL.newCustomer.zipcode + "')";
                                    //run query to store data in database
                                    clsSQL.InsertOrder(strQuery);
                                }
                               

                                PrintReceipt(CreateInvoice());
                                MessageBox.Show("Order Submitted!", "Thank You!!", MessageBoxButtons.OK);


                                List<item> itemList = new List<item>();
                                item newItem = new item();

                                //clear cart and list boxes
                                UserView.cart.Clear();
                                UserView.vc.lstOrderSummary.Items.Clear();
                                UserView.vc.lblTotal.Text = "";
                                UserView.fltTOTAL = 0;
                                fltAmountDiscount = 0;
                                intCount = 0;
                                bolCouponUsed = false;
                                UserView.intAmount = 0;
                                UserView.item[] newItemArr = new UserView.item[1000];
                                UserView.items = newItemArr;

                                if(ManagerView.bolPOSCustomer)
                                {
                                    //ask if the user wants to keep shopping
                                    DialogResult dlgResultOne = MessageBox.Show("Do you want to continue shopping?", "Continue",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (dlgResultOne == DialogResult.Yes) //if yes return to sale page
                                    {
                                        UserView uv = new UserView();
                                        uv.Show();
                                        this.Hide();
                                    }
                                    else //if no return to main menu
                                    {
                                        this.Hide();
                                        ManagerView manager = new ManagerView();
                                        manager.Show();
                                    }
                                }
                                else
                                {
                                    //ask if the user wants to keep shopping
                                    DialogResult dlgResult = MessageBox.Show("Do you want to continue shopping?", "Continue",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (dlgResult == DialogResult.Yes) //if yes return to sale page
                                    {
                                        UserView uv = new UserView();
                                        uv.Show();
                                        this.Hide();
                                    }
                                    else //if no return to main menu
                                    {
                                        this.Hide();
                                        frmMain main = new frmMain();
                                        main.Show();
                                    }
                                }
                            }
                        }
                        else
                        {

                            SqlDataAdapter da = new SqlDataAdapter();
                            //cs.Open();
                            string payment = "";
                            int intCustomerID;

                            if (rdoVisa.Checked)
                            {
                                payment = "VISA";
                            }
                            else if (rdoMastercard.Checked)
                            {
                                payment = "MASTER CARD";
                            }

                            if (rdoStandard.Checked)
                            {
                                strShipping = "STANDARD";
                            }
                            else
                            {
                                strShipping = "EXPRESS";
                            }

                            string strQuery = "";
                            int intPOSCustomer;
                            if (ManagerView.bolPOSCustomer)
                            {
                                intPOSCustomer = ManagerView.posCustomer.id;
                                //query to store payment info into database
                                strQuery = "insert into FurlowK21Fa2332.OrderTable values('" + intPOSCustomer + "', '" + orderDate + "', " +
                                    "'" + strShipping + "', '" + payment + "', '" + ManagerView.posCustomer.address + "', '" + ManagerView.posCustomer.address2 + "', '" + ManagerView.posCustomer.address3 + "'," +
                                    "'" + ManagerView.posCustomer.city + "', '" + ManagerView.posCustomer.state + "', '" + ManagerView.posCustomer.zipcode + "')";
                                //run query to store data in database
                                clsSQL.InsertOrder(strQuery);
                            }
                            else
                            {
                                //stores customer id
                                intCustomerID = clsSQL.newCustomer.id;
                                //query to store payment info into database
                                strQuery = "insert into FurlowK21Fa2332.OrderTable values('" + intCustomerID + "', '" + orderDate + "', " +
                                    "'" + strShipping + "', '" + payment + "', '" + clsSQL.newCustomer.address + "', '" + clsSQL.newCustomer.address2 + "', '" + clsSQL.newCustomer.address3 + "'," +
                                    "'" + clsSQL.newCustomer.city + "', '" + clsSQL.newCustomer.state + "', '" + clsSQL.newCustomer.zipcode + "')";
                                //run query to store data in database
                                clsSQL.InsertOrder(strQuery);
                            }

                            PrintReceipt(CreateInvoice());
                            MessageBox.Show("Order Submitted!", "Thank You!!", MessageBoxButtons.OK);

                            List<item> itemList = new List<item>();
                            item newItem = new item();

                            //clear cart and list boxes
                            UserView.cart.Clear();
                            UserView.vc.lstOrderSummary.Items.Clear();
                            UserView.vc.lblTotal.Text = "";
                            UserView.fltTOTAL = 0;
                            fltAmountDiscount = 0;
                            intCount = 0;
                            bolCouponUsed = false;
                            UserView.intAmount = 0;
                            UserView.item[] newItemArr = new UserView.item[1000];
                            UserView.items = newItemArr;
                            if (ManagerView.bolPOSCustomer)
                            {
                                //ask if the user wants to keep shopping
                                DialogResult dlgResultOne = MessageBox.Show("Do you want to continue shopping?", "Continue",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (dlgResultOne == DialogResult.Yes) //if yes return to sale page
                                {
                                    UserView uv = new UserView();
                                    uv.Show();
                                    this.Hide();
                                }
                                else //if no return to main menu
                                {
                                    this.Hide();
                                    ManagerView manager = new ManagerView();
                                    manager.Show();
                                }
                            }
                            else
                            {
                                //ask if the user wants to keep shopping
                                DialogResult dlgResult = MessageBox.Show("Do you want to continue shopping?", "Continue",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (dlgResult == DialogResult.Yes) //if yes return to sale page
                                {
                                    UserView uv = new UserView();
                                    uv.Show();
                                    this.Hide();
                                }
                                else //if no return to main menu
                                {
                                    this.Hide();
                                    frmMain main = new frmMain();
                                    main.Show();
                                }
                            }
                        }
                            
                    }
                    else
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        string payment = "";
                        int intCustomerID;

                        if (rdoVisa.Checked)
                        {
                            payment = "VISA";
                        }
                        else if (rdoMastercard.Checked)
                        {
                            payment = "MASTER CARD";
                        }

                        if (rdoStandard.Checked)
                        {
                            strShipping = "STANDARD";
                        }
                        else
                        {
                            strShipping = "EXPRESS";
                        }

                        string strQuery = "";
                        int intPOSCustomer;
                        if (ManagerView.bolPOSCustomer)
                        {
                            intPOSCustomer = ManagerView.posCustomer.id;
                            //query to store payment info into database
                            strQuery = "insert into FurlowK21Fa2332.OrderTable values('" + intPOSCustomer + "', '" + orderDate + "', " +
                                "'" + strShipping + "', '" + payment + "', '" + ManagerView.posCustomer.address + "', '" + ManagerView.posCustomer.address2 + "', '" + ManagerView.posCustomer.address3 + "'," +
                                "'" + ManagerView.posCustomer.city + "', '" + ManagerView.posCustomer.state + "', '" + ManagerView.posCustomer.zipcode + "')";
                            //run query to store data in database
                            clsSQL.InsertOrder(strQuery);
                        }
                        else
                        {
                            //stores customer id
                            intCustomerID = clsSQL.newCustomer.id;
                            //query to store payment info into database
                            strQuery = "insert into FurlowK21Fa2332.OrderTable values('" + intCustomerID + "', '" + orderDate + "', " +
                                "'" + strShipping + "', '" + payment + "', '" + clsSQL.newCustomer.address + "', '" + clsSQL.newCustomer.address2 + "', '" + clsSQL.newCustomer.address3 + "'," +
                                "'" + clsSQL.newCustomer.city + "', '" + clsSQL.newCustomer.state + "', '" + clsSQL.newCustomer.zipcode + "')";
                            //run query to store data in database
                            clsSQL.InsertOrder(strQuery);
                        }

                        PrintReceipt(CreateInvoice());
                        MessageBox.Show("Order Submitted!", "Thank You!!", MessageBoxButtons.OK);

                        List<item> itemList = new List<item>();
                        item newItem = new item();

                        //clear cart and list boxes
                        UserView.cart.Clear();
                        UserView.vc.lstOrderSummary.Items.Clear();
                        UserView.vc.lblTotal.Text = "";
                        UserView.fltTOTAL = 0;
                        fltAmountDiscount = 0;
                        intCount = 0;
                        bolCouponUsed = false;
                        UserView.intAmount = 0;
                        UserView.item[] newItemArr = new UserView.item[1000];
                        UserView.items = newItemArr;

                        if (ManagerView.bolPOSCustomer)
                        {
                            //ask if the user wants to keep shopping
                            DialogResult dlgResultOne = MessageBox.Show("Do you want to continue shopping?", "Continue",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dlgResultOne == DialogResult.Yes) //if yes return to sale page
                            {
                                UserView uv = new UserView();
                                uv.Show();
                                this.Hide();
                            }
                            else //if no return to main menu
                            {
                                this.Hide();
                                ManagerView manager = new ManagerView();
                                manager.Show();
                            }
                        }
                        else
                        {
                            //ask if the user wants to keep shopping
                            DialogResult dlgResult = MessageBox.Show("Do you want to continue shopping?", "Continue",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (dlgResult == DialogResult.Yes) //if yes return to sale page
                            {
                                UserView uv = new UserView();
                                uv.Show();
                                this.Hide();
                            }
                            else //if no return to main menu
                            {
                                this.Hide();
                                frmMain main = new frmMain();
                                main.Show();
                            }
                        }
                    }
                }
  
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error connecting to database \n\n" + ex.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error checking out \n\n" + ex.ToString());
            }
        }

        private void rdoStandard_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoStandard.Checked)
            {
                deliveryFee = 9.99f;
                lblDeliveryFee.Text = deliveryFee.ToString("C2");
                strShipping = "STANDARD";

                float add = 0;
                add = total + deliveryFee - 9.99f;
                lblTotal.Text = add.ToString("C2");
            }
        }

        private void rdoExpress_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoExpress.Checked)
            {
                deliveryFee = 15.99f;
                lblDeliveryFee.Text = deliveryFee.ToString("C2");
                strShipping = "EXPRESS";
                float add = 0;
                add = total + deliveryFee - 9.99f;
                lblTotal.Text = add.ToString("C2");
            }
        }

        private void PrintReceipt(StringBuilder html)
        {
            try
            {
                using (StreamWriter wr = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Order.html"))
                {
                    wr.WriteLine(html);
                }
                System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Order.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //help menu
        private void lnkNeedHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("For coupons enter code then click Coupon to activate discount\n" +
                "1. Select Payment Type then fill out information(First name, Last name, etc.)\n" +
                "2. Select a Delivery Type\n" +
                "3. Click Cancel Order to cancel the order and clear the cart\n" +
                "4. Click Add Items to continue shopping\n" +
                "5. Click Check Out to complete the order\n" +
                "6. Click Main to return to the Main Menu", "Helpful Information", MessageBoxButtons.OK);
        }

        //closes application
        private void CheckOut_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        int intCount = 0;
        float fltAmountDiscount;
        bool bolCouponUsed = false;
        float fltTotal = 0.0f;
        private void btnCoupon_Click(object sender, EventArgs e)
        {
            string[] strCoupons = new string[] { "333UNIVERSE", "COMMANDO2021", "WRECKCENTRAL99" };
            float fltDiscount = 0.0f;
            fltTotal = 0.0f;
            string strCouponText = txtCoupon.Text;

            string strUserCoupon = txtCoupon.Text;
            DateTime currentDate = DateTime.Now;

            if(String.IsNullOrWhiteSpace(txtCoupon.Text))
            {
                MessageBox.Show("Please enter coupon code.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (bolCouponUsed == false && intCount == 0)
            {
                foreach(ManagerView.coupon coupon in ManagerView.couponArr)
                {
                    if(coupon == null)
                    {
                        MessageBox.Show("Coupon code not valid.", "Invalid Input",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;  
                    }
                    if((strUserCoupon == coupon.couponName && coupon.couponExpDate >= currentDate) && coupon != null)
                    {
                        fltDiscount = coupon.couponDiscount;
                        fltAmountDiscount = total * fltDiscount;
                        lblTotal.Text = total.ToString("C2");
                        lblDiscountAmount.Text = fltAmountDiscount.ToString("C2");
                        fltTotal = total - fltAmountDiscount;
                        lblDiscount.Text = fltTotal.ToString("C2");
                        bolCouponUsed = true;
                        intCount = 1;
                        break;
                    }
                }
            }
        }

        //closes the form when user clicks 'X'
        private void CheckOut_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Are you sure you want to exit? Your cart will be cleared", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgResult == DialogResult.Yes)
            {
                List<item> itemList = new List<item>();
                item newItem = new item();

                //clear cart and list boxes
                UserView.cart.Clear();
                UserView.vc.lstOrderSummary.Items.Clear();
                UserView.vc.lblTotal.Text = "";
                UserView.fltTOTAL = 0;
                fltAmountDiscount = 0;
                intCount = 0;
                bolCouponUsed = false;
                UserView.intAmount = 0;
                UserView.item[] newItemArr = new UserView.item[1000];
                UserView.items = newItemArr;
                frmMain main = new frmMain();
                main.Show();
            }
            else if (dlgResult == DialogResult.No)
                e.Cancel = true;
        }

        private void txtCreditCard_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateCreditCardInput(e);
        }

        //place cursor furthest left when clicking txtCreditCard
        private void txtCreditCard_Click(object sender, EventArgs e)
        {
            this.txtCreditCard.Select(0, 0);
        }

        private void btnUserView_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserView uv = new UserView();
            uv.Show();
        }
    }
}
