using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Modular.Core;
using Modular.Core.Data;
using Modular.Modules.Core.Models;
using Modular.Modules.Core.Services;
using Modular.Modules.Core.ViewModels.Account.Manage;

namespace Modular.Modules.Core.Controllers
{
    //[Area("Core")]
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ManageController : Controller
    {
        private IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        //  private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly SettingDefinitionProvider _settingDefinitionProvider;
        private readonly ISettingService _settingService;
        private readonly IRepository<UserDocuments> _userDocuments;

        public ManageController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IEmailSender emailSender,
        //  ISmsSender smsSender,
        ILoggerFactory loggerFactory
        , SettingDefinitionProvider settingDefinitionProvider,
        ISettingService settingService,
        IRepository<UserDocuments> userDocuments,
        IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            // _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<ManageController>();
            _settingDefinitionProvider = settingDefinitionProvider;
            _settingService = settingService;
            _userDocuments = userDocuments;
            _configuration = configuration;
        }

        [HttpGet("user")]
        public async Task<IActionResult> Index(ManageMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var user = await GetCurrentUserAsync();
            var model = new IndexViewModel
            {
                HasPassword = await _userManager.HasPasswordAsync(user),
                PhoneNumber = await _userManager.GetPhoneNumberAsync(user),
                TwoFactor = await _userManager.GetTwoFactorEnabledAsync(user),
                Logins = await _userManager.GetLoginsAsync(user),
                BrowserRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user),
                FullName = user.FullName,
                Email = user.Email
            };
            return View(model);
        }

        [HttpGet("user/info")]
        public async Task<IActionResult> UserInfo()
        {
            var user = await GetCurrentUserAsync();
            var model = new UserInfoVm { Email = user.Email, FullName = user.FullName };
            return View(model);
        }

        [HttpPost("user/info")]
        public async Task<IActionResult> UserInfo(UserInfoVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await GetCurrentUserAsync();
            user.FullName = model.FullName;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("UserInfo");
        }

        [HttpGet("user/documents")]
        public async Task<IActionResult> Documents()
        {
            var user = await GetCurrentUserAsync();

            IEnumerable<UserDocuments> m = _userDocuments.Query().Where(x => x.UserId == user.Id);

            return View(m);
        }

        [HttpPost("user/add_documents")]
        public async Task<IActionResult> Add_Documents(string type, IFormCollection data)
        {
            var user = await GetCurrentUserAsync();

            if (data.Files != null)
            {
                if (data.Files.Count > 0)
                {
                    string imagen = string.Empty;

                    foreach (var formFile in data.Files)
                    {
                        if (formFile.FileName.ToLower().Contains(".png") || formFile.FileName.ToLower().Contains(".jpg"))
                        {
                            if (formFile != null && formFile.Length > 0)
                            {
                                string fileNameApplication = Path.GetFileName(formFile.FileName);
                                string fileExtensionApplication = Path.GetExtension(fileNameApplication);

                                // generating a random guid for a new file at server for the uploaded file
                                string newFile = Guid.NewGuid().ToString() + fileExtensionApplication;
                                // getting a valid server path to save

                                var path = Path.Combine(
                          Directory.GetCurrentDirectory(), $"wwwroot/Imagenes/User/{user.Id}", newFile);
                                //var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                                try
                                {
                                    using (var stream = new FileStream(path, FileMode.Create))
                                    {
                                        await formFile.CopyToAsync(stream);

                                        imagen = formFile.FileName;

                                    }
                                }
                                catch (Exception ex)
                                {
                                    imagen = DateTime.Now.ToString("ddMMyyyyhhmmss") + fileExtensionApplication;

                                    var path2 = Path.Combine(
                                      Directory.GetCurrentDirectory(), $"wwwroot/Imagenes/User/{user.Id}", imagen);

                                    using (var stream = new FileStream(path2, FileMode.CreateNew))
                                    {
                                        await formFile.CopyToAsync(stream);

                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    return Redirect("/user/documents?formato=0");
                }
            }
            return Redirect("/user/documents");
        }

        [HttpPost("user/confirm_documents")]
        public async Task<IActionResult> Confirm_Documents()
        {
            //TblAdmUsuarios _u = data.TblAdmUsuarios.Where(x => x.UserId == WebSecurity.CurrentUserId).Single<TblAdmUsuarios>();
            //_u.DocumentoConfirmado = true;
            //data.SubmitChanges();

            //StringBuilder sp = new StringBuilder();
            //sp.Append("<strong> Envío de Confirmación de Cliente " + DateTime.Now + "</strong><br/><br/>");
            //sp.Append("<strong>Cliente:</strong>" + _u.UserNombre + " " + _u.UserApellido + "<br/>");
            //sp.Append("<strong>Email:</strong>" + _u.UserEmail + "<br/>");
            //sp.Append("<strong>Ir a Confirmar</strong> <a href='" + Configuracion.getDatosConfiguracion().Url + "/Admin-web/Clientes/ClientesDatos/" + _u.UserId + "'> Ir a ficha </a><br/>");
            ////sp.Append("<strong> " + px.ProductosNombre + ": </strong> <a href='" + Configuracion.getDatosConfiguracion().Url + "/admin-web/Productos/Productos/Edit/" + px.ProductosId + "'>[Editar]</a><br/>");

            //EmailModels EnviarEmail = new EmailModels()
            //{
            //    From = Configuracion.getDatosConfiguracion().Correo_smtp,
            //    Subject = "Confirmación de Cliente",
            //    To = Configuracion.getDatosConfiguracion().Correo_principal,
            //    Body = sp.ToString()
            //};
            //EnviarEmail.SendEmail();


            return Redirect("/Clientes/MisDocumentos?enviado=1");
        }

        [HttpGet("user/settings")]
        public async Task<IActionResult> UserSettings()
        {
            var model = new UserSettingViewModel
            {
                SettingDefinitions = _settingDefinitionProvider.SettingDefinitions,
                UserSettings = await _settingService.GetAllSettingsAsync()
            };

            return View(model);
        }

        [HttpPost("user/settings")]
        public async Task<IActionResult> UserSettings(UserSettingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await GetCurrentUserAsync();
            foreach (var item in model.UserSettings)
            {
                _settingService.SetCustomSettingValueForUser(user, item.Key, item.Value);
            }

            await _userManager.UpdateAsync(user);

            return RedirectToAction("UserSettings");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLogin(RemoveLoginViewModel account)
        {
            ManageMessageId? message = ManageMessageId.Error;
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.RemoveLoginAsync(user, account.LoginProvider, account.ProviderKey);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    message = ManageMessageId.RemoveLoginSuccess;
                }
            }
            return RedirectToAction(nameof(ManageLogins), new { Message = message });
        }

        [HttpGet("user/change-password")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost("user/change-password")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User changed their password successfully.");
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                AddErrors(result);
                return View(model);
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }

        [HttpGet("user/set-password")]
        public IActionResult SetPassword()
        {
            return View();
        }

        [HttpPost("user/set-password")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.AddPasswordAsync(user, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
                return View(model);
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }

        [HttpGet("user/manage-logins")]
        public async Task<IActionResult> ManageLogins(ManageMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.AddLoginSuccess ? "The external login was added."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await _userManager.GetLoginsAsync(user);
            var schemes = await _signInManager.GetExternalAuthenticationSchemesAsync();
            var otherLogins = schemes.Where(auth => userLogins.All(ul => auth.Name != ul.LoginProvider)).ToList();
            ViewData["ShowRemoveButton"] = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = Url.Action("LinkLoginCallback", "Manage");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _userManager.GetUserId(User));
            return Challenge(properties, provider);
        }

        [HttpGet]
        public async Task<ActionResult> LinkLoginCallback()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            var info = await _signInManager.GetExternalLoginInfoAsync(await _userManager.GetUserIdAsync(user));
            if (info == null)
            {
                return RedirectToAction(nameof(ManageLogins), new { Message = ManageMessageId.Error });
            }
            var result = await _userManager.AddLoginAsync(user, info);
            var message = result.Succeeded ? ManageMessageId.AddLoginSuccess : ManageMessageId.Error;
            return RedirectToAction(nameof(ManageLogins), new { Message = message });
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            AddLoginSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        private Task<User> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
