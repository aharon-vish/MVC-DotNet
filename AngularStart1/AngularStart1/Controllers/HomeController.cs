using System;
using System.Data.Objects.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AngularStart1.Models;
using System.Web.Security;
using System.Net;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using System.Web.UI;

using System.Threading.Tasks;
using System.Net.Mail;


namespace AngularStart1.Controllers
{

    public class HomeController : Controller
    {

        //check id of store
        List<GiftCards> GiftCard = new List<GiftCards>();

        [Authorize]
        [HttpGet]
        public JsonResult GiftCardParm(string param1)
        {
            int _GiftCardID;
            if (int.TryParse(param1, out _GiftCardID))
            {
                using (GiftCardEntities db = new GiftCardEntities())
                {
                    GiftCards giftCard = new GiftCards();
                    try
                    {
                        giftCard = db.GiftCards.Where(g => g.GiftCardID == _GiftCardID).SingleOrDefault();

                    }
                    catch
                    {

                    }
                    if (giftCard != null)
                    {
                        GiftCards _GiftCard = new GiftCards()
                        {
                            Credit = giftCard.Credit,
                            Email = giftCard.Email,
                            FirstName = giftCard.FirstName,
                            FromWho = giftCard.FromWho,
                            GiftCardID = giftCard.GiftCardID,
                            GiftCardValid = giftCard.GiftCardValid,
                            LastName = giftCard.LastName,
                            StoreID = giftCard.StoreID,
                            StoreName = giftCard.StoreName
                        };

                        return new JsonResult { Data = _GiftCard, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                }
            }



            return this.Json(new { success = false });

        }
        //check id of store
        [Authorize]
        public JsonResult autocomplete(string term)
        {
            List<int> StoreUser_ = new List<int>();// for autocomplete 
            using (GiftCardEntities db = new GiftCardEntities())
            {
                StoreUser_ = db.GiftCards.Where(s => SqlFunctions.StringConvert((double)s.GiftCardID).TrimStart()
                    .StartsWith(term))
                    .Select(s => s.GiftCardID).ToList();
            }
            List<string> _StoreUser = StoreUser_.ConvertAll<string>(x => x.ToString());
            return Json(_StoreUser, JsonRequestBehavior.AllowGet);

        }


        public ActionResult Index()
        {

            return View();
        }



        [Authorize]
        [HttpGet]
        public JsonResult annualReport()
        {

            AnnualReport annualReport = new AnnualReport();
            List<string> annualReportDate = new List<string>();
            List<string> annualReportDateUse = new List<string>();
            string stroreN = this.User.Identity.Name;
            using (GiftCardEntities db = new GiftCardEntities())
            {
                annualReportDate = db.GiftCards.Where(s => s.StoreID == stroreN).
                    Select(s => s.GiftCardValid).ToList();
                annualReportDateUse = db.Receipts.Where(r => r.StoreId == stroreN).
                  Select(s => s.DatePurchas).ToList();
            }
            //gift catd buy
            foreach (string date in annualReportDate)
            {
                if (date.Contains(DateTime.Now.AddYears(1).Year.ToString()))
                {

                    annualReport.CountA++;
                }
                if (date.Contains(DateTime.Now.AddYears(0).Year.ToString()))
                {

                    annualReport.CountB++;
                }
                if (date.Contains(DateTime.Now.AddYears(-1).Year.ToString()))
                {

                    annualReport.CountC++;
                }
            }

            //use
            foreach (string date in annualReportDateUse)
            {
                if (date.Contains(DateTime.Now.Year.ToString()))
                {
                    annualReport.UsegA++;

                }
                if (date.Contains(DateTime.Now.AddYears(-1).Year.ToString()))
                {
                    annualReport.UsegB++;

                }
                if (date.Contains(DateTime.Now.AddYears(-2).Year.ToString()))
                {
                    annualReport.UsegC++;

                }
            }

            return new JsonResult { Data = annualReport, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        
        public ActionResult Contact(ContactUs d)
        {

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();

            using (var smtp = new Email())
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add("electronic.gift.card@gmail.com");
                    mail.From = new MailAddress(d.Email);
                    mail.Subject = d.Name;
                    string Body = d.Message;
                    mail.Body = Body;
                    mail.IsBodyHtml = true;
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    mail.Dispose();
                    return Json(new { success = true, errors = true });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, errors = true });
                }
            }
 
           
        }

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult Angular()
        {
            return View();
        }

        public ActionResult Demo()
        {
            return View();
        }

