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
    public partial class CreateUserForm : Form
    {

        List<User> userList;
        User user;

        public CreateUserForm()
        {
            InitializeComponent();
            userList = new List<User>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != textBox4.Text)
                MessageBox.Show("Пароли не совпадают", "Внимание!");
            else
            {
                try
                {
                    XmlSerializer deserializeUser = new XmlSerializer(typeof(List<User>));
                    //сначало загружаем базу
                    using (var stream = new FileStream($"UsersData.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        userList = deserializeUser.Deserialize(stream) as List<User>;
                        Service.LogWrite($"User {textBox1.Text} загружен");
                    }
                }
                catch (Exception)
                {
                }

                user = new User
                {
                    Fio = textBox2.Text,
                    Name = textBox1.Text,
                    Password = textBox4.Text
                };

                userList.Add(user);

                XmlSerializer formatterUser = new XmlSerializer(typeof(List<User>));

                using (var stream = new FileStream($"UsersData.xml", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read))
                {
                    formatterUser.Serialize(stream, userList);
                    Service.LogWrite($"Создан {user.Name}");
                }
                MessageBox.Show($"Аккаунт {user.Name} создан.");
                this.Close();
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

    [Serializable]
    public class User
    {
        private string name;
        private string fio;
        private string password;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Fio
        {
            get { return fio; }
            set { fio = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public User()
        {
        }
    }
}


