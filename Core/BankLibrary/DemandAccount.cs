using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BankLibrary
{
    [XmlInclude(typeof(Account))]
    public class DemandAccount : Account
    {
        public DemandAccount()
        {

        }
        public DemandAccount(decimal sum, int percentage, string name) : base(sum, percentage, name)
        {
        }

        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs($"Открыт новый счет до востребования!" /*Id счета: {this.Id}"*/, this.Sum));
        }
    }
}