        public ActionResult cardImplement(Receipts d)

        {
            using (GiftCardEntities db = new GiftCardEntities())
            {
                try
                {
                    GiftCards _GiftCard = db.GiftCards.Where(g => g.GiftCardID == d.GiftCardID).FirstOrDefault();
                    if (d.PurchaseAmount == 0)
                    {
                        d.PurchaseAmount = Convert.ToInt32(_GiftCard.Credit);
                        _GiftCard.Credit = 0;
                    }
                    else if (d.PurchaseAmount > 0)
                    {
                        int KeepOriganalCredit = Convert.ToInt32(_GiftCard.Credit);
                        _GiftCard.Credit = d.PurchaseAmount;
                        d.PurchaseAmount = KeepOriganalCredit - d.PurchaseAmount;

                    }
                    else if (d.PurchaseAmount < 0)
                    {
                        d.PurchaseAmount = (d.PurchaseAmount + (Convert.ToInt32(_GiftCard.Credit) * -1)) * -1;
                        _GiftCard.Credit = 0;
                    }
                    _GiftCard.FirstName = "amicam";
                    _GiftCard.Receipts.Add(d);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    
                   return Json(new { success = false, errors = true });
                }

                 return Json(new { success = true, errors = true });
            }
           
        }

        [Authorize]
        [HttpGet]
        public ActionResult monthlyReport()
        {
            int[,] CountOfBuying = new int[3, 12];
            int[,] CountOfImplement = new int[3, 12];
         
            
            List<MonthlyReport> monthlyReportList = new List<MonthlyReport>();
                         string stroreN = this.User.Identity.Name;

                         List<string> monthlyReportDate = new List<string>();
                         List<string> monthlyReportDateUse = new List<string>();

                         using (GiftCardEntities db = new GiftCardEntities())
                         {
                             monthlyReportDate = db.GiftCards.Where(s => s.StoreID == stroreN).
                                             Select(s => s.GiftCardValid).ToList();
                             monthlyReportDateUse = db.Receipts.Where(r => r.StoreId == stroreN).
                               Select(s => s.DatePurchas).ToList(); 
                         }
                         // count BuyCard in month
                         foreach (var BuyCard in monthlyReportDate)
                         {
                             DateTime dt = Convert.ToDateTime(BuyCard);
                             //2015
                             if (dt.Year == 2016)
                             {
                                 int month = dt.Month;
                                 month--;
                                 CountOfBuying[0,month]++;
                                 
                             }
                             //2014
                             if (dt.Year == 2015)
                             {
                                 int month = dt.Month;
                                 month--;
                                 CountOfBuying[1,month]++;
                                 
                             }
                             //2013
                             if (dt.Year == 2014)
                             {
                                 int month = dt.Month;
                                 month--;
                                  CountOfBuying[2,month]++;
                                 
                             }
                         }

                         // count UseCard in month
                         foreach (var UseCard in monthlyReportDateUse)
                         {
                             DateTime dt = Convert.ToDateTime(UseCard);
                             //2015
                             if (dt.Year == 2015)
                             {
                                 int month = dt.Month;
                                 month--;
                                 CountOfImplement[0, month]++;

                             }
                             //2014
                             if (dt.Year == 2014)
                             {
                                 int month = dt.Month;
                                 month--;
                                 CountOfImplement[1, month]++;

                             }
                             //2013
                             if (dt.Year == 2013)
                             {
                                 int month = dt.Month;
                                 month--;
                                 CountOfImplement[2, month]++;

                             }
                         }
                         var result = new { CountOfBuying = CountOfBuying, CountOfImplement = CountOfImplement };

                         return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index");
        }
        [AllowAnonymous]
        [HttpPost]
        public JsonResult UserLogin(StoreUser d)
        {
            if (!String.IsNullOrEmpty(d.StoreName) && !String.IsNullOrEmpty(d.Password))
            {
                int idUser = Convert.ToInt32(d.StoreName);
                using (GiftCardEntities db = new GiftCardEntities())
                {
                    var user = db.StoreUsers.Where(s => s.IDUser == idUser && s.Password.Equals(d.Password)).FirstOrDefault();
                      if (user!=null && user.Password!=null)
                    {
                        FormsAuthentication.SetAuthCookie(user.IDUser.ToString(), false);
                        return Json(new { success = true });
                    }

                     return Json(new { success = false, errors = true });
                }
            }
            else
                return Json(new { success = false, errors = true });
        }
    }
}
