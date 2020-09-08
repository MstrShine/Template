namespace Template.Controllers.UserManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Mvc;

    using Template.BL.Authentication;
    using Template.BL.Management.User;
    using Template.Models.User;

    public class LoginController : Controller
    {
        private readonly IAuthentication _authentication;

        private readonly IUserManagement _userManagement;

        public LoginController(IAuthentication authentication, IUserManagement userManagement)
        {
            this._authentication = authentication;
            this._userManagement = userManagement;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("Index", model);
            }

            if (await this._authentication.LoginAsync(model.Username, model.Password).ConfigureAwait(false))
            {
                var userClaims = await this._userManagement.GetUserWithRolesAsync(model.Username).ConfigureAwait(false);

                var claims = new List<Claim>
                                 {
                                     new Claim(ClaimTypes.Name, model.Username)
                                 };

                claims.AddRange(userClaims.Select(userClaim => new Claim(ClaimTypes.Role, userClaim.Claim.ToString())));

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IssuedUtc = DateTimeOffset.UtcNow,
                    IsPersistent = model.StayLoggedIn
                };

                await this.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties)
                    .ConfigureAwait(false);

                return this.RedirectToAction("Index", "Home");
            }

            this.ModelState.AddModelError(string.Empty, "Login failed");

            return this.View("Index", model);
        }

        public IActionResult LogOut()
        {
            this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return this.RedirectToAction("Index");
        }
    }
}