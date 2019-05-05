using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ProjektGG.BL;
using ProjektGG.ViewModels.Authentication;


namespace ProjektGG.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(LoginVM loginVM, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                Authorization authorization = new Authorization();
                if (authorization.checkLogin(loginVM.User.Username, loginVM.User.Password))
                {
                    FormsAuthentication.SetAuthCookie(loginVM.User.Username, false);
                    if (ReturnUrl == null)
                    {
                        return RedirectToAction("Przepisy", "Home");
                    }
                    return Redirect(ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("CredentialError", "Niepoprawne hasło lub nazwa użytkownika");
                    return View("Login");
                }
            }
            else return View(loginVM);
            
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            RegisterVM registerVM = new RegisterVM();
            return View(registerVM);
        }

        [HttpPost]
        public ActionResult Register(RegisterVM registerVM)
        {
            Authorization authorization = new Authorization();
            if (ModelState.IsValid)
            {
                Boolean check = authorization.AddUser(registerVM.User);
                if (check)
                    return RedirectToAction("Przepisy", "Home");
                else
                {
                    ModelState.AddModelError("CredentialError2", "Podana nazwa jest już zajęta!!");
                    return View("Register");
                }
            }
            else return View(registerVM);
        }

    }
}