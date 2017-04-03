using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
       
        
        
        [AllowAnonymous]   
        public ActionResult Index(int? id)        
        {       
            if (id != null && this.User.Identity.IsAuthenticated)
            {
                User user = new User();
                using (var db = new DBCon())
                {
                    user = db.Users.Find(id);
                   
                }
                if (user == null)
                {                    
                    return HttpNotFound();
                }
                else
                {
                    return View(user);
                }
            }
            else
            {
                return View();

            }
        }
        public ActionResult GetImage(int id)
        {

            using (var db = new DBCon())
            {
                var store = db.Stores.FirstOrDefault(s => s.StoreID == id);
                ViewData["NameOfStore"] = store.NameOfStroe;
                return File(store.Image, "image/jpg");
            }



        }

        [Authorize]
        public ActionResult BuyGiftCard(string StoreID)
        {
            ViewData["StoreID"] = StoreID;
            using (var db = new DBCon())
            {
                int id = Convert.ToInt32(StoreID);
                var store = db.Stores.FirstOrDefault(s => s.StoreID == id);
                ViewData["NameOfStroe"] = store.NameOfStroe;
            }
            return View();
        } 
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Upload(GiftCard GiftTo, HttpPostedFileBase fileUploader, string imageDataCard)
        {
            Boolean CheckExceptionDbSaVE = false;

            string Username = this.User.Identity.Name;
            string fullName;
            string fileName = GiftTo.GiftCardID.ToString()+".png";
            string fileNameWitPath = Path.Combine(Server.MapPath("~/App_Data/GiftCards"), fileName);
        
            using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(imageDataCard);
                    bw.Write(data);
                    bw.Close();
                }
                fs.Close();
            }

            using (var smtp = new Email())
            {
                MailMessage mail = new MailMessage();



                using (var db = new DBCon())
                {
                    var _storeId = Convert.ToInt16(GiftTo.StoreID);
                    var _Store = db.Stores.FirstOrDefault(s => s.StoreID == _storeId);

                    GiftTo.StoreName = _Store.NameOfStroe;
                    DateTime Now = DateTime.Now.AddYears(1);
                    GiftTo.GiftCardValid = Now.ToString("dd/MM/yyyy");

                    var _User = db.Users.FirstOrDefault(u => u.Email == Username);
                    fullName = _User.FirstName + " " + _User.LastName;
                    GiftTo.FromWho = _User.FirstName + " " + _User.LastName;
                    _User.GiftCards.Add(GiftTo);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch(Exception ex)
                    {
                        CheckExceptionDbSaVE = true;
                        // error log
                    }
                }


                mail.To.Add(GiftTo.Email);
                mail.From = new MailAddress("aharon.vish@gmail.com");
                mail.Subject = "You got gift card from " + fullName;
                string Body = null;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                smtp.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

                mail.Attachments.Add(new Attachment(Server.MapPath("~/App_Data/GiftCards/" + fileName)));

                if (fileUploader != null && fileUploader.ContentLength > 0)
                {
                    mail.Attachments.Add(new Attachment(fileUploader.InputStream, Path.GetFileName(fileUploader.FileName)));

                }
                try
                {
                    if (!CheckExceptionDbSaVE)
                    {
                        await smtp.SendMailAsync(mail);
                        mail.Dispose();
                    }
                }
                catch(Exception e)
                {
                    // error log
                    using (var db = new DBCon())
                    {

                        var giftCardToDelete = db.GiftCards.FirstOrDefault(g => g.GiftCardID == GiftTo.GiftCardID);
                        db.GiftCards.Remove(giftCardToDelete);
                        db.SaveChanges();
                    }

                    mail.Dispose();
                    return RedirectToAction("Error");
                   
                }
            }


            return RedirectToAction("Successfully");
        
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
        [Authorize]
        [HttpPost]
        public ActionResult doesIdCardExist(string idGiftCard)
        {
            double IdGiftCard = Convert.ToDouble(idGiftCard);
            var db = new DBCon();
            var GiftCards = db.GiftCards.FirstOrDefault(c => c.GiftCardID == IdGiftCard);

            if (GiftCards == null)
            {
                return Content("true");

            }
            return Content("false");
        }

        public ActionResult ViewRecVoice()
        {
            return View();
        }
        public ActionResult ViewSketch()
        {
            return View();
        }
        public ActionResult Help()
        {
            return View();
        }
        public ActionResult ViewTextGreet()
        {
            return View();
        }
        //search store  
       [HttpPost]
        public ActionResult Search(string search)
        {          
            using (var db = new DBCon())
            {
               var _store = from store in db.Stores
                            where store.NameOfStroe.StartsWith(search)
                            select store;
                return View(_store.ToList());
            }          
        }
    
        
        public ActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ContactUs(string nameToContact, string emailToContact, string messageToContact)
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
                    mail.From = new MailAddress(emailToContact);
                    mail.Subject = nameToContact + " " + emailToContact;
                    string Body = messageToContact;
                    mail.Body = Body;
                    mail.IsBodyHtml = true;
                    await smtp.SendMailAsync(mail);
                    mail.Dispose();
                    return Content("The Message Was Sent Successfully");
                }
                catch (Exception ex)
                {
                    return PartialView("The Message Not Sent try Refreshing The Browser");
                }
            }
            
        }
         [Authorize]
         [HttpGet]
         public ActionResult GiftBox()
         {
             string User = this.User.Identity.Name.ToString();
             using (var db = new DBCon())
             {
                 var _User = db.Users.FirstOrDefault(u => u.Email == User);

                 var _giftCard = db.GiftCards.Where(g => g.UserId == _User.IDUser);
                 return View(_giftCard.ToList());
             } 
            
         }

         // // Return gift cards that was send from me 
         public PartialViewResult FromMe()
         {
             string User = this.User.Identity.Name.ToString();
             using (var db = new DBCon())
             {
                 var _User = db.Users.FirstOrDefault(u => u.Email == User);
                 string tempId =_User.IDUser.ToString();
                 string Name =_User.FirstName +" "+_User.LastName;
                 var _giftCard = db.GiftCards.Where(g => g.StoreID == tempId);
                 _giftCard.OrderByDescending(g => g.FromWho);
                 return PartialView("_FromMe", _giftCard.ToList());
             } 
         }

         // Return gift cards that was send to me 
        [HttpGet]
         public PartialViewResult ToMe()
         {
             string User = this.User.Identity.Name.ToString();
             using (var db = new DBCon())
             {
                 var _User = db.Users.FirstOrDefault(u => u.Email == User);

                 List<GiftCard> model = db.GiftCards.Where(g => g.Email == _User.Email).ToList();

                 model.OrderByDescending(g=>g.FromWho);
                 return PartialView("_ToMe", model);
             } 
         }
      
        
        [Authorize]
        public ActionResult Successfully()
        {
            return View("Successfully");
        }
      
        [Authorize]
        public ActionResult Error()
        {
            return View("Error");
        }

    }
}