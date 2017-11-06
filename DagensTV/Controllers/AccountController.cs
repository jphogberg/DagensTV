﻿using DagensTV.Data;
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


            return View();
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
                if(dbo.CheckUser(model.Username, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);

                    var persons = db.Person.ToList();
                    
                    var roleId = 0;
                    foreach (var p in persons)
                    {
                        if (p.Username.Trim().Equals(model.Username))
                        {
                            roleId = p.RoleId.GetValueOrDefault();
                            //Person.activeUser.Username = p.Username;
                            //Person.activeUser.Password = p.Password;
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