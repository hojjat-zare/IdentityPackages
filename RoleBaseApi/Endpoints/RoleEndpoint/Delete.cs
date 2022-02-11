using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RoleBaseApi.Endpoints.RoleEndpoint
{
    public class Delete : BaseAsyncEndpoint.WithRequest<string>.WithoutResponse
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public Delete(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpDelete("api/roles")]
        [SwaggerOperation(
            Summary = "delete a role",
            Description = "delete a role",
            OperationId = "DeleteRole",
            Tags = new[] { "DeleteRoleEndpoints" })
        ]
        public async override Task<ActionResult> HandleAsync(string roleName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(roleName)) return BadRequest(new { errors = new string[] { "roleName can't be empty" } });
            var role = await _roleManager.FindByNameAsync(roleName);
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded) return Ok();
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("errors", error.Description);
            }
            return BadRequest(ModelState);
        }
    }
}
