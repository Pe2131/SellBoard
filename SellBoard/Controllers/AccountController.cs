using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Model;
using DAL.Model.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SellBoard.ViewModel;

namespace SellBoard.Controllers
{
    public class AccountController : Controller
    {
        UnitOfWork db = new UnitOfWork();
        private readonly ApplicationDbContext DbInjection;
        private readonly UserManager<Tbl_User> userManager;
        private readonly SignInManager<Tbl_User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AccountController(UserManager<Tbl_User> _userManager, SignInManager<Tbl_User> _signInManager, RoleManager<IdentityRole> _roleManager, ApplicationDbContext database)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
            DbInjection = database;
        }
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(ViewModel_Login model, string returnUrl = null)
        {
            try
            {
                ViewData["ReturnUrl"] = returnUrl;
                if (ModelState.IsValid)
                {
                    var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                    var user = signInManager.UserManager.Users.Where(a => a.Email == model.Email).FirstOrDefault();
                    if (result.Succeeded)
                    {

                        var role = await userManager.GetRolesAsync(user);
                        if (role.Contains("Admin"))
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        if (role.Contains("Agent"))
                        {
                            return RedirectToAction("Index", "Reservation");
                        }
                        return RedirectToLocal(returnUrl);
                    }
                    if (result.IsLockedOut)
                    {
                        if (returnUrl != null)
                        {
                            RedirectToLocal(returnUrl);
                        }
                        ModelState.AddModelError(string.Empty, "Your Account is locked");
                        return View();
                    }
                    else
                    {
                        if (returnUrl != null)
                        {
                            RedirectToLocal(returnUrl);
                        }
                        ModelState.AddModelError(string.Empty, "username or password is invalid");
                        return View();
                    }
                }
                else
                {
                    if (returnUrl != null)
                    {
                        RedirectToLocal(returnUrl);
                    }
                    ModelState.AddModelError(string.Empty, ModelState.GetErrors());
                    return View();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ViewModel_Register model, string returnUrl = null)
        {
            try
            {
                ViewData["ReturnUrl"] = returnUrl;
                if (ModelState.IsValid)
                {
                    var user = new Tbl_User { Email = model.Email, UserName = model.Email };
                    var result = await userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                       // await CreateRole("Admin");   // for checking user role is Exict or not
                        var result1 = await userManager.AddToRoleAsync(user, "Admin");
                        var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                        //await emailSender.SendEmailAsync(model.Email, "Confirm Account",
                        //   $"Pleas Enter the link: <a href='{callbackUrl}'>Link</a>");
                        TempData["Message"] = "Registration was Successfull";
                        return View("Login", model);
                    }

                    AddErrors(result);
                }
                return View("Login", model);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(string Email)
        {

            try
            {
                var user = await userManager.FindByNameAsync(Email);
                if (user == null || !(await userManager.IsEmailConfirmedAsync(user)))
                {
                    ModelState.AddModelError(string.Empty, "this account isnt exist" + Environment.NewLine + "or your account isnt enable");
                    return View();
                }

                var code = await userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account",
                new { UserId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                //await emailSender.SendEmailAsync(Email, "Password Recovery",
                //        "please enter the link: <a href=\"" + callbackUrl + "\">link</a>");
                TempData["Message"] = "Recovery Link was sent";
                return View();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult ResetPassword(ViewModel_ChangePass model)
        {
            ViewBag.userid = model.UserId;
            ViewBag.code = model.code;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string password, ViewModel_ChangePass model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    var token = model.code;
                    var result = await userManager.ResetPasswordAsync(user, token, password);
                    if (result.Succeeded)
                    {
                        TempData["Message"] = "password is changed";
                        return View("Login");
                    }
                    else
                    {
                        AddErrors(result);
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Username is invalid!");
                    return View();
                }
            }
            else
            {

                ModelState.AddModelError(string.Empty, ModelState.GetErrors());
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> LogOff()
        {
            if (User.Identity.IsAuthenticated)
            {
                await signInManager.SignOutAsync();
            }
            // return RedirectToAction(nameof(Login));
            return RedirectToAction("index", "Home");
        }
        public async Task<IActionResult> getUser() //get all User with their Roles
        {
            var userRoles = new List<ViewModel_User>();
            var q = db.UserRepository.Get();
            foreach (var user in q)
            {
                var r = new ViewModel_User
                {
                    FullName = user.FullName,

                    Mobile = user.Mobile


                };
                userRoles.Add(r);
                //Get all the Roles for our users
                foreach (var user1 in userRoles)
                {
                    user1.RoleNames = await userManager.GetRolesAsync(db.UserRepository.Get(a => a.Id.Equals(user1.Id)).SingleOrDefault());
                }
            }

            var model = userRoles.Where(a => a.RoleNames.ToList().Contains("User")).ToList();
            return View(model);
        }
        public async Task<IActionResult> GetUserDetail(string id)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id);
                ViewModel_User model = new ViewModel_User
                {
                    FullName=user.FullName,
                    Mobile = user.Mobile,
                    Id = user.Id,
                    RoleNames = await userManager.GetRolesAsync(user)

                };
                return PartialView("P_GetUserDetail", model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IActionResult> GetUserRoles(string id)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id);
                ViewModel_User model = new ViewModel_User
                {
                    FullName = user.FullName,

                    Mobile = user.Mobile
                };
                string roles = "";
                foreach (var item in model.RoleNames)
                {
                    roles += item + ",";
                }
                ViewBag.ExictRoles = roleManager.Roles.ToList();
                ViewBag.UsersRoles = roles;
                return PartialView("P_GetUserRoles", model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<JsonResult> addOrRemoveRoles(string id, string roles)
        {
            try
            {
                if (roles != null)
                {
                    var user = await userManager.FindByIdAsync(id);
                    var currentRoles = await userManager.GetRolesAsync(user);
                    await userManager.RemoveFromRolesAsync(user, currentRoles);
                    List<string> ListAddroles = roles.StringToList();
                    await userManager.AddToRolesAsync(user, ListAddroles);
                    await db.UserRepository.Save();
                    return Json(1);
                }
                else
                {
                    return Json(0);
                }

            }
            catch (Exception)
            {

                return Json(0);
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id);
                ViewModel_User model = new ViewModel_User();
                model.Mobile = user.Mobile;
                model.FullName = user.FullName;
                return View(model);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        [HttpPost]
        public async Task<IActionResult> EditUser(ViewModel_User model, string id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByIdAsync(id);
                    user.Mobile = model.Mobile;
                    user.FullName = model.FullName;
                    var result = await userManager.UpdateAsync(user);
                    if (model.Password != null)
                    {

                        // Generate the reset token (this would generally be sent out as a query parameter as part of a 'reset' link in an email)
                        string resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
                        // Use the reset token to verify the provenance of the reset request and reset the password.
                        IdentityResult updatePassResult = await userManager.ResetPasswordAsync(user, resetToken, model.Password);
                        if (result.Succeeded && updatePassResult.Succeeded)
                        {
                            var result1 = await userManager.AddToRoleAsync(user, "Agent");
                            TempData["Style"] = "alert alert-success text-center";
                            TempData["Message"] = "succefully saved";
                            return View(model);
                        }
                        else
                        {
                            TempData["Style"] = "alert alert-warning text-center";
                            TempData["Message"] = "there is problem to save!";
                            return View(model);

                        }
                    }
                    else
                    {


                        if (result.Succeeded)
                        {
                            var result1 = await userManager.AddToRoleAsync(user, "Agent");
                            TempData["Style"] = "alert alert-success text-center";
                            TempData["Message"] = "succefully saved";
                            return View(model);
                        }
                        else
                        {
                            TempData["Style"] = "alert alert-warning text-center";
                            TempData["Message"] = "there is problem to save!";
                            return View(model);

                        }
                    }

                }
                else
                {
                    TempData["Style"] = "alert alert-warning text-center";
                    TempData["Message"] = ModelState.GetErrors();
                    return View(model);

                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(ViewModel_User model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (await userManager.FindByEmailAsync(model.Mobile) == null)
                    {
                        var user = new Tbl_User();
                        user.FullName = model.FullName;
                        user.Mobile = model.Mobile;
                        var result = await userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            var result1 = await userManager.AddToRoleAsync(user, "User");
                            TempData["Style"] = "alert alert-success text-center";
                            TempData["Message"] = "succefully saved";
                            return View(model);
                        }
                        else
                        {
                            TempData["Style"] = "alert alert-warning text-center";
                            TempData["Message"] = "there is problem to save!";
                            return View(model);

                        }
                    }
                    else
                    {
                        TempData["Style"] = "alert alert-warning text-center";
                        TempData["Message"] = "this User Email Exist in Database";
                        return View(model);
                    }
                }
                else
                {
                    TempData["Style"] = "alert alert-warning text-center";
                    TempData["Message"] = ModelState.GetErrors();
                    return View(model);

                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public async Task CreateRole(string Role)
        {
            try
            {
                bool isRoleExict = await roleManager.RoleExistsAsync(Role);
                if (!isRoleExict)
                {
                    var role = new IdentityRole();
                    role.Name = Role;
                    var result = await roleManager.CreateAsync(role);
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public void AddErrors(IdentityResult result) // for add identity Error to Model State
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}