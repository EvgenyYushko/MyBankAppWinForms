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
    public partial class ViewUserForm : UserForm
    {
        IDeCheaper deCheaper;

        public ViewUserForm(Form inputForm) :
            base(inputForm)
        {
            base.Text = "Ваш профиль";
            base._tbOk.Text = "Сохранить";
            deCheaper = new XORCipher();
        }

        public override bool SaveOrInsertIntoDB()
        {
            return DataBaseService.ExecUpdate("User_ID = @User_ID", "User_ID", $"{((MainForm)base._form)._userID}", "tbUsers",
                $"SET [Gender] = '{_gender}', [FName] = '{_FName}', [LName] = '{_LName}', [Password] = '{_password}', [Login] = '{_login}', [DateOfBirth] = '{_dataOfBirthStr}' ");
        }

        public override void userForm_Load(object sender, EventArgs e)
        {
            var dt = DataBaseService.ExecSelect("SELECT * FROM tbUsers", "User_ID = @User_ID", "User_ID", $"{((MainForm)_form)._userID}", "tbUsers");

            if (dt.Rows.Count != 0)
            {
                DataRow row = dt.Rows[0];

                var FName = (string)row["FName"];
                var LName = (string)row["LName"];
                var login = (string)row["Login"];
                var gender = (bool)row["Gender"];
                var dataOfBirth = (DateTime)row["DateOfBirth"];

                var cheapPas = (string)row["Password"];
                var password = deCheaper.Decrypt(cheapPas);

                if (!SaveOrInsertIntoDB())
                {
                    Service.LogWrite($"Ошибка добавления/обновления данных в базу: ");
                    throw new Exception("Ошибка добавления/обновления данных в базу");
                }

                this._tbName.Text = FName;
                this._tbLName.Text = LName;
                this._tbReapidPassword.Text = this._tbPassword.Text = password;
                this._tbLogin.Text = login;
                this._cbGender.SelectedIndex = gender ? 1 : 0;
                this._dataOfBirth.Value = dataOfBirth;
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


