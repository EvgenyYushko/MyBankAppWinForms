﻿using System;
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
using BankApplicationsWinForm.Models;

namespace BankApplicationsWinForm
{
    public partial class OpenForm : Form
    {
        MainForm mainForm;
        Bank<Account> bank;
        AccountEventsArcuments _accountEventsArcuments;
        AccountType accountType;

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
            this._accountEventsArcuments = mainForm._accountEventsArcuments;
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
                _accountEventsArcuments.AddSumHandler,  // обработчик добавления средств на счет
                _accountEventsArcuments.WithdrawSumHandler, // обработчик вывода средств
                                                            /*(o, e) => mainForm.labelPer.Text = e.Message,*/ // обработчик начислений процентов в виде лямбда-выражения
                _accountEventsArcuments.CloseAccountHandler, // обработчик закрытия счета
                _accountEventsArcuments.OpenAccountHandler); // обработчик открытия счета

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
