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
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginVM model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                if(db.CheckUser(model.Username, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return Redirect(ReturnUrl);
                    //behöver hantera om url inte finns
                }
                else
                {
                    ModelState.AddModelError("", "Felaktigt användarnamn eller lösenord.");
                    
                }
            }
            return View();
        }

    }
}