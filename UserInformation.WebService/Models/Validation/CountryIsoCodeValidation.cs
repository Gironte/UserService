using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace UserInformation.WebService.Models
{
    class CountryIsoCodeValidationAttribute : ValidationAttribute
    {
        private string CountryISOCode { get; set; }

        public override bool IsValid(object value)
        {
            CountryISOCode = (string)value;

            if (string.IsNullOrEmpty(CountryISOCode)) return false;

            return CultureInfo.GetCultures(CultureTypes.AllCultures).
                Any(x => x.TwoLetterISOLanguageName.ToUpper().Equals(CountryISOCode.ToUpper())); 
        }


        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture,
              ErrorMessageString, name, this.CountryISOCode);
        }
    }
}
