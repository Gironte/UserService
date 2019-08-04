using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace UserInformation.WebService.Models
{
    public class LocaleValidationAttribute : ValidationAttribute
    {
        private string LocaleISOCode { get; set; }

        public override bool IsValid(object value)
        {
            LocaleISOCode = (string)value;
            if (string.IsNullOrEmpty(LocaleISOCode)) return true; 
                                                                   

            return CultureInfo.GetCultures(CultureTypes.AllCultures).
                Any(x => x.TextInfo.CultureName.Equals(LocaleISOCode));
        }


        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture,
              ErrorMessageString, name, this.LocaleISOCode);
        }
    }
}
