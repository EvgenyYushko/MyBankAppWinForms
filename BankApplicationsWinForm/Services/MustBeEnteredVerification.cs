using BankApplicationsWinForm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankApplicationsWinForm.Services
{
    class MustBeEnteredVerification : IMustBeEntered
    {
        Form form;

        public MustBeEnteredVerification(Form form)
        {
            this.form = form;
        }

        public bool GetMustBeEnteredFields(out string fieldsText)
        {
            Dictionary<Control, string> _mustBeEnteredFields = new Dictionary<Control, string>();
            StringBuilder _fieldsTextList = new StringBuilder();

            int count = 0;
            fieldsText = string.Empty;

            var controls = this.form.Controls;

            foreach (Control control in controls)
            {
                if (control is TextBox)
                {
                    TextBox c = control as TextBox;

                    if (string.IsNullOrEmpty(c.Text))
                    {
                        _mustBeEnteredFields.Add(c, c.Tag.ToString());
                        _fieldsTextList.AppendLine($"{++count}. \"{c.Tag}\"");
                        c.Focus();
                    }
                }
            }

            fieldsText = _fieldsTextList.ToStringFormated();

            return _mustBeEnteredFields.Count > 0;
        }


    }
}
