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

namespace BankApplicationsWinForm
{
    public partial class MainForm : Form
    {
        ValidateForm validateForm;
        ToolStripLabel dateLabel;
        ToolStripLabel timeLabel;
        ToolStripLabel infoLabel;
        Timer timer;
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
            //if (validateForm.ValidTextBox.Text.Equals("Евгений"))
            //{
            //_id = 1;
            LoadDocuments($"{validateForm._name}");
            //}
            //if (validateForm.ValidTextBox.Text.Equals("Вика"))
            //{
            //    _id = 2;
            //    LoadDocuments(_id);
            //}
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
        #endregion

        void timer_Tick(object sender, EventArgs e)
        {
            dateLabel.Text = DateTime.Now.ToLongDateString();
            timeLabel.Text = DateTime.Now.ToLongTimeString();
        }

        Bank<Account> bank = new Bank<Account>("ЮнитБанк");

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

            //mainForm.labelDay.Text += bank.CalculatePercentage();
            //mainForm.LabelInfoProp.ForeColor = Color.YellowGreen;
            //mainForm.LabelInfoProp.Text = "Счёт закрыт";
            Panel.BackColor = Color.Red;
            Panel.ForeColor = Color.Black;

            //this.Close();
            Service.Refresh(this);
            UpdateInfo();

            //
            //CloseForm closeForm = new CloseForm(this, bank);
            //closeForm.Show();
            //bank.CalculatePercentage();
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
                //textBox3.Text = bank.GetPercent(acc.Id);
                TextBox2Prop.Text = acc.Sum.ToString();
                TextBox3Prop.Text = bank.GetPercent(acc.Id);
                TextBox4Prop.Text = acc._days.ToString();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveDocuments(_idName);
            validateForm.Close();
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

        #region обработчики событий если объекты дессериализованы( костыль :( )

        private void OpenAccountHandler(object sender, AccountEventArgs e)
        {
            MessageBox.Show(e.Message, "Результат");
            Service.LogWrite(e.Message);

            LabelInfoProp.Text = e.Message;
            Close();
        }
        // обработчик добавления денег на счет
        private void AddSumHandler(object sender, AccountEventArgs e)
        {
            LabelInfoProp.Text = e.Message;
            Service.LogWrite(e.Message);
            LabelInfoProp.Text += "\n" + "Общая сумма равна:" + e.Sum;
        }
        // обработчик вывода средств
        private void WithdrawSumHandler(object sender, AccountEventArgs e)
        {
            LabelInfoProp.Text = e.Message;
            Service.LogWrite(e.Message);
        }
        // обработчик закрытия счета
        private void CloseAccountHandler(object sender, AccountEventArgs e)
        {
            LabelInfoProp.Text = e.Message;
            Service.LogWrite(e.Message);
        }

        #endregion


        #region Сериализация/Десериализация объеткта Account

        public string _fullpath;

