namespace Template.Controllers.UserManagement
{
    using System.Linq;
    using System.Security.Claims;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Template.BL.Authentication;
    using Template.DAL;
    using Template.Models.User;

    [Authorize]
    public class ChangePasswordController : Controller
    {
        private readonly DataContext _context;

        private readonly IAuthentication _auth;

        public ChangePasswordController(DataContext dataContext, IAuthentication auth)
        {
            this._context = dataContext;
            this._auth = auth;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChangePassword(ChangePasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View("Index");
            }

            var username = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Name).Value;

            var user = this._context.Users.First(x => x.Email == username);

            if (this._auth.ValidatePassword(user.Password, viewModel.OldPassword))
            {
                var newPassword = this._auth.CreatePasswordHash(viewModel.NewPassword);

                user.Password = newPassword;

                this._context.Users.Update(user);
                this._context.SaveChanges();

                return this.RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Password did not match the saved one.");

                return this.View("Index");
            }
        }
    }
}