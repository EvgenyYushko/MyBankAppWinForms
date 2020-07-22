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

        override public void btOK_Click(object sender, EventArgs e)
        {
            if (_tbPassword.Text != _tbReapidPassword.Text)
                MessageBox.Show("Пароли не совпадают", "Внимание!");
            else
            {
                var FName = _tbName.Text;
                var LName = _tbLName.Text;
                var password = _tbReapidPassword.Text;
                var login = _tbLogin.Text;
                bool gender = _cbGender.SelectedIndex == 1 ? true : false;

                //Новая реализация сохранения в базу
                string sqlExpression = $"INSERT tbUsers VALUES ('{gender}', NULL, '{password}', '{login}', NULL, NULL, '{FName}', '{LName}' )";
                if (!DataBaseService.ExecInsert(sqlExpression))
                {
                    Service.LogWrite($"Ошибка добавления данных в базу: ");
                    throw new Exception("Ошибка добавления данных в базу");
                }

                #region OldRealisation

                //try
                //{
                //    XmlSerializer deserializeUser = new XmlSerializer(typeof(List<User>));
                //    //сначало загружаем базу
                //    using (var stream = new FileStream($"UsersData.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
                //    {
                //        userList = deserializeUser.Deserialize(stream) as List<User>;
                //        Service.LogWrite($"User {textBox1.Text} загружен");
                //    }
                //}
                //catch (Exception)
                //{
                //}

                //user = new User
                //{
                //    Fio = textBox2.Text,
                //    Name = textBox1.Text,
                //    Password = textBox4.Text
                //};

                //userList.Add(user);

                //XmlSerializer formatterUser = new XmlSerializer(typeof(List<User>));

                //using (var stream = new FileStream($"UsersData.xml", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read))
                //{
                //    formatterUser.Serialize(stream, userList);
                //    Service.LogWrite($"Создан {user.Name}");
                //}
                //MessageBox.Show($"Аккаунт {user.Name} создан."); 
                #endregion

                Close();
            }
        }
    }
}


