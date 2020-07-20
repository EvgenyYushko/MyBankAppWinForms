using BankApplicationsWinForm.Interfaces;
using BankApplicationsWinForm.Services;
using BankLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace BankApplicationsWinForm.Models
{
    public class SaveOrLoader : ISaveOrLoadable
    {
        MainForm mainForm;
        Bank<Account> bank;

        public SaveOrLoader()
        {
        }

        public SaveOrLoader(MainForm mainForm, Bank<Account> bank)
        {

            this.mainForm = mainForm;
            this.bank = bank;
        }

        /// <summary>
        /// Сохранение данных
        /// </summary>
        /// <param name="idName"></param>
        /// <returns></returns>
        public bool SaveDocuments(string idName)
        {
            XmlSerializer formatterDemand = new XmlSerializer(typeof(DemandAccount[]));
            XmlSerializer formatterDeposit = new XmlSerializer(typeof(DepositAccount[]));
            List<DemandAccount> demList = new List<DemandAccount>();
            List<DepositAccount> depList = new List<DepositAccount>();
            bool saveDamand = false;
            bool saveDeposit = false;
            try
            {
                try // Сохранение DemandAccount в базу
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

                    var stringWriter = new StringWriter();

                    formatterDemand.Serialize(stringWriter, demList.ToArray());
                    var data = stringWriter.ToString();

                    DataBaseService.ExecUpdate("Login = @Login", "Login", "Евгений", "tbUsers", $"{data}", mainForm._userID, "DemandData");

                    Service.LogWrite("Объект DemandAccount сохранён");
                    saveDamand = true;
                }
                catch (NullReferenceException nullExep)
                {
                    Service.LogWrite($"Нет аккаунта DemandAccount_{idName}! {nullExep.Message}");
                }
                #region OldRealization (Сохранение DemandAccount в xml)
                //DirectoryInfo dirInfo = new DirectoryInfo($"{_idName}");
                //if (!dirInfo.Exists)
                //{
                //    dirInfo.Create();
                //}

                //_fullpath = dirInfo.FullName;
                //using (var stream = new FileStream($@"{_fullpath}\DemandAccounts_{idName}.xml", FileMode.Create, FileAccess.Write, FileShare.Read))
                //{
                //    try
                //    {
                //        DemandAccount s;
                //        foreach (Account item in bank.accounts)
                //        {
                //            if (item is DemandAccount)
                //            {
                //                s = item as DemandAccount;
                //                s.dateTime = DateTime.Now;
                //                demList.Add(s);
                //            }
                //        }

                //        formatterDemand.Serialize(stream, demList.ToArray());
                //        Service.LogWrite("Объект DemandAccount сохранён");
                //        saveDamand = true;
                //    }
                //    catch (NullReferenceException nullExep)
                //    {
                //        Service.LogWrite($"Нет аккаунта DemandAccount_{idName}! {nullExep.Message}");
                //    }
                //}
                #endregion

                try // Сохранение DepositAccount в базу
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

                    var stringWriter = new StringWriter();

                    formatterDeposit.Serialize(stringWriter, depList.ToArray());
                    var data = stringWriter.ToString();

                    DataBaseService.ExecUpdate("Login = @Login", "Login", "Евгений", "tbUsers", $"{data}", mainForm._userID, "DepositData");

                    Service.LogWrite($"Объект DepositAccounts_{idName} сохранён");
                    saveDeposit = true;
                }
                catch (NullReferenceException nullExep)
                {
                    Service.LogWrite($"Нет аккаунта DepositAccounts_{idName}! {nullExep.Message}");
                }
                #region OldRealisation (Сохранение DepositAccount в xml)
                //using (var stream = new FileStream($@"{_fullpath}\DepositAccounts_{idName}.xml", FileMode.Create, FileAccess.Write, FileShare.Read))
                //{
                //    try
                //    {
                //        DepositAccount s;
                //        foreach (Account item in bank.accounts)
                //        {
                //            if (item is DepositAccount)
                //            {
                //                s = item as DepositAccount;
                //                s.dateTime = DateTime.Now;
                //                depList.Add(s);
                //            }
                //        }

                //        formatterDeposit.Serialize(stream, depList.ToArray());
                //        Service.LogWrite($"Объект DepositAccounts_{idName} сохранён");
                //        saveDeposit = true;
                //    }
                //    catch (NullReferenceException nullExep)
                //    {
                //        Service.LogWrite($"Нет аккаунта DepositAccounts_{idName}! {nullExep.Message}");
                //    }
                //} 
                #endregion

                if (saveDamand && saveDeposit)
                {
                    mainForm._button1.Text = "ОК";
                    Service.LogWrite("Все объекты успешно сохранены ОК");
                    return true;
                }
                else throw new Exception("Ошибка сохранения!");

            }
            catch (Exception ex)
            {
                mainForm._button1.Text = "Error";
                Service.LogWrite(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Получение данных
        /// </summary>
        /// <param name="idName"></param>
        /// <returns></returns>
        public bool LoadDocuments(string idName)
        {
            XmlSerializer serializerDem = new XmlSerializer(typeof(DemandAccount[]));
            XmlSerializer serializerDep = new XmlSerializer(typeof(DepositAccount[]));

            DemandAccount[] demAcc = null;
            DepositAccount[] depAcc = null;

            bool loadDamand = false;
            bool loadDeposit = false;
            try
            {
                try // Восстанавливаем объект DemandAccount из базы.
                {
                    var dt = DataBaseService.ExecSelect("SELECT * FROM tbUsers", "Login = @Login", "Login", $"{mainForm._idName}", "tbUsers");
                    if (dt.Rows.Count != 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            StringReader demandData = new StringReader((string)row["DemandData"]);

                            if (!string.IsNullOrEmpty(demandData.ToString()))
                            {
                                demAcc = serializerDem.Deserialize(demandData) as DemandAccount[];
                                if (demAcc != null)
                                {
                                    loadDamand = true;
                                    Service.LogWrite($"Объект DemandAccount_{idName} загружен");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Отсутствуют данные по пользователю", "Ошибка входа");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Проверьте базу", "Ошибка получения данных");
                    }

                }
                catch (Exception ex)
                {
                    Service.LogWrite(ex.Message);
                }

                #region OldRealozation (загрузка DemandAccount из xml)
                //using (var stream = new FileStream($@"{idName}\DemandAccounts_{idName}.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
                //{
                //    try
                //    {
                //        // Восстанавливаем объект из XML-файла.
                //        demAcc = serializerDem.Deserialize(stream) as DemandAccount[];
                //        Service.LogWrite($"Объект DemandAccount_{idName} загружен");
                //        loadDamand = true;
                //    }
                //    catch (Exception ex)
                //    {
                //        Service.LogWrite(ex.Message);
                //    }
                //} 
                #endregion

                try // Восстанавливаем объект DepositAccount из базы.
                {
                    var dt = DataBaseService.ExecSelect("SELECT * FROM tbUsers", "Login = @Login", "Login", $"{mainForm._idName}", "tbUsers");
                    if (dt.Rows.Count != 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            StringReader depositData = new StringReader((string)row["DepositData"]);

                            if (!string.IsNullOrEmpty(depositData.ToString()))
                            {
                                depAcc = serializerDep.Deserialize(depositData) as DepositAccount[];
                                if (depAcc != null)
                                {
                                    Service.LogWrite($"Объект DepositAccount_{idName} загружен");
                                    loadDeposit = true;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Отсутствуют данные по пользователю", "Ошибка входа");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Проверьте базу", "Ошибка получения данных");
                    }

                }
                catch (Exception ex)
                {
                    Service.LogWrite(ex.Message);
                }

                if (loadDamand && loadDeposit)
                {
                    mainForm._button2.Text = "ОК";
                    Service.LogWrite("Все объекты успешно загружены");
                }
                else
                {
                    Service.LogWrite("Ошибка получения данных!");
                    return false;
                }
                #region OldRealozation (загрузка DepositAccounts из xml)
                //using (var stream = new FileStream($@"{idName}\DepositAccounts_{idName}.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
                //{
                //    try
                //    {
                //        // Восстанавливаем объект из XML-файла.
                //        depAcc = serializerDep.Deserialize(stream) as DepositAccount[];
                //        Service.LogWrite($"Объект DepositAccount_{idName} загружен");
                //        loadDeposit = true;
                //    }
                //    catch (Exception ex)
                //    {
                //        Service.LogWrite(ex.Message);
                //    } 
                //}
                #endregion

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

                mainForm.ComboBox.DataSource = acc;
                mainForm.ComboBox.DisplayMember = "Name";
                mainForm.ComboBox.ValueMember = "Id";
                mainForm._button2.Text = "ОК";
                Service.LogWrite("Загрзка ОК");

                return true;
            }
            catch (Exception exep)
            {
                Service.LogWrite(exep.Message);
                mainForm._button2.Text = "Error";
                return false;
            }
        }
    }
}
