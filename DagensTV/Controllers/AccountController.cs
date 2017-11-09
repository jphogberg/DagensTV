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
                #region Get user input
                Person p = new Person();
                p.Firstname = model.Firstname;
                p.Lastname = model.Lastname;
                p.Username = model.Username;
                p.Password = model.Password;
                p.RoleId = 2;
                #endregion

                #region Set Account
                bool exists = false;
                foreach (var item in dbo.GetAllPersons())
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
                    ModelState.AddModelError(string.Empty, "Användarnamnet finns redan!");
                    return View("CreateAccount");
                }
                else
                {
                    db.Person.Add(p);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                #endregion
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Alla fält måste fyllas i!");
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
                #region Login
                if (dbo.CheckUser(model.Username, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);

                    var roleId = 0;
                    foreach (var p in dbo.GetAllPersons())
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
                #endregion
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