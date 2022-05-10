using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OTP.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string UserId { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy HH:mm:ss}")]
        public DateTime DataGenerareOTP { get; set; }

    }
}
