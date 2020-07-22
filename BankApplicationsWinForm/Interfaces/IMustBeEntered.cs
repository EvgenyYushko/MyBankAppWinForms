using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankApplicationsWinForm.Interfaces
{
    interface IMustBeEntered
    {
        bool GetMustBeEnteredFields(out string fieldsText);
    }
}
