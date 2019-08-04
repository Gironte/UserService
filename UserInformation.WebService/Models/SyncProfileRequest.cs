using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UserInformation.WebService.Models
{
    [UserValidation]
    public class SyncProfileRequest : MyAccountRequestBase
    {
        public bool? AdvertisingOptIn { get; set; }

        [Required]
        [StringLength(2)]
        [CountryIsoCodeValidation]
        public string CountryIsoCode { get; set; }

        private DateTime dateModified { get; set; }

        public DateTime DateModified {
            get { return dateModified.Equals(DateTime.MinValue) ? DateTime.Now : dateModified; }
            set { dateModified = value; }
        }

        [LocaleValidation]
        public string Locale { get; set; }
    }

}
