using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BankLibrary;
using BankLibrary.Enams;
using System.IO;

namespace BankApplicationsWinForm
{
    public partial class OpenForm : Form
    {
        MainForm mainForm;
        Bank<Account> bank;

        public OpenForm()
        {
            InitializeComponent();
        }

        public OpenForm(MainForm mainForm, Bank<Account> bank)
        {
            InitializeComponent();
            mainForm.Enabled = false;
            this.mainForm = mainForm;
            this.bank = bank;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AccountType accountType;

            if (radioButton1.Checked)
                accountType = AccountType.Ordinary;
            else if (radioButton2.Checked)
                accountType = AccountType.Deposit;
            else accountType = AccountType.EmptyValue;

            if (accountType != AccountType.EmptyValue)
                OpenAccount(bank, accountType);
           
            //mainForm.labelDay.Text = bank.CalculatePercentage();

            Account[] acc = bank.GetAccunts();

            mainForm.ComboBox.DataSource = acc;
            mainForm.ComboBox.DisplayMember = "Name";
            mainForm.ComboBox.ValueMember = "Id";
            mainForm.Panel.BackColor = Color.Lime;
            this.Close();
        }

        private void OpenAccount(Bank<Account> bank, AccountType accountType)
        {
            decimal sum = Convert.ToDecimal(this.textBox1.Text == string.Empty ? "0" : this.textBox1.Text);

            //int type = Convert.ToInt32(this.textBox2.Text);
            bank.Open(accountType,
                sum,
                AddSumHandler,  // обработчик добавления средств на счет
                WithdrawSumHandler, // обработчик вывода средств
                                    /*(o, e) => mainForm.labelPer.Text = e.Message,*/ // обработчик начислений процентов в виде лямбда-выражения
                CloseAccountHandler, // обработчик закрытия счета
                OpenAccountHandler); // обработчик открытия счета

        }

        // обработчики событий класса Account
        // обработчик открытия счета
        private void OpenAccountHandler(object sender, AccountEventArgs e)
        {
            MessageBox.Show(e.Message, "Результат");
            Service.LogWrite(e.Message);
            
            this.mainForm.LabelInfoProp.Text = e.Message;
            this.Close();
        }
        // обработчик добавления денег на счет
        private void AddSumHandler(object sender, AccountEventArgs e)
        {
            mainForm.LabelInfoProp.Text = e.Message;
            Service.LogWrite(e.Message);
            mainForm.LabelInfoProp.Text += "\n" + "Общая сумма равна:" + e.Sum;
        }
        // обработчик вывода средств
        private void WithdrawSumHandler(object sender, AccountEventArgs e)
        {
            mainForm.LabelInfoProp.Text = e.Message;
            Service.LogWrite(e.Message);
        }
        // обработчик закрытия счета
        private void CloseAccountHandler(object sender, AccountEventArgs e)
        {
            mainForm.LabelInfoProp.Text = e.Message;
            Service.LogWrite(e.Message);
        }

        private void OpenForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.Enabled = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {   //вводим только цифры
            char number = e.KeyChar;
            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }
    }
}
