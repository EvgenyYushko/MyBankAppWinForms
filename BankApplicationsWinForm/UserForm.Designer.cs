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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btLoadImage = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(166, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Имя";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(166, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Фамилия";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(166, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Придумайте пароль";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(166, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Повторите пароль";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(273, 6);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(110, 20);
            this.tbName.TabIndex = 1;
            this.tbName.Tag = "Имя";
            this.tbName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbName_KeyPress);
            // 
            // tbLName
            // 
            this.tbLName.Location = new System.Drawing.Point(273, 29);
            this.tbLName.Name = "tbLName";
            this.tbLName.Size = new System.Drawing.Size(110, 20);
            this.tbLName.TabIndex = 2;
            this.tbLName.Tag = "Фамилия";
            this.tbLName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbName_KeyPress);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(273, 159);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(110, 20);
            this.tbPassword.TabIndex = 6;
            this.tbPassword.Tag = "Придумайте пароль";
            // 
            // tbReapidPassword
            // 
            this.tbReapidPassword.Location = new System.Drawing.Point(273, 187);
            this.tbReapidPassword.Name = "tbReapidPassword";
            this.tbReapidPassword.PasswordChar = '*';
            this.tbReapidPassword.Size = new System.Drawing.Size(110, 20);
            this.tbReapidPassword.TabIndex = 7;
            this.tbReapidPassword.Tag = "Повторите пароль";
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(273, 216);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(110, 32);
            this.btOK.TabIndex = 8;
            this.btOK.Text = "Создать профиль";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // tbLogin
            // 
            this.tbLogin.Location = new System.Drawing.Point(273, 79);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(110, 20);
            this.tbLogin.TabIndex = 4;
            this.tbLogin.Tag = "Логин";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(166, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Логин";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(166, 61);
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
            this.cbGender.Location = new System.Drawing.Point(273, 52);
            this.cbGender.Name = "cbGender";
            this.cbGender.Size = new System.Drawing.Size(110, 21);
            this.cbGender.TabIndex = 3;
            this.cbGender.Tag = "Пол";
            // 
            // dtpDataOfBirth
            // 
            this.dtpDataOfBirth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataOfBirth.Location = new System.Drawing.Point(273, 105);
            this.dtpDataOfBirth.Name = "dtpDataOfBirth";
            this.dtpDataOfBirth.Size = new System.Drawing.Size(110, 20);
            this.dtpDataOfBirth.TabIndex = 5;
            this.dtpDataOfBirth.Tag = "Дата рождения";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(166, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Дата рождения";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(13, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(147, 194);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            this.pictureBox1.MouseHover += new System.EventHandler(this.pictureBox1_MouseHover);
            // 
            // btLoadImage
            // 
            this.btLoadImage.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btLoadImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btLoadImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btLoadImage.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btLoadImage.Location = new System.Drawing.Point(13, 178);
            this.btLoadImage.Name = "btLoadImage";
            this.btLoadImage.Size = new System.Drawing.Size(147, 30);
            this.btLoadImage.TabIndex = 12;
            this.btLoadImage.Text = "Загрузить фотографию";
            this.btLoadImage.UseVisualStyleBackColor = false;
            this.btLoadImage.Visible = false;
            this.btLoadImage.Click += new System.EventHandler(this.btLoadImage_Click);
            this.btLoadImage.MouseHover += new System.EventHandler(this.btLoadImage_MouseHover);
            // 
            // btClose
            // 
            this.btClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClose.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btClose.Location = new System.Drawing.Point(127, 13);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(33, 36);
            this.btClose.TabIndex = 13;
            this.btClose.Text = "X";
            this.btClose.UseVisualStyleBackColor = false;
            this.btClose.Visible = false;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            this.btClose.MouseHover += new System.EventHandler(this.btClose_MouseHover);
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 260);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btLoadImage);
            this.Controls.Add(this.pictureBox1);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btLoadImage;
        private System.Windows.Forms.Button btClose;
    }
}