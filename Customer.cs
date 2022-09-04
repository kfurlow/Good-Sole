using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA21_Final_Project
{
    class Customer
    {
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
            public int id { get; set; }

            public override string ToString()
            {
                return fName + " " + mName + " " + lName;
            }
        }
    }
}
