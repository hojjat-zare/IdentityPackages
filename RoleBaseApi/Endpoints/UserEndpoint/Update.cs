﻿using Ardalis.ApiEndpoints;
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
    public class Update : BaseAsyncEndpoint.WithRequest<UpdateRequest>.WithoutResponse
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public Update(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPut("api/users")]
        [SwaggerOperation(
            Summary = "Update a user",
            Description = "Update a user",
            OperationId = "Update",
            Tags = new[] { "UpdateEndpoints" })
        ]
        public async override Task<ActionResult> HandleAsync(UpdateRequest request, CancellationToken cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(request.UserId);
                if (user == null) return NotFound();
                user.UserName = request.Username ?? user.UserName ;
                user.Email = request.Email ?? user.Email;
                user.EmailConfirmed = request.IsEmailConfirmed ?? user.EmailConfirmed ;
                user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded) return Ok();
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("errors", error.Description);
                }
            }
            return BadRequest(ModelState);
        }
    }
}
