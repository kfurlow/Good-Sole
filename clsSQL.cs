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
using static FA21_Final_Project.Customer;
using System.IO;

namespace FA21_Final_Project
{
    class clsSQL
    {
        //creates customer
        public static customer newCustomer = new customer();

        //establishes connection to database
        public static SqlConnection cs = new SqlConnection("Data Source=cstnt.tstc.edu;Initial Catalog=inew2332fa21;Persist Security Info=True;User ID=FurlowK21Fa2332;Password=1782473");

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

        public class ValidationException : Exception
        {
            public ValidationException(string message)
                : base(message) { }
        }


        //method to check if username is unique
        public static bool UniqueUsername(string strUsername, bool isUnique)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("SELECT * FROM FurlowK21Fa2332.Logon WHERE LogonName= '" + strUsername + "'", cs);
                cs.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if (dr.HasRows == true)
                    {
                        isUnique = false;
                        cs.Close();
                        MessageBox.Show("Username must be unique\nPlease enter unique Username","Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                    }
                }
                cs.Close();
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return isUnique;
        }
        //populates item dataset
        public static DataSet PopulateItemDataSet(string strQuery)
        {
            try
            {
                DataSet dataSet = new DataSet();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = new SqlCommand(strQuery, cs);
                sqlDataAdapter.Fill(dataSet);
                return dataSet;

            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error loading items from database\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        //populated manager data set
        public static DataSet PopulateManagerDataSet(string strQuery)
        {
            try
            {
                DataSet personDataSet = new DataSet();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = new SqlCommand(strQuery, cs);
                sqlDA.Fill(personDataSet);
                return personDataSet;
            }
            catch (SqlException exc)
            {
                MessageBox.Show("Error loading Managers from database\n" + exc.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        //populate customer data set
        public static DataSet PopulateCustomerDataSet(string strQuery)
        {
            try
            {
                DataSet customerDataSet = new DataSet();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = new SqlCommand(strQuery, cs);
                sqlDA.Fill(customerDataSet);
                return customerDataSet;
            }
            catch (SqlException exc)
            {
                MessageBox.Show("Error loading Managers from database\n" + exc.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static DataSet DisplayDailyTotal(string strDate, string strAddDay)
        {
            try
            {
                DataSet reportDailyTotal = new DataSet();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                string strQuery = "SELECT SUM(FurlowK21fa2332.OrderDetail.ItemPrice) AS TotalSales FROM FurlowK21fa2332.OrderDetail INNER JOIN FurlowK21fa2332.OrderTable ON FurlowK21fa2332.OrderDetail.OrderID = FurlowK21fa2332.OrderTable.OrderID " +
                "WHERE FurlowK21fa2332.OrderTable.OrderDate >= '" +strDate+ "' AND FurlowK21fa2332.OrderTable.OrderDate < '" +strAddDay+ "'";
                sqlDA.SelectCommand = new SqlCommand(strQuery, cs);
                sqlDA.Fill(reportDailyTotal);
                return reportDailyTotal;

            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error loading daily total from database\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public static DataSet DisplayWeeklyTotal(string strSelectedDate, string strAddDays)
        {
            try
            {
                DataSet reportDailyTotal = new DataSet();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                string strQuery = "SELECT SUM(FurlowK21fa2332.OrderDetail.ItemPrice) AS TotalSales FROM FurlowK21fa2332.OrderDetail INNER JOIN FurlowK21fa2332.OrderTable ON FurlowK21fa2332.OrderDetail.OrderID = FurlowK21fa2332.OrderTable.OrderID " +
                "WHERE FurlowK21fa2332.OrderTable.OrderDate >= '" +strSelectedDate+ "' AND FurlowK21fa2332.OrderTable.OrderDate <= '" +strAddDays+ "'";
                sqlDA.SelectCommand = new SqlCommand(strQuery, cs);
                sqlDA.Fill(reportDailyTotal);
                return reportDailyTotal;

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error loading daily total from database\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public static DataSet DisplayMonthlyTotal(string strSelectedDate, string strAddDays)
        {
            try
            {
                DataSet reportDailyTotal = new DataSet();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                string strQuery = "SELECT SUM(FurlowK21fa2332.OrderDetail.ItemPrice) AS TotalSales FROM FurlowK21fa2332.OrderDetail INNER JOIN FurlowK21fa2332.OrderTable ON FurlowK21fa2332.OrderDetail.OrderID = FurlowK21fa2332.OrderTable.OrderID " +
                "WHERE FurlowK21fa2332.OrderTable.OrderDate >= '" + strSelectedDate + "' AND FurlowK21fa2332.OrderTable.OrderDate <= '" + strAddDays + "'";
                sqlDA.SelectCommand = new SqlCommand(strQuery, cs);
                sqlDA.Fill(reportDailyTotal);
                return reportDailyTotal;

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error loading daily total from database\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static void CreateOrderDetail(string strQuery)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand(strQuery, cs);
                cs.Open();
                sqlCommand.ExecuteNonQuery();
                cs.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error updating item quantity.\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void UpdateQuantity(string strQuery)
        {
            try
            {
                cs.Open();
                SqlCommand sqlCommand = new SqlCommand(strQuery, cs);
                sqlCommand.ExecuteNonQuery();
                cs.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error updating item quantity.\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void ClearCart(string strQuery)
        {
            try
            {
                SqlCommand sqlCmd = new SqlCommand(strQuery, cs);
                cs.Open();
                sqlCmd.ExecuteNonQuery();
                cs.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error clearing cart.\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void DeductQuantity(string strQuery)
        {
            try
            {
                SqlCommand sqlCmd = new SqlCommand(strQuery, cs);
                cs.Open();
                sqlCmd.ExecuteNonQuery();
                cs.Close();
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error deducting item quantity.\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static float GetSalary(int intManagerID)
        {
            float fltSalary = 0.00f;
            try
            {
                string strQuery = "Select Salary from FurlowK21Fa2332.EmployeeSalary where PersonID = '" + intManagerID + "'";
                SqlCommand sqlCommand = new SqlCommand(strQuery, cs);
                cs.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                sqlDataReader.Read();
                fltSalary = float.Parse(sqlDataReader["Salary"].ToString());
                cs.Close();
                return fltSalary;
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error getting manager salary.\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return fltSalary;
            }
            
        }
        //method to pull item data from table in database
        public static UserView.item GetItemInfo(string strQuery)
        {
            //declare item to store item data
            UserView.item newItem = new UserView.item();
            try
            {
                cs.Open();
                SqlCommand sqlCommand = new SqlCommand(strQuery, cs);
                SqlDataReader sqlReader = sqlCommand.ExecuteReader();
                sqlReader.Read();
                newItem.itemDescription = sqlReader["ItemDescription"].ToString();
                newItem.itemID = int.Parse(sqlReader["InventoryID"].ToString());
                newItem.itemPrice = float.Parse(sqlReader["RetailPrice"].ToString());
                newItem.itemName = sqlReader["ItemName"].ToString();
                newItem.itemInventory = int.Parse(sqlReader["Quantity"].ToString());

                cs.Close();
                return newItem;
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error loading item data.\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return newItem;
            }
        }

        //method to display previous item in database
        public static UserView.item PreviousItem(string strQuery, UserView.item newItemTest)
        {
            try
            {
                //open connection and run query
                cs.Open();
                SqlCommand cmd = new SqlCommand(strQuery, cs);
                SqlDataReader reader = cmd.ExecuteReader();

                //if there is data capture it and store it in newItem
                if (reader.Read())
                {
                    //store pulled data from Inventory Table in newItem
                    UserView.item newItem = new UserView.item();
                    newItem.itemDescription = reader["ItemDescription"].ToString();
                    newItem.itemID = int.Parse(reader["InventoryID"].ToString());
                    newItem.itemPrice = float.Parse(reader["RetailPrice"].ToString());
                    newItem.itemName = reader["ItemName"].ToString();
                    newItem.itemInventory = int.Parse(reader["Quantity"].ToString());
                    Byte[] image = ((byte[])reader["ItemImage"]);

                    //check if image is null
                    if (image == null)
                    {
                        newItem.itemImage = null;
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(image);
                        newItem.itemImage = image;
                    }

                    //close connection
                    cs.Close();
                    newItemTest = newItem;
                }
                else //if no data is present pull last data item from Inventory table 
                {
                    //open connection
                    cs.Close();
                    cs.Open();

                    //query to grab last item in Inventory Table
                    strQuery = "SELECT * FROM FurlowK21Fa2332.Inventory WHERE Discontinued = 0 ORDER BY InventoryID DESC";
                    SqlCommand command = new SqlCommand(strQuery, cs);
                    SqlDataReader sqlReader = command.ExecuteReader();
                    sqlReader.Read();

                    //newItem to store pulled data from Inventory Table
                    UserView.item newItem = new UserView.item();
                    newItem.itemDescription = sqlReader["ItemDescription"].ToString();
                    newItem.itemID = int.Parse(sqlReader["InventoryID"].ToString());
                    newItem.itemPrice = float.Parse(sqlReader["RetailPrice"].ToString());
                    newItem.itemName = sqlReader["ItemName"].ToString();
                    newItem.itemInventory = int.Parse(sqlReader["Quantity"].ToString());
                    Byte[] image = ((byte[])sqlReader["ItemImage"]);

                    //reset itemID
                    UserView.intItemID = newItem.itemID;

                    //check if image is null
                    if (image == null)
                    {
                        newItem.itemImage = null;
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(image);
                        newItem.itemImage = image;
                    }

                    //close connection
                    cs.Close();
                    newItemTest = newItem;
                }

                //close connection and return newItemTest
                cs.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error displaying item from database.\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return newItemTest;
        }
        //method to display next item in database
        public static  UserView.item NextItem(string strQuery,UserView.item newItemTest)
        {
            try
            {
                cs.Open();
                SqlCommand cmd = new SqlCommand(strQuery, cs);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    UserView.item newItem = new UserView.item();
                    newItem.itemDescription = reader["ItemDescription"].ToString();
                    newItem.itemID = int.Parse(reader["InventoryID"].ToString());
                    newItem.itemPrice = float.Parse(reader["RetailPrice"].ToString());
                    newItem.itemName = reader["ItemName"].ToString();
                    newItem.itemInventory = int.Parse(reader["Quantity"].ToString());
                    Byte[] image = ((byte[])reader["ItemImage"]);



                    if (image == null)
                    {
                        newItem.itemImage = null;
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(image);
                        newItem.itemImage = image;
                    }
                    cs.Close();
                    newItemTest = newItem;
                }
                else
                {
                    cs.Close();
                    cs.Open();
                    strQuery = "SELECT * FROM FurlowK21Fa2332.Inventory WHERE Discontinued = 0 ORDER BY InventoryID ASC";
                    SqlCommand command = new SqlCommand(strQuery, cs);
                    SqlDataReader sqlReader = command.ExecuteReader();
                    sqlReader.Read();

                    UserView.item newItem = new UserView.item();
                    newItem.itemDescription = sqlReader["ItemDescription"].ToString();
                    newItem.itemID = int.Parse(sqlReader["InventoryID"].ToString());
                    newItem.itemPrice = float.Parse(sqlReader["RetailPrice"].ToString());
                    newItem.itemName = sqlReader["ItemName"].ToString();
                    newItem.itemInventory = int.Parse(sqlReader["Quantity"].ToString());
                    Byte[] image = ((byte[])sqlReader["ItemImage"]);

                    UserView.intItemID = newItem.itemID;

                    if (image == null)
                    {
                        newItem.itemImage = null;
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(image);
                        newItem.itemImage = image;
                    }
                    cs.Close();
                    newItemTest = newItem;
                }

                cs.Close();
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error displaying item from database.\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return newItemTest;
        }

        public static Image byteArrayToImage(Byte[] byteArr)
        {
            using (MemoryStream memstr = new MemoryStream(byteArr))
            {
                Image img = Image.FromStream(memstr);
                return img;
            }
        }

        //remove purchased items from database
        public static void RemoveItem(List<int> cart)
        {
            try
            {
                cs.Open();

                for (int i = 0; i < UserView.items.Length; i ++)
                {
                    if (UserView.items[i] == null)
                    {

                    }
                    else
                    {
                        int intItemID = UserView.items[i].itemID;
                        int intQuantity = UserView.items[i].purchaseAmount;
                        string strQuery = "UPDATE FurlowK21Fa2332.Inventory set QUANTITY = QUANTITY - '" + intQuantity + "' where InventoryID = '" + intItemID + "'";
                        SqlCommand command = new SqlCommand(strQuery, cs);
                        command.ExecuteNonQuery();
                    }
                }
                cs.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error removing item from database\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //get orderID from database
        public static int GetOrderID(string strQuery)
        {
            try
            {
                cs.Open();
                SqlCommand cmd = new SqlCommand(strQuery, cs);
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                dr.Read();

                int intOrderNum = int.Parse(dr["OrderID"].ToString());
                cs.Close();
                return intOrderNum;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error completing purchase\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        //stores payment info into database
        public static void InsertOrder(string strQuery)
        {
            try
            {
                cs.Open();
                SqlCommand cmd = new SqlCommand(strQuery, cs);
                cmd.ExecuteNonQuery();
                cs.Close();
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error completing purchase\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //reset password for user
        public static void ResetPassword(string strUsername, string strNewPassword)
        {
            try
            {
                //query to update password in database
                string strUpdatePasswordQuery = "update FurlowK21Fa2332.Logon set Password = '" + strNewPassword + "' where " +
                    "LogonName = '" + strUsername + "'";
                SqlCommand command = new SqlCommand(strUpdatePasswordQuery, cs);

                //open connection
                cs.Open();

                //execute query
                command.ExecuteNonQuery();

                //close connection
                cs.Close();
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error updating password..." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void GetSecurity(string strUsername)
        {
            //query to select all matching data from database
            string strQuery = "SELECT * FROM FurlowK21Fa2332.Logon WHERE LogonName = '" + strUsername + "' ";

            //strings to hold security questions
            string strSecurity1, strSecurity2, strSecurity3, strPassword, strAnswer1, strAnswer2, strAnswer3;

            SqlCommand cmd = new SqlCommand(strQuery, cs);

            //create SQLdatareader
            SqlDataReader reader;

            //open connection
            cs.Open();

            //pulls results of cmd command
            reader = cmd.ExecuteReader();
            reader.Read();

            ResetPassword reset = new ResetPassword();

            if (strUsername == reader["LogonName"].ToString())
            {
                //allow grpResetPass to be edited
                reset.grpSecurity.Enabled = true;

                //stores pulled data into variables
                strPassword = reader["Password"].ToString();
                strSecurity1 = reader["FirstChallengeQuestion"].ToString();
                strSecurity2 = reader["SecondChallengeQuestion"].ToString();
                strSecurity3 = reader["ThirdChallengeQuestion"].ToString();
                strAnswer1 = reader["FirstChallengeAnswer"].ToString();
                strAnswer2 = reader["SecondChallengeAnswer"].ToString();
                strAnswer3 = reader["ThirdChallengeAnswer"].ToString();

                //displays security questions into labels
                reset.lblSecurityOne.Text = strSecurity1;
                reset.lblSecurityTwo.Text = strSecurity2;
                reset.lblSecurityThree.Text = strSecurity3;
                cs.Close();
            }
            else
            {
                MessageBox.Show("Username not correct, please try again.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        //logins in to server
        public static bool Login(string strQuery)
        {
            try
            {
                SqlConnection connection = cs;

                //query to select all matching data from database 
                SqlCommand cmd = new SqlCommand(strQuery, cs);

                //create SQLdatareader
                SqlDataReader reader, readAllCustomer;


                cs.Open();

                //pulls results of cmd command
                reader = cmd.ExecuteReader();
                reader.Read();


                //store person id number
                newCustomer.id = int.Parse(reader["PersonID"].ToString());
                newCustomer.position = reader["PositionTitle"].ToString();

                cs.Close();
                connection.Open();

                //grab all customer information from database and store it in customer variable
                string strSelectAllPerson = "SELECT * FROM FurlowK21Fa2332.Person WHERE PersonID = '" + newCustomer.id + "' ";
                string strTemp = "SELECT * FROM FurlowK21Fa2332.Person INNER JOIN FurlowK21Fa2332.Logon on FurlowK21Fa2332.Person.PersonID = FurlowK21fa2332.Logon.PersonID WHERE FurlowK21Fa2332.Person.PersonID = '" + newCustomer.id + "'";
                SqlCommand commandPerson = new SqlCommand(strTemp, connection);

                //pull person results 
                readAllCustomer = commandPerson.ExecuteReader();
                readAllCustomer.Read();

                //store customer information in newCustomer
                newCustomer.fName = readAllCustomer["NameFirst"].ToString();
                newCustomer.lName = readAllCustomer["NameLast"].ToString();
                newCustomer.mName = readAllCustomer["NameMiddle"].ToString();
                newCustomer.address = readAllCustomer["Address1"].ToString();
                newCustomer.address2 = readAllCustomer["Address2"].ToString();
                newCustomer.address3 = readAllCustomer["Address3"].ToString();
                newCustomer.city = readAllCustomer["City"].ToString();
                newCustomer.state = readAllCustomer["State"].ToString();
                newCustomer.zipcode = readAllCustomer["Zipcode"].ToString();
                newCustomer.title = readAllCustomer["Title"].ToString();
                newCustomer.suffix = readAllCustomer["Suffix"].ToString();
                newCustomer.phone = readAllCustomer["PhonePrimary"].ToString();
                newCustomer.position = readAllCustomer["PositionTitle"].ToString();
                bool bolPersonDeleted = (bool)readAllCustomer["AccountDeleted"];
                connection.Close();

                if (newCustomer.position == "Manager")
                {
                    connection.Open();
                    string strSalaryQuery = "SELECT Salary FROM FurlowK21Fa2332.EmployeeSalary WHERE PersonID = '"+newCustomer.id+"'";
                    SqlCommand sqlCommand = new SqlCommand(strSalaryQuery, connection);
                    SqlDataReader sqlDataReader;
                    sqlDataReader = sqlCommand.ExecuteReader();
                    sqlDataReader.Read();
                    //grab employee salary
                    newCustomer.salary = float.Parse(sqlDataReader["Salary"].ToString());
                    connection.Close();
                }
                
                if(bolPersonDeleted == true)
                {
                    MessageBox.Show("This account no longer exists", "Login Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    bolPersonDeleted = false;
                    return false;
                }


                ResetPassword reset = new ResetPassword();
                reset.grpResetPass.Enabled = true;
                reset.btnResetPass.Enabled = true;

                

                return true;
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error signing in.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch
            {
                cs.Close();
                MessageBox.Show("Username or Password invalid.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
        }

        //populates dataset based on query search
        public static DataSet SearchForCustomer(string strFirst, string strLast, string strPhone)
        {
            try
            {
                string strQuery = "SELECT * FROM FurlowK21Fa2332.Person WHERE NameFirst = '" + strFirst + "' AND NameLast = '" + strLast + "' AND " +
                "PhonePrimary = '" + strPhone + "'";

                DataSet posDataSet = new DataSet();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = new SqlCommand(strQuery, cs);
                sqlDA.Fill(posDataSet);
                return posDataSet;

            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error loading Customer from database\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        //inserts new item to database
        public static void InsertItem(string strItemName, string strItemDesc, string strItemRetail, string strItemCost, string strInventory, byte[] byteImageArr)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.InsertCommand = new SqlCommand("INSERT INTO FurlowK21Fa2332.Inventory Values (@ItemName, @ItemDescription, @RetailPrice, @Cost, @Quantity, @ItemImage, @Discontinued)", cs);
                da.InsertCommand.Parameters.Add("@ItemName", SqlDbType.VarChar).Value = strItemName;
                da.InsertCommand.Parameters.Add("@ItemDescription", SqlDbType.VarChar).Value = strItemDesc;
                da.InsertCommand.Parameters.Add("@RetailPrice", SqlDbType.Decimal).Value = strItemRetail;
                da.InsertCommand.Parameters.Add("@Cost", SqlDbType.Decimal).Value = strItemCost;
                da.InsertCommand.Parameters.Add("@Quantity", SqlDbType.Int).Value = strInventory;
                da.InsertCommand.Parameters.Add("@ItemImage", SqlDbType.Image).Value = byteImageArr;
                da.InsertCommand.Parameters.Add("@Discontinued", SqlDbType.Bit).Value = 0;
                cs.Open();
                da.InsertCommand.ExecuteNonQuery();
                cs.Close();

                MessageBox.Show("Item added to database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error adding item to database.\n" + ex.ToString(), "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //insert employee salary to database
        public static void InsertSalary(float fltSalary)
        {
            try
            {
                string strSelectTop = "SELECT TOP 1 PersonID FROM FurlowK21Fa2332.Person ORDER BY PersonID DESC";
                SqlCommand sqlCommand = new SqlCommand(strSelectTop, cs);
                cs.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                sqlDataReader.Read();
                int intManagerID = int.Parse(sqlDataReader["PersonID"].ToString());
                cs.Close();

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.InsertCommand = new SqlCommand("INSERT INTO FurlowK21Fa2332.EmployeeSalary VALUES(@PersonID, @Salary)", cs);
                sqlDataAdapter.InsertCommand.Parameters.Add("@PersonID", SqlDbType.Int).Value = intManagerID;
                sqlDataAdapter.InsertCommand.Parameters.Add("@Salary", SqlDbType.Float).Value = fltSalary;
                cs.Open();
                sqlDataAdapter.InsertCommand.ExecuteNonQuery();
                cs.Close();
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error adding salary to database\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void UpdateManagerSalary(float fltSalary, int intPersonID)
        {
            try
            {
                string strSalaryQuery = "update Furlowk21fa2332.EmployeeSalary set Salary = '" + fltSalary + "' where PersonID = '" + intPersonID + "'";
                SqlCommand sqlCommand = new SqlCommand(strSalaryQuery, cs);
                cs.Open();
                sqlCommand.ExecuteNonQuery();
                cs.Close();
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error updating manager salary\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //disable selected item in database
        public static void DisableItem(int intItemID)
        {
            try
            {
                string strQuery = "UPDATE FurlowK21Fa2332.Inventory SET Discontinued = 1 WHERE InventoryID = '" + intItemID + "'";
                SqlCommand sqlCmd = new SqlCommand(strQuery, cs);
                cs.Open();
                sqlCmd.ExecuteNonQuery();
                cs.Close();
                MessageBox.Show("Item successfully removed from database.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error removing item from database\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //updates item information
        public static void UpdateItem(string strItemName, string strProdDesc, string strRetail, string strCost, string strQty, int intItemID)
        {
            try
            {
                //string to update item information in database
                string strQuery = "UPDATE FurlowK21Fa2332.Inventory SET ItemName = @ItemName, ItemDescription = @ItemDescription, RetailPrice = @RetailPrice, Cost = @Cost, Quantity = @Quantity, Discontinued = 0 WHERE InventoryID = '" + intItemID + "'";
                //execute non query to update item information in database
                SqlCommand sqlCommand = new SqlCommand(strQuery, cs);
                cs.Open();
                sqlCommand.Parameters.Add("@ItemName", SqlDbType.VarChar).Value = strItemName;
                sqlCommand.Parameters.Add("@ItemDescription", SqlDbType.VarChar).Value = strProdDesc;
                sqlCommand.Parameters.Add("@RetailPrice", SqlDbType.Float).Value = strRetail;
                sqlCommand.Parameters.Add("@Cost", SqlDbType.Float).Value = strCost;
                sqlCommand.Parameters.Add("@Quantity", SqlDbType.Int).Value = strQty;
                sqlCommand.ExecuteNonQuery();
                cs.Close();
                //give user feedback
                MessageBox.Show("Item information updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error updating items in database\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void UpdatePerson(string strTitle, string strFirst, string strMiddle, string strLast, string strSuffix, string strAddress1, string strAddress2,
            string strAddress3, string strCity, string strState, string strZipcode, string strEmail, string strPhone, string strAltPhone, int intPersonID)
        {
            try
            {
                string strQuery = "UPDATE FurlowK21Fa2332.Person SET Title = @Title, NameFirst = @NameFirst, NameMiddle = @NameMiddle," +
                "NameLast = @NameLast, Suffix = @Suffix, Address1 = @Address1, Address2 = @Address2, Address3 = @Address3, City = @City, Zipcode = @Zipcode," +
                "Email = @Email, PhonePrimary = @PhonePrimary, PhoneSecondary = @PhoneSecondary, PersonDeleted = 0 WHERE FurlowK21Fa2332.Person.PersonID = '" + intPersonID + "'";

                SqlCommand sqlCommand = new SqlCommand(strQuery, cs);
                cs.Open();
                sqlCommand.Parameters.Add("@Title", SqlDbType.VarChar).Value = strTitle;
                sqlCommand.Parameters.Add("@NameFirst", SqlDbType.VarChar).Value = strFirst;
                sqlCommand.Parameters.Add("@NameMiddle", SqlDbType.VarChar).Value = strMiddle;
                sqlCommand.Parameters.Add("@NameLast", SqlDbType.VarChar).Value = strLast;
                sqlCommand.Parameters.Add("@Suffix", SqlDbType.VarChar).Value = strSuffix;
                sqlCommand.Parameters.Add("@Address1", SqlDbType.VarChar).Value = strAddress1;
                sqlCommand.Parameters.Add("@Address2", SqlDbType.VarChar).Value = strAddress2;
                sqlCommand.Parameters.Add("@Address3", SqlDbType.VarChar).Value = strAddress3;
                sqlCommand.Parameters.Add("@City", SqlDbType.VarChar).Value = strCity;
                sqlCommand.Parameters.Add("@Zipcode", SqlDbType.VarChar).Value = strZipcode;
                sqlCommand.Parameters.Add("@State", SqlDbType.VarChar).Value = strState;
                sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = strEmail;
                sqlCommand.Parameters.Add("@PhonePrimary", SqlDbType.VarChar).Value = strPhone;
                sqlCommand.Parameters.Add("@PhoneSecondary", SqlDbType.VarChar).Value = strAltPhone;
                sqlCommand.ExecuteNonQuery();
                cs.Close();
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error updating manager information\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void RemovePerson(int intPersonID)
        {
            try
            {
                //disable manager in database
                string strQuery = "UPDATE FurlowK21Fa2332.Person SET PersonDeleted = 1 WHERE PersonID = '" + intPersonID + "'";
                SqlCommand sqlCmd = new SqlCommand(strQuery, cs);
                cs.Open();
                sqlCmd.ExecuteNonQuery();
                cs.Close();

                //disable logon for manager in database
                string strQueryLog = "UPDATE FurlowK21Fa2332.Logon SET AccountDeleted = 1 WHERE PersonID = '" + intPersonID + "'";
                SqlCommand sqlCommand = new SqlCommand(strQueryLog, cs);
                cs.Open();
                sqlCommand.ExecuteNonQuery();
                cs.Close();
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error removing Manager from database\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //inserts customer data into database
        public static void InsertData(string strTitle, string strFirst, string strMiddle, string strLast, string strSuffix, string strAddress1, string strAddress2,
            string strAddress3, string strCity, string strState, string strZipcode, string strEmail, string strPhone, string strAltPhone)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();

                //capturing and inserting customer account information into database
                da.InsertCommand = new SqlCommand("INSERT INTO FurlowK21Fa2332.Person VALUES(@Title, @FirstName, @MiddleName, @LastName," +
                    "@Suffix, @Address1, @Address2, @Address3, @City, @Zipcode, @State, @Email, @PhonePrimary, @PhoneSecondary, @PersonImage, @PersonType)", cs);
                da.InsertCommand.Parameters.Add("@Title", SqlDbType.VarChar).Value = strTitle;
                da.InsertCommand.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = strFirst;
                da.InsertCommand.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = strMiddle;
                da.InsertCommand.Parameters.Add("@LastName", SqlDbType.VarChar).Value = strLast;
                da.InsertCommand.Parameters.Add("@Suffix", SqlDbType.VarChar).Value = strSuffix;
                da.InsertCommand.Parameters.Add("@Address1", SqlDbType.VarChar).Value = strAddress1;
                da.InsertCommand.Parameters.Add("@Address2", SqlDbType.VarChar).Value = strAddress2;
                da.InsertCommand.Parameters.Add("@Address3", SqlDbType.VarChar).Value = strAddress3;
                da.InsertCommand.Parameters.Add("@City", SqlDbType.VarChar).Value = strCity;
                da.InsertCommand.Parameters.Add("@State", SqlDbType.VarChar).Value = strState;
                da.InsertCommand.Parameters.Add("@Zipcode", SqlDbType.VarChar).Value = strZipcode;
                da.InsertCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = strEmail;
                da.InsertCommand.Parameters.Add("@PhonePrimary", SqlDbType.VarChar).Value = strPhone;
                da.InsertCommand.Parameters.Add("@PhoneSecondary", SqlDbType.VarChar).Value = strAltPhone;
                da.InsertCommand.Parameters.Add("@PersonImage", SqlDbType.VarBinary).Value = DBNull.Value;
                da.InsertCommand.Parameters.Add("@PersonType", SqlDbType.Int).Value = 0;


                cs.Open();
                da.InsertCommand.ExecuteNonQuery();
                cs.Close();
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error inserting Customer data to database.\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //inserting login information to database
        public static bool InsertLoginInfo(string strUsername, string strPassword, string strSecurity1, string strSecurity2, string strSecurity3, string strAnswer1,
            string strAnswer2, string strAnswer3, string strPosition)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();

                SqlDataReader sdr;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cs;
                cmd.CommandText = "Select Top(1) FurlowK21Fa2332.Person.PersonID from FurlowK21Fa2332.Person order by FurlowK21Fa2332.Person.PersonID desc";

                cs.Open();
                sdr = cmd.ExecuteReader();
                String strPersonID = "";
                int intPersonID = 1;

                if (sdr.Read())
                {
                    strPersonID = sdr.GetValue(0).ToString();
                }

                int.TryParse(strPersonID, out intPersonID);
                cs.Close();

                //inserting login information into database
                da.InsertCommand = new SqlCommand("INSERT INTO FurlowK21Fa2332.Logon VALUES(@PersonID, @LogonName, @Password," +
                    "@FirstChallengeQuestion, @FirstChallengeAnswer, @SecondChallengeQuestion, @SecondChallengeAnswer, @ThirdChallengeQuestion, @ThirdChallengeAnswer," +
                    "@PositionTitle, @AccountDisabled, @AccountDeleted)");
                da.InsertCommand.Connection = cs;
                da.InsertCommand.Parameters.Add("@PersonID", SqlDbType.Int).Value = intPersonID;
                da.InsertCommand.Parameters.Add("@LogonName", SqlDbType.VarChar).Value = strUsername;
                da.InsertCommand.Parameters.Add("@Password", SqlDbType.VarChar).Value = strPassword;
                da.InsertCommand.Parameters.Add("@FirstChallengeQuestion", SqlDbType.VarChar).Value = strSecurity1;
                da.InsertCommand.Parameters.Add("@SecondChallengeQuestion", SqlDbType.VarChar).Value = strSecurity2;
                da.InsertCommand.Parameters.Add("@ThirdChallengeQuestion", SqlDbType.VarChar).Value = strSecurity3;
                da.InsertCommand.Parameters.Add("@FirstChallengeAnswer", SqlDbType.VarChar).Value = strAnswer1;
                da.InsertCommand.Parameters.Add("@SecondChallengeAnswer", SqlDbType.VarChar).Value = strAnswer2;
                da.InsertCommand.Parameters.Add("@ThirdChallengeAnswer", SqlDbType.VarChar).Value = strAnswer3;
                da.InsertCommand.Parameters.Add("@PositionTitle", SqlDbType.VarChar).Value = strPosition;
                da.InsertCommand.Parameters.Add("@AccountDisabled", SqlDbType.Bit).Value = 0;
                da.InsertCommand.Parameters.Add("@AccountDeleted", SqlDbType.Bit).Value = 0;

                cs.Open();
                da.InsertCommand.ExecuteNonQuery();
                cs.Close();

                return true;
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error inserting Login data to database.\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
