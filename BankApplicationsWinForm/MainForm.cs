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
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;
using BankApplicationsWinForm.Services;
using System.Xml;
using BankApplicationsWinForm.Models;
using BankApplicationsWinForm.Interfaces;

namespace BankApplicationsWinForm
{
    public partial class MainForm : Form
    {
        ValidateForm validateForm;
        ToolStripLabel dateLabel;
        ToolStripLabel timeLabel;
        ToolStripLabel infoLabel;
        Timer timer;
        Bank<Account> bank;
        public AccountEventsArcuments _accountEventsArcuments;
        ISaveOrLoadable _saveOrLoadable;
        public string _idName;
        public int _userID;

        public MainForm()
        {
        }

        public MainForm(ValidateForm validateForm)
        {
            InitializeComponent();
            this.validateForm = validateForm;
            infoLabel = new ToolStripLabel();
            infoLabel.Text = "Текущие дата и время:";
            dateLabel = new ToolStripLabel();
            timeLabel = new ToolStripLabel();

            statusStrip1.Items.Add(infoLabel);
            statusStrip1.Items.Add(dateLabel);
            statusStrip1.Items.Add(timeLabel);

            timer = new Timer() { Interval = 1000 };
            timer.Tick += timer_Tick;
            timer.Start();

            _idName = validateForm._name;
            _userID = validateForm._userId;
            groupBox1.Text = "Клиент: " + validateForm._name;
            _accountEventsArcuments = new AccountEventsArcuments(this);

            bank = new Bank<Account>("ЮнитБанк");

            _saveOrLoadable = new SaveOrLoader(this, bank);

            LoadDocuments($"{validateForm._name}");
        }

        #region Свойства для доступа к полям MainForm

        public Panel Panel
        {
            get { return panel1; }
            set { panel1 = value; }
        }

        public ComboBox ComboBox
        {
            get { return comboBox1; }
            set { comboBox1 = value; }
        }

        public Label labelDayProp
        {
            get => labelDay;
            set => labelDay = value;
        }

        public Label LabelInfoProp
        {
            get => labelInfo;
            set => labelInfo = value;
        }

        public TextBox TextBox2Prop
        {
            get { return textBox2; }
            set { textBox2 = value; }
        }

        public TextBox TextBox3Prop
        {
            get { return textBox3; }
            set { textBox3 = value; }
        }

        public TextBox TextBox4Prop
        {
            get { return textBox4; }
            set { textBox4 = value; }
        }

        public Button _button1
        {
            get { return button1; }
            set { button1 = value; }
        }

        public Button _button2
        {
            get { return button2; }
            set { button2 = value; }
        }
        #endregion

        void timer_Tick(object sender, EventArgs e)
        {
            dateLabel.Text = DateTime.Now.ToLongDateString();
            timeLabel.Text = DateTime.Now.ToLongTimeString();
        }

        private void bOpen_Click(object sender, EventArgs e)
        {
            OpenForm openForm = new OpenForm(this, bank);
            openForm.Show();
        }

        private void bWithdraw_Click(object sender, EventArgs e)
        {
            WithdrawForm whothdrawFrom = new WithdrawForm(this, bank);
            whothdrawFrom.Show();
        }

        private void bPut_Click(object sender, EventArgs e)
        {
            PutForm putForm = new PutForm(this, bank);
            putForm.Show();
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Account acc = (Account)comboBox1.SelectedItem;
            if (acc != null)
            {
                int id = acc.Id;
                CloseAccount(bank, id);
            }
            else return;

            Panel.BackColor = Color.Red;
            Panel.ForeColor = Color.Black;

            Service.Refresh(this);
            UpdateInfo();
        }

        private void CloseAccount(Bank<Account> bank, int id)
        {
            try
            {
                bank.Close(id);

                Account[] acc = bank.GetAccunts();

                ComboBox.DataSource = acc;
                ComboBox.DisplayMember = "Name";
                ComboBox.ValueMember = "Id";
                Service.LogWrite("Счёт закрыт");
            }
            catch (Exception)
            {
                MessageBox.Show("Нет такого счёта!", "Ошибка");
            }
        }

        private void bSkip_Click(object sender, EventArgs e)
        {
            labelDayProp.Text = bank.CalculatePercentage();
            UpdateInfo();
            Service.LogWrite(labelDayProp.Text);
        }

        private void bRefresh_Click(object sender, EventArgs e)
        {
            Service.Refresh(this);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateInfo();
        }

        public void UpdateInfo()
        {
            Account acc = (Account)comboBox1.SelectedItem;
            if (acc != null)
            {
                TextBox2Prop.Text = acc.Sum.ToString();
                TextBox3Prop.Text = bank.GetPercent(acc.Id);
                TextBox4Prop.Text = acc._days.ToString();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveDocuments(_idName);
            validateForm.Close();

            DirectoryInfo dirInfo = new DirectoryInfo($"cash");
            foreach (FileInfo f in dirInfo.GetFiles())
                f.Delete();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveDocuments(_idName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadDocuments(_idName);
        }

        private void журналСобытийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"trace.txt");
        }

        #region Сериализация/Десериализация объеткта Account

        private async void SaveDocuments(string idName)
        {
            if (!await _saveOrLoadable.SaveDocumentsAsunc(idName))
                Service.LogWrite("Ошибка сохранения данных");
        }

        private async void LoadDocuments(string idName)
        {
            if (await _saveOrLoadable.LoadDocumentsAsunc(idName))
            {
                //Присваивание обработчиков для аккаунтов если они были десиреализованы
                bank.Open(_accountEventsArcuments.AddSumHandler, _accountEventsArcuments.WithdrawSumHandler,
                    _accountEventsArcuments.CloseAccountHandler, _accountEventsArcuments.OpenAccountHandler);
            }
            else
            {
                Service.LogWrite("Ошибка загрузки данных");
                this.button2.Text = "Error";
            }
        }
        #endregion

        private void аккаунтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewUserForm viewUserForm = new ViewUserForm(this);
            viewUserForm.Show();
        }
    }

}
