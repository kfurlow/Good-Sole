
namespace FA21_Final_Project
{
    partial class UserView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserView));
            this.btnBackToMain = new System.Windows.Forms.Button();
            this.btnCheckOut = new System.Windows.Forms.Button();
            this.btnViewCart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnManager = new System.Windows.Forms.Button();
            this.btnEmployee = new System.Windows.Forms.Button();
            this.lnkNeedHelp = new System.Windows.Forms.LinkLabel();
            this.picShoe = new System.Windows.Forms.PictureBox();
            this.lblShoeName = new System.Windows.Forms.Label();
            this.lblShoePrice = new System.Windows.Forms.Label();
            this.lblShoeQuantity = new System.Windows.Forms.Label();
            this.lblShoeDescription = new System.Windows.Forms.Label();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnAddToCart = new System.Windows.Forms.Button();
            this.lblPOSCus = new System.Windows.Forms.Label();
            this.lblPOSCustomer = new System.Windows.Forms.Label();
            this.lblPOSEmployee = new System.Windows.Forms.Label();
            this.lblPOSEmp = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picShoe)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBackToMain
            // 
            this.btnBackToMain.Location = new System.Drawing.Point(528, 15);
            this.btnBackToMain.Margin = new System.Windows.Forms.Padding(6);
            this.btnBackToMain.Name = "btnBackToMain";
            this.btnBackToMain.Size = new System.Drawing.Size(161, 32);
            this.btnBackToMain.TabIndex = 0;
            this.btnBackToMain.Text = "&Main Menu";
            this.btnBackToMain.UseVisualStyleBackColor = true;
            this.btnBackToMain.Click += new System.EventHandler(this.btnBackToMain_Click);
            // 
            // btnCheckOut
            // 
            this.btnCheckOut.Location = new System.Drawing.Point(874, 15);
            this.btnCheckOut.Margin = new System.Windows.Forms.Padding(6);
            this.btnCheckOut.Name = "btnCheckOut";
            this.btnCheckOut.Size = new System.Drawing.Size(161, 32);
            this.btnCheckOut.TabIndex = 2;
            this.btnCheckOut.Text = "&Check Out";
            this.btnCheckOut.UseVisualStyleBackColor = true;
            this.btnCheckOut.Click += new System.EventHandler(this.btnCheckOut_Click);
            // 
            // btnViewCart
            // 
            this.btnViewCart.Location = new System.Drawing.Point(701, 15);
            this.btnViewCart.Margin = new System.Windows.Forms.Padding(6);
            this.btnViewCart.Name = "btnViewCart";
            this.btnViewCart.Size = new System.Drawing.Size(161, 32);
            this.btnViewCart.TabIndex = 1;
            this.btnViewCart.Text = "&View Cart";
            this.btnViewCart.UseVisualStyleBackColor = true;
            this.btnViewCart.Click += new System.EventHandler(this.btnViewCart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Impact", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(295, 80);
            this.label1.TabIndex = 3;
            this.label1.Text = "Good Sole";
            // 
            // btnManager
            // 
            this.btnManager.Enabled = false;
            this.btnManager.Location = new System.Drawing.Point(182, 15);
            this.btnManager.Margin = new System.Windows.Forms.Padding(6);
            this.btnManager.Name = "btnManager";
            this.btnManager.Size = new System.Drawing.Size(161, 32);
            this.btnManager.TabIndex = 4;
            this.btnManager.Text = "Manage&r";
            this.btnManager.UseVisualStyleBackColor = true;
            this.btnManager.Visible = false;
            this.btnManager.Click += new System.EventHandler(this.btnManager_Click);
            // 
            // btnEmployee
            // 
            this.btnEmployee.Enabled = false;
            this.btnEmployee.Location = new System.Drawing.Point(355, 15);
            this.btnEmployee.Margin = new System.Windows.Forms.Padding(6);
            this.btnEmployee.Name = "btnEmployee";
            this.btnEmployee.Size = new System.Drawing.Size(161, 32);
            this.btnEmployee.TabIndex = 5;
            this.btnEmployee.Text = "&Employee";
            this.btnEmployee.UseVisualStyleBackColor = true;
            this.btnEmployee.Visible = false;
            this.btnEmployee.Click += new System.EventHandler(this.btnEmployee_Click);
            // 
            // lnkNeedHelp
            // 
            this.lnkNeedHelp.AutoSize = true;
            this.lnkNeedHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkNeedHelp.Location = new System.Drawing.Point(12, 15);
            this.lnkNeedHelp.Name = "lnkNeedHelp";
            this.lnkNeedHelp.Size = new System.Drawing.Size(85, 18);
            this.lnkNeedHelp.TabIndex = 6;
            this.lnkNeedHelp.TabStop = true;
            this.lnkNeedHelp.Text = "Need Help?";
            this.lnkNeedHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkNeedHelp_LinkClicked);
            // 
            // picShoe
            // 
            this.picShoe.Location = new System.Drawing.Point(401, 190);
            this.picShoe.Name = "picShoe";
            this.picShoe.Size = new System.Drawing.Size(266, 208);
            this.picShoe.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picShoe.TabIndex = 0;
            this.picShoe.TabStop = false;
            // 
            // lblShoeName
            // 
            this.lblShoeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShoeName.Location = new System.Drawing.Point(401, 147);
            this.lblShoeName.Name = "lblShoeName";
            this.lblShoeName.Size = new System.Drawing.Size(266, 40);
            this.lblShoeName.TabIndex = 1;
            this.lblShoeName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblShoePrice
            // 
            this.lblShoePrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShoePrice.Location = new System.Drawing.Point(401, 401);
            this.lblShoePrice.Name = "lblShoePrice";
            this.lblShoePrice.Size = new System.Drawing.Size(149, 40);
            this.lblShoePrice.TabIndex = 2;
            this.lblShoePrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblShoeQuantity
            // 
            this.lblShoeQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShoeQuantity.Location = new System.Drawing.Point(556, 401);
            this.lblShoeQuantity.Name = "lblShoeQuantity";
            this.lblShoeQuantity.Size = new System.Drawing.Size(111, 40);
            this.lblShoeQuantity.TabIndex = 3;
            this.lblShoeQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblShoeDescription
            // 
            this.lblShoeDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShoeDescription.Location = new System.Drawing.Point(246, 453);
            this.lblShoeDescription.Name = "lblShoeDescription";
            this.lblShoeDescription.Size = new System.Drawing.Size(576, 142);
            this.lblShoeDescription.TabIndex = 4;
            this.lblShoeDescription.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(246, 278);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(149, 40);
            this.btnPrevious.TabIndex = 4;
            this.btnPrevious.Text = "&Previous";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(673, 278);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(149, 40);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "&Next ";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnAddToCart
            // 
            this.btnAddToCart.Location = new System.Drawing.Point(460, 598);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(149, 40);
            this.btnAddToCart.TabIndex = 5;
            this.btnAddToCart.Text = "&Add to Cart";
            this.btnAddToCart.UseVisualStyleBackColor = true;
            this.btnAddToCart.Click += new System.EventHandler(this.btnAddToCart_Click);
            // 
            // lblPOSCus
            // 
            this.lblPOSCus.AutoSize = true;
            this.lblPOSCus.Location = new System.Drawing.Point(870, 121);
            this.lblPOSCus.Name = "lblPOSCus";
            this.lblPOSCus.Size = new System.Drawing.Size(91, 24);
            this.lblPOSCus.TabIndex = 8;
            this.lblPOSCus.Text = "Customer";
            this.lblPOSCus.Visible = false;
            // 
            // lblPOSCustomer
            // 
            this.lblPOSCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPOSCustomer.Location = new System.Drawing.Point(869, 145);
            this.lblPOSCustomer.Name = "lblPOSCustomer";
            this.lblPOSCustomer.Size = new System.Drawing.Size(190, 33);
            this.lblPOSCustomer.TabIndex = 9;
            this.lblPOSCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPOSCustomer.Visible = false;
            // 
            // lblPOSEmployee
            // 
            this.lblPOSEmployee.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPOSEmployee.Location = new System.Drawing.Point(869, 77);
            this.lblPOSEmployee.Name = "lblPOSEmployee";
            this.lblPOSEmployee.Size = new System.Drawing.Size(190, 33);
            this.lblPOSEmployee.TabIndex = 11;
            this.lblPOSEmployee.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPOSEmployee.Visible = false;
            // 
            // lblPOSEmp
            // 
            this.lblPOSEmp.AutoSize = true;
            this.lblPOSEmp.Location = new System.Drawing.Point(870, 53);
            this.lblPOSEmp.Name = "lblPOSEmp";
            this.lblPOSEmp.Size = new System.Drawing.Size(96, 24);
            this.lblPOSEmp.TabIndex = 10;
            this.lblPOSEmp.Text = "Employee";
            this.lblPOSEmp.Visible = false;
            // 
            // UserView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::FA21_Final_Project.Properties.Resources.shoesBackground;
            this.ClientSize = new System.Drawing.Size(1069, 653);
            this.Controls.Add(this.lblPOSEmployee);
            this.Controls.Add(this.lblPOSEmp);
            this.Controls.Add(this.lblPOSCustomer);
            this.Controls.Add(this.lblPOSCus);
            this.Controls.Add(this.btnAddToCart);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.lnkNeedHelp);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnEmployee);
            this.Controls.Add(this.lblShoeDescription);
            this.Controls.Add(this.btnManager);
            this.Controls.Add(this.lblShoeQuantity);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblShoePrice);
            this.Controls.Add(this.btnViewCart);
            this.Controls.Add(this.lblShoeName);
            this.Controls.Add(this.picShoe);
            this.Controls.Add(this.btnCheckOut);
            this.Controls.Add(this.btnBackToMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User View";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserView_FormClosing);
            this.Load += new System.EventHandler(this.UserView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picShoe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBackToMain;
        private System.Windows.Forms.Button btnCheckOut;
        private System.Windows.Forms.Button btnViewCart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnManager;
        private System.Windows.Forms.Button btnEmployee;
        private System.Windows.Forms.LinkLabel lnkNeedHelp;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnAddToCart;
        public System.Windows.Forms.Label lblShoeDescription;
        public System.Windows.Forms.Label lblShoeQuantity;
        public System.Windows.Forms.Label lblShoePrice;
        public System.Windows.Forms.Label lblShoeName;
        public System.Windows.Forms.PictureBox picShoe;
        private System.Windows.Forms.Label lblPOSCus;
        public System.Windows.Forms.Label lblPOSCustomer;
        public System.Windows.Forms.Label lblPOSEmployee;
        private System.Windows.Forms.Label lblPOSEmp;
    }
}