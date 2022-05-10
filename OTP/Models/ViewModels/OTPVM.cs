using System.ComponentModel.DataAnnotations;

namespace OTP.Models.ViewModels
{
    public class OTPVM
    {
        public ApplicationUser ApplicationUser { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OTPDateTime { get; set; }
    }
}
