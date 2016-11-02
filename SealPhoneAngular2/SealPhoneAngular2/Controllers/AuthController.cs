using SealPhoneAngular2.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SealPhoneAngular2.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Login([Bind(Include = "AccountName,Password")] UserAD user_input)
        public ActionResult Login(String AccountName, String Password)
        {
            try 
            {
                if (!(AccountName.ToLower().Equals("jmunoz")|| AccountName.ToLower().Equals("mjuarez") || AccountName.ToLower().Equals("sistemas07")))
                {
                    ViewBag.MessageError = "Usuario no tiene permisos";
                    return View();
                }

                UserAD userad = new Models.UserAD();

                string DomainPath = "LDAP://192.168.50.174";// DC=XXXXXX,DC=YYY";
                DirectoryEntry adSearchRoot = new DirectoryEntry(DomainPath, AccountName, Password);
                DirectorySearcher adSearcher = new DirectorySearcher(adSearchRoot);

                adSearcher.Filter = "(&(objectClass=user)(objectCategory=person))";
                adSearcher.PropertiesToLoad.Add("samaccountname");
                adSearcher.PropertiesToLoad.Add("mail");
                adSearcher.PropertiesToLoad.Add("displayname");
                adSearcher.PropertiesToLoad.Add("department");

                SearchResult result;
                SearchResultCollection iResult = adSearcher.FindAll();

                if (iResult != null)
                {
                    for (int counter = 0; counter < iResult.Count; counter++)
                    {
                        result = iResult[counter];
                        if (result.Properties.Contains("samaccountname"))
                        {
                            if (result.Properties.Contains("mail"))
                            {
                                String account = (String)result.Properties["samaccountname"][0];
                                if (account.ToLower() == AccountName.ToLower())
                                {
                                    userad.AccountName = (String)result.Properties["samaccountname"][0];
                                    userad.DisplayName = (String)result.Properties["displayname"][0];
                                    userad.Mail = (String)result.Properties["mail"][0];
                                    userad.Password = Password;
                                    userad.Area = (String)result.Properties["department"][0];
                                }
                            }
                        }
                    }
                }

                adSearcher.Dispose();
                adSearchRoot.Dispose();               

                Session["user"] = userad;
                return RedirectToAction("Index", "Cortes");
            }
            catch (Exception ex)
            {
                ViewBag.MessageError = "Usuario o Password erroneos";
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}