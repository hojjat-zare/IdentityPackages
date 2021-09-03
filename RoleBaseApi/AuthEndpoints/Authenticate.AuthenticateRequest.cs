using RoleBaseApi.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBaseApi.AuthEndpoints
{
    public class AuthenticateRequest:BaseRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
