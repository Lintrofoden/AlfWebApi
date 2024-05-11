using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiWeb.Models;

namespace WebApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: api/Roles
        [HttpGet]
        public ActionResult<IEnumerable<IdentityRole>> GetRoles()
        {
            return Ok(_roleManager.Roles);
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return Ok();
                }
                return BadRequest(result.Errors);
            }
            return BadRequest("Invalid role name");
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return Ok();
                }
                return BadRequest(result.Errors);
            }
            return NotFound();
        }

        // GET: api/Roles/Users
        [HttpGet("Users")]
        public ActionResult<IEnumerable<IdentityUser>> GetUsers()
        {
            return Ok(_userManager.Users);
        }

        // GET: api/Roles/Users/5
        [HttpGet("Users/{userId}")]
        public async Task<ActionResult<UserRoleModel>> GetUserRoles(string userId)
        {
            IdentityUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = new List<string>(await _userManager.GetRolesAsync(user));
                var allRoles = _roleManager.Roles.ToList();
                var model = new UserRoleModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return Ok(model);
            }
            return NotFound();
        }

        // PUT: api/Roles/Users/5
        [HttpPut("Users/{userId}")]
        public async Task<IActionResult> UpdateUserRoles(string userId, List<string> roles)
        {
            IdentityUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);
                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return Ok();
            }
            return NotFound();
        }
    }
}