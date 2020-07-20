using BankLibrary.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BankLibrary
{
    [Serializable]
    public abstract class Account : IAccount
    {
        //Событие, возникающее при выводе денег
        protected internal event AccountStateHandler Withdrawed;
        // Событие возникающее при добавление на счет
        protected internal event AccountStateHandler Added;
        // Событие возникающее при открытии счета
        protected internal event AccountStateHandler Opened;
        // Событие возникающее при закрытии счета
        protected internal event AccountStateHandler Closed;
        // Событие возникающее при начислении процентов
        protected internal event AccountStateHandler Calculated;

        public int Counter
        {
            get { return counter; }
            set { counter = value; }
        }

        public static int counter = 0;
        public int _days = 0; // время с момента открытия счета

        public DateTime dateTime = DateTime.Now;

        public Account()
        {
        }

        public Account(decimal sum, int percentage, string name)
        {
            Sum = sum;
            Percentage = percentage;
            Id = ++Counter;
            Name = name; 
        }

        // Текущая сумма на счету
        
        public decimal Sum { get;  set; }
        // Процент начислений
        
        public int Percentage { get;  set; }
        // Уникальный идентификатор счета
        public int Id { get;  set; }

        public string Name { get; set; }

        // вызов событий
        private void CallEvent(AccountEventArgs e, AccountStateHandler handler)
        {
            if (e != null)
                handler?.Invoke(this, e);
        }

        // вызов отдельных событий. Для каждого события определяется свой витуальный метод
        protected virtual void OnOpened(AccountEventArgs e)
        {
            CallEvent(e, Opened);
        }
        protected virtual void OnWithdrawed(AccountEventArgs e)
        {
            CallEvent(e, Withdrawed);
        }
        protected virtual void OnAdded(AccountEventArgs e)
        {
            CallEvent(e, Added);
        }
        protected virtual void OnClosed(AccountEventArgs e)
        {
            CallEvent(e, Closed);
        }
        protected virtual void OnCalculated(AccountEventArgs e)
        {
            CallEvent(e, Calculated);
        }
        
        public virtual void Put(decimal sum)
        {
            Sum += sum;
            OnAdded(new AccountEventArgs("На счет поступило " + sum, Sum));
        }

        
        // метод снятия со счета, возвращает сколько снято со счета
        public virtual decimal Withdraw(decimal sum)
        {
            decimal result = 0;
            if (Sum >= sum)
            {
                Sum -= sum;
                result = sum;
                OnWithdrawed(new AccountEventArgs($"Сумма {sum} снята со счета", sum));
            }
            else
            {
                OnWithdrawed(new AccountEventArgs($"Недостаточно денег на счете" /*{Id}*/, 0));
            }
            return result;
        }
        // открытие счета
        protected internal virtual void Open()
        {
            OnOpened(new AccountEventArgs($"Открыт новый счет! \nId счета: {Id}", Sum));
        }
        // закрытие счета
        protected internal virtual void Close()
        {
            OnClosed(new AccountEventArgs($"Счет {Id} закрыт.  Итоговая сумма: {Sum}", Sum));
        }

        protected internal void IncrementDays()
        {
            _days++;
            //return $"Прошло {_days} дней с открытия счёта {Id}";
        }
        // начисление процентов
        protected internal virtual void Calculate()
        {
            decimal increment = Sum * Percentage / 100;
            Sum = Math.Round(Sum + increment,2);
            OnCalculated(new AccountEventArgs($"Начислены проценты в размере: {increment} для счёта {Id}", increment));
        }

       
        protected internal virtual decimal GetPercent()
        {
            decimal increment = Sum * Percentage / 100;
            return Math.Round(increment,2);
        }
    }
}
