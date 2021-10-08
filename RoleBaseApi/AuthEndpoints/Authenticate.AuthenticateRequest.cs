using RoleBaseApi.BaseViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBaseApi.AuthEndpoints
{
    public class AuthenticateRequest:BaseRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public bool RemmemberMe { get; set; }
    }
}
