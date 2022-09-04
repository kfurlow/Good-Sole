//*******************************************
//*******************************************
// Programmer: Kanin Furlow
// Course: INEW 2332.{7Z1} (Final Project)
// Program Description: A foot wear application that sells a wide variety of shoes ranging
// from track & field to formal wear. 
//*******************************************
// Form Purpose: This form will be for the managers. Allowing them to add/remove/update items
// in the database as well as add/remove/update other managers profiles. The manager can also create
// coupons. The POS Check Out is another system the manager can use which will allow them to
// search for a customer and check as said customer.
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
    public partial class ManagerView : Form
    {
        //creating database connection
        SqlConnection cs = new SqlConnection("Data Source=cstnt.tstc.edu;Initial Catalog=inew2332fa21;Persist Security Info=True;User ID=FurlowK21Fa2332;Password=1782473");
        DataSet dataSet = new DataSet();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
        ViewCart vc = new ViewCart();
        int intPersonID = 0;
        //int to store item id
        int itemID = 0;
        //bool to check if item is already discontinued
        bool bolDiscontinued = false;
        //bool to check if person was deleted
        bool bolPersonDeleted = false;
        //int to store customerID
        int intCustomerID = 0;
        //bool to check if customer was deleted
        bool bolCustomerDeleted = false;
        public ManagerView()
        {
            InitializeComponent();

            cboPercent.SelectedIndex = 0;
            //display all items from Inventory table in database
            try
            {
                string strQuery = "SELECT * FROM FurlowK21Fa2332.Inventory";
                dataSet = clsSQL.PopulateItemDataSet(strQuery);
                itemTable.DataSource = dataSet.Tables[0];
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error loading items from database\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //display all data from Person in database that is a manager
            try
            {
                //string for where condition in query
                string strPosition = "Manager";
                string strQuery = "SELECT FurlowK21Fa2332.Person.PersonID, Title, NameFirst, NameMiddle, NameLast, Address1, Address2, Address3, City, Zipcode, State, Email, PhonePrimary, PhoneSecondary, PersonDeleted, FurlowK21Fa2332.Logon.AccountDeleted " +
                    "FROM FurlowK21Fa2332.Person INNER JOIN FurlowK21Fa2332.Logon ON FurlowK21fa2332.Person.PersonID = FurlowK21fa2332.Logon.PersonID WHERE PositionTitle = '" + strPosition + "'";
                //pull all managers from database
                DataSet personDataSet = new DataSet();
                personDataSet = clsSQL.PopulateManagerDataSet(strQuery);
                personTable.DataSource = personDataSet.Tables[0];
            }
            catch (SqlException exc)
            {
                MessageBox.Show("Error loading Managers from database\n" + exc.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //display all data from Person in database that is a customer
            try
            {
                //string for where condition in query
                string strPosition = "Customer";
                string strQuery = "SELECT FurlowK21Fa2332.Person.PersonID, Title, NameFirst, NameMiddle, NameLast, Address1, Address2, Address3, City, Zipcode, State, Email, PhonePrimary, PhoneSecondary, PersonDeleted, FurlowK21Fa2332.Logon.AccountDeleted " +
                    "FROM FurlowK21Fa2332.Person INNER JOIN FurlowK21Fa2332.Logon ON FurlowK21fa2332.Person.PersonID = FurlowK21fa2332.Logon.PersonID WHERE PositionTitle = '" + strPosition + "'";
                DataSet customerDataSet = new DataSet();
                customerDataSet = clsSQL.PopulateCustomerDataSet(strQuery);
                customerTable.DataSource = customerDataSet.Tables[0];
            }
            catch (SqlException exc)
            {
                MessageBox.Show("Error loading Managers from database\n" + exc.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ManagerView_Load(object sender, EventArgs e)
        {

        }

        //create a method to save picture
        private byte[]SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            picProduct.Image.Save(ms, picProduct.Image.RawFormat);
            return ms.GetBuffer();
        }

        //doesn't allow special characters for item name
        private void txtProductName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) && char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //only allows letters for product description
        private void txtProductDesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //validates that entered data in decimal
        private void txtRetail_KeyPress(object sender, KeyPressEventArgs e)
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

        //validates that entered data in decimal
        private void txtSupplier_KeyPress(object sender, KeyPressEventArgs e)
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

        //makes sure input only allows whole numbers
        private void txtInventory_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //refreshes itemTable to display current items in the database
        public void RefreshItemTable()
        {
            //display all items from Inventory table in database
            try
            {
                string strQuery = "SELECT * FROM FurlowK21Fa2332.Inventory";
                dataSet.Clear();
                dataSet = clsSQL.PopulateItemDataSet(strQuery);
                itemTable.DataSource = dataSet.Tables[0];
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error loading items from database\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RefreshCustomerTable()
        {
            //display all data from Person in database that is a customer
            try
            {
                //string for where condition in query
                string strPosition = "Customer";
                string strQuery = "SELECT FurlowK21Fa2332.Person.PersonID, Title, NameFirst, NameMiddle, NameLast, Address1, Address2, Address3, City, Zipcode, State, Email, PhonePrimary, PhoneSecondary, PersonDeleted, FurlowK21Fa2332.Logon.AccountDeleted " +
                    "FROM FurlowK21Fa2332.Person INNER JOIN FurlowK21Fa2332.Logon ON FurlowK21fa2332.Person.PersonID = FurlowK21fa2332.Logon.PersonID WHERE PositionTitle = '" + strPosition + "'";
                DataSet customerDataSet = new DataSet();
                customerDataSet.Clear();
                customerDataSet = clsSQL.PopulateCustomerDataSet(strQuery);
                customerTable.DataSource = customerDataSet.Tables[0];
            }
            catch (SqlException exc)
            {
                MessageBox.Show("Error loading Managers from database\n" + exc.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //displays data in labels from cellclick
        private void itemTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblItemID.Text = itemTable[0, itemTable.CurrentRow.Index].Value.ToString();
            lblItemName.Text = itemTable[1, itemTable.CurrentRow.Index].Value.ToString();
            lblQuantity.Text = itemTable[5, itemTable.CurrentRow.Index].Value.ToString();
            lblRetailCost.Text = itemTable[3, itemTable.CurrentRow.Index].Value.ToString();
            lblSupplierCost.Text = itemTable[4, itemTable.CurrentRow.Index].Value.ToString();
            bolDiscontinued = (bool)itemTable[7, itemTable.CurrentRow.Index].Value;
            var data = (Byte[])(itemTable[6, itemTable.CurrentRow.Index].Value);
            var stream = new MemoryStream(data);
            picItemImage.Image = Image.FromStream(stream);
            itemID = int.Parse(lblItemID.Text);

            txtUInventoryQty.Text = itemTable[5, itemTable.CurrentRow.Index].Value.ToString();
            txtUProductDesc.Text = itemTable[2, itemTable.CurrentRow.Index].Value.ToString();
            txtUProductName.Text = itemTable[1, itemTable.CurrentRow.Index].Value.ToString();
            txtUProductRetail.Text = itemTable[3, itemTable.CurrentRow.Index].Value.ToString();
            txtUSupplierPrice.Text = itemTable[4, itemTable.CurrentRow.Index].Value.ToString();
            txtUInventoryQty.Text = itemTable[5, itemTable.CurrentRow.Index].Value.ToString();
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            if (UserView.cart.Any())
            {
                foreach (UserView.item item in vc.lstOrderSummary.Items)
                {
                    int intQty = item.purchaseAmount;
                    string strDeduct = "UPDATE FurlowK21Fa2332.Inventory SET Quantity += '" + intQty + "' WHERE InventoryID = '" + item.itemID + "'";
                    SqlCommand sqlCmd = new SqlCommand(strDeduct, cs);
                    sqlCmd.ExecuteNonQuery();
                }
            }
            frmMain main = new frmMain();
            this.Hide();
            main.Show();
        }

        //click event to browse for picture
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select picture";
            dialog.Filter = "jpg files(*.jpg)|*.jpg|png files(*.png)|*.png|All files(*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                picProduct.Image = new Bitmap(dialog.FileName);
            }
        }

        //refreshes personTable to display current data
        public void RefreshPersonTable()
        {
            //display all data from Person in database that is a manager
            try
            {
                //string for where condition in query
                string strPosition = "Manager";
                string strQuery = "SELECT FurlowK21Fa2332.Person.PersonID, Title, NameFirst, NameMiddle, NameLast, Address1, Address2, Address3, City, Zipcode, State, Email, PhonePrimary, PhoneSecondary, PersonDeleted, FurlowK21Fa2332.Logon.AccountDeleted " +
                    "FROM FurlowK21Fa2332.Person INNER JOIN FurlowK21Fa2332.Logon ON FurlowK21fa2332.Person.PersonID = FurlowK21fa2332.Logon.PersonID WHERE PositionTitle = '" + strPosition + "'";
                //pull all managers from database
                DataSet personDataSet = new DataSet();
                personDataSet.Clear();
                personDataSet = clsSQL.PopulateManagerDataSet(strQuery);
                personTable.DataSource = personDataSet.Tables[0];
            }
            catch (SqlException exc)
            {
                MessageBox.Show("Error loading Managers from database\n" + exc.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //displays content from cell click to labels and textboxes
        private void itemTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lblItemID.Text = itemTable[0, itemTable.CurrentRow.Index].Value.ToString();
            lblItemName.Text = itemTable[1, itemTable.CurrentRow.Index].Value.ToString();
            lblQuantity.Text = itemTable[5, itemTable.CurrentRow.Index].Value.ToString();
            lblRetailCost.Text = itemTable[3, itemTable.CurrentRow.Index].Value.ToString();
            lblSupplierCost.Text = itemTable[4, itemTable.CurrentRow.Index].Value.ToString();
            bolDiscontinued = (bool)itemTable[7, itemTable.CurrentRow.Index].Value;
            var data = (Byte[])(itemTable[6, itemTable.CurrentRow.Index].Value);
            var stream = new MemoryStream(data);
            picItemImage.Image = Image.FromStream(stream);
            itemID = int.Parse(lblItemID.Text);

            txtUInventoryQty.Text = itemTable[5, itemTable.CurrentRow.Index].Value.ToString();
            txtUProductDesc.Text = itemTable[2, itemTable.CurrentRow.Index].Value.ToString();
            txtUProductName.Text = itemTable[1, itemTable.CurrentRow.Index].Value.ToString();
            txtUProductRetail.Text = itemTable[3, itemTable.CurrentRow.Index].Value.ToString();
            txtUSupplierPrice.Text = itemTable[4, itemTable.CurrentRow.Index].Value.ToString();
            txtUInventoryQty.Text = itemTable[5, itemTable.CurrentRow.Index].Value.ToString();
        }

        float fltManagerSalary = 0.00f;
        //display data in textboxes on cell click
        private void personTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            intPersonID = int.Parse(personTable[0, personTable.CurrentRow.Index].Value.ToString());
            cboUpdateTitle.Text = personTable[1, personTable.CurrentRow.Index].Value.ToString();
            txtUpdateFirst.Text = personTable[2, personTable.CurrentRow.Index].Value.ToString();
            txtUpdateMiddle.Text = personTable[3, personTable.CurrentRow.Index].Value.ToString();
            txtUpdateLast.Text = personTable[4, personTable.CurrentRow.Index].Value.ToString();
            txtUpdateAddress1.Text = personTable[5, personTable.CurrentRow.Index].Value.ToString();
            txtUpdateAddress2.Text = personTable[6, personTable.CurrentRow.Index].Value.ToString();
            txtUpdateAddress3.Text = personTable[7, personTable.CurrentRow.Index].Value.ToString();
            txtUpdateCity.Text = personTable[8, personTable.CurrentRow.Index].Value.ToString();
            txtUpdateZipcode.Text = personTable[9, personTable.CurrentRow.Index].Value.ToString();
            string strUpdateState = personTable[10, personTable.CurrentRow.Index].Value.ToString();
            cboUpdateState.Text = strUpdateState;
            txtUpdateEmail.Text = personTable[11, personTable.CurrentRow.Index].Value.ToString();
            txtUpdatePrimaryNum.Text = personTable[12, personTable.CurrentRow.Index].Value.ToString();
            txtUpdateSecondaryNum.Text = personTable[13, personTable.CurrentRow.Index].Value.ToString();
            bolPersonDeleted = (bool)personTable[14, personTable.CurrentRow.Index].Value;
            lblEditing.Text = txtUpdateFirst.Text + " " + txtUpdateLast.Text;
            fltManagerSalary = clsSQL.GetSalary(intPersonID);
            txtUpdateSalary.Text = fltManagerSalary.ToString("N");

            //string to hold first and last name
            string strFirst = txtUpdateFirst.Text;
            string strLast = txtUpdateLast.Text;

            //display manager to be removed in labels
            lblRemoveManagerID.Text = intPersonID.ToString();
            lblManagerName.Text = strFirst + " " + strLast;
        }

        //updates manager information
        private void btnUpdateManager_Click(object sender, EventArgs e)
        {
            try
            {
                bool bolCheckInput = false;
                //capture all user input and store in variables
                string strPhone = txtUpdatePrimaryNum.Text;
                strPhone = strPhone.Replace("-", string.Empty);
                string strPhoneAlt = txtUpdateSecondaryNum.Text;
                strPhoneAlt = strPhoneAlt.Replace("-", string.Empty);
                string strZipcode = txtUpdateZipcode.Text;
                strZipcode = strZipcode.Replace("-", string.Empty);
                strZipcode = strZipcode.Replace("_", string.Empty);
                string strTitle = cboUpdateTitle.Text;
                string strFirst = txtUpdateFirst.Text;
                string strMiddle = txtUpdateMiddle.Text;
                string strLast = txtUpdateLast.Text;
                string strSuffix = cboUpdateSuffix.Text;
                string strAddress1 = txtUpdateAddress1.Text;
                string strAddress2 = txtUpdateAddress2.Text;
                string strAddress3 = txtUpdateAddress3.Text;
                string strCity = txtUpdateCity.Text;
                string strState = cboUpdateState.Text;
                string strEmail = txtUpdateEmail.Text;
                string strSalary = txtUpdateSalary.Text;

                if(String.IsNullOrWhiteSpace(strSalary))
                {
                    MessageBox.Show("Please enter Salary for manager", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    strSalary = strSalary.Trim(',');
                    float fltSalary = float.Parse(strSalary);

                    clsSQL.GetSalary(intPersonID);
                    bolCheckInput = clsValidation.CheckInput(txtUpdateFirst.Text, txtUpdateMiddle.Text, txtUpdateLast.Text, txtUpdateEmail.Text, strPhone, txtUpdateAddress1.Text,
                        txtUpdateAddress2.Text, txtUpdateAddress3.Text, txtUpdateCity.Text, strZipcode, strPhoneAlt);

                    if (bolCheckInput)
                    {
                        clsSQL.UpdatePerson(strTitle, strFirst, strMiddle, strLast, strSuffix, strAddress1, strAddress2,
                            strAddress3, strCity, strState, strZipcode, strEmail, strPhone, strPhoneAlt, intPersonID);

                        //query to update the manager salary
                        clsSQL.UpdateManagerSalary(fltSalary, intPersonID);

                        //updates personTable with current data
                        RefreshPersonTable();

                        //clear textboxes
                        cboUpdateTitle.SelectedIndex = 0;
                        cboUpdateSuffix.SelectedIndex = 0;
                        cboUpdateState.SelectedIndex = 0;
                        txtUpdateAddress1.Text = "";
                        txtUpdateAddress2.Text = "";
                        txtUpdateAddress3.Text = "";
                        txtUpdateCity.Text = "";
                        txtUpdateEmail.Text = "";
                        txtUpdateFirst.Text = "";
                        txtUpdateLast.Text = "";
                        txtUpdateMiddle.Text = "";
                        txtUpdatePrimaryNum.Text = "";
                        txtUpdateSecondaryNum.Text = "";
                        txtUpdateZipcode.Text = "";
                        txtUpdateSalary.Text = "";

                        //give user feedback
                        MessageBox.Show("Manager information updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch(SqlException ex)
            {
                cs.Close();
                MessageBox.Show("Error updating manager information\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //lets user select image for upload
        private void btnBrowse_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select picture";
            dialog.Filter = "jpg files(*.jpg)|*.jpg|png files(*.png)|*.png|All files(*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                picProduct.Image = new Bitmap(dialog.FileName);
            }
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            try
            {
                //make sure there is data present to be added to database
                if (String.IsNullOrWhiteSpace(txtProductName.Text) || String.IsNullOrWhiteSpace(txtProductDesc.Text) || String.IsNullOrWhiteSpace(txtRetail.Text)
                    || String.IsNullOrWhiteSpace(txtSupplier.Text) || String.IsNullOrWhiteSpace(txtInventory.Text))
                {
                    MessageBox.Show("Some of the data is empty. Please fill in all categories", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (clsValidation.CheckIfDecimal(txtRetail) && clsValidation.CheckIfDecimal(txtSupplier))
                {
                    string strItemName = txtProductName.Text;
                    string strItemDesc = txtProductDesc.Text;
                    string strItemRetail = txtRetail.Text;
                    string strItemCost = txtSupplier.Text;
                    string strInventory = txtInventory.Text;
                    byte[] byteImageArr = SavePhoto();
                    clsSQL.InsertItem(strItemName, strItemDesc, strItemRetail, strItemCost,strInventory, byteImageArr);

                    txtInventory.Text = "";
                    txtProductDesc.Text = "";
                    txtProductName.Text = "";
                    txtRetail.Text = "";
                    txtSupplier.Text = "";

                    //refresh itemTable
                    RefreshItemTable();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error adding item to database.\n" + ex.ToString());
            }
        }

        private void btnUpdateItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                //make sure user data isn't null or empty
                if (String.IsNullOrWhiteSpace(txtUSupplierPrice.Text) && String.IsNullOrWhiteSpace(txtUProductRetail.Text) && String.IsNullOrWhiteSpace(txtUProductDesc.Text)
                    && String.IsNullOrWhiteSpace(txtUProductName.Text))
                {
                    MessageBox.Show("Some of the data is empty. Please fill in all categories", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (clsValidation.CheckIfDecimal(txtUProductRetail) && clsValidation.CheckIfDecimal(txtUSupplierPrice))
                {
                    string strItemName = txtUProductName.Text;
                    string strItemDesc = txtUProductDesc.Text;
                    string strItemRetail = txtUProductRetail.Text;
                    string strItemCost = txtUSupplierPrice.Text;
                    string strItemQty = txtUInventoryQty.Text;
                    clsSQL.UpdateItem(strItemName, strItemDesc, strItemRetail, strItemCost, strItemQty, itemID);
                    //refresh itemTable
                    RefreshItemTable();
                }
            }
            catch (SqlException ex)
            {
                cs.Close();
                MessageBox.Show("Error updating items in database\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //displays content from cell click to labels and textboxes
        private void btnRemove_Click_2(object sender, EventArgs e)
        {
            try
            {
                if (itemID == 0 || bolDiscontinued == true)
                {
                    MessageBox.Show("Please select an item to remove.", "Select Item", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    clsSQL.DisableItem(itemID);
                    RefreshItemTable();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error removing item from database\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUProductRetail_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePrice(sender, e);
        }

        private void txtUSupplierPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePrice(sender, e);
        }

        private void txtUInventoryQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePhoneInput(e);
        }

        private void txtRetail_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePrice(sender, e);
        }

        private void txtSupplier_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePrice(sender, e);
        }

        private void txtInventory_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePhoneInput(e);
        }

        private void btnAddManager_Click(object sender, EventArgs e)
        {
            try
            {
                bool bolCheckInput = false;

                string strPhone = txtAddPrimaryNum.Text;
                strPhone = strPhone.Replace("-", string.Empty);
                string strPhoneAlt = txtAddSecondaryNum.Text;
                strPhoneAlt = strPhoneAlt.Replace("-", string.Empty);
                string strZipcode = txtAddZipcode.Text;
                strZipcode = strZipcode.Replace("-", string.Empty);
                strZipcode = strZipcode.Replace("_", string.Empty);

                //capture username and password
                string strUsername, strPassword;
                strPassword = txtAddPassword.Text;
                strUsername = txtAddUsername.Text;
                if (String.IsNullOrEmpty(cboSecurity1.Text) || String.IsNullOrEmpty(cboSecurity2.Text) || String.IsNullOrEmpty(cboSecurity3.Text))
                {
                    MessageBox.Show("Please select security questions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (String.IsNullOrWhiteSpace(txtAnswer1.Text) || String.IsNullOrWhiteSpace(txtAnswer2.Text) || String.IsNullOrWhiteSpace(txtAnswer3.Text))
                {
                    MessageBox.Show("Please answer security questions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (String.IsNullOrWhiteSpace(txtSalary.Text))
                {
                    MessageBox.Show("Please enter Salary for manager", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (clsLogon.ValidatePassword(strPassword) && clsLogon.UniqueUsername(strUsername) && clsLogon.ValidateUsername(strUsername))
                    {
                        bolCheckInput = clsValidation.CheckInput(txtAddFirst.Text, txtAddMiddle.Text, txtAddLast.Text, txtAddEmail.Text, strPhone, txtAddAddress1.Text,
                    txtAddAddress2.Text, txtAddAddress3.Text, txtAddCity.Text, strZipcode, strPhoneAlt);
                        if (bolCheckInput)
                        {

                            //capturing and inserting customer account information into database
                            clsSQL.InsertData(cboAddTitle.Text, txtAddFirst.Text, txtAddMiddle.Text, txtAddLast.Text, cboAddSuffix.Text, txtAddAddress1.Text, txtAddAddress2.Text, txtAddAddress3.Text,
                                txtAddCity.Text, cboAddState.Text, strZipcode, txtAddEmail.Text, strPhone, strPhoneAlt);

                            //string to store answers and set all input to lower case
                            string strAnswer1, strAnswer2, strAnswer3, strAnswerLow1, strAnswerLow2, strAnswerLow3, strUsernameLow;
                            strAnswer1 = txtAnswer1.Text;
                            strAnswer2 = txtAnswer2.Text;
                            strAnswer3 = txtAnswer3.Text;
                            strAnswerLow1 = strAnswer1.ToLower();
                            strAnswerLow2 = strAnswer2.ToLower();
                            strAnswerLow3 = strAnswer3.ToLower();
                            strUsernameLow = strUsername.ToLower();
                            string strPosition = "Manager";
                            //storing logon information into database
                            if (clsSQL.InsertLoginInfo(strUsernameLow, strPassword, cboSecurity1.Text, cboSecurity2.Text, cboSecurity3.Text, strAnswerLow1, strAnswerLow2, strAnswerLow3, strPosition))
                            {
                                //query to update salary information
                                string strSalary = txtSalary.Text;
                                float fltSalary = float.Parse(strSalary);
                                clsSQL.InsertSalary(fltSalary);
                                

                                MessageBox.Show("Manager successfully added to database.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                RefreshPersonTable();

                                //clear data from textboxes
                                txtAddPrimaryNum.Text = "";
                                txtAddSecondaryNum.Text = "";
                                txtAddUsername.Text = "";
                                txtAddPassword.Text = "";
                                txtAddFirst.Text = "";
                                txtAddLast.Text = "";
                                txtAddMiddle.Text = "";
                                cboAddState.SelectedIndex = 0;
                                cboAddTitle.SelectedIndex = 0;
                                cboSecurity1.SelectedIndex = 0;
                                cboSecurity2.SelectedIndex = 0; 
                                cboSecurity3.SelectedIndex = 0;
                                txtAnswer1.Text = "";
                                txtAnswer2.Text = "";
                                txtAnswer3.Text = "";
                                txtAddEmail.Text = "";
                                txtAddAddress1.Text = "";
                                txtAddAddress2.Text = "";
                                txtAddAddress3.Text = "";
                                txtAddCity.Text = "";
                                txtAddZipcode.Text = "";
                                cboAddSuffix.Items.Clear();
                                txtSalary.Text = "";
                            }
                        }
                    }
                    
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error adding manager to database\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUpdateFirst_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateNameInput(e);
        }

        private void txtUpdateMiddle_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateNameInput(e);
        }

        private void txtUpdateLast_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateNameInput(e);
        }

        private void txtUpdatePrimaryNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePhoneInput(e);
        }

        private void txtUpdateSecondaryNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePhoneInput(e);
        }

        private void txtUpdateCity_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateNameInput(e);
        }

        private void txtUpdateZipcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePhoneInput(e);
        }

        private void txtUpdateAddress1_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateAddressInput(e);
        }

        private void txtUpdateAddress2_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateAddressInput(e);
        }

        private void txtUpdateAddress3_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateAddressInput(e);
        }

        private void txtAddFirst_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateNameInput(e);
        }

        private void txtAddMiddle_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateNameInput(e);
        }

        private void txtAddLast_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateNameInput(e);
        }

        private void txtAddAddress1_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateAddressInput(e);
        }

        private void txtAddAddress2_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateAddressInput(e);
        }

        private void txtAddAddress3_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateAddressInput(e);
        }

        private void txtAddCity_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateNameInput(e);
        }

        private void txtAddZipcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePhoneInput(e);
        }

        private void txtAddPrimaryNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePhoneInput(e);
        }

        private void txtAddSecondaryNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePhoneInput(e);
        }

        private void rdoShow_Click(object sender, EventArgs e)
        {
            if (rdoShow.Checked && txtAddPassword.PasswordChar == '*')
            {
                txtAddPassword.PasswordChar = '\0';
            }
            else if (rdoShow.Checked)
            {
                txtAddPassword.PasswordChar = '*';
            }
        }

        private void txtAddUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateUsernameInput(e);
        }

        private void txtAddPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePasswordInput(e);
        }

        //makes manager inaccessible
        private void btnRemoveManager_Click(object sender, EventArgs e)
        {
            try
            {
                if (intPersonID == 0 || bolPersonDeleted == true)
                {
                    MessageBox.Show("Please select an manager to remove.", "Select Item", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    clsSQL.RemovePerson(intPersonID);

                    MessageBox.Show("Manager successfully removed from database.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshPersonTable();
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error removing Manager from database\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //grabs all the customer data and stores it in labels and textboxes
        private void customerTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            intCustomerID = int.Parse(customerTable[0, customerTable.CurrentRow.Index].Value.ToString());
            cboUpdateCustTitle.Text = customerTable[1, customerTable.CurrentRow.Index].Value.ToString();
            txtUpdateCustFirst.Text = customerTable[2, customerTable.CurrentRow.Index].Value.ToString();
            txtUpdateCustMiddle.Text = customerTable[3, customerTable.CurrentRow.Index].Value.ToString();
            txtUpdateCustLast.Text = customerTable[4, customerTable.CurrentRow.Index].Value.ToString();
            txtUpdateCustAddress1.Text = customerTable[5, customerTable.CurrentRow.Index].Value.ToString();
            txtUpdateCustAddress2.Text = customerTable[6, customerTable.CurrentRow.Index].Value.ToString();
            txtUpdateCustAddress3.Text = customerTable[7, customerTable.CurrentRow.Index].Value.ToString();
            txtUpdateCustCity.Text = customerTable[8, customerTable.CurrentRow.Index].Value.ToString();
            txtUpdateCustZipcode.Text = customerTable[9, customerTable.CurrentRow.Index].Value.ToString();
            string strUpdateState = customerTable[10, customerTable.CurrentRow.Index].Value.ToString();
            cboUpdateCustState.Text = strUpdateState;
            txtUpdateCustEmail.Text = customerTable[11, customerTable.CurrentRow.Index].Value.ToString();
            txtUpdateCustPrimary.Text = customerTable[12, customerTable.CurrentRow.Index].Value.ToString();
            txtUpdateCustSecondary.Text = customerTable[13, customerTable.CurrentRow.Index].Value.ToString();
            bolCustomerDeleted = (bool)customerTable[14, customerTable.CurrentRow.Index].Value;
            lblEditingCust.Text = txtUpdateFirst.Text + " " + txtUpdateLast.Text;

            //string to hold first and last name
            string strFirst = txtUpdateCustFirst.Text;
            string strLast = txtUpdateCustLast.Text;

            //display customer to be removed in labels
            lblCustomerID.Text = intCustomerID.ToString();
            lblCustomerName.Text = strFirst + " " + strLast;
        }

        //update customer information
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                bool bolCheckInput = false;

                string strPhone = txtUpdateCustPrimary.Text;
                strPhone = strPhone.Replace("-", string.Empty);
                string strPhoneAlt = txtUpdateCustSecondary.Text;
                strPhoneAlt = strPhoneAlt.Replace("-", string.Empty);
                string strZipcode = txtUpdateCustZipcode.Text;
                strZipcode = strZipcode.Replace("-", string.Empty);
                strZipcode = strZipcode.Replace("_", string.Empty);
                string strTitle = cboUpdateCustTitle.Text;
                string strFirst = txtUpdateCustFirst.Text;
                string strMiddle = txtUpdateCustMiddle.Text;
                string strLast = txtUpdateCustLast.Text;
                string strSuffix = cboUpdateCustSuffix.Text;
                string strAddress1 = txtUpdateCustAddress1.Text;
                string strAddress2 = txtUpdateCustAddress2.Text;
                string strAddress3 = txtUpdateCustAddress3.Text;
                string strCity = txtUpdateCustCity.Text;
                string strState = cboUpdateCustState.Text;
                string strEmail = txtUpdateCustEmail.Text;
                bolCheckInput = clsValidation.CheckInput(txtUpdateCustFirst.Text, txtUpdateCustMiddle.Text, txtUpdateCustLast.Text, txtUpdateCustEmail.Text, strPhone, txtUpdateCustAddress1.Text,
                    txtUpdateCustAddress2.Text, txtUpdateCustAddress3.Text, txtUpdateCustCity.Text, strZipcode, strPhoneAlt);

                if (bolCheckInput)
                {
                    clsSQL.UpdatePerson(strTitle, strFirst, strMiddle, strLast, strSuffix, strAddress1, strAddress2,
                        strAddress3, strCity, strState, strZipcode, strEmail, strPhone, strPhoneAlt, intCustomerID);

                    //updates personTable with current data
                    RefreshCustomerTable();

                    //clear textboxes
                    cboUpdateCustTitle.Items.Clear();
                    cboUpdateCustSuffix.Items.Clear();
                    cboUpdateCustState.Items.Clear();
                    txtUpdateCustAddress1.Text = "";
                    txtUpdateCustAddress2.Text = "";
                    txtUpdateCustAddress3.Text = "";
                    txtUpdateCustCity.Text = "";
                    txtUpdateCustEmail.Text = "";
                    txtUpdateCustFirst.Text = "";
                    txtUpdateCustLast.Text = "";
                    txtUpdateCustMiddle.Text = "";
                    txtUpdateCustPrimary.Text = "";
                    txtUpdateCustSecondary.Text = "";
                    txtUpdateCustZipcode.Text = "";

                    //give user feedback
                    MessageBox.Show("Manager information updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException ex)
            {
                cs.Close();
                MessageBox.Show("Error updating manager information\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemoveCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                if (intCustomerID == 0 || bolCustomerDeleted == true)
                {
                    MessageBox.Show("Please select a customer to remove.", "Select Item", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    clsSQL.RemovePerson(intCustomerID);
                    MessageBox.Show("Customer successfully removed from database.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshCustomerTable();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error removing Customer from database\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAddCustAddress1_TextChanged(object sender, EventArgs e)
        {

        }

        //adds new customer to database
        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                bool bolCheckInput = false;

                string strPhone = txtAddCustPrimary.Text;
                strPhone = strPhone.Replace("-", string.Empty);
                string strPhoneAlt = txtAddCustSecondary.Text;
                strPhoneAlt = strPhoneAlt.Replace("-", string.Empty);
                string strZipcode = txtAddZipcode.Text;
                strZipcode = strZipcode.Replace("-", string.Empty);
                strZipcode = strZipcode.Replace("_", string.Empty);

                //capture username and password
                string strUsername, strPassword;
                strPassword = txtAddCustPassword.Text;
                strUsername = txtAddCustUsername.Text;
                if (String.IsNullOrEmpty(cboAddCustQuestion1.Text) || String.IsNullOrEmpty(cboAddCustQuestion2.Text) || String.IsNullOrEmpty(cboAddCustQuestion3.Text))
                {
                    MessageBox.Show("Please select security questions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (String.IsNullOrWhiteSpace(txtAddCustAnswer1.Text) || String.IsNullOrWhiteSpace(txtAddCustAnswer2.Text) || String.IsNullOrWhiteSpace(txtAddCustAnswer3.Text))
                {
                    MessageBox.Show("Please answer security questions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (clsLogon.ValidatePassword(strPassword) && clsLogon.UniqueUsername(strUsername) && clsLogon.ValidateUsername(strUsername))
                    {
                        bolCheckInput = clsValidation.CheckInput(txtAddCustFirst.Text, txtAddCustMiddle.Text, txtAddCustLast.Text, txtAddCustEmail.Text, strPhone, txtAddCustAddress1.Text,
                    txtAddCustAddress2.Text, txtAddCustAddress3.Text, txtAddCustCity.Text, strZipcode, strPhoneAlt);
                        if (bolCheckInput)
                        {

                            //capturing and inserting customer account information into database
                            clsSQL.InsertData(cboAddCustTitle.Text, txtAddCustFirst.Text, txtAddCustMiddle.Text, txtAddCustLast.Text, cboAddCustSuffix.Text, txtAddCustAddress1.Text, txtAddCustAddress2.Text, txtAddCustAddress3.Text,
                                txtAddCustCity.Text, cboAddCustState.Text, strZipcode, txtAddCustEmail.Text, strPhone, strPhoneAlt);

                            //string to store answers and set all input to lower case
                            string strAnswer1, strAnswer2, strAnswer3, strAnswerLow1, strAnswerLow2, strAnswerLow3, strUsernameLow;
                            strAnswer1 = txtAddCustAnswer1.Text;
                            strAnswer2 = txtAddCustAnswer2.Text;
                            strAnswer3 = txtAddCustAnswer3.Text;
                            strAnswerLow1 = strAnswer1.ToLower();
                            strAnswerLow2 = strAnswer2.ToLower();
                            strAnswerLow3 = strAnswer3.ToLower();
                            strUsernameLow = strUsername.ToLower();
                            string strPosition = "Customer";
                            //storing logon information into database
                            if (clsSQL.InsertLoginInfo(strUsernameLow, strPassword, cboAddCustQuestion1.Text, cboAddCustQuestion2.Text, cboAddCustQuestion3.Text, strAnswerLow1, strAnswerLow2, strAnswerLow3, strPosition))
                            {
                                MessageBox.Show("Customer successfully added to database.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                RefreshPersonTable();

                                //clear data from textboxes
                                txtAddCustPrimary.Text = "";
                                txtAddCustSecondary.Text = "";
                                txtAddCustUsername.Text = "";
                                txtAddCustPassword.Text = "";
                                txtAddCustFirst.Text = "";
                                txtAddCustLast.Text = "";
                                txtAddCustMiddle.Text = "";
                                cboAddCustState.Items.Clear();
                                cboAddCustTitle.Items.Clear();
                                cboAddCustQuestion1.Items.Clear();
                                cboAddCustQuestion2.Items.Clear();
                                cboAddCustQuestion3.Items.Clear();
                                txtAddCustAnswer1.Text = "";
                                txtAddCustAnswer2.Text = "";
                                txtAddCustAnswer3.Text = "";
                                txtAddCustEmail.Text = "";
                                txtAddCustAddress1.Text = "";
                                txtAddCustAddress2.Text = "";
                                txtAddCustAddress3.Text = "";
                                txtAddCustCity.Text = "";
                                txtAddCustZipcode.Text = "";
                                cboAddCustSuffix.Items.Clear();
                            }
                        }
                    }

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error adding customer to database\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAddCustFirst_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateNameInput(e);
        }

        private void txtAddCustMiddle_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateNameInput(e);
        }

        private void txtAddCustLast_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateNameInput(e);
        }

        private void txtAddCustAddress1_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateAddressInput(e);
        }

        private void txtAddCustAddress2_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateAddressInput(e);
        }

        private void txtAddCustAddress3_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateAddressInput(e);
        }

        private void txtAddCustCity_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateAddressInput(e);
        }

        private void txtAddCustZipcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePhoneInput(e);
        }

        private void txtAddCustPrimary_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePhoneInput(e);
        }

        private void txtAddCustSecondary_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePhoneInput(e);
        }

        private void txtAddCustUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateUsernameInput(e);
        }

        private void txtAddCustPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePasswordInput(e);
        }

        private void txtUpdateCustFirst_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateNameInput(e);
        }

        private void txtUpdateCustMiddle_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateNameInput(e);
        }

        private void txtUpdateCustLast_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateNameInput(e);
        }

        private void txtUpdateCustAddress1_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateAddressInput(e);
        }

        private void txtUpdateCustAddress2_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateAddressInput(e);
        }

        private void txtUpdateCustAddress3_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateAddressInput(e);
        }

        private void txtUpdateCustCity_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateNameInput(e);
        }

        private void txtUpdateCustZipcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePhoneInput(e);
        }

        private void txtUpdateCustPrimary_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePhoneInput(e);
        }

        private void txtUpdateCustSecondary_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePhoneInput(e);
        }

        private void rdoShowCustPass_Click(object sender, EventArgs e)
        {
            if (rdoShowCustPass.Checked && txtAddCustPassword.PasswordChar == '*')
            {
                txtAddCustPassword.PasswordChar = '\0';
            }
            else if (rdoShow.Checked)
            {
                txtAddCustPassword.PasswordChar = '*';
            }
        }
        public static bool bolPOSCustomer = false;
        //customer class to store user information
        public class customer
        {
            public string fName { get; set; }
            public string lName { get; set; }
            public string mName { get; set; }
            public string title { get; set; }
            public string suffix { get; set; }
            public string address { get; set; }
            public string address2 { get; set; }
            public string address3 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string zipcode { get; set; }
            public string position { get; set; }
            public float salary { get; set; }
            public int id { get; set; }

            public override string ToString()
            {
                return fName + " " + mName + " " + lName;
            }
        }
        //class to store item information
        public class item
        {
            public string itemDescription { get; set; }
            public string itemName { get; set; }
            public float itemPrice { get; set; }
            public float itemCost { get; set; }
            public int itemID { get; set; }
            public int itemInventory { get; set; }
            public Byte[] itemImage { get; set; }
            public int purchaseAmount { get; set; }
            public bool itemAvailability { get; set; }

            public int intCounter = 0;
            public override string ToString()
            {
                return itemName + " " + itemPrice.ToString("C2") + " x" + purchaseAmount;
            }
        }

        public static customer posCustomer = new customer();
        private void btnPOSCheckOut_Click(object sender, EventArgs e)
        {
            string strPhone = txtUpdateCustPrimary.Text;
            strPhone = strPhone.Replace("-", string.Empty);
            string strZipcode = txtUpdateCustZipcode.Text;
            strZipcode = strZipcode.Replace("-", string.Empty);
            strZipcode = strZipcode.Replace("_", string.Empty);
            if (intCustomerID != 0)
            {
                posCustomer.fName = txtUpdateCustFirst.Text;
                posCustomer.mName = txtUpdateCustMiddle.Text;
                posCustomer.lName = txtUpdateCustLast.Text;
                posCustomer.id = intCustomerID;
                posCustomer.address = txtUpdateCustAddress1.Text;
                posCustomer.address2 = txtUpdateCustAddress2.Text;
                posCustomer.address2 = txtUpdateCustAddress3.Text;
                posCustomer.city = txtUpdateCustCity.Text;
                posCustomer.state = cboUpdateCustState.Text;
                posCustomer.phone = strPhone;
                posCustomer.zipcode = strZipcode;
                bolPOSCustomer = true;
            }
            this.Hide();
            UserView userView = new UserView();
            userView.Show();

        }
        public static SqlConnection con = new SqlConnection("Data Source=cstnt.tstc.edu;Initial Catalog=inew2332fa21;Persist Security Info=True;User ID=FurlowK21Fa2332;Password=1782473");
        //variables to store POS Customer information for checkOut
        string strFirst, strLast, strMiddle, strAddress1, strAddress2, strAddress3, strCity, strState, strPhone, strZipcode;
        //pulls all manager information from database
        private StringBuilder ManagerInvoice()
        {
            StringBuilder html = new StringBuilder();
            StringBuilder css = new StringBuilder();
            css.Append("<style>");
            css.Append("table {width: 800px;}");
            css.Append("th {text-align:left;}");
            css.Append("table, th, td{border: 1px solid #000;border-collapse: collapse;}");
            css.Append("caption{font:bold;}");
            css.Append("h1 {font:bold 100% Georgia; letter-spacing:0.5em;text-align: center; text-transform: uppercase;}");
            css.Append("</style>");
            html.Append("<html>");
            html.Append($"<head>{css}<title>{"Good Sole"}</title></head>");
            html.Append("<body>");
            html.Append($"<h1>{"Good Sole"}</h1>");
            html.Append("<table>");
            html.Append("<caption><h3>Manager Information</caption></h3>");
            html.Append("<tr><th>Title</th><th>First Name</th><th>Middle Name</th><th>Last Name</th><th>Suffix</th><th>Phone</th>" +
                "<th>Email</th><th>Position</th><th>Salary</th></tr>");

            string strQuery = "SELECT FurlowK21Fa2332.Person.PersonID, Title, NameFirst, NameMiddle, NameLast, Suffix, Address1, Address2, Address3, City, Zipcode, State, Email, PhonePrimary, PhoneSecondary, PersonDeleted, FurlowK21Fa2332.Logon.AccountDeleted " +
                    "FROM FurlowK21Fa2332.Person INNER JOIN FurlowK21Fa2332.Logon ON FurlowK21fa2332.Person.PersonID = FurlowK21fa2332.Logon.PersonID WHERE PositionTitle = 'Manager' AND PersonDeleted = 0";
            SqlCommand sqlCommand = new SqlCommand(strQuery, cs);

            cs.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            
            while (sqlDataReader.Read())
            {
                customer manager = new customer();
                manager.id = int.Parse(sqlDataReader["PersonID"].ToString());
                manager.fName = sqlDataReader["NameFirst"].ToString();
                manager.lName = sqlDataReader["NameLast"].ToString();
                manager.mName = sqlDataReader["NameMiddle"].ToString();
                manager.address = sqlDataReader["Address1"].ToString();
                manager.address2 = sqlDataReader["Address2"].ToString();
                manager.address3 = sqlDataReader["Address3"].ToString();
                manager.city = sqlDataReader["City"].ToString();
                manager.state = sqlDataReader["State"].ToString();
                manager.zipcode = sqlDataReader["Zipcode"].ToString();
                manager.title = sqlDataReader["Title"].ToString();
                manager.suffix = sqlDataReader["Suffix"].ToString();
                manager.phone = sqlDataReader["PhonePrimary"].ToString();
                manager.salary = 45000.00f;
                manager.position = "Manager";
                manager.email = sqlDataReader["Email"].ToString();
                bool bolPersonDeleted = (bool)sqlDataReader["AccountDeleted"];

                //query to pull managers salary
                string strSalaryQuery = "SELECT Salary FROM FurlowK21fa2332.EmployeeSalary WHERE PersonID = '" +manager.id+"'";
                SqlCommand sqlCommandSalary = new SqlCommand(strSalaryQuery, con);
                con.Open();
                SqlDataReader sqlSalaryReader = sqlCommandSalary.ExecuteReader();
                sqlSalaryReader.Read();
                manager.salary = float.Parse(sqlSalaryReader["Salary"].ToString());
                con.Close();

                html.Append("<tr><td>" + manager.title + "</td>" +
                           "<td>" + manager.fName + "</td>" +
                           "<td>" + manager.mName + "</td>" +
                           "<td>" + manager.lName + "</td>" +
                           "<td>" + manager.suffix + "</td>" +
                           "<td>" + manager.phone + "</td>" +
                           "<td>" + manager.email + "</td>" +
                           "<td>" + manager.position + "</td>" +
                           "<td>" + manager.salary + "</td></tr>");
            }
            cs.Close();
            html.Append("</table>");
            html.Append("<br>");

            html.Append("</body></html>");
            return html;

        }

        private void PrintReceipt(StringBuilder html)
        {
            try
            {
                using (StreamWriter wr = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ManagerInfo.html"))
                {
                    wr.WriteLine(html);
                }
                System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ManagerInfo.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrintManagerInfo_Click(object sender, EventArgs e)
        {
            PrintReceipt(ManagerInvoice());
        }
        private StringBuilder InventoryInvoice()
        {
            StringBuilder html = new StringBuilder();
            StringBuilder css = new StringBuilder();
            css.Append("<style>");
            css.Append("table {width: 800px;}");
            css.Append("th {text-align:left;}");
            css.Append("table, th, td{border: 1px solid #000;border-collapse: collapse;}");
            css.Append("caption{font:bold;}");
            css.Append("h1 {font:bold 100% Georgia; letter-spacing:0.5em;text-align: center; text-transform: uppercase;}");
            css.Append("</style>");
            html.Append("<html>");
            html.Append($"<head>{css}<title>{"Good Sole"}</title></head>");
            html.Append("<body>");
            html.Append($"<h1>{"Good Sole"}</h1>");
            html.Append("<table>");
            html.Append("<caption><h3>All Available Items</caption></h3>");
            html.Append("<tr><th>Item ID</th><th>Item Name</th><th>Item Retail</th><th>Item Cost</th><th>Quantity</th><th>Discontinued</th></tr>");

            string strQuery = "select * from FurlowK21fa2332.Inventory where Discontinued = 0";
            SqlCommand sqlCommand = new SqlCommand(strQuery, cs);
            cs.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                item item = new item();
                item.itemDescription = sqlDataReader["ItemDescription"].ToString();
                item.itemID = int.Parse(sqlDataReader["InventoryID"].ToString());
                item.itemPrice = float.Parse(sqlDataReader["RetailPrice"].ToString());
                item.itemName = sqlDataReader["ItemName"].ToString();
                item.itemInventory = int.Parse(sqlDataReader["Quantity"].ToString());
                item.itemCost = float.Parse(sqlDataReader["Cost"].ToString());
                item.itemAvailability = (bool)sqlDataReader["Discontinued"];
                Byte[] image = ((byte[])sqlDataReader["ItemImage"]);

                html.Append("<tr><td>" + item.itemID + "</td>" +
                           "<td>" + item.itemName + "</td>" +
                           "<td>" + item.itemPrice.ToString("C2") + "</td>" +
                           "<td>" + item.itemCost.ToString("C2") + "</td>" +
                           "<td>" + item.itemInventory + "</td>" +
                           "<td>" + item.itemAvailability + "</td></tr>");
                            
            }
            cs.Close();
            html.Append("</table>");
            html.Append("<br>");

            html.Append("</body></html>");
            return html;

        }
        private void PrintInventoryReceipt(StringBuilder html)
        {
            try
            {
                using (StreamWriter wr = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\InventoryInfo.html"))
                {
                    wr.WriteLine(html);
                }
                System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\InventoryInfo.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrintAll_Click(object sender, EventArgs e)
        {
            PrintInventoryReceipt(InventoryInvoice());
        }
        private StringBuilder AllInventoryInvoice()
        {
            StringBuilder html = new StringBuilder();
            StringBuilder css = new StringBuilder();
            css.Append("<style>");
            css.Append("table {width: 800px;}");
            css.Append("th {text-align:left;}");
            css.Append("table, th, td{border: 1px solid #000;border-collapse: collapse;}");
            css.Append("caption{font:bold;}");
            css.Append("h1 {font:bold 100% Georgia; letter-spacing:0.5em;text-align: center; text-transform: uppercase;}");
            css.Append("</style>");
            html.Append("<html>");
            html.Append($"<head>{css}<title>{"Good Sole"}</title></head>");
            html.Append("<body>");
            html.Append($"<h1>{"Good Sole"}</h1>");
            html.Append("<table>");
            html.Append("<caption><h3>All Inventory Items</caption></h3>");
            html.Append("<tr><th>Item ID</th><th>Item Name</th><th>Item Retail</th><th>Item Cost</th><th>Quantity</th><th>Discontinued</th></tr>");

            string strQuery = "select * from FurlowK21fa2332.Inventory";
            SqlCommand sqlCommand = new SqlCommand(strQuery, cs);
            cs.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                item itemDisplay = new item();
                itemDisplay.itemDescription = sqlDataReader["ItemDescription"].ToString();
                itemDisplay.itemID = int.Parse(sqlDataReader["InventoryID"].ToString());
                itemDisplay.itemPrice = float.Parse(sqlDataReader["RetailPrice"].ToString());
                itemDisplay.itemName = sqlDataReader["ItemName"].ToString();
                itemDisplay.itemInventory = int.Parse(sqlDataReader["Quantity"].ToString());
                itemDisplay.itemCost = float.Parse(sqlDataReader["Cost"].ToString());
                itemDisplay.itemAvailability = (bool)sqlDataReader["Discontinued"];

                html.Append("<tr><td>" + itemDisplay.itemID + "</td>" +
                           "<td>" + itemDisplay.itemName + "</td>" +
                           "<td>" + itemDisplay.itemPrice.ToString("C2") + "</td>" +
                           "<td>" + itemDisplay.itemCost.ToString("C2") + "</td>" +
                           "<td>" + itemDisplay.itemInventory + "</td>" + 
                           "<td>" + itemDisplay.itemAvailability + "</td></tr>");
            }
            cs.Close();
            html.Append("</table>");
            html.Append("<br>");

            html.Append("</body></html>");
            return html;

        }
        private void PrintInventoryAvailableReceipt(StringBuilder html)
        {
            try
            {
                using (StreamWriter wr = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\InventoryInfo.html"))
                {
                    wr.WriteLine(html);
                }
                System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\InventoryInfo.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //prints all items from database that are both discontinued and available
        private void btnPrintAllAvailableItems_Click(object sender, EventArgs e)
        {
            PrintInventoryAvailableReceipt(AllInventoryInvoice());
        }

        private void btnPrintRestock_Click(object sender, EventArgs e)
        {
            PrintInventoryRestockReceipt(InventoryRestockInvoice());
        }
        //prints html for all items that need to be restocked
        private StringBuilder InventoryRestockInvoice()
        {
            StringBuilder html = new StringBuilder();
            StringBuilder css = new StringBuilder();
            css.Append("<style>");
            css.Append("table {width: 800px;}");
            css.Append("th {text-align:left;}");
            css.Append("table, th, td{border: 1px solid #000;border-collapse: collapse;}");
            css.Append("caption{font:bold;}");
            css.Append("h1 {font:bold 100% Georgia; letter-spacing:0.5em;text-align: center; text-transform: uppercase;}");
            css.Append("</style>");
            html.Append("<html>");
            html.Append($"<head>{css}<title>{"Good Sole"}</title></head>");
            html.Append("<body>");
            html.Append($"<h1>{"Good Sole"}</h1>");
            html.Append("<table>");
            html.Append("<caption><h3>Restock Inventory Information</caption></h3>");
            html.Append("<tr><th>Item ID</th><th>Item Name</th><th>Item Retail</th><th>Item Cost</th><th>Quantity</th><th>Available</th></tr>");

            string strQuery = "select * from FurlowK21fa2332.Inventory where Quantity <= 25 and Discontinued = 0";
            SqlCommand sqlCommand = new SqlCommand(strQuery, cs);
            cs.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                item itemDisplay = new item();
                itemDisplay.itemDescription = sqlDataReader["ItemDescription"].ToString();
                itemDisplay.itemID = int.Parse(sqlDataReader["InventoryID"].ToString());
                itemDisplay.itemPrice = float.Parse(sqlDataReader["RetailPrice"].ToString());
                itemDisplay.itemName = sqlDataReader["ItemName"].ToString();
                itemDisplay.itemInventory = int.Parse(sqlDataReader["Quantity"].ToString());
                itemDisplay.itemCost = float.Parse(sqlDataReader["Cost"].ToString());
                itemDisplay.itemAvailability = (bool)sqlDataReader["Discontinued"];

                html.Append("<tr><td>" + itemDisplay.itemID + "</td>" +
                           "<td>" + itemDisplay.itemName + "</td>" +
                           "<td>" + itemDisplay.itemPrice.ToString("C2") + "</td>" +
                           "<td>" + itemDisplay.itemCost.ToString("C2") + "</td>" +
                           "<td>" + itemDisplay.itemInventory + "</td>" +
                           "<td>" + itemDisplay.itemAvailability + "</td></tr>");
            }
            cs.Close();
            html.Append("</table>");
            html.Append("<br>");

            html.Append("</body></html>");
            return html;

        }
        private void PrintInventoryRestockReceipt(StringBuilder html)
        {
            try
            {
                using (StreamWriter wr = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\InventoryRestockInfo.html"))
                {
                    wr.WriteLine(html);
                }
                System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\InventoryRestockInfo.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //strings for monthly sales total query
        string strAddDayMonthly = "", strDateMonthly = "";
        //displays monthly sales totals
        private void btnMonthlyTotal_Click(object sender, EventArgs e)
        {
            DateTime Date = dateTimeSalesTotal.Value;
            strAddDayMonthly = Date.AddDays(30).ToString("yyyy-MM-dd");
            strDateMonthly = dateTimeSalesTotal.Text;
            PrintMonthlySalesTotal(MonthlySalesTotal());
        }
        private StringBuilder MonthlySalesTotal()
        {
            StringBuilder html = new StringBuilder();
            StringBuilder css = new StringBuilder();
            css.Append("<style>");
            css.Append("table {width: 800px;}");
            css.Append("th {text-align:left;}");
            css.Append("table, th, td{border: 1px solid #000;border-collapse: collapse;}");
            css.Append("caption{font:bold;}");
            css.Append("h1 {font:bold 100% Georgia; letter-spacing:0.5em;text-align: center; text-transform: uppercase;}");
            css.Append("</style>");
            html.Append("<html>");
            html.Append($"<head>{css}<title>{"Good Sole"}</title></head>");
            html.Append("<body>");
            html.Append($"<h1>{"Good Sole"}</h1>");
            html.Append("<table>");
            html.Append("<caption><h3>Weekly Sales Total</caption></h3>");
            html.Append("<tr><th>Total Sales</th><th>Start Date</th><th>End Date</th></tr>");

            string strQuery = "SELECT SUM(FurlowK21fa2332.OrderDetail.ItemPrice) AS TotalSales FROM FurlowK21fa2332.OrderDetail INNER JOIN FurlowK21fa2332.OrderTable ON FurlowK21fa2332.OrderDetail.OrderID = FurlowK21fa2332.OrderTable.OrderID " +
                "WHERE FurlowK21fa2332.OrderTable.OrderDate >= '" + strDateMonthly + "' AND FurlowK21fa2332.OrderTable.OrderDate < '" + strAddDayMonthly + "'";
            SqlCommand sqlCommand = new SqlCommand(strQuery, cs);
            cs.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            if (sqlDataReader.Read())
            {
                string strTotalSales = sqlDataReader["TotalSales"].ToString();
                float fltTotalDailySales = float.Parse(strTotalSales);

                html.Append("<tr><td>" + fltTotalDailySales.ToString("C2") + "</td>" +
                           "<td>" + strDateMonthly + "</td>" +
                           "<td>" + strAddDayMonthly + "</td></tr>");
            }
            cs.Close();
            html.Append("</table>");
            html.Append("<br>");

            html.Append("</body></html>");
            return html;

        }
        private void PrintMonthlySalesTotal(StringBuilder html)
        {
            try
            {
                using (StreamWriter wr = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\InventoryRestockInfo.html"))
                {
                    wr.WriteLine(html);
                }
                System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\InventoryRestockInfo.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //string for daily totals query
        string strAddDayWeekly = "", strDateWeekly = "";
        //displays weekly sales totals 
        private void btnWeeklyTotal_Click(object sender, EventArgs e)
        {
            DataSet dailyTotalDataSet = new DataSet();
            DateTime Date = dateTimeSalesTotal.Value;
            strAddDayWeekly = Date.AddDays(7).ToString("yyyy-MM-dd");
            strDateWeekly = dateTimeSalesTotal.Text;
            dailyTotalDataSet = clsSQL.DisplayWeeklyTotal(strDateWeekly, strAddDayWeekly);
            PrintWeeklySalesTotal(WeeklySalesTotal());
        }
        private StringBuilder WeeklySalesTotal()
        {
            StringBuilder html = new StringBuilder();
            StringBuilder css = new StringBuilder();
            css.Append("<style>");
            css.Append("table {width: 800px;}");
            css.Append("th {text-align:left;}");
            css.Append("table, th, td{border: 1px solid #000;border-collapse: collapse;}");
            css.Append("caption{font:bold;}");
            css.Append("h1 {font:bold 100% Georgia; letter-spacing:0.5em;text-align: center; text-transform: uppercase;}");
            css.Append("</style>");
            html.Append("<html>");
            html.Append($"<head>{css}<title>{"Good Sole"}</title></head>");
            html.Append("<body>");
            html.Append($"<h1>{"Good Sole"}</h1>");
            html.Append("<table>");
            html.Append("<caption><h3>Weekly Sales Total</caption></h3>");
            html.Append("<tr><th>Total Sales</th><th>Start Date</th><th>End Date</th></tr>");

            string strQuery = "SELECT SUM(FurlowK21fa2332.OrderDetail.ItemPrice) AS TotalSales FROM FurlowK21fa2332.OrderDetail INNER JOIN FurlowK21fa2332.OrderTable ON FurlowK21fa2332.OrderDetail.OrderID = FurlowK21fa2332.OrderTable.OrderID " +
                "WHERE FurlowK21fa2332.OrderTable.OrderDate >= '" + strDateWeekly + "' AND FurlowK21fa2332.OrderTable.OrderDate < '" + strAddDayWeekly + "'";
            SqlCommand sqlCommand = new SqlCommand(strQuery, cs);
            cs.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            if (sqlDataReader.Read())
            {
                string strTotalSales = sqlDataReader["TotalSales"].ToString();
                float fltTotalDailySales = float.Parse(strTotalSales);

                html.Append("<tr><td>" + fltTotalDailySales.ToString("C2") + "</td>" +
                           "<td>" + strDateWeekly + "</td>" +
                           "<td>" + strAddDayWeekly + "</td></tr>");
            }
            cs.Close();
            html.Append("</table>");
            html.Append("<br>");

            html.Append("</body></html>");
            return html;

        }
        private void PrintWeeklySalesTotal(StringBuilder html)
        {
            try
            {
                using (StreamWriter wr = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\InventoryRestockInfo.html"))
                {
                    wr.WriteLine(html);
                }
                System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\InventoryRestockInfo.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //class to store coupon information
        public class coupon
        {
            public string couponName { get; set; }
            public DateTime couponDate { get; set; }
            public DateTime couponExpDate { get; set; }
            public float couponDiscount { get; set; }
        }
        public static coupon[] couponArr = new coupon[5];
        int intAmountOfCoupons = 0;
        private void btnCreateCoupon_Click(object sender, EventArgs e)
        {
            //declare variable to store name and discount percent
            string strCouponName = txtCouponName.Text;
            //declare variable to parse out float value from cboPercent
            string strDiscount = cboPercent.Text;
            strDiscount = strDiscount.Replace("%", "");
            strDiscount = strDiscount.Insert(0, ".");
            float fltDiscount = float.Parse(strDiscount);
            //declare variable to store present date and coupon exp date
            DateTime presentDate = DateTime.Now;
            DateTime expireDate = dtpCoupon.Value;
            //if coupon exp date is less than present date
            if(presentDate > expireDate) //give user feedback
            {
                MessageBox.Show("Expiration date is invalid. \nPlease select future date", "Invalid Date",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (intAmountOfCoupons > 5)
            {
                MessageBox.Show("Maximum amount of coupons created.", "MAXIMUM",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //create coupon and store coupon info
                coupon coupons = new coupon();
                coupons.couponName = strCouponName;
                coupons.couponDate = presentDate;
                coupons.couponExpDate = expireDate;
                coupons.couponDiscount = fltDiscount;
                couponArr[intAmountOfCoupons] = coupons;
                intAmountOfCoupons++;

                MessageBox.Show("Coupon created.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnUserView_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserView uv = new UserView();
            uv.Show();
        }

        private void ManagerView_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmMain main = new frmMain();
            main.Show();
        }

        private void txtUpdateSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePrice(sender, e);
        }

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePrice(sender, e);
        }

        private void txtCouponName_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateNameInput(e);
        }

        private void lnkNeedHelpReports_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HelpFile helpFile = new HelpFile();
            helpFile.Show();
        }

        //string for daily totals query
        string strAddDayDaily = "", strDateDaily = "";
        //displays daily sales totals
        private void btnDailyTotal_Click(object sender, EventArgs e)
        {
            DateTime Date = dateTimeSalesTotal.Value;
            strAddDayDaily = Date.AddDays(1).ToString("yyyy-MM-dd");
            strDateDaily = dateTimeSalesTotal.Text;
            PrintDailySalesTotal(DailySalesTotal());

        }
        private StringBuilder DailySalesTotal()
        {
            StringBuilder html = new StringBuilder();
            StringBuilder css = new StringBuilder();
            css.Append("<style>");
            css.Append("table {width: 800px;}");
            css.Append("th {text-align:left;}");
            css.Append("table, th, td{border: 1px solid #000;border-collapse: collapse;}");
            css.Append("caption{font:bold;}");
            css.Append("h1 {font:bold 100% Georgia; letter-spacing:0.5em;text-align: center; text-transform: uppercase;}");
            css.Append("</style>");
            html.Append("<html>");
            html.Append($"<head>{css}<title>{"Good Sole"}</title></head>");
            html.Append("<body>");
            html.Append($"<h1>{"Good Sole"}</h1>");
            html.Append("<table>");
            html.Append("<caption><h3>Daily Sales Total</caption></h3>");
            html.Append("<tr><th>Total Sales</th><th>Start Date</th><th>End Date</th></tr>");

            string strQuery = "SELECT SUM(FurlowK21fa2332.OrderDetail.ItemPrice) AS TotalSales FROM FurlowK21fa2332.OrderDetail INNER JOIN FurlowK21fa2332.OrderTable ON FurlowK21fa2332.OrderDetail.OrderID = FurlowK21fa2332.OrderTable.OrderID " +
                "WHERE FurlowK21fa2332.OrderTable.OrderDate >= '" + strDateDaily + "' AND FurlowK21fa2332.OrderTable.OrderDate < '" + strAddDayDaily + "'";
            SqlCommand sqlCommand = new SqlCommand(strQuery, cs);
            cs.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            if (sqlDataReader.Read())
            {
                string strTotalSales = sqlDataReader["TotalSales"].ToString();
                float fltTotalDailySales = float.Parse(strTotalSales);

                html.Append("<tr><td>" + fltTotalDailySales.ToString("C2") + "</td>" +
                           "<td>" + strDateDaily + "</td>" +
                           "<td>" + strAddDayDaily + "</td></tr>"); 
            }
            cs.Close();
            html.Append("</table>");
            html.Append("<br>");

            html.Append("</body></html>");
            return html;

        }
        private void PrintDailySalesTotal(StringBuilder html)
        {
            try
            {
                using (StreamWriter wr = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\InventoryRestockInfo.html"))
                {
                    wr.WriteLine(html);
                }
                System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\InventoryRestockInfo.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAddCustomer_Click_1(object sender, EventArgs e)
        {
            try
            {
                bool bolCheckInput = false;

                string strPhone = txtAddCustPrimary.Text;
                strPhone = strPhone.Replace("-", string.Empty);
                string strPhoneAlt = txtAddCustSecondary.Text;
                strPhoneAlt = strPhoneAlt.Replace("-", string.Empty);
                string strZipcode = txtAddZipcode.Text;
                strZipcode = strZipcode.Replace("-", string.Empty);
                strZipcode = strZipcode.Replace("_", string.Empty);

                //capture username and password
                string strUsername, strPassword;
                strPassword = txtAddCustPassword.Text;
                strUsername = txtAddCustUsername.Text;
                if (String.IsNullOrEmpty(cboAddCustQuestion1.Text) || String.IsNullOrEmpty(cboAddCustQuestion2.Text) || String.IsNullOrEmpty(cboAddCustQuestion3.Text))
                {
                    MessageBox.Show("Please select security questions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (String.IsNullOrWhiteSpace(txtAddCustAnswer1.Text) || String.IsNullOrWhiteSpace(txtAddCustAnswer2.Text) || String.IsNullOrWhiteSpace(txtAddCustAnswer3.Text))
                {
                    MessageBox.Show("Please answer security questions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (clsLogon.ValidatePassword(strPassword) && clsLogon.UniqueUsername(strUsername) && clsLogon.ValidateUsername(strUsername))
                    {
                        bolCheckInput = clsValidation.CheckInput(txtAddCustFirst.Text, txtAddCustMiddle.Text, txtAddCustLast.Text, txtAddCustEmail.Text, strPhone, txtAddCustAddress1.Text,
                    txtAddCustAddress2.Text, txtAddCustAddress3.Text, txtAddCustCity.Text, strZipcode, strPhoneAlt);
                        if (bolCheckInput)
                        {

                            //capturing and inserting customer account information into database
                            clsSQL.InsertData(cboAddCustTitle.Text, txtAddCustFirst.Text, txtAddCustMiddle.Text, txtAddCustLast.Text, cboAddCustSuffix.Text, txtAddCustAddress1.Text, txtAddCustAddress2.Text, txtAddCustAddress3.Text,
                                txtAddCustCity.Text, cboAddCustState.Text, strZipcode, txtAddCustEmail.Text, strPhone, strPhoneAlt);

                            //string to store answers and set all input to lower case
                            string strAnswer1, strAnswer2, strAnswer3, strAnswerLow1, strAnswerLow2, strAnswerLow3, strUsernameLow;
                            strAnswer1 = txtAddCustAnswer1.Text;
                            strAnswer2 = txtAddCustAnswer2.Text;
                            strAnswer3 = txtAddCustAnswer3.Text;
                            strAnswerLow1 = strAnswer1.ToLower();
                            strAnswerLow2 = strAnswer2.ToLower();
                            strAnswerLow3 = strAnswer3.ToLower();
                            strUsernameLow = strUsername.ToLower();
                            string strPosition = "Customer";
                            //storing logon information into database
                            if (clsSQL.InsertLoginInfo(strUsernameLow, strPassword, cboAddCustQuestion1.Text, cboAddCustQuestion2.Text, cboAddCustQuestion3.Text, strAnswerLow1, strAnswerLow2, strAnswerLow3, strPosition))
                            {
                                MessageBox.Show("Customer successfully added to database.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                RefreshPersonTable();

                                //clear data from textboxes
                                txtAddCustPrimary.Text = "";
                                txtAddCustSecondary.Text = "";
                                txtAddCustUsername.Text = "";
                                txtAddCustPassword.Text = "";
                                txtAddCustFirst.Text = "";
                                txtAddCustLast.Text = "";
                                txtAddCustMiddle.Text = "";
                                cboAddCustState.Items.Clear();
                                cboAddCustTitle.Items.Clear();
                                cboAddCustQuestion1.Items.Clear();
                                cboAddCustQuestion2.Items.Clear();
                                cboAddCustQuestion3.Items.Clear();
                                txtAddCustAnswer1.Text = "";
                                txtAddCustAnswer2.Text = "";
                                txtAddCustAnswer3.Text = "";
                                txtAddCustEmail.Text = "";
                                txtAddCustAddress1.Text = "";
                                txtAddCustAddress2.Text = "";
                                txtAddCustAddress3.Text = "";
                                txtAddCustCity.Text = "";
                                txtAddCustZipcode.Text = "";
                                cboAddCustSuffix.Items.Clear();
                            }
                        }
                    }

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error adding customer to database\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        int intPOSCustomerID = 0;
        private void posCustomerCheckOut_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            intPOSCustomerID = int.Parse(posCustomerCheckOut[0, posCustomerCheckOut.CurrentRow.Index].Value.ToString());
            strFirst = posCustomerCheckOut[2, posCustomerCheckOut.CurrentRow.Index].Value.ToString();
            strLast = posCustomerCheckOut[4, posCustomerCheckOut.CurrentRow.Index].Value.ToString();
            strMiddle = posCustomerCheckOut[3, posCustomerCheckOut.CurrentRow.Index].Value.ToString();
            strAddress1 = posCustomerCheckOut[6, posCustomerCheckOut.CurrentRow.Index].Value.ToString();
            strAddress2 = posCustomerCheckOut[7, posCustomerCheckOut.CurrentRow.Index].Value.ToString();
            strAddress3 = posCustomerCheckOut[8, posCustomerCheckOut.CurrentRow.Index].Value.ToString();
            strCity = posCustomerCheckOut[9, posCustomerCheckOut.CurrentRow.Index].Value.ToString();
            strZipcode = posCustomerCheckOut[10, posCustomerCheckOut.CurrentRow.Index].Value.ToString();
            strState = posCustomerCheckOut[11, posCustomerCheckOut.CurrentRow.Index].Value.ToString();
            strPhone = posCustomerCheckOut[13, posCustomerCheckOut.CurrentRow.Index].Value.ToString();
            //display customer for POS checkout
            lblPOSCustomerID.Text = intPOSCustomerID.ToString();
            lblPOSCustomerName.Text = strFirst + " " + strLast;
        }

        //searches for customer for POS CheckOut
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet posDataSet = new DataSet();
                posDataSet = clsSQL.SearchForCustomer(txtPOSFirst.Text, txtPOSLast.Text, txtPOSPhone.Text);
                posCustomerCheckOut.DataSource = posDataSet.Tables[0];

            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error loading Customer from database\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPOSFirst_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidateNameInput(e);
        }

        private void txtPOSPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidation.ValidatePhoneInput(e);
        }

        private void btnPOSCheckOut_Click_1(object sender, EventArgs e)
        {
            if (intPOSCustomerID != 0)
            {
                posCustomer.fName = strFirst;
                posCustomer.mName = strMiddle;
                posCustomer.lName = strLast;
                posCustomer.id = intPOSCustomerID;
                posCustomer.address = strAddress1;
                posCustomer.address2 = strAddress2;
                posCustomer.address2 = strAddress3;
                posCustomer.city = strCity;
                posCustomer.state = strState;
                posCustomer.phone = strPhone;
                posCustomer.zipcode = strZipcode;
                bolPOSCustomer = true;
            }
            else
            {
                MessageBox.Show("Please search for customer for POS Check Out", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Hide();
            UserView userView = new UserView();
            userView.Show();
        }
    }
}
