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
    public class Create : BaseAsyncEndpoint.WithRequest<string>.WithoutResponse
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public Create(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost("api/roles")]
        [SwaggerOperation(
            Summary = "add a role",
            Description = "add a role",
            OperationId = "CreateRole",
            Tags = new[] { "CreateRoleEndpoints" })
        ]
        public async override Task<ActionResult> HandleAsync(string roleName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(roleName)) return BadRequest(new { errors = new string[] { "roleName can't be empty" } });
            var role = new IdentityRole()
            {
                Name = roleName
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded) return Ok();
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("errors",error.Description);
            }
            return BadRequest(ModelState);
        }
    }
}
