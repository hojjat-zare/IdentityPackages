using Ardalis.ApiEndpoints;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RoleBaseApi.Endpoints.UserEndpoint
{
    public class ListPaged : BaseAsyncEndpoint.WithRequest<ListPagedRequest>.WithResponse<ListPagedResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ListPaged(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("api/users")]
        [SwaggerOperation(
            Summary = "get users",
            Description = "get users",
            OperationId = "GetUsers",
            Tags = new[] { "GetUsersEndpoints" })
        ]
        public async override Task<ActionResult<ListPagedResponse>> HandleAsync([FromQuery] ListPagedRequest request, CancellationToken cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                int numberOfUserToSkip = (request.PageIndex - 1) * request.PageSize;
                var users = await _userManager.Users.Skip(numberOfUserToSkip).Take(request.PageSize).ToArrayAsync();
                var result = new ListPagedResponse();
                foreach (var user in users)
                {
                    result.Users.Add(new UserDto()
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        Email = user.Email,
                        IsEmailConfirmed = user.EmailConfirmed,
                        PhoneNumber = user.PhoneNumber
                    });
                }
                return result;
            }
            return BadRequest(ModelState);
        }
    }
}
