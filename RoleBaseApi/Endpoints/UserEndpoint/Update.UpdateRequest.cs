using RoleBaseApi.BaseViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBaseApi.Endpoints.UserEndpoint
{
    public class UpdateRequest : BaseRequest
    {
        [Required]
        public string UserId { get; set; }

        public string Username { get; set; } = null;

        [EmailAddress]
        public string Email { get; set; } = null;

        public bool? IsEmailConfirmed { get; set; } = null;

        [Phone]
        public string PhoneNumber { get; set; } = null;

        public string[] Roles { get; set; } = null;
    }
}
