using RoleBaseApi.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBaseApi.Endpoints.UserEndpoint
{
    public class GetByIdResponse : BaseResponse
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;

        public string[] Roles { get; set; }
    }
}
