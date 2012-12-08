using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using db_proj4.Models;
using db_proj4.Models.entities;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace db_proj4.Controllers
{
    public class AccountController : Controller
    {
        AccountsRepository accRepo;
        bool credsintable = false; //used to check table's credentials
        public int currentuserid = 0;
        public AccountController()
        {
            accRepo = new AccountsRepository();
        }

        public int getuserid()
        {
            return currentuserid;
        }

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {

            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            var listofaccts = accRepo.BuildList();
            foreach (var thisguy in listofaccts) //checking user table for user+pass
                if (thisguy.Username == model.Username)
                {
                    if (thisguy.Pass == model.Password)
                        credsintable = true; //name = name in table & that pass =the pass given
                }


            if (ModelState.IsValid)
            {
                if (/*Membership.ValidateUser(model.Username, model.Password) &&*/ credsintable)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                       
                        return RedirectToAction("Index", "Jobs");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.Username, model.Type, model.Password, null, null, true, null, out createStatus);
                var listofacct = accRepo.BuildList();
                foreach (var thisguy in listofacct) //checking user table for username
                    if (thisguy.Username == model.Username)
                    {
                        credsintable = true;//name in Jobloader DB
                    }
                if (/*createStatus == MembershipCreateStatus.Success*/!credsintable)
                {
                    //We check our DB for name, -> we don't need cookie
                    //FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    Database db = DatabaseFactory.CreateDatabase();
                    //User is added to Users database below.
                    DbCommand command = db.GetStoredProcCommand("Users_InsertUser");
                    //User ID automatically generated
                    db.AddInParameter(command, "@Username", System.Data.DbType.String, model.Username);
                    db.AddInParameter(command, "@Type", System.Data.DbType.String, model.Type);
                    db.AddInParameter(command, "@Pass", System.Data.DbType.String, model.Password);

                    db.ExecuteScalar(command);
                    var listofacct1 = accRepo.BuildList();
                    foreach (var thisguy in listofacct1)
                    { //checking user table for username
                        if (thisguy.Username == model.Username && model.Type == "Applicant")
                        {
                            // string name = User.Identity.Name;
                            var modelUser = accRepo.FindUserid(model.Username);
                            return RedirectToAction("Create", "Applicants", new { Id = modelUser.Userid });
                        }
                        else if (thisguy.Username == model.Username && model.Type == "Recruiter")
                        {
                            //  string name = User.Identity.Name;
                            var modelUser = accRepo.FindUserid(model.Username);
                            return RedirectToAction("Create", "Recruiters", new { Id = modelUser.Userid });

                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
