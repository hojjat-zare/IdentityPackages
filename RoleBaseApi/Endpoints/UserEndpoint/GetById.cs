using Ardalis.ApiEndpoints;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RoleBaseApi.Endpoints.UserEndpoint
{
    public class GetById : BaseAsyncEndpoint.WithRequest<string>.WithResponse<GetByIdResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetById(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("api/users/{userId}")]
        [SwaggerOperation(
           Summary = "Get a user",
           Description = "Get a user",
           OperationId = "GetById",
           Tags = new[] { "GetByIdEndpoints" })
       ]
        public async override Task<ActionResult<GetByIdResponse>> HandleAsync(string userId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(userId)) return BadRequest(new {errors = "userId can not be empty" });
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();
            var roles = await _userManager.GetRolesAsync(user);
            return new GetByIdResponse()
            {
                Username = user.UserName,
                Email = user.Email,
                IsEmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                Roles = roles.ToArray()
            };
        }
    }
}
