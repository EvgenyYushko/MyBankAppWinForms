using BankApplicationsWinForm.Interfaces;
using BankApplicationsWinForm.Interfaces.Cheaper;
using BankApplicationsWinForm.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace BankApplicationsWinForm
{
    public partial class UserForm : Form
    {
        protected Form _form;
        protected string _FName;
        protected string _LName;
        protected string _password;
        protected string _login;
        protected bool _gender;
        protected string _dataOfBirthStr;
        string _fullFileName;
        protected int _User_ID;
        protected byte[] _imageData;
        private int myVar;
        bool _resultSaveImage;

        protected PictureBox _pictureBox1
        {
            get { return pictureBox1; }
            set { pictureBox1 = value; }
        }


        IMustBeEntered _mustBeEntered;
        ICheaper _cheaper;
        string _fieldsText;
        Timer timer;

        public UserForm(Form inputForm)
        {
            this._form = inputForm;

            inputForm.Enabled = false;
            InitializeComponent();

            string[] genders = { "Женский", "Мужской" };
            cbGender.Items.AddRange(genders);
            cbGender.SelectedItem = "Мужской";

            _mustBeEntered = new MustBeEnteredVerification(this);
            _cheaper = new XORCipher();

            if (this._form is MainForm)
                _User_ID = ((MainForm)this._form)._userID;
        }

        #region Properties

        protected DateTimePicker _dataOfBirth
        {
            get { return dtpDataOfBirth; }
            set { _dataOfBirth = value; }
        }

        protected TextBox _tbName
        {
            get { return tbName; }
            set { tbName = value; }
        }

        protected TextBox _tbLName
        {
            get { return tbLName; }
            set { tbLName = value; }
        }

        protected ComboBox _cbGender
        {
            get { return cbGender; }
            set { cbGender = value; }
        }

        protected TextBox _tbLogin
        {
            get { return tbLogin; }
            set { tbLogin = value; }
        }

        protected TextBox _tbReapidPassword
        {
            get { return tbReapidPassword; }
            set { tbReapidPassword = value; }
        }

        protected TextBox _tbPassword
        {
            get { return tbPassword; }
            set { tbPassword = value; }
        }

        protected Button _tbOk { get => btOK; set => btOK = value; }

        #endregion

        private async void btOK_Click(object sender, EventArgs e)
        {
            if (_mustBeEntered.GetMustBeEnteredFields(out _fieldsText))
                MessageBox.Show($"Не заполненны все обязательные поля\n\n{_fieldsText}", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            else if (_tbPassword.Text != _tbReapidPassword.Text || string.IsNullOrWhiteSpace(tbPassword.Text) || string.IsNullOrWhiteSpace(_tbReapidPassword.Text))
                MessageBox.Show("Пароли не совпадают", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            else
            {
                _FName = _tbName.Text;
                _LName = _tbLName.Text;
                _login = _tbLogin.Text;
                _gender = _cbGender.SelectedIndex == 1 ? true : false;
                _dataOfBirthStr = _dataOfBirth.Value.ToShortDateString();
                _password = _cheaper.Encrypt(_tbReapidPassword.Text);

                if (!await SaveData())
                {
                    Service.LogWrite($"Ошибка добавления/обновления данных в базу: ");
                    throw new Exception("Ошибка добавления/обновления данных в базу");
                }

                if (!await SaveImage())
                {
                    Service.LogWrite($"Ошибка добавления/обновления изображения в базу: ");
                    throw new Exception("Ошибка добавления/обновления изображения в базу");
                }

                Close();
                _form.Activate();
            }
        }

        private async Task<bool> SaveImage()
        {
            if (_imageData != null)
            {
                // Костыль
                var dtu = await DataBaseService.ExecSelect("SELECT TOP 1 * FROM tbUsers ORDER BY [User_ID] DESC", "tbUsers");
                if (dtu.Rows.Count > 0)
                {
                    DataRow row = dtu.Rows[0];

                    var User_ID = (int)row["User_ID"];
                    _User_ID = User_ID;
                }

                var dt = await DataBaseService.ExecSelect("SELECT * FROM tbFiles", "User_ID = @User_ID", "User_ID", $"{ _User_ID}", "tbFiles");
                if (dt.Rows.Count > 0)
                {
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                    {
                        await connection.OpenAsync();
                        SqlCommand command = new SqlCommand();
                        command.Connection = connection;
                        command.CommandText = @"UPDATE tbFiles SET Image = @ImageData WHERE User_ID = @User_ID";
                        command.Parameters.Add("@User_ID", SqlDbType.NVarChar, 1000);
                        command.Parameters.Add("@ImageData", SqlDbType.Image, 1000000000);

                        command.Parameters["@User_ID"].Value = _User_ID;
                        command.Parameters["@ImageData"].Value = _imageData;

                        return await command.ExecuteNonQueryAsync() == 1 ? true : false;
                    }
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                    {
                        await connection.OpenAsync();
                        SqlCommand command = new SqlCommand();
                        command.Connection = connection;
                        command.CommandText = @"INSERT INTO tbFiles VALUES (@User_ID, @ImageData)";
                        command.Parameters.Add("@User_ID", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@ImageData", SqlDbType.Image, 1000000000);

                        command.Parameters["@User_ID"].Value = _User_ID;
                        command.Parameters["@ImageData"].Value = _imageData;

                        return await command.ExecuteNonQueryAsync() == 1 ? true : false;
                    }
                }
            }
            else return true;
        }

        public async virtual Task<bool> SaveData()
        {
            return await DataBaseService.ExecSelect("test", "test", "test", "test", "test", "test");
        }

        private void tbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            char letter = e.KeyChar;
            if (!Char.IsLetter(letter) && letter != 8)
            {
                e.Handled = true;
            }
        }

        private void userForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _form.Enabled = true;
        }

        public virtual void userForm_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            this.btLoadImage.Visible = true;
            this.btClose.Visible = true;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            timer = new Timer() { Interval = 500 };
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.btLoadImage.Visible = false;
            this.btClose.Visible = false;
            timer.Stop();
        }

        private void btLoadImage_MouseHover(object sender, EventArgs e)
        {
            timer.Stop();
            this.btLoadImage.Visible = true;
            this.btClose.Visible = true;
        }

        private void btClose_MouseHover(object sender, EventArgs e)
        {
            timer.Stop();
            this.btLoadImage.Visible = true;
            this.btClose.Visible = true;
        }

        private void btLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения |*.jpg;*.jpeg;";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.DialogResult = DialogResult.OK;
                _fullFileName = openFileDialog.FileName;

                FileInfo file = new FileInfo(_fullFileName);
                if (file.Length > 20971520)
                {
                    Service.LogWrite($"Файл {_fullFileName} слишком большой, и не может быть загружен в базу!");
                    throw new Exception($"Файл {_fullFileName} слишком большой, и не может быть загружен в базу!");
                }

                using (FileStream fs = new FileStream(_fullFileName, FileMode.Open))
                {
                    _imageData = new byte[fs.Length];
                    fs.Read(_imageData, 0, _imageData.Length);
                }
                
                pictureBox1.Image = Image.FromFile(_fullFileName);
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;// RemovePropertyItem(1);

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"DELETE tbFiles WHERE User_ID = @User_ID";
                command.Parameters.Add("@User_ID", SqlDbType.NVarChar, 50);

                command.Parameters["@User_ID"].Value = _User_ID;

                command.ExecuteNonQuery();
            }
        }
    }
    #region OldRealization
    //[Serializable]
    //public class User
    //{
    //    private string name;
    //    private string fio;
    //    private string password;

    //    public string Name
    //    {
    //        get { return name; }
    //        set { name = value; }
    //    }

    //    public string Fio
    //    {
    //        get { return fio; }
    //        set { fio = value; }
    //    }

    //    public string Password
    //    {
    //        get { return password; }
    //        set { password = value; }
    //    }

    //    public User()
    //    {
    //    } 
    //}
    #endregion
}


