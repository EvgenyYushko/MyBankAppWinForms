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
    public partial class CreateUserForm : UserForm
    {
        public CreateUserForm(Form inputForm) :
            base(inputForm)
        {
            base.Text = "Регистарция";
            base._tbOk.Text = "Создать профиль";
        }

        override public bool SaveOrInsertIntoDB()
        {
            string sqlExpression = $"INSERT tbUsers VALUES ('{_gender}', '{_dataOfBirthStr}', '{_password}', '{_login}', NULL, NULL, '{_FName}', '{_LName}' )";

            return DataBaseService.ExecInsert(sqlExpression);
        }
    }
}


