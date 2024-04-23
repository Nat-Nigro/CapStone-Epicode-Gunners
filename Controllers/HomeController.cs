using HomeTeamWebSite.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Mvc;
using System.Web.Security;

namespace HomeTeamWebSite.Controllers
{

    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Utenti utente)
        {
            using (var context = new ArsenalDbContext())
            {
                var existingUser = context.Utenti.FirstOrDefault(u => u.Username == utente.Username);
                if (existingUser != null)
                {
                    TempData["Registrazione Fallita"] = "Username già in uso";
                    return RedirectToAction("Index", "Home");
                }

                if (string.IsNullOrEmpty(utente.Username) || string.IsNullOrEmpty(utente.Psw) ||
                    string.IsNullOrEmpty(utente.Nome) || string.IsNullOrEmpty(utente.Cognome) || string.IsNullOrEmpty(utente.Mail))
                {
                    ModelState.AddModelError("", "");
                    return View("Index", utente);
                }

                utente.Psw = HashPassword(utente.Psw);

                context.Utenti.Add(utente);
                context.SaveChanges();

            }
            FormsAuthentication.SetAuthCookie(utente.Username, false);
            TempData["Registrazione Riuscita"] = "Welcome aboard! Your signup was successful. We're thrilled to have you with us.";
            return RedirectToAction("Home", "Home");
        }

        public string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }

        public bool VerifyPassword(string enteredPassword, string savedPasswordHash)
        {
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);

            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }


        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string psw)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(psw))
            {
                TempData["LoginFallito"] = "It looks like some required fields are missing. Please fill them out and try submitting again.";
                return RedirectToAction("Index", "Home");
            }

            using (var context = new ArsenalDbContext())
            {
                var existingUser = context.Utenti.FirstOrDefault(u => u.Username == username);
                if (existingUser != null && existingUser.Psw != null && VerifyPassword(psw, existingUser.Psw))
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    TempData["LoginRiuscito"] = "Login successful. Welcome back!";
                    return RedirectToAction("Home", "Home");
                }
                else
                {
                    TempData["LoginFallito"] = "Oops! The username or password you entered doesn't match our records. Please double-check and try again.";
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            TempData["Logout"] = "You've been successfully logged out. See you next time!";
            Session.Clear();
            return RedirectToAction("Index");
        }


        public ActionResult Home()
        {
            return View();
        }
    }
}
