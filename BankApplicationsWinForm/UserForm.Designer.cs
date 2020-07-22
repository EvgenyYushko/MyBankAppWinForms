namespace BankApplicationsWinForm
{
    partial class UserForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbLName = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbReapidPassword = new System.Windows.Forms.TextBox();
            this.btOK = new System.Windows.Forms.Button();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbGender = new System.Windows.Forms.ComboBox();
            this.dtpDataOfBirth = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Имя";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Фамилия";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Придумайте пароль";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Повторите пароль";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(119, 2);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(110, 20);
            this.tbName.TabIndex = 1;
            this.tbName.Tag = "Имя";
            this.tbName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbName_KeyPress);
            // 
            // tbLName
            // 
            this.tbLName.Location = new System.Drawing.Point(119, 25);
            this.tbLName.Name = "tbLName";
            this.tbLName.Size = new System.Drawing.Size(110, 20);
            this.tbLName.TabIndex = 2;
            this.tbLName.Tag = "Фамилия";
            this.tbLName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbName_KeyPress);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(119, 155);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(110, 20);
            this.tbPassword.TabIndex = 6;
            this.tbPassword.Tag = "Придумайте пароль";
            // 
            // tbReapidPassword
            // 
            this.tbReapidPassword.Location = new System.Drawing.Point(119, 183);
            this.tbReapidPassword.Name = "tbReapidPassword";
            this.tbReapidPassword.PasswordChar = '*';
            this.tbReapidPassword.Size = new System.Drawing.Size(110, 20);
            this.tbReapidPassword.TabIndex = 7;
            this.tbReapidPassword.Tag = "Повторите пароль";
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(68, 216);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(110, 32);
            this.btOK.TabIndex = 8;
            this.btOK.Text = "Создать профиль";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // tbLogin
            // 
            this.tbLogin.Location = new System.Drawing.Point(119, 75);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(110, 20);
            this.tbLogin.TabIndex = 4;
            this.tbLogin.Tag = "Логин";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Логин";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Пол";
            // 
            // cbGender
            // 
            this.cbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGender.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbGender.FormattingEnabled = true;
            this.cbGender.Location = new System.Drawing.Point(119, 48);
            this.cbGender.Name = "cbGender";
            this.cbGender.Size = new System.Drawing.Size(110, 21);
            this.cbGender.TabIndex = 3;
            this.cbGender.Tag = "Пол";
            // 
            // dtpDataOfBirth
            // 
            this.dtpDataOfBirth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataOfBirth.Location = new System.Drawing.Point(119, 101);
            this.dtpDataOfBirth.Name = "dtpDataOfBirth";
            this.dtpDataOfBirth.Size = new System.Drawing.Size(110, 20);
            this.dtpDataOfBirth.TabIndex = 5;
            this.dtpDataOfBirth.Tag = "Дата рождения";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Дата рождения";
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 260);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dtpDataOfBirth);
            this.Controls.Add(this.cbGender);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbLogin);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.tbReapidPassword);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbLName);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Регистрация";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.userForm_FormClosed);
            this.Load += new System.EventHandler(this.userForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbLName;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbReapidPassword;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.TextBox tbLogin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbGender;
        private System.Windows.Forms.DateTimePicker dtpDataOfBirth;
        private System.Windows.Forms.Label label7;
    }
}