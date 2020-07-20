using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLibrary.Enams;
using System.Xml.Serialization;
using System.IO;

namespace BankLibrary
{
    [Serializable]
    public class Bank<T> where T : Account
    {
        public T[] accounts;
        public DateTime DateTime { get; set; }
        public int Days { get; set; } = 0;

        public string Name { get;  set; }

        public Bank()
        {
        }
        public Bank(string name)
        {
            this.Name = name;
        }
        //Присваивание обработчиков для аккаунтов если они были десиреализованы
        public void Open(AccountStateHandler addSumHandler, AccountStateHandler withdrawSumHandler,
             AccountStateHandler closeAccountHandler, AccountStateHandler openAccountHandler)
        {
            foreach (var item in accounts)
            {
                item.Added += addSumHandler;
                item.Withdrawed += withdrawSumHandler;
                item.Closed += closeAccountHandler;
                item.Opened += openAccountHandler;
            }
        }
        // метод создания счета
        public void Open(AccountType accountType, decimal sum,
            AccountStateHandler addSumHandler, AccountStateHandler withdrawSumHandler,
            /*AccountStateHandler calculationHandler,*/ AccountStateHandler closeAccountHandler,
            AccountStateHandler openAccountHandler)
        {
            T newAccount = null;

            int demAc = 1;
            int depAc = 1;
            if (accounts != null)
            {
                foreach (T item in accounts)
                {
                    if (item is DemandAccount)
                        demAc++;
                    if (item is DepositAccount)
                        depAc++;
                }
            }
            Random rand = new Random();
            switch (accountType)
            {
                case AccountType.Ordinary:
                    newAccount = new DemandAccount(sum, 1, $"До востребования №{demAc.ToString()}-{rand.Next(100, 999).ToString()}") as T;
                    break;
                case AccountType.Deposit:
                    newAccount = new DepositAccount(sum, 40, $"Депозитный счёт №{depAc.ToString()}-{rand.Next(100, 999).ToString()}") as T;
                    break;
            }

            if (newAccount == null)
                throw new Exception("Ошибка создания счета");
            // добавляем новый счет в массив счетов      
            if (accounts == null)
                accounts = new T[] { newAccount };
            else
            {
                T[] tempAccounts = new T[accounts.Length + 1];
                for (int i = 0; i < accounts.Length; i++)
                    tempAccounts[i] = accounts[i];
                tempAccounts[tempAccounts.Length - 1] = newAccount;
                accounts = tempAccounts;
            }

            // установка обработчиков событий счета
            newAccount.Added += addSumHandler;
            newAccount.Withdrawed += withdrawSumHandler;
            newAccount.Closed += closeAccountHandler;
            newAccount.Opened += openAccountHandler;
            //newAccount.Calculated += calculationHandler;
            newAccount.Open();
        }
        //добавление средств на счет
        public void Put(decimal sum, int id)
        {
            T account = FindAccount(id);
            if (account == null)
                throw new Exception("Счет не найден");
            account.Put(sum);
        }
        // вывод средств
        public void Withdraw(decimal sum, int id)
        {
            T account = FindAccount(id);
            if (account == null)
                throw new Exception("Счет не найден");
            account.Withdraw(sum);
        }
        // закрытие счета
        public void Close(int id)
        {
            int index;
            T account = FindAccount(id, out index);
            if (account == null)
                throw new Exception("Счет не найден");

            account.Close();

            if (accounts.Length <= 1)
                accounts = null;
            else
            {
                // уменьшаем массив счетов, удаляя из него закрытый счет
                T[] tempAccounts = new T[accounts.Length - 1];
                for (int i = 0, j = 0; i < accounts.Length; i++)
                {
                    if (i != index)
                        tempAccounts[j++] = accounts[i];
                }
                accounts = tempAccounts;
            }
        }

        // начисление процентов по счетам
        public string CalculatePercentage()
        {
            string incrementDays = null;
            if (accounts == null) // если массив не создан, выходим из метода
                return null;
            for (int i = 0; i < accounts.Length; i++)
            {
                accounts[i].IncrementDays();
                accounts[i].Calculate();
            }
            Days = ++Days;
            return $"В банке прошло {Days.ToString()} дней.";
        }

        // поиск счета по id
        public T FindAccount(int id)
        {
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].Id == id)
                    return accounts[i];
            }
            return null;
        }
        // перегруженная версия поиска счета
        public T FindAccount(int id, out int index)
        {
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].Id == id)
                {
                    index = i;
                    return accounts[i];
                }
            }
            index = -1;
            return null;
        }

        public string GetPercent(int id)
        {
            T acc = FindAccount(id);
            return acc.GetPercent().ToString();
        }

        public T[] GetAccunts()
        {
            return accounts;
        }

 
            

    }
}
