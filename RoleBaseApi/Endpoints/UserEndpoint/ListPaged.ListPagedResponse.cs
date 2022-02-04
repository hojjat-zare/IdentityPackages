using RoleBaseApi.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBaseApi.Endpoints.UserEndpoint
{
    public class ListPagedResponse : BaseResponse
    {
        public List<UserDto> Users { get; set; } = new  List<UserDto>();
    }
}
