using System;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserInformation.WCFService.Objects
{
    [DataContract]
    [Table("MyAccountRequestBases")]
    public class UserInfo
    {
        [Key]
        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public bool? AdvertisingOptIn { get; set; }
                     
        [DataMember]

        public string CountryIsoCode { get; set; }

        [DataMember]

        public DateTime DateModified { get; set; }

        [DataMember]

        public string Locale { get; set; }
    }

}
