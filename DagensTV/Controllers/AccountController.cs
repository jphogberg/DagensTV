using DagensTV.Data;
using DagensTV.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DagensTV.Controllers
{
    public class AccountController : Controller
    {
        DbOperations db = new DbOperations();

        // GET: Account
        public ActionResult Login()
        {
            
            return View("Login");
        }


        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                if(db.CheckUser(model.Username, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Felaktigt användarnamn eller lösenord.");
                    
                }
            }
            return View();
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}