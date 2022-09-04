
namespace FA21_Final_Project
{
    partial class LogonScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogonScreen));
            this.lnkHelp = new System.Windows.Forms.LinkLabel();
            this.lnkViewInventory = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lnkPassword = new System.Windows.Forms.LinkLabel();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnMain = new System.Windows.Forms.Button();
            this.rdoShow = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // lnkHelp
            // 
            this.lnkHelp.AutoSize = true;
            this.lnkHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkHelp.Location = new System.Drawing.Point(15, 9);
            this.lnkHelp.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lnkHelp.Name = "lnkHelp";
            this.lnkHelp.Size = new System.Drawing.Size(85, 18);
            this.lnkHelp.TabIndex = 0;
            this.lnkHelp.TabStop = true;
            this.lnkHelp.Text = "Need Help?";
            this.lnkHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHelp_LinkClicked);
            // 
            // lnkViewInventory
            // 
            this.lnkViewInventory.AutoSize = true;
            this.lnkViewInventory.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkViewInventory.Location = new System.Drawing.Point(332, 9);
            this.lnkViewInventory.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lnkViewInventory.Name = "lnkViewInventory";
            this.lnkViewInventory.Size = new System.Drawing.Size(102, 18);
            this.lnkViewInventory.TabIndex = 1;
            this.lnkViewInventory.TabStop = true;
            this.lnkViewInventory.Text = "View Inventory";
            this.lnkViewInventory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkViewInventory_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(135, 145);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.ShortcutsEnabled = false;
            this.txtUsername.Size = new System.Drawing.Size(182, 29);
            this.txtUsername.TabIndex = 2;
            this.txtUsername.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUsername_KeyPress);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(135, 204);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.ShortcutsEnabled = false;
            this.txtPassword.Size = new System.Drawing.Size(182, 29);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // lnkPassword
            // 
            this.lnkPassword.AutoSize = true;
            this.lnkPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkPassword.Location = new System.Drawing.Point(160, 236);
            this.lnkPassword.Name = "lnkPassword";
            this.lnkPassword.Size = new System.Drawing.Size(131, 18);
            this.lnkPassword.TabIndex = 4;
            this.lnkPassword.TabStop = true;
            this.lnkPassword.Text = "Forgot Password?";
            this.lnkPassword.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkPassword_LinkClicked);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(46, 296);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(102, 32);
            this.btnCreate.TabIndex = 5;
            this.btnCreate.Text = "&Create Account";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(173, 296);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(102, 32);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.Text = "&Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnMain
            // 
            this.btnMain.Location = new System.Drawing.Point(300, 296);
            this.btnMain.Name = "btnMain";
            this.btnMain.Size = new System.Drawing.Size(102, 32);
            this.btnMain.TabIndex = 7;
            this.btnMain.Text = "&Main";
            this.btnMain.UseVisualStyleBackColor = true;
            this.btnMain.Click += new System.EventHandler(this.btnMain_Click);
            // 
            // rdoShow
            // 
            this.rdoShow.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoShow.BackgroundImage = global::FA21_Final_Project.Properties.Resources.download;
            this.rdoShow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rdoShow.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rdoShow.Location = new System.Drawing.Point(314, 204);
            this.rdoShow.Name = "rdoShow";
            this.rdoShow.Size = new System.Drawing.Size(35, 29);
            this.rdoShow.TabIndex = 8;
            this.rdoShow.TabStop = true;
            this.rdoShow.UseVisualStyleBackColor = true;
            this.rdoShow.Click += new System.EventHandler(this.rdoShow_Click);
            // 
            // LogonScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 371);
            this.Controls.Add(this.rdoShow);
            this.Controls.Add(this.btnMain);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.lnkPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lnkViewInventory);
            this.Controls.Add(this.lnkHelp);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogonScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Logon Screen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LogonScreen_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lnkHelp;
        private System.Windows.Forms.LinkLabel lnkViewInventory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.LinkLabel lnkPassword;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnMain;
        private System.Windows.Forms.RadioButton rdoShow;
    }
}