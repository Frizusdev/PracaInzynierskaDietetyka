// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using PracaInzynierskaDietetyka.Data;
using PracaInzynierskaDietetyka.DTO.UserDataDTOS;
using PracaInzynierskaDietetyka.Services.UserDataServices;

namespace PracaInzynierskaDietetyka.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<XUser> _signInManager;
        private readonly UserManager<XUser> _userManager;
        private readonly RoleManager<XRole> _roleManager;
        private readonly IUserStore<XUser> _userStore;
        private readonly IUserEmailStore<XUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IUserDataService _user;

        public RegisterModel(
            UserManager<XUser> userManager,
            RoleManager<XRole> roleManager,
            IUserStore<XUser> userStore,
            SignInManager<XUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IUserDataService user)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _user = user;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "What is your wish")]
            [StringLength(20, ErrorMessage = "Wish cannot be longer than 20 letters")]
            public string Wish_Weight { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "What is your activity")]
            [StringLength(20, ErrorMessage = "Wish cannot be longer than 20 letters")]
            public string Activity { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "First Name")]
            [StringLength(20, ErrorMessage = "First Name cannot be longer than 20 letters")]
            public string First_Name { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Last Name")]
            [StringLength(20, ErrorMessage = "Last Name cannot be longer than 20 letters")]
            public string Last_Name { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "More About U")]
            [StringLength(255, ErrorMessage = "More about u cannot be longer than 255 letters")]
            public string Extras { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Your Weight")]
            [StringLength(3, ErrorMessage = "Weight cannot be longer than 3 letters")]
            public string Weight { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Your Height")]
            [StringLength(3, ErrorMessage = "Height cannot be longer than 3 letters")]
            public string Height { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Sex")]
            [StringLength(6, ErrorMessage = "Sex cannot be longer than 6 letters")]
            public string Sex { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Age")]
            [StringLength(3, ErrorMessage = "Age cannot be longer than 3 letters")]
            public string Age { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");


                    var defaultrole = _roleManager.FindByNameAsync("Member").Result;

                    if (defaultrole != null)
                    {
                        IdentityResult roleresult = await _userManager.AddToRoleAsync(user, defaultrole.Name);
                    }

                    // Liczenie zapotrzebowania

                    var UserMacro = new UserDataDTO();

                    var weight = Convert.ToInt32(Input.Weight);
                    var height = Convert.ToInt32(Input.Height);
                    var age = Convert.ToInt32(Input.Age);


                    UserMacro.GUID = user.Id;
                    UserMacro.Email = user.Email;
                    UserMacro.Extras = Input.Extras;
                    UserMacro.First_Name = Input.First_Name;
                    UserMacro.Last_Name = Input.Last_Name;
                    UserMacro.Dietetyk_ID = null;
                    UserMacro.Wish_Weight = Input.Wish_Weight;
                    UserMacro.Weight = weight;
                    UserMacro.Height = height;
                    UserMacro.Sex = Input.Sex;
                    UserMacro.Age = age;

                    double kcal, protein, fat, carbon;
                    switch (Input.Wish_Weight)
                    {
                        case "Lose on Weight":
                            if (Input.Sex == "Men")
                            {
                                kcal = ((66.5 + (13.7 * weight) + (5 * height) - (6.8 * age)) * 0.8) * double.Parse(Input.Activity, System.Globalization.CultureInfo.InvariantCulture);
                                protein = 2 * weight;
                                fat = 1.2 * weight;
                                carbon = kcal - (protein * 4 + fat * 9);

                                UserMacro.Kcal = Math.Round(kcal);
                                UserMacro.Protein = Math.Round(protein);
                                UserMacro.Fat = Math.Round(fat);
                                UserMacro.Carbon = Math.Round(carbon / 4);
                            }
                            else
                            {
                                kcal = ((655 + (9.6 * weight) + (1.85 * height) - (4.7 * age)) * 0.8) * double.Parse(Input.Activity, System.Globalization.CultureInfo.InvariantCulture);
                                protein = 2 * weight;
                                fat = 1.2 * weight;
                                carbon = kcal - (protein * 4 + fat * 9);

                                UserMacro.Kcal = Math.Round(kcal);
                                UserMacro.Protein = Math.Round(protein);
                                UserMacro.Fat = Math.Round(fat);
                                UserMacro.Carbon = Math.Round(carbon / 4);
                            }

                            break;

                        case "Stay on Weight":
                            if (Input.Sex == "Men")
                            {
                                kcal = (66.5 + (13.7 * weight) + (5 * height) - (6.8 * age)) * double.Parse(Input.Activity, System.Globalization.CultureInfo.InvariantCulture);
                                protein = 2 * weight;
                                fat = 1.2 * weight;
                                carbon = kcal - (protein * 4 + fat * 9);

                                UserMacro.Kcal = Math.Round(kcal);
                                UserMacro.Protein = Math.Round(protein);
                                UserMacro.Fat = Math.Round(fat);
                                UserMacro.Carbon = Math.Round(carbon / 4);
                            }
                            else
                            {
                                kcal = (655 + (9.6 * weight) + (1.85 * height) - (4.7 * age)) * double.Parse(Input.Activity, System.Globalization.CultureInfo.InvariantCulture);
                                protein = 2 * weight;
                                fat = 1.2 * weight;
                                carbon = kcal - (protein * 4 + fat * 9);

                                UserMacro.Kcal = Math.Round(kcal);
                                UserMacro.Protein = Math.Round(protein);
                                UserMacro.Fat = Math.Round(fat);
                                UserMacro.Carbon = Math.Round(carbon / 4);
                            }

                            break;

                        case "Gain on Weight":
                            if (Input.Sex == "Men")
                            {
                                kcal = ((66.5 + (13.7 * weight) + (5 * height) - (6.8 * age)) * 1.2) * double.Parse(Input.Activity, System.Globalization.CultureInfo.InvariantCulture);
                                protein = 2 * weight;
                                fat = 1.2 * weight;
                                carbon = kcal - (protein * 4 + fat * 9);

                                UserMacro.Kcal = Math.Round(kcal);
                                UserMacro.Protein = Math.Round(protein);
                                UserMacro.Fat = Math.Round(fat);
                                UserMacro.Carbon = Math.Round(carbon / 4);
                            }
                            else
                            {
                                kcal = ((655 + (9.6 * weight) + (1.85 * height) - (4.7 * age)) * 1.2) * double.Parse(Input.Activity, System.Globalization.CultureInfo.InvariantCulture);
                                protein = 2 * weight;
                                fat = 1.2 * weight;
                                carbon = kcal - (protein * 4 + fat * 9);

                                UserMacro.Kcal = Math.Round(kcal);
                                UserMacro.Protein = Math.Round(protein);
                                UserMacro.Fat = Math.Round(fat);
                                UserMacro.Carbon = Math.Round(carbon / 4);
                            }

                            break;

                        default:
                            break;
                    }

                    _user.AddNewPerson(UserMacro);


                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private XUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<XUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(XUser)}'. " +
                    $"Ensure that '{nameof(XUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<XUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<XUser>)_userStore;
        }
    }
}
