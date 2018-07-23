using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NawazEyeWebProject_NEW_.ViewModels;
using NawazEyeWebProject_NEW_.Models;
using System.Collections.Generic;

namespace NawazEyeWebProject_NEW_.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            try
            {


                ViewBag.StatusMessage =
                    message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                    : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                    : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                    : message == ManageMessageId.Error ? "An error has occurred."
                    : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                    : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                    : "";

                var userId = User.Identity.GetUserId();
                Account a = new Account(User.Identity.GetUserId());
                var model = new IndexViewModel
                {
                    HasPassword = HasPassword(),
                    PhoneNumber = a.Buyer.PhoneNumber,
                    TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                    Logins = await UserManager.GetLoginsAsync(userId),
                    BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId),
                    Address = a.Buyer.Address,
                    OrdersCount = a.Buyer.GetOrders().Count
                };
                return View(model);
            }
            catch(Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Manage", "Index");
                return RedirectToAction("Index", "Error", new { model = error });
            }
        }
        public ActionResult ViewOrders()
        {
            try
            {
                Buyer b = new Account(User.Identity.GetUserId()).Buyer;
                List<Order> lstOrder = b.GetOrders();
                List<ViewOrdersViewModel> model = new List<ViewOrdersViewModel>();
                foreach (var item in lstOrder)
                {
                    model.Add(new ViewOrdersViewModel()
                    {
                        DispatchDate = item.DispatchDate.ToShortDateString(),
                        OrderDate = item.OrderDate.ToShortDateString(),
                        Status = item.Status,
                        TotalPrice = decimal.Round(item.TotalPrice).ToString(),
                        DeliveryCharges = decimal.Round(b.City.DeliverCharges).ToString(),
                        Id = item.Cart.CartId
                    });
                }
                return View(model);
            }
            catch (Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Manage", "ViewOrders");
                return RedirectToAction("Index", "Error", new { model = error });
            }
        }
        [HttpGet]
        public ActionResult ViewOderItems(int id)
        {
            try
            {
                Cart c = new Cart(id);
                List<ViewOrderItemsViewModel> model = new List<ViewOrderItemsViewModel>();
                foreach (var item in c.PrescriptionGlasses)
                {
                    model.Add(new ViewOrderItemsViewModel()
                    {
                        Image = item.PrescriptionGlasses.PrimaryImage,
                        Name = item.PrescriptionGlasses.Name,
                        Price = decimal.Round(item.PrescriptionGlasses.Price).ToString(),
                        Quantity = item.Quantity.ToString()
                    });
                }
                return View(model);
            }
            catch (Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Manage", "ViewOderItems");
                return RedirectToAction("Index", "Error", new { model = error });
            }
        }
        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Manage", "AddPhoneNumber");
                return RedirectToAction("Index", "Error", new { model = error });
            }

        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                Account a = new Account(User.Identity.GetUserId());
                a.Buyer.PhoneNumber = model.Number;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Manage", "AddPhoneNumber");
                return RedirectToAction("Index", "Error", new { model = error });
            }

        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Manage", "ChangePassword");
                return RedirectToAction("Index", "Error", new { model = error });
            }
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                AddErrors(result);
                return View(model);
            }
            catch (Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Manage", "ChangePassword");
                return RedirectToAction("Index", "Error", new { model = error });
            }
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Manage", "SetPassword");
                return RedirectToAction("Index", "Error", new { model = error });
            }
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        if (user != null)
                        {
                            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        }
                        return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    AddErrors(result);
                }

                // If we got this far, something failed, redisplay form
                return View(model);
            }
            catch (Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Manage", "SetPassword");
                return RedirectToAction("Index", "Error", new { model = error });
            }
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            try
            {
                ViewBag.StatusMessage =
                    message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                    : message == ManageMessageId.Error ? "An error has occurred."
                    : "";
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user == null)
                {
                    return View("Error");
                }
                var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
                var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
                ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
                return View(new ManageLoginsViewModel
                {
                    CurrentLogins = userLogins,
                    OtherLogins = otherLogins
                });
            }
            catch (Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Manage", "ManageLogins");
                return RedirectToAction("Index", "Error", new { model = error });
            }
        }
        public ActionResult ChangeAddress()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Manage", "ChangeAddress");
                return RedirectToAction("Index", "Error", new { model = error });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeAddress(ChangeAddressViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                Account a = new Account(User.Identity.GetUserId());
                a.Buyer.Address = model.NewAddress;
                a.Buyer.City = new City(model.CityId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Manage", "ChangeAddress");
                return RedirectToAction("Index", "Error", new { model = error });
            }
        }
        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            try
            {
                // Request a redirect to the external login provider to link a login for the current user
                return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
            }
            catch (Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Manage", "LinkLogin");
                return RedirectToAction("Index", "Error", new { model = error });
            }
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            try
            {
                var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
                if (loginInfo == null)
                {
                    return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
                }
                var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
                return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            catch (Exception ex)
            {
                HandleErrorInfo error = new HandleErrorInfo(ex, "Manage", "LinkLoginCallback");
                return RedirectToAction("Index", "Error", new { model = error });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}