using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GCMP.Models.AccountModel;
using GCMP.Models.Entities;
using GCMP.Models.Helper;
using GCMP.Models.Security;
using GCMP.ViewModels.AccountViewModel;
using Microsoft.Ajax.Utilities;
using WebMatrix.WebData;

namespace GCMP.Controllers.Account
{
    public class AccountController : Controller
    {
        readonly AccountModel _model = new AccountModel();

        [ChildActionOnly]
        public ActionResult Register()
        {
            var user = User.Identity.Name;
            if (user == "")
            {
                return PartialView();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public ActionResult Register(AccountRegisterViewModel regModel)
        {
           
            if (ModelState.IsValid)
            {
                _model.RegisterAccount(regModel);
            }
            return PartialView(regModel);
        }

        [HttpPost]
        public WrappedJsonResult UploadImage(HttpPostedFileWrapper imageFile)
        {
            if (imageFile == null || imageFile.ContentLength == 0)
            {
                return new WrappedJsonResult
                {
                    Data = new
                    {
                        IsValid = false,
                        Message = "No file was uploaded.",
                        ImagePath = string.Empty
                    }
                };
            }
            var fileName = String.Format("{0}.jpg", Guid.NewGuid().ToString());
            var imagePath = Path.Combine(Server.MapPath(Url.Content("~/Content/Uploads/UsersAvatar/")), fileName);
            using (var input = new Bitmap(imageFile.InputStream))
            {
                int width;
                int height;
                if (input.Width > input.Height)
                {
                    width = 160;
                    height = 160 * input.Height / input.Width;
                }
                else
                {
                    height = 200;
                    width = 200 * input.Width / input.Height;
                }

                using (var thumb = new Bitmap(width, height))
                using (var graphic = Graphics.FromImage(thumb))
                {
                    graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphic.SmoothingMode = SmoothingMode.AntiAlias;
                    graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    graphic.DrawImage(input, 0, 0, width, height);
                    using (var output = System.IO.File.Create(imagePath))
                    {
                        thumb.Save(output, ImageFormat.Jpeg);
                    }
                }
            }
            
            return new WrappedJsonResult
            {
                Data = new
                {
                    IsValid = true,
                    Message = string.Empty,
                    ImagePath = Url.Content(String.Format("~/Content/Uploads//UsersAvatar/{0}", fileName))
                }
            };
        }


        [HttpPost]
        public ActionResult CheckExistUser(string username)
        {
            if (_model.CheckExistedUser(username))
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }


        [HttpPost]
        public ActionResult CheckExistIDcard(string iDcard)
        {
            if (_model.CheckExitstedIdCard(iDcard))
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult Authentication()
        {
            var user = User.Identity.Name;
            if (user == "")
            {
                return PartialView();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Authentication(LoginViewModel account, string returnUrl)
        {
            string messageReturn = "";
            if (ModelState.IsValid)
            {
                var gcmpMembership = new GCMPMembershipProvider();

                if (gcmpMembership.ValidateUser(account.UserName, account.Password))
                {
                   
                    FormsAuthentication.SetAuthCookie(account.UserName, account.RememberMe);
                    var aa = User.Identity.Name;
                    return RedirectToAction("Index", "Home");
                }
            }
            else if (!account.UserName.IsNullOrWhiteSpace() && !account.Password.IsNullOrWhiteSpace())
            {
                AccountRegisterViewModel regmodel = new AccountRegisterViewModel
                {
                    Username = account.UserName,
                    Password = account.Password
                };

                var rs = _model.RegisterAccount(regmodel);
                if (rs)
                {

                    return RedirectToAction("RegisterSuccess", "Account", new { returnUrl = returnUrl, username = account.UserName, password = account.Password, remember = account.RememberMe });
                }
                else
                {
                    messageReturn = "Có lỗi trong quá trình đăng ký, xin vui lòng thử lại!";
                    return RedirectToAction("Authenfailed", "Account", new { returnUrl = returnUrl, message = messageReturn });
                }
            }
            // * in case of, email and password aren't correct!!
            messageReturn = "Sai email đăng nhập hoặc mật khẩu! Xin vui lòng thử lại!";

            return RedirectToAction("Authenfailed", "Account", new { returnUrl = returnUrl, message = messageReturn});
        }

        public ActionResult RegisterSuccess(string returnUrl, string username, string password, bool remember )
        {
            var account = new LoginViewModel
            {
                UserName = username,
                Password = password,
                Register = true,
                RememberMe = remember
            };
            @ViewBag.returnURL = returnUrl;
            return View(account);
        }

       
        public ActionResult Authenfailed(string returnUrl, string message)
        {
            ViewBag.returnMessage = message;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //login via Ajax
        [HttpPost]
        public ActionResult ALogin(string username, string password)
        {
            LoginViewModel account = new LoginViewModel()
            {
                UserName = username,
                Password = password,
                RememberMe = true

            };

            string message = "";
            if (ModelState.IsValid)
            {
                var evasMembership = new GCMPMembershipProvider();
                if (evasMembership.ValidateUser(account.UserName, account.Password))
                {

                    var evasRole = new GCMPRoleProvider();
                    if (evasRole.IsUserInRole(account.UserName, "Administrator"))
                    {
                        FormsAuthentication.SetAuthCookie(account.UserName, true);
                        // * redirect to Admin Dashboard
                        message = "Chúc mừng Admin đã đăng nhập thành công";
                        //return RedirectToAction("Index", "AdminDashboard");

                    }
                    if (evasRole.IsUserInRole(account.UserName, "Moderator"))
                    {
                        FormsAuthentication.SetAuthCookie(account.UserName, true);
                        // * redirect to Retailer Dashboard
                        // return RedirectToAction("Index", "CustomerService");
                    }
                    if (evasRole.IsUserInRole(account.UserName, "User"))
                    {
                        FormsAuthentication.SetAuthCookie(account.UserName, true);
                        // * redirect to Retailer Dashboard
                        // return RedirectToAction homepage

                        //MigrateShoppingCart(account.UserName);

                    }
                }
                else
                {
                    message = "Email đăng nhập hoặc mật khẩu không chính xác.";
                }
            }
            // * in case of, email and password aren't correct!!

            return Json(message);
        }


        public ActionResult LogOff(string username)
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");

        }
    }

}