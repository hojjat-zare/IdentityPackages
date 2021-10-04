using RoleBaseApi.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBaseApi.RegisterEndpoints
{
    public class ConfirmEmailRequest : BaseRequest
    {
        public string userId;
        public string code;
    }
}
