using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace UserInformation.WebService.Models
{
    public class UserValidationAttribute : ValidationAttribute
    {
        private SyncProfileRequest SyncProfileRequest { get; set; }

        public override bool IsValid(object value)
        {
            SyncProfileRequest = (SyncProfileRequest)value;
            return SyncProfileRequest != null;
        }


        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture,
              ErrorMessageString, name, this.SyncProfileRequest);
        }
    }
}
