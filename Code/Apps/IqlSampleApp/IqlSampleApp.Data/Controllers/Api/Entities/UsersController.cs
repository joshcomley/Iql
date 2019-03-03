using System.Linq;
using System.Threading.Tasks;
using IqlSampleApp.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IqlSampleApp.Data.Controllers.Api.Entities
{
    public class UsersController : IqlSampleAppController<ApplicationUser>
    {
        #region Imports
        public async Task<IActionResult> Me()
        {
            return Ok();
        }

        public async Task<IActionResult> GeneratePasswordResetLink(string id)
        {
            return Ok();
        }

        public async Task<IActionResult> SendPasswordResetEmail(string id)
        {
            return Ok();
        }

        public async Task<IActionResult> AccountConfirm(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            var code = await UserManager.GenerateEmailConfirmationTokenAsync(user);
            await UserManager.ConfirmEmailAsync(user, code);
            return Ok();
        }

        public async Task<IActionResult> ReinstateUser(string id)
        {
            return Ok();
        }

        public IActionResult ForClient(int id, int? type)
        {
            var users = Crud.Secured.Context.Users.Where(u => u.ClientId == id);
            if (type.HasValue)
            {
                users = users.Where(u => (int)u.UserType == type);
            }
            return Ok(users);
        }

        #endregion Imports
    }
}
