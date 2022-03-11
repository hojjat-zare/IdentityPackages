using Ardalis.ApiEndpoints;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace RoleBaseApi.Endpoints.UserEndpoint
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Delete : BaseAsyncEndpoint.WithRequest<DeleteRequest>.WithoutResponse
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public Delete(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }

        [HttpDelete("api/users")]
        [SwaggerOperation(
            Summary = "Delete a user",
            Description = "Delete a user",
            OperationId = "Delete",
            Tags = new[] { "DeleteEndpoints" })
        ]
        public async override Task<ActionResult> HandleAsync(DeleteRequest request, CancellationToken cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(request.Username);
                if (user == null) return NotFound();
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded) return Ok();
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("errors",error.Description);
                }
            }      
            return BadRequest(ModelState);

        }
    }
}
