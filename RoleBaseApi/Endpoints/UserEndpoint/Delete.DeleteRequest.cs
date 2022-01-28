using RoleBaseApi.BaseViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBaseApi.Endpoints.UserEndpoint
{
    public class DeleteRequest : BaseRequest
    {
        [Required()]
        public string Username { get; set; }
    }
}
