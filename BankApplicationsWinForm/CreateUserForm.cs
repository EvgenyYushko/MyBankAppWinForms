using BankApplicationsWinForm.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
            base._btOk.Text = "Создать профиль";
        }

        public async override Task<bool> SaveData()
        {
            string sqlExpression = $"INSERT tbUsers VALUES ('{_gender}', '{_dataOfBirthStr}', '{_password}', '{_login}', NULL, NULL, '{_FName}', '{_LName}' )";

            return await DataBaseService.ExecInsertAsync(sqlExpression);
        }
    }
}


