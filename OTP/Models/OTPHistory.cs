using System.ComponentModel.DataAnnotations;

namespace OTP.Models
{
    public class OTPHistory
    {
        public int Id { get; set; }
        [Display(Name ="UserId")]
        public string UserGuidId { get; set; }
        public string UserId { get; set; }
        public string OTP { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Status { get; set; }
    }
}
