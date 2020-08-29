using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplicationsWinForm.Exceptions
{
    class BankException : Exception
    {
        public BankException(string ex)
            : base(ex)
        {
            Service.LogWrite(ex);
        }
    }
}
