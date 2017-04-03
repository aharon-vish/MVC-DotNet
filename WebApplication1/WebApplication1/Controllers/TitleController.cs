using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TitleController : Controller
    {
        private DBCon db = new DBCon();
        public ActionResult Index()
        {
            var emp = db.Users.ToList();
            return View();
        }
    }
}