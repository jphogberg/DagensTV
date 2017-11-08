using DagensTV.Data;
using DagensTV.Models;
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
        DbOperations dbo = new DbOperations();
        DagensTVEntities db = new DagensTVEntities();

        public ActionResult CreateAccount()
        {
            return View("CreateAccount");
        }

        [HttpPost]
        public ActionResult SetAccount(CreateAccountVM model)
        {
            if (ModelState.IsValid)
            {
                Person p = new Person();
                p.Firstname = model.Firstname;
                p.Lastname = model.Lastname;
                p.Username = model.Username;
                p.Password = model.Password;
                p.RoleId = 2;

                var checkUsers = db.Person.ToList();
                bool exists = false;
                foreach (var item in checkUsers)
                {
                    if (p.Username.Trim() == item.Username.Trim()
                        && p.Password.Trim() == item.Password.Trim())
                    {
                        exists = true;
                        break;
                    }
                }

                if (exists)
                {
                    //ViewBag.Message("Kontot är upptaget!");
                    return View("CreateAccount");
                }
                else
                {
                    db.Person.Add(p);
                    db.SaveChanges();
                    //ViewBag.Message("Ditt konto är nu skapat, vänligen logga in!");
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Alla fält måste fyllas i!");
                return View("CreateAccount");
            }
        }


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
                //return Redirect(ReturnUrl) om fler länkar i nav

                if (dbo.CheckUser(model.Username, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);

                    var persons = db.Person.ToList();

                    var roleId = 0;
                    foreach (var p in persons)
                    {
                        if (p.Username.Trim().Equals(model.Username))
                        {
                            roleId = p.RoleId.GetValueOrDefault();
                            Person.activeUser.Username = p.Username;
                            Person.activeUser.Password = p.Password;
                            Person.activeUser.Id = p.Id;
                            Person.activeUser.RoleId = p.RoleId;
                            Person.activeUser.RoleName = p.RoleName;
                        }
                    }

                    if (roleId == 1)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    if (roleId == 2)
                    {
                        return RedirectToAction("MyPage", "User");
                    }
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