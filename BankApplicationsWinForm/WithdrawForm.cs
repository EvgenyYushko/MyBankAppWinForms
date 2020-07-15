using BankLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankApplicationsWinForm
{
    public partial class WithdrawForm : Form
    {
        public WithdrawForm()
        {
            InitializeComponent();
        }
        MainForm mainForm;
        Bank<Account> bank;
        public WithdrawForm(MainForm mainForm, Bank<Account> bank)
        {
            InitializeComponent();
            mainForm.Enabled = false;
            this.mainForm = mainForm;
            this.bank = bank;

            Account[] acc = bank.GetAccunts();

            this.comboBox1.DataSource = acc;
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.ValueMember = "Id";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Account acc = (Account)comboBox1.SelectedItem;
            if (acc != null)
            {
                int id = acc.Id;
                Withdraw(bank, id); 
            }
            else return;

            mainForm.Panel.BackColor = Color.YellowGreen;
            mainForm.Panel.ForeColor = Color.Black;
            //form1.labelDay.Text = bank.CalculatePercentage();
            this.Close();

        }

        private void Withdraw(Bank<Account> bank, int id)
        {
            decimal sum = Convert.ToDecimal(this.textBox1.Text);
            bank.Withdraw(sum, id);
        }

        private void WithdrawForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.Enabled = true;
            this.mainForm.UpdateInfo();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {   //вводим только цифры
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
    }
}
