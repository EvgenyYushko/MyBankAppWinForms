using BankLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplicationsWinForm
{
    static public class Service
    {
        static public void Refresh(MainForm mainForm)
        {
            MainForm mainForm1 = mainForm;
            mainForm1.labelDayProp.Text = "";
            //mainForm1.LabelInfoProp.Text = "";
            mainForm1.TextBox3Prop.Text = "";
            mainForm1.TextBox2Prop.Text = "";
            mainForm1.TextBox4Prop.Text = "";
        }

        static public void LogWrite(string msg)
        {
            using (StreamWriter sw = new StreamWriter(@"trace.txt", true, Encoding.Default))
            {
                sw.WriteLine("[" + DateTime.Now.ToString() + "] " + msg);
            }
        }
    }
}
