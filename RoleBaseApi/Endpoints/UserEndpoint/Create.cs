using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity;

namespace RoleBaseApi.Endpoints.UserEndpoint
{
    public class Create : BaseAsyncEndpoint.WithRequest<CreateRequeste>.WithoutResponse
    {
        private UserManager<ApplicationUser> _userManager { get; set; }
        public Create(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("api/users")]
        [SwaggerOperation(
            Summary = "add a user",
            Description = "add a user",
            OperationId = "CreateUser",
            Tags = new[] { "CreateUserEndpoints" })
        ]
        public override async Task<ActionResult> HandleAsync(CreateRequeste request, CancellationToken cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = request.Username,
                    Email = request.Email,
                    EmailConfirmed = request.IsEmailConfirmed,
                    PhoneNumber = request.PhoneNumber
                };
                var result = await _userManager.CreateAsync(user,request.Password);
                if (result.Succeeded)
                {
                    return Ok();
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("errors", error.Description);
                }
            }
            return BadRequest(ModelState);
        }
    }
}
