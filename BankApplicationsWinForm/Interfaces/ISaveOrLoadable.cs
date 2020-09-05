using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplicationsWinForm.Interfaces
{
    interface ISaveOrLoadable
    {
        Task<bool> SaveDocumentsAsunc(string str);
        Task<bool> LoadDocumentsAsunc(string str);
    }
}
