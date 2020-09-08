namespace Template.Controllers.UserManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Template.BL.Management.User;
    using Template.DAL.Models;
    using Template.Models.User;

    [Authorize]
    public class UserManagementController : Controller
    {
        private readonly IUserManagement _userManagement;


        public UserManagementController(IUserManagement userManagement)
        {
            this._userManagement = userManagement;
        }

        public async Task<IActionResult> Index()
        {
            return this.View(await this.GetUsersForView().ConfigureAwait(false));
        }

        public async Task<IActionResult> EditUser(Guid id)
        {
            var user = await this._userManagement.GetByIdAsync(id).ConfigureAwait(false);

            var viewModel = new UserManagementViewModel(user.Id, user.Firstname, user.MiddleName, user.Lastname, user.Email);

            return this.View(viewModel);
        }

        public async Task<IActionResult> Edit(Guid id, UserManagementViewModel viewModel)
        {
            var user = await this._userManagement.GetByIdAsync(id).ConfigureAwait(false);
            UpdateUserWithViewmodel(user, viewModel);

            await this._userManagement.UpdateAsync(user).ConfigureAwait(false);

            TempData["EditUserSuccess"] = "Successful edited user!";

            return this.View("EditUser", viewModel);
        }

        private static void UpdateUserWithViewmodel(User user, UserManagementViewModel viewModel)
        {
            user.Email = viewModel.Email;
            user.Firstname = viewModel.Firstname;
            user.MiddleName = viewModel.MiddleName;
            user.Lastname = viewModel.Lastname;
        }

        private async Task<IEnumerable<UserManagementViewModel>> GetUsersForView()
        {
            var users = await this._userManagement.GetAllAsync().ConfigureAwait(false);

            return users.Select(user => new UserManagementViewModel(user.Id, user.Firstname, user.MiddleName, user.Lastname, user.Email));
        }
    }
}