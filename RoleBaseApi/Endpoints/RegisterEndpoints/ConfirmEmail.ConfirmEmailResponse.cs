using RoleBaseApi.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBaseApi.Endpoints.RegisterEndpoints
{
    public class ConfirmEmailResponse : BaseResponse
    {
        public string Message { get; set; }
    }
}