        private void SaveDocuments(string idName)
        {
            XmlSerializer formatterDemand = new XmlSerializer(typeof(DemandAccount[]));
            XmlSerializer formatterDeposit = new XmlSerializer(typeof(DepositAccount[]));
            List<DemandAccount> demList = new List<DemandAccount>();
            List<DepositAccount> depList = new List<DepositAccount>();
            bool saveDamand = false;
            bool saveDeposit = false;
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo($"{_idName}");
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                _fullpath = dirInfo.FullName;
                using (var stream = new FileStream($@"{_fullpath}\DemandAccounts_{idName}.xml", FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    try
                    {
                        DemandAccount s;
                        foreach (Account item in bank.accounts)
                        {
                            if (item is DemandAccount)
                            {
                                s = item as DemandAccount;
                                s.dateTime = DateTime.Now;
                                demList.Add(s);
                            }
                        }

                        // Новая реализация
                        var stringWriter = new StringWriter();

                        formatterDemand.Serialize(stringWriter, demList.ToArray());

                        var data = stringWriter.ToString();
                        DataBaseService.ExecUpdate("Login = @Login", "Login", "Евгений", "tbUsers", $"{data}", _userID, "DemandData");
                        //

                        formatterDemand.Serialize(stream, demList.ToArray());
                        Service.LogWrite("Объект DemandAccount сохранён");
                        saveDamand = true;
                    }
                    catch (NullReferenceException nullExep)
                    {
                        Service.LogWrite($"Нет аккаунта DemandAccount_{idName}! {nullExep.Message}");
                    }
                }
                using (var stream = new FileStream($@"{_fullpath}\DepositAccounts_{idName}.xml", FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    try
                    {
                        DepositAccount s;
                        foreach (Account item in bank.accounts)
                        {
                            if (item is DepositAccount)
                            {
                                s = item as DepositAccount;
                                s.dateTime = DateTime.Now;
                                depList.Add(s);
                            }
                        }

                        // Новая реализация
                        var stringWriter = new StringWriter();

                        formatterDemand.Serialize(stringWriter, depList.ToArray());

                        var data = stringWriter.ToString();
                        DataBaseService.ExecUpdate("Login = @Login", "Login", "Евгений", "tbUsers", $"{data}", _userID, "DepositData");
                        //

                        formatterDeposit.Serialize(stream, depList.ToArray());
                        Service.LogWrite($"Объект DepositAccounts_{idName} сохранён");
                        saveDeposit = true;
                    }
                    catch (NullReferenceException nullExep)
                    {
                        Service.LogWrite($"Нет аккаунта DepositAccounts_{idName}! {nullExep.Message}");
                    }
                }
                if (saveDamand && saveDeposit)
                {
                    this.button1.Text = "ОК";
                    Service.LogWrite("Сохранение ОК");
                }
                else throw new Exception("Ошибка сохранения!");
            }
            catch (Exception ex)
            {
                this.button1.Text = "Error";
                Service.LogWrite(ex.Message);
            }
        }

        private void LoadDocuments(string idName)
        {
            XmlSerializer serializerDem = new XmlSerializer(typeof(DemandAccount[]));
            XmlSerializer serializerDep = new XmlSerializer(typeof(DepositAccount[]));

            DemandAccount[] demAcc = null;
            DepositAccount[] depAcc = null;

            bool loadDamand = false;
            bool loadDeposit = false;
            try
            {
                using (var stream = new FileStream($@"{idName}\DemandAccounts_{idName}.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    try
                    {
                        // Восстанавливаем объект из XML-файла.
                        demAcc = serializerDem.Deserialize(stream) as DemandAccount[];
                        Service.LogWrite($"Объект DemandAccount_{idName} загружен");
                        loadDamand = true;
                    }
                    catch (Exception ex)
                    {
                        Service.LogWrite(ex.Message);
                    }
                }

                using (var stream = new FileStream($@"{idName}\DepositAccounts_{idName}.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    try
                    {
                        // Восстанавливаем объект из XML-файла.
                        depAcc = serializerDep.Deserialize(stream) as DepositAccount[];
                        Service.LogWrite($"Объект DepositAccount_{idName} загружен");
                        loadDeposit = true;
                    }
                    catch (Exception ex)
                    {
                        Service.LogWrite(ex.Message);
                    }
                }

                Account[] tempAccounts = new Account[demAcc.Length + depAcc.Length];
                for (int i = 0; i < demAcc.Length; i++)
                    tempAccounts[i] = demAcc[i];
                for (int i = 0, s = demAcc.Length, t = demAcc.Length; t < depAcc.Length + demAcc.Length; i++, s++, t++)
                    tempAccounts[s] = depAcc[i];
                bank.accounts = tempAccounts;

                foreach (var item in bank.accounts)
                {
                    DateTime dateTimeOld = item.dateTime;
                    TimeSpan resultTime = (DateTime.Now - dateTimeOld);

                    if (resultTime.Days != 0)
                    {
                        for (int i = 0; i < resultTime.Days; i++)
                        {
                            bank.CalculatePercentage();
                        }
                        //item._days += resultTime.Days;
                    }
                }

                Account[] acc = bank.GetAccunts();

                ComboBox.DataSource = acc;
                ComboBox.DisplayMember = "Name";
                ComboBox.ValueMember = "Id";
                this.button2.Text = "ОК";
                Service.LogWrite("Загрзка ОК");

                //Присваивание обработчиков для аккаунтов если они были десиреализованы
                bank.Open(AddSumHandler, WithdrawSumHandler, CloseAccountHandler, OpenAccountHandler);
            }
            catch (Exception exep)
            {
                Service.LogWrite(exep.Message);
                this.button2.Text = "Error";
            }
        }
        #endregion
    }

}
