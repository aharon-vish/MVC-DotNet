using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebApplication1.Models;
using System.Web.Security;
using System.Web.Routing;
using System.Text;
using System.Net.Mail;
using WebApplication1.Models;
using System.Threading.Tasks;
using System.ComponentModel;


namespace WebApplication1.Controllers
{

    public class AccountController : Controller
    {
        [HttpPost]
        public JsonResult ForgotPassword(string email)
        {
            string error = "error";
            string NewPass = "New password send to your mail";
            string NoMail = "Your mail was not found!!";
            using (var db = new DBCon())
            {
                try
                {
                  var _User = db.Users.FirstOrDefault(u => u.Email == email);
                if(_User==null || _User.Email == null)
                {
                    return Json(NoMail, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var Crypto = new SimpleCrypto.PBKDF2();
                    var EncPass = Crypto.Compute("58291");

                    _User.Password = EncPass;
                    _User.PasswardSalt = Crypto.Salt;

                    db.SaveChanges();
                    using (var smtp = new Email())
                    {
                        MailMessage mail = new MailMessage();
                        mail.To.Add(_User.Email);
                        mail.From = new MailAddress("electronic.gift.card@gmail.com");
                        mail.Subject ="new password is waiting you in the mail please change ";
                        string Body = "58291";
                        mail.Body = Body;
                        mail.IsBodyHtml = true;
                        smtp.Send(mail);
                        mail.Dispose();

                        return Json(NewPass, JsonRequestBehavior.AllowGet);
                    }
                }
                }
                catch (Exception)
                {

                    return Json(error, JsonRequestBehavior.AllowGet);
                }

            }

            
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        [Authorize]
        public ActionResult UserAccount(User user)
        {


            var Cryptro = new SimpleCrypto.PBKDF2();

            string User = this.User.Identity.Name.ToString();
            using (var db = new DBCon())
            {
                var _User = db.Users.FirstOrDefault(u => u.Email == User);
                if (user.Password == Cryptro.Compute(_User.Password, _User.PasswardSalt) || user.Password == _User.Password)
                {

                    _User.FirstName = user.FirstName;
                    _User.LastName = user.LastName;
                    _User.Address = user.Address;
                    db.SaveChanges();
                }
                else
                {
                    var Crypto = new SimpleCrypto.PBKDF2();
                    var EncPass = Crypto.Compute(user.Password);

                    _User.Password = EncPass;
                    _User.PasswardSalt = Crypto.Salt;
                    _User.FirstName = user.FirstName;
                    _User.LastName = user.LastName;
                    _User.Address = user.Address;
                    db.SaveChanges();
                }

                return RedirectToAction("Index", "Home", new { id = _User.IDUser });
            }


        }

        [HttpGet]
        [Authorize]
        public ActionResult UserAccount()
        {
            string User = this.User.Identity.Name.ToString();
            using (var db = new DBCon())
            {

                var _User = db.Users.FirstOrDefault(u => u.Email == User);
                return View(_User);
            }

        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            User Model = new User();

            if (IsValid(Email, Password))
            {
                using (var db = new DBCon())
                {
                    Model = db.Users.FirstOrDefault(u => u.Email == Email);
                    FormsAuthentication.SetAuthCookie(Email, false);
                }
                return RedirectToAction("Index", "Home", new { id = Model.IDUser });
            }
            else
            {

                ViewData["Msg"] = "Login data is incorrect.";
                Email = null;
                Password = null;

            }

            return View();
        }


        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public JsonResult doesUserNameExist(string email)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                var db = new DBCon();
                var user = db.Users.FirstOrDefault(u => u.Email == email);

                return Json(user == null);
            }
            return Json(true);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Registration(User user)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DBCon())
                {
                    var Crypto = new SimpleCrypto.PBKDF2();
                    var EncPass = Crypto.Compute(user.Password);
                    var User = db.Users.Create();

                    User.Email = user.Email;
                    User.FirstName = user.FirstName;
                    User.LastName = user.LastName;
                    User.Password = EncPass;
                    User.PasswardSalt = Crypto.Salt;
                    User.Address = user.Address;
                    db.Users.Add(User);
                    db.SaveChanges();

                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    ViewData["userName"] = user.FirstName.ToString();
                    User Model = db.Users.FirstOrDefault(u => u.Email == user.Email);

                    return RedirectToAction("Index", "Home", new { id = Model.IDUser });
                }
            }
            return View();
        }

        private bool IsValid(string email, string password)
        {
            var Cryptro = new SimpleCrypto.PBKDF2();
            bool isValid = false;

            using (var db = new DBCon())
            {
                var user = db.Users.FirstOrDefault(u => u.Email == email);

                if (user != null)
                {
                    if (user.Password == Cryptro.Compute(password, user.PasswardSalt))
                    {
                        isValid = true;
                    }
                }
            }

            return isValid;
        }
        void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                /*error log*/
            }
            if (e.Error != null)
            {
                /*Serror log*/
            }
            else
            {
                /* successfully  log*/

            }
        }


    }
}