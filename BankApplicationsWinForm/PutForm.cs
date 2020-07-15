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
    public partial class PutForm : Form
    {
        MainForm mainForm;
        Bank<Account> bank;
        public PutForm()
        {
            InitializeComponent();
        }

        public PutForm(MainForm mainForm, Bank<Account> bank)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.mainForm.Enabled = false;
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
                Put(bank, id);
            }
            else return;
            //openForm.labelDay.Text = bank.CalculatePercentage();
            this.Close();
            this.mainForm.UpdateInfo(); 
        }

        private void Put(Bank<Account> bank, int id)
        {
            decimal sum = Convert.ToDecimal(this.textBox1.Text == string.Empty? "0" : this.textBox1.Text);

            bank.Put(sum, id);
            mainForm.Panel.BackColor = Color.Yellow;
            mainForm.Panel.ForeColor = Color.Black;
            Service.LogWrite("Счёт пополнен");
        }

        private void PutForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.mainForm.Enabled = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {   //вводим только цифры
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 44) // цифры, клавиша BackSpace и запятая
            {
                e.Handled = true;
            }
        }
    }
}
