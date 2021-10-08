using RoleBaseApi.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBaseApi.RegisterEndpoints
{
    public class ConfirmEmailResponse : BaseResponse
    {
        public string Message { get; set; }
    }
}
