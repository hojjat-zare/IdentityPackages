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

namespace RoleBaseApi.Endpoints.RegisterEndpoints
{
    public class ConfirmEmail : BaseAsyncEndpoint
        .WithRequest<ConfirmEmailRequest>
        .WithResponse<ConfirmEmailResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ConfirmEmail(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("api/confirm-email")]
        [SwaggerOperation(
            Summary = "Confirm Email",
            Description = "Confirm Email",
            OperationId = "ConfirmEmail",
            Tags = new[] { "ConfirmEmailEndpoints" })
        ]
        public async override Task<ActionResult<ConfirmEmailResponse>> HandleAsync([FromQuery]ConfirmEmailRequest request, CancellationToken cancellationToken = default)
        {
            if (request.userId == null || request.code == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(request.userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{request.userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, request.code);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error confirming email for user with ID '{request.userId}':");
            }

            return new ConfirmEmailResponse() { Message = "your email confirmed successfully"};
        }
    }
}
