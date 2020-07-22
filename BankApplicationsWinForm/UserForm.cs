using BankApplicationsWinForm.Interfaces;
using BankApplicationsWinForm.Interfaces.Cheaper;
using BankApplicationsWinForm.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        IMustBeEntered _mustBeEntered;
        ICheaper _cheaper;
        string _fieldsText;

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

        private void btOK_Click(object sender, EventArgs e)
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

                if (!SaveOrInsertIntoDB())
                {
                    Service.LogWrite($"Ошибка добавления/обновления данных в базу: ");
                    throw new Exception("Ошибка добавления/обновления данных в базу");
                }

                Close();
            }
        }

        public virtual bool SaveOrInsertIntoDB() => false;

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


