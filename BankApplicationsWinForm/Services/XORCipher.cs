using BankApplicationsWinForm.Interfaces.Cheaper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BankApplicationsWinForm.Services.GlobalData;

namespace BankApplicationsWinForm.Services
{
    class XORCipher : ICheaper, IDeCheaper
    {
        //Подгоняет длинну пароля под длинну строки
        private string GetRepeatKey(string secretKey, int passwordLenght)
        {
            var r = secretKey;
            while (r.Length < passwordLenght)
            {
                r += r;
            }

            return r.Substring(0, passwordLenght);
        }

        //метод шифрования/дешифровки
        private string Cipher(string password, string secretKey)
        {
            var currentKey = GetRepeatKey(secretKey, password.Length);
            var res = string.Empty;
            for (var i = 0; i < password.Length; i++)
            {
                res += ((char)(password[i] ^ currentKey[i])).ToString();
            }

            return res;
        }

        //шифрование пароля
        public string Encrypt(string password) => 
            Cipher(password, SECRET_KEY);

        //расшифровка пароля
        public string Decrypt(string password) =>
             Cipher(password, SECRET_KEY);
    }
}
