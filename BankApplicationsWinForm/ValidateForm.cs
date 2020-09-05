using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using BankApplicationsWinForm.Services;
using BankApplicationsWinForm.Interfaces.Cheaper;
using BankApplicationsWinForm.Exceptions;

namespace BankApplicationsWinForm
{
    public partial class ValidateForm : Form
    {
        MainForm mainFrom;
        UserForm createUserForm;
        public string _name;
        public int _userId;
        bool _isLoad = false;

        public ValidateForm()
        {
            InitializeComponent();
            Invalidate();
            textBox1.Text = "Евгений"; //временно для удобства пользования
            //задание условий для прогрес бара
            timer1.Interval = 100;
            timer1.Enabled = false;
            progressBar1.Visible = false;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
        }

        public TextBox ValidTextBox
        {
            get { return textBox1; }
            set { textBox1 = value; }
        }

        private async void btOK_Click(object sender, EventArgs e)
        {
            // Новая реализация
            if (!_isLoad)
                throw new BankException("Отсутствует подключение к БД! Обратитесь к администратор!");

            var dt = await DataBaseService.ExecSelectAsync("SELECT * FROM tbUsers", "Login = @Login", "Login", $"{textBox1.Text}", "tbUsers");

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var login = row["Login"];
                    var userId = row["User_ID"];

                    IDeCheaper deCheaper = new XORCipher();
                    var cheapPas = (string)row["Password"];
                    var password = deCheaper.Decrypt(cheapPas);

                    if (textBox2.Text.Equals(password))
                    {
                        _name = (string)login;
                        _userId = (int)userId;
                        progressBar1.Value = 0;
                        progressBar1.Visible = true;
                        timer1.Enabled = true;
                        timer1.Start();
                        this.Enabled = false;
                    }
                    else MessageBox.Show("Неверный пароль", "Ошибка входа");
                }
            }
            else
            {
                Service.LogWrite($"Пользователя {textBox1.Text} нет в системе");
                MessageBox.Show($"Пользователя {textBox1.Text} нет в системе", "Ошибка входа");
                return;
            }

            #region OldRealization
            //XmlSerializer deserializeUser = new XmlSerializer(typeof(List<User>));
            //try
            //{
            //    using (var stream = new FileStream($"UsersData.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
            //    {
            //        listUser = deserializeUser.Deserialize(stream) as List<User>;
            //        Service.LogWrite($"User {textBox1.Text} загружен");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Service.LogWrite($"{ex.Message}");
            //    MessageBox.Show($"Пользователя {textBox1.Text} нет в системе", "Ошибка входа");
            //    return;
            //}


            //foreach (User item in listUser)
            //{
            //    if (item.Name == textBox1.Text)
            //    {
            //        user = item;
            //        Service.LogWrite($"{user.Name} найден в системе");
            //        if (textBox2.Text.Equals(user.Password))
            //        {
            //            progressBar1.Value = 0;
            //            progressBar1.Visible = true;
            //            timer1.Enabled = true;
            //            timer1.Start();
            //            this.Enabled = false;
            //        }
            //        else MessageBox.Show("Неверный логин", "Ошибка входа");
            //    }
            //} 
            #endregion
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!_isLoad)
                throw new BankException("Отсутствует подключение к БД! Обратитесь к администратор!");

            this.createUserForm = new CreateUserForm(this);
            createUserForm.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value == 100)
            {
                timer1.Enabled = false;

                this.mainFrom = new MainForm(this);
                mainFrom.Show();
                this.Hide();
            }
            else
            {
                progressBar1.Value += 10;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {   
            // Вводим только буквы
            char letter = e.KeyChar;
            if (!Char.IsLetter(letter) && letter != 8)
            {
                e.Handled = true;
            }
        }

        private async void ValidateForm_Load(object sender, EventArgs e)
        {
            // Создавать базу если её нету.
            Cursor = Cursors.WaitCursor;
            try
            {
                if (!await DataBaseService.CheckCreateDBAsync())
                    throw new BankException("В процессе проверки и создания БД возникли ошибки! Проверьте настройки подключения! Смотри лог ошибок!");
                else _isLoad = true;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}
