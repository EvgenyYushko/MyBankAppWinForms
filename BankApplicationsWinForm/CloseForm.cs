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
    public partial class CloseForm : Form
    {
        MainForm mainForm;
        Bank<Account> bank;
        public CloseForm()
        {
            InitializeComponent();
        }

        public CloseForm(MainForm mainForm, Bank<Account> bank)
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
                CloseAccount(bank, id);
            }
            else return;

            //mainForm.labelDay.Text += bank.CalculatePercentage();
            //mainForm.LabelInfoProp.ForeColor = Color.YellowGreen;
            //mainForm.LabelInfoProp.Text = "Счёт закрыт";
            mainForm.Panel.BackColor = Color.Red;
            mainForm.Panel.ForeColor = Color.Black;

            this.Close();
            Service.Refresh(mainForm);
            mainForm.UpdateInfo();
        }

        private void CloseAccount(Bank<Account> bank, int id)
        {
            try
            {
                bank.Close(id);

                Account[] acc = bank.GetAccunts();

                mainForm.ComboBox.DataSource = acc;
                mainForm.ComboBox.DisplayMember = "Name";
                mainForm.ComboBox.ValueMember = "Id";
                Service.LogWrite("Счёт закрыт");
            }
            catch (Exception)
            {
                MessageBox.Show("Нет такого счёта!", "Ошибка");
            }
        }

        private void CloseForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
