using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OTP.Data;
using OTP.Models;
using OTP.Models.ViewModels;
using OTP.Repository.IRepository;
using OTP.Utility;
using System.Diagnostics;
using System.Security.Claims;

namespace OTP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> OTPGenerator()
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);

            OTPVM otpVM = new OTPVM {
                ApplicationUser = applicationUser,
                OTPDateTime = DateTime.Now
            };
            //var userEmail = _userManager.GetUserName(User);
            //var user = await _db.Users.FindAsync(userEmail);
            return View(otpVM);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> OTPGenerator(OTPVM otpVM)
        {
            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            //ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);
            OTPHistory oTPHistory = new OTPHistory {
                UserGuidId = otpVM.ApplicationUser.Id,
                UserId = otpVM.ApplicationUser.Id,
                OTP = GenerateOTPPassword(),
                TimeStamp = otpVM.OTPDateTime,
                Status = 1
            };

            _unitOfWork.OTPHistory.Add(oTPHistory);
            _unitOfWork.Save();


            oTPHistory.TimeStamp = otpVM.OTPDateTime.AddMonths(-1).AddSeconds(31);

            return RedirectToAction(nameof(FinalOTP), oTPHistory);
        }

        public IActionResult FinalOTP(OTPHistory oTPHistory)
        {

            return View(oTPHistory);
        }


        private string GenerateOTPPassword()
        {
           // generate only numbers
           Random rnd = new Random();
            return rnd.Next(100000, 999999).ToString();
            //return RandomString(6);
        }

        // generate alphanumerical
    //    public static string RandomString(int length)
    //    { 
    //         Random random = new Random();
    //        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    //        return new string(Enumerable.Repeat(chars, length)
    //            .Select(s => s[random.Next(s.Length)]).ToArray());

    //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}


        //API CALLS
        public void CancelingOTP(string otp)
        {
            OTPHistory oTPHistory  = _unitOfWork.OTPHistory.GetFirstOrDefault(o => o.OTP == otp);
            if (oTPHistory != null)
            {
                oTPHistory.Status = 0;
            }
            _unitOfWork.OTPHistory.Update(oTPHistory);
            _unitOfWork.Save();
        }

        public IActionResult ValidateOTP(string otp)
        {
            OTPHistory oTPHistory = _unitOfWork.OTPHistory.GetFirstOrDefault(o => o.OTP == otp);
            return oTPHistory.Status == 0 ? Json(new { success = true }) : Json(new { success = false });
        }

    }
}