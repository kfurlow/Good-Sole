
namespace FA21_Final_Project
{
    partial class CheckOut
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckOut));
            this.grpCustomer = new System.Windows.Forms.GroupBox();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.lstOrderSummary = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSubTotal = new System.Windows.Forms.Label();
            this.lblTax = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDeliveryFee = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.grpPaymentType = new System.Windows.Forms.GroupBox();
            this.rdoMastercard = new System.Windows.Forms.RadioButton();
            this.rdoVisa = new System.Windows.Forms.RadioButton();
            this.grpCreditCard = new System.Windows.Forms.GroupBox();
            this.txtCreditCard = new System.Windows.Forms.MaskedTextBox();
            this.cboYear = new System.Windows.Forms.ComboBox();
            this.cboMonth = new System.Windows.Forms.ComboBox();
            this.txtCRV = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtMiddleName = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grpDeliveryType = new System.Windows.Forms.GroupBox();
            this.rdoExpress = new System.Windows.Forms.RadioButton();
            this.rdoStandard = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddItems = new System.Windows.Forms.Button();
            this.btnCheckOut = new System.Windows.Forms.Button();
            this.lnkNeedHelp = new System.Windows.Forms.LinkLabel();
            this.txtCoupon = new System.Windows.Forms.TextBox();
            this.btnCoupon = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.lblDiscount1 = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.lblDiscountAmount = new System.Windows.Forms.Label();
            this.btnUserView = new System.Windows.Forms.Button();
            this.grpCustomer.SuspendLayout();
            this.grpPaymentType.SuspendLayout();
            this.grpCreditCard.SuspendLayout();
            this.grpDeliveryType.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpCustomer
            // 
            this.grpCustomer.Controls.Add(this.lblCustomerName);
            this.grpCustomer.Location = new System.Drawing.Point(13, 26);
            this.grpCustomer.Name = "grpCustomer";
            this.grpCustomer.Size = new System.Drawing.Size(344, 62);
            this.grpCustomer.TabIndex = 0;
            this.grpCustomer.TabStop = false;
            this.grpCustomer.Text = "Customer";
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.Location = new System.Drawing.Point(19, 25);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(217, 34);
            this.lblCustomerName.TabIndex = 0;
            this.lblCustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstOrderSummary
            // 
            this.lstOrderSummary.FormattingEnabled = true;
            this.lstOrderSummary.ItemHeight = 24;
            this.lstOrderSummary.Location = new System.Drawing.Point(13, 95);
            this.lstOrderSummary.Name = "lstOrderSummary";
            this.lstOrderSummary.Size = new System.Drawing.Size(344, 292);
            this.lstOrderSummary.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(379, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sub Total";
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSubTotal.Location = new System.Drawing.Point(383, 119);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(166, 30);
            this.lblSubTotal.TabIndex = 3;
            this.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTax
            // 
            this.lblTax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTax.Location = new System.Drawing.Point(383, 187);
            this.lblTax.Name = "lblTax";
            this.lblTax.Size = new System.Drawing.Size(166, 30);
            this.lblTax.TabIndex = 5;
            this.lblTax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(379, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tax";
            // 
            // lblDeliveryFee
            // 
            this.lblDeliveryFee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDeliveryFee.Location = new System.Drawing.Point(383, 255);
            this.lblDeliveryFee.Name = "lblDeliveryFee";
            this.lblDeliveryFee.Size = new System.Drawing.Size(166, 30);
            this.lblDeliveryFee.TabIndex = 7;
            this.lblDeliveryFee.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(379, 231);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 24);
            this.label4.TabIndex = 6;
            this.label4.Text = "Delivery fee";
            // 
            // lblTotal
            // 
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotal.Location = new System.Drawing.Point(383, 330);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(166, 30);
            this.lblTotal.TabIndex = 9;
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(379, 306);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 24);
            this.label5.TabIndex = 8;
            this.label5.Text = "Total";
            // 
            // grpPaymentType
            // 
            this.grpPaymentType.Controls.Add(this.rdoMastercard);
            this.grpPaymentType.Controls.Add(this.rdoVisa);
            this.grpPaymentType.Location = new System.Drawing.Point(13, 393);
            this.grpPaymentType.Name = "grpPaymentType";
            this.grpPaymentType.Size = new System.Drawing.Size(648, 76);
            this.grpPaymentType.TabIndex = 10;
            this.grpPaymentType.TabStop = false;
            this.grpPaymentType.Text = "Payment Type";
            // 
            // rdoMastercard
            // 
            this.rdoMastercard.AutoSize = true;
            this.rdoMastercard.Location = new System.Drawing.Point(370, 28);
            this.rdoMastercard.Name = "rdoMastercard";
            this.rdoMastercard.Size = new System.Drawing.Size(121, 28);
            this.rdoMastercard.TabIndex = 1;
            this.rdoMastercard.Text = "Mastercard";
            this.rdoMastercard.UseVisualStyleBackColor = true;
            // 
            // rdoVisa
            // 
            this.rdoVisa.AutoSize = true;
            this.rdoVisa.Checked = true;
            this.rdoVisa.Location = new System.Drawing.Point(172, 28);
            this.rdoVisa.Name = "rdoVisa";
            this.rdoVisa.Size = new System.Drawing.Size(64, 28);
            this.rdoVisa.TabIndex = 0;
            this.rdoVisa.TabStop = true;
            this.rdoVisa.Text = "Visa";
            this.rdoVisa.UseVisualStyleBackColor = true;
            // 
            // grpCreditCard
            // 
            this.grpCreditCard.Controls.Add(this.txtCreditCard);
            this.grpCreditCard.Controls.Add(this.cboYear);
            this.grpCreditCard.Controls.Add(this.cboMonth);
            this.grpCreditCard.Controls.Add(this.txtCRV);
            this.grpCreditCard.Controls.Add(this.txtLastName);
            this.grpCreditCard.Controls.Add(this.txtMiddleName);
            this.grpCreditCard.Controls.Add(this.txtFirstName);
            this.grpCreditCard.Controls.Add(this.label11);
            this.grpCreditCard.Controls.Add(this.label10);
            this.grpCreditCard.Controls.Add(this.label9);
            this.grpCreditCard.Controls.Add(this.label8);
            this.grpCreditCard.Controls.Add(this.label7);
            this.grpCreditCard.Controls.Add(this.label6);
            this.grpCreditCard.Controls.Add(this.label2);
            this.grpCreditCard.Location = new System.Drawing.Point(13, 475);
            this.grpCreditCard.Name = "grpCreditCard";
            this.grpCreditCard.Size = new System.Drawing.Size(648, 237);
            this.grpCreditCard.TabIndex = 11;
            this.grpCreditCard.TabStop = false;
            this.grpCreditCard.Text = "Credit Card Info";
            // 
            // txtCreditCard
            // 
            this.txtCreditCard.Location = new System.Drawing.Point(202, 127);
            this.txtCreditCard.Mask = "0000-0000-0000-0000";
            this.txtCreditCard.Name = "txtCreditCard";
            this.txtCreditCard.ShortcutsEnabled = false;
            this.txtCreditCard.Size = new System.Drawing.Size(341, 29);
            this.txtCreditCard.TabIndex = 3;
            this.txtCreditCard.Click += new System.EventHandler(this.txtCreditCard_Click);
            this.txtCreditCard.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCreditCard_KeyPress_1);
            // 
            // cboYear
            // 
            this.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboYear.FormattingEnabled = true;
            this.cboYear.Items.AddRange(new object[] {
            "2021",
            "2022",
            "2023",
            "2024",
            "2025",
            "2026",
            "2027",
            "2028",
            "2029",
            "2030",
            "2031",
            "2032"});
            this.cboYear.Location = new System.Drawing.Point(459, 162);
            this.cboYear.Name = "cboYear";
            this.cboYear.Size = new System.Drawing.Size(84, 32);
            this.cboYear.TabIndex = 5;
            // 
            // cboMonth
            // 
            this.cboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMonth.FormattingEnabled = true;
            this.cboMonth.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"});
            this.cboMonth.Location = new System.Drawing.Point(202, 159);
            this.cboMonth.Name = "cboMonth";
            this.cboMonth.Size = new System.Drawing.Size(84, 32);
            this.cboMonth.TabIndex = 4;
            // 
            // txtCRV
            // 
            this.txtCRV.Location = new System.Drawing.Point(202, 194);
            this.txtCRV.Name = "txtCRV";
            this.txtCRV.Size = new System.Drawing.Size(84, 29);
            this.txtCRV.TabIndex = 6;
            this.txtCRV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCRV_KeyPress);
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(202, 95);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(341, 29);
            this.txtLastName.TabIndex = 2;
            this.txtLastName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLastName_KeyPress);
            // 
            // txtMiddleName
            // 
            this.txtMiddleName.Location = new System.Drawing.Point(202, 61);
            this.txtMiddleName.Name = "txtMiddleName";
            this.txtMiddleName.Size = new System.Drawing.Size(341, 29);
            this.txtMiddleName.TabIndex = 1;
            this.txtMiddleName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMiddleName_KeyPress);
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(202, 29);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(341, 29);
            this.txtFirstName.TabIndex = 0;
            this.txtFirstName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFirstName_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(355, 162);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 24);
            this.label11.TabIndex = 6;
            this.label11.Text = "Exp. Year:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(58, 194);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(105, 24);
            this.label10.TabIndex = 5;
            this.label10.Text = "CRV Code:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(58, 162);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 24);
            this.label9.TabIndex = 4;
            this.label9.Text = "Exp Month:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(58, 130);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(129, 24);
            this.label8.TabIndex = 3;
            this.label8.Text = "Card Number:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(58, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 24);
            this.label7.TabIndex = 2;
            this.label7.Text = "Last name:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(58, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 24);
            this.label6.TabIndex = 1;
            this.label6.Text = "Middle name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "First name:";
            // 
            // grpDeliveryType
            // 
            this.grpDeliveryType.Controls.Add(this.rdoExpress);
            this.grpDeliveryType.Controls.Add(this.rdoStandard);
            this.grpDeliveryType.Location = new System.Drawing.Point(13, 718);
            this.grpDeliveryType.Name = "grpDeliveryType";
            this.grpDeliveryType.Size = new System.Drawing.Size(648, 76);
            this.grpDeliveryType.TabIndex = 12;
            this.grpDeliveryType.TabStop = false;
            this.grpDeliveryType.Text = "Delivery Type";
            // 
            // rdoExpress
            // 
            this.rdoExpress.AutoSize = true;
            this.rdoExpress.Location = new System.Drawing.Point(381, 28);
            this.rdoExpress.Name = "rdoExpress";
            this.rdoExpress.Size = new System.Drawing.Size(177, 28);
            this.rdoExpress.TabIndex = 1;
            this.rdoExpress.Text = "Express Shipping";
            this.rdoExpress.UseVisualStyleBackColor = true;
            this.rdoExpress.CheckedChanged += new System.EventHandler(this.rdoExpress_CheckedChanged);
            // 
            // rdoStandard
            // 
            this.rdoStandard.AutoSize = true;
            this.rdoStandard.Checked = true;
            this.rdoStandard.Location = new System.Drawing.Point(91, 28);
            this.rdoStandard.Name = "rdoStandard";
            this.rdoStandard.Size = new System.Drawing.Size(183, 28);
            this.rdoStandard.TabIndex = 0;
            this.rdoStandard.TabStop = true;
            this.rdoStandard.Text = "Standard Shipping";
            this.rdoStandard.UseVisualStyleBackColor = true;
            this.rdoStandard.CheckedChanged += new System.EventHandler(this.rdoStandard_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(24, 801);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(143, 32);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel &Order";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddItems
            // 
            this.btnAddItems.Location = new System.Drawing.Point(344, 801);
            this.btnAddItems.Name = "btnAddItems";
            this.btnAddItems.Size = new System.Drawing.Size(143, 32);
            this.btnAddItems.TabIndex = 14;
            this.btnAddItems.Text = "&Add Items";
            this.btnAddItems.UseVisualStyleBackColor = true;
            this.btnAddItems.Click += new System.EventHandler(this.btnAddItems_Click);
            // 
            // btnCheckOut
            // 
            this.btnCheckOut.Location = new System.Drawing.Point(500, 801);
            this.btnCheckOut.Name = "btnCheckOut";
            this.btnCheckOut.Size = new System.Drawing.Size(143, 32);
            this.btnCheckOut.TabIndex = 15;
            this.btnCheckOut.Text = "&Check Out";
            this.btnCheckOut.UseVisualStyleBackColor = true;
            this.btnCheckOut.Click += new System.EventHandler(this.btnCheckOut_Click);
            // 
            // lnkNeedHelp
            // 
            this.lnkNeedHelp.AutoSize = true;
            this.lnkNeedHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkNeedHelp.Location = new System.Drawing.Point(19, 9);
            this.lnkNeedHelp.Name = "lnkNeedHelp";
            this.lnkNeedHelp.Size = new System.Drawing.Size(85, 18);
            this.lnkNeedHelp.TabIndex = 17;
            this.lnkNeedHelp.TabStop = true;
            this.lnkNeedHelp.Text = "Need Help?";
            this.lnkNeedHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkNeedHelp_LinkClicked);
            // 
            // txtCoupon
            // 
            this.txtCoupon.Location = new System.Drawing.Point(383, 63);
            this.txtCoupon.Name = "txtCoupon";
            this.txtCoupon.Size = new System.Drawing.Size(166, 29);
            this.txtCoupon.TabIndex = 0;
            // 
            // btnCoupon
            // 
            this.btnCoupon.Location = new System.Drawing.Point(569, 60);
            this.btnCoupon.Name = "btnCoupon";
            this.btnCoupon.Size = new System.Drawing.Size(92, 32);
            this.btnCoupon.TabIndex = 1;
            this.btnCoupon.Text = "Coupon";
            this.btnCoupon.UseVisualStyleBackColor = true;
            this.btnCoupon.Click += new System.EventHandler(this.btnCoupon_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(379, 36);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(78, 24);
            this.label12.TabIndex = 20;
            this.label12.Text = "Coupon";
            // 
            // lblDiscount1
            // 
            this.lblDiscount1.AutoSize = true;
            this.lblDiscount1.Location = new System.Drawing.Point(565, 306);
            this.lblDiscount1.Name = "lblDiscount1";
            this.lblDiscount1.Size = new System.Drawing.Size(83, 24);
            this.lblDiscount1.TabIndex = 21;
            this.lblDiscount1.Text = "Discount";
            // 
            // lblDiscount
            // 
            this.lblDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDiscount.Location = new System.Drawing.Point(569, 330);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(92, 30);
            this.lblDiscount.TabIndex = 22;
            this.lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDiscountAmount
            // 
            this.lblDiscountAmount.ForeColor = System.Drawing.Color.Red;
            this.lblDiscountAmount.Location = new System.Drawing.Point(387, 366);
            this.lblDiscountAmount.Name = "lblDiscountAmount";
            this.lblDiscountAmount.Size = new System.Drawing.Size(162, 24);
            this.lblDiscountAmount.TabIndex = 23;
            this.lblDiscountAmount.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnUserView
            // 
            this.btnUserView.Location = new System.Drawing.Point(185, 801);
            this.btnUserView.Name = "btnUserView";
            this.btnUserView.Size = new System.Drawing.Size(143, 32);
            this.btnUserView.TabIndex = 24;
            this.btnUserView.Text = "&Shop";
            this.btnUserView.UseVisualStyleBackColor = true;
            this.btnUserView.Click += new System.EventHandler(this.btnUserView_Click);
            // 
            // CheckOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = global::FA21_Final_Project.Properties.Resources.shoesBackground;
            this.ClientSize = new System.Drawing.Size(673, 845);
            this.Controls.Add(this.btnUserView);
            this.Controls.Add(this.lblDiscountAmount);
            this.Controls.Add(this.lblDiscount);
            this.Controls.Add(this.lblDiscount1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtCoupon);
            this.Controls.Add(this.btnCoupon);
            this.Controls.Add(this.lnkNeedHelp);
            this.Controls.Add(this.btnCheckOut);
            this.Controls.Add(this.btnAddItems);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grpDeliveryType);
            this.Controls.Add(this.grpCreditCard);
            this.Controls.Add(this.grpPaymentType);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblDeliveryFee);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblTax);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblSubTotal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstOrderSummary);
            this.Controls.Add(this.grpCustomer);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CheckOut";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Check Out";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CheckOut_FormClosing);
            this.Load += new System.EventHandler(this.CheckOut_Load);
            this.grpCustomer.ResumeLayout(false);
            this.grpPaymentType.ResumeLayout(false);
            this.grpPaymentType.PerformLayout();
            this.grpCreditCard.ResumeLayout(false);
            this.grpCreditCard.PerformLayout();
            this.grpDeliveryType.ResumeLayout(false);
            this.grpDeliveryType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpCustomer;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.ListBox lstOrderSummary;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSubTotal;
        private System.Windows.Forms.Label lblTax;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDeliveryFee;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox grpPaymentType;
        private System.Windows.Forms.RadioButton rdoMastercard;
        private System.Windows.Forms.RadioButton rdoVisa;
        private System.Windows.Forms.GroupBox grpCreditCard;
        private System.Windows.Forms.ComboBox cboYear;
        private System.Windows.Forms.ComboBox cboMonth;
        private System.Windows.Forms.TextBox txtCRV;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtMiddleName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpDeliveryType;
        private System.Windows.Forms.RadioButton rdoExpress;
        private System.Windows.Forms.RadioButton rdoStandard;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAddItems;
        private System.Windows.Forms.Button btnCheckOut;
        private System.Windows.Forms.LinkLabel lnkNeedHelp;
        private System.Windows.Forms.TextBox txtCoupon;
        private System.Windows.Forms.Button btnCoupon;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblDiscount1;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label lblDiscountAmount;
        private System.Windows.Forms.MaskedTextBox txtCreditCard;
        private System.Windows.Forms.Button btnUserView;
    }
}