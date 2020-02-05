namespace ClientTester
{
    partial class Main
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
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnGreetMe = new System.Windows.Forms.Button();
            this.grpLogin = new System.Windows.Forms.GroupBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnLogOff = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.grpMessage = new System.Windows.Forms.GroupBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpServiceCallback = new System.Windows.Forms.GroupBox();
            this.txtServiceMessage = new System.Windows.Forms.TextBox();
            this.grpLogin.SuspendLayout();
            this.grpMessage.SuspendLayout();
            this.grpServiceCallback.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Location = new System.Drawing.Point(200, 163);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(129, 59);
            this.btnLogin.TabIndex = 7;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnGreetMe
            // 
            this.btnGreetMe.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGreetMe.Location = new System.Drawing.Point(10, 47);
            this.btnGreetMe.Name = "btnGreetMe";
            this.btnGreetMe.Size = new System.Drawing.Size(169, 64);
            this.btnGreetMe.TabIndex = 9;
            this.btnGreetMe.Text = "Greet Me";
            this.btnGreetMe.UseVisualStyleBackColor = true;
            this.btnGreetMe.Click += new System.EventHandler(this.btnGreetMe_Click);
            // 
            // grpLogin
            // 
            this.grpLogin.BackColor = System.Drawing.Color.Bisque;
            this.grpLogin.Controls.Add(this.btnRegister);
            this.grpLogin.Controls.Add(this.btnLogOff);
            this.grpLogin.Controls.Add(this.txtPassword);
            this.grpLogin.Controls.Add(this.txtUsername);
            this.grpLogin.Controls.Add(this.lblPassword);
            this.grpLogin.Controls.Add(this.lblUsername);
            this.grpLogin.Controls.Add(this.btnLogin);
            this.grpLogin.Location = new System.Drawing.Point(12, 97);
            this.grpLogin.Name = "grpLogin";
            this.grpLogin.Size = new System.Drawing.Size(511, 228);
            this.grpLogin.TabIndex = 2;
            this.grpLogin.TabStop = false;
            this.grpLogin.Text = "Authenticate";
            // 
            // btnRegister
            // 
            this.btnRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegister.Location = new System.Drawing.Point(22, 163);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(161, 59);
            this.btnRegister.TabIndex = 6;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnLogOff
            // 
            this.btnLogOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogOff.Location = new System.Drawing.Point(346, 163);
            this.btnLogOff.Name = "btnLogOff";
            this.btnLogOff.Size = new System.Drawing.Size(156, 59);
            this.btnLogOff.TabIndex = 8;
            this.btnLogOff.Text = "Log Off";
            this.btnLogOff.UseVisualStyleBackColor = true;
            this.btnLogOff.Click += new System.EventHandler(this.btnLogOff_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(200, 97);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(277, 44);
            this.txtPassword.TabIndex = 5;
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(200, 36);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(277, 44);
            this.txtUsername.TabIndex = 3;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(15, 100);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(164, 37);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "password:";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.Location = new System.Drawing.Point(15, 36);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(168, 37);
            this.lblUsername.TabIndex = 2;
            this.lblUsername.Text = "username:";
            // 
            // grpMessage
            // 
            this.grpMessage.BackColor = System.Drawing.Color.Bisque;
            this.grpMessage.Controls.Add(this.txtResult);
            this.grpMessage.Controls.Add(this.btnGreetMe);
            this.grpMessage.Location = new System.Drawing.Point(12, 349);
            this.grpMessage.Name = "grpMessage";
            this.grpMessage.Size = new System.Drawing.Size(511, 132);
            this.grpMessage.TabIndex = 3;
            this.grpMessage.TabStop = false;
            this.grpMessage.Text = "Service Operations";
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(200, 26);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(286, 85);
            this.txtResult.TabIndex = 10;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.Info;
            this.lblTitle.Location = new System.Drawing.Point(28, 32);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(486, 36);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "WCF Authenticate Service Tester";
            // 
            // grpServiceCallback
            // 
            this.grpServiceCallback.BackColor = System.Drawing.Color.Bisque;
            this.grpServiceCallback.Controls.Add(this.txtServiceMessage);
            this.grpServiceCallback.Location = new System.Drawing.Point(12, 497);
            this.grpServiceCallback.Name = "grpServiceCallback";
            this.grpServiceCallback.Size = new System.Drawing.Size(511, 148);
            this.grpServiceCallback.TabIndex = 5;
            this.grpServiceCallback.TabStop = false;
            this.grpServiceCallback.Text = "Service Callback";
            // 
            // txtServiceMessage
            // 
            this.txtServiceMessage.Location = new System.Drawing.Point(21, 37);
            this.txtServiceMessage.Multiline = true;
            this.txtServiceMessage.Name = "txtServiceMessage";
            this.txtServiceMessage.ReadOnly = true;
            this.txtServiceMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtServiceMessage.Size = new System.Drawing.Size(464, 99);
            this.txtServiceMessage.TabIndex = 11;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCoral;
            this.ClientSize = new System.Drawing.Size(543, 662);
            this.Controls.Add(this.grpServiceCallback);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.grpMessage);
            this.Controls.Add(this.grpLogin);
            this.Name = "Main";
            this.Text = "WCF Authenticate Client Tester";
            this.grpLogin.ResumeLayout(false);
            this.grpLogin.PerformLayout();
            this.grpMessage.ResumeLayout(false);
            this.grpMessage.PerformLayout();
            this.grpServiceCallback.ResumeLayout(false);
            this.grpServiceCallback.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnGreetMe;
        private System.Windows.Forms.GroupBox grpLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.GroupBox grpMessage;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnLogOff;
        private System.Windows.Forms.GroupBox grpServiceCallback;
        private System.Windows.Forms.TextBox txtServiceMessage;
        private System.Windows.Forms.Button btnRegister;
    }
}

