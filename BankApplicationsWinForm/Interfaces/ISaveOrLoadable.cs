using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplicationsWinForm.Interfaces
{
    interface ISaveOrLoadable
    {
        bool SaveDocuments(string str);
        bool LoadDocuments(string str);
    }
}
