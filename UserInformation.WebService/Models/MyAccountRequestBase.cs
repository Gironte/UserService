using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace UserInformation.WebService.Models
{
    
    public abstract class MyAccountRequestBase
    {
        private Guid requestId { get; set; }

        [Key]
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid RequestId
        {
            get { return requestId.Equals(Guid.Empty) ? Guid.NewGuid() : requestId; }
            set { requestId = value; }
        }
    }
}
