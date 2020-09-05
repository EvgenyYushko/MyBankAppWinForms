using BankLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankApplicationsWinForm.Models
{
    public class AccountEventsArcuments
    {
        MainForm mainForm;

        public AccountEventsArcuments()
        {
        }

        public AccountEventsArcuments(MainForm mainForm)
        {
             this.mainForm = mainForm;
        }

        public void OpenAccountHandler(object sender, AccountEventArgs e)
        {
            Service.LogWrite(e.Message);
            mainForm.LabelInfoProp.Text = e.Message;
        }
        // обработчик добавления денег на счет!!
        public void AddSumHandler(object sender, AccountEventArgs e)
        {
            mainForm.LabelInfoProp.Text = e.Message;
            Service.LogWrite(e.Message);
            mainForm.LabelInfoProp.Text += "\n" + "Общая сумма равна:" + e.Sum;
        }
        // обработчик вывода средств
        public void WithdrawSumHandler(object sender, AccountEventArgs e)
        {
            mainForm.LabelInfoProp.Text = e.Message;
            Service.LogWrite(e.Message);
        }
        // обработчик закрытия счета
        public void CloseAccountHandler(object sender, AccountEventArgs e)
        {
            mainForm.LabelInfoProp.Text = e.Message;
            Service.LogWrite(e.Message);
        }
    }
}
