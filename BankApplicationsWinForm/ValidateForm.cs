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

namespace BankApplicationsWinForm
{
    public partial class ValidateForm : Form
    {
        MainForm mainFrom;
        CreateUserForm createUserForm;
        public User user;
        List<User> listUser;

        public ValidateForm()
        {
            InitializeComponent();
            Invalidate();
            textBox1.Text = "Евгений"; //временно для удобства пользования
            listUser = new List<User>();
            //задание условий для прогрес бара
            timer1.Interval = 100;
            timer1.Enabled = false;
            progressBar1.Visible = false;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
        }

        //public string _name = "Евгений";
        //string _pas = "1";

        public TextBox ValidTextBox
        {
            get { return textBox1; }
            set { textBox1 = value; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlSerializer deserializeUser = new XmlSerializer(typeof(List<User>));
            try
            {
                using (var stream = new FileStream($"UsersData.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    listUser = deserializeUser.Deserialize(stream) as List<User>;
                    Service.LogWrite($"User {textBox1.Text} загружен");
                }
            }
            catch (Exception ex)
            {
                Service.LogWrite($"{ex.Message}");
                MessageBox.Show($"Пользователя {textBox1.Text} нет в системе", "Ошибка входа");
                return;
            }

            foreach (User item in listUser)
            {
                if (item.Name == textBox1.Text)
                {
                    user = item;
                    Service.LogWrite($"{user.Name} найден в системе");
                    if (textBox2.Text.Equals(user.Password))
                    {
                        progressBar1.Value = 0;
                        progressBar1.Visible = true;
                        timer1.Enabled = true;
                        timer1.Start();
                        this.Enabled = false;
                    }
                    else MessageBox.Show("Неверный логин", "Ошибка входа");
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.createUserForm = new CreateUserForm();
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
        {   //вводим только буквы
            char letter = e.KeyChar;
            if (!Char.IsLetter(letter))
            {
                e.Handled = true;
            }
        }
    }
}
