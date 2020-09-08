namespace Template.Controllers.UserManagement
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Template.BL.Management.User;
    using Template.Models.User;

    public class RegisterController : Controller
    {
        private readonly IUserManagement _userManagement;

        public RegisterController(IUserManagement userManagement)
        {
            this._userManagement = userManagement;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("Index", model);
            }

            try
            {
                await this._userManagement.CreateUserAsync(
                    model.Email,
                    model.Firstname,
                    model.MiddleName,
                    model.Lastname,
                    model.Password).ConfigureAwait(false);
            }
            catch
            {
                this.ModelState.AddModelError(string.Empty, "Could not create user");
                return this.View("Index", model);
            }

            return this.RedirectToAction("Index", "Login");
        }
    }
}