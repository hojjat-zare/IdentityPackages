using RoleBaseApi.BaseViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBaseApi.Endpoints.UserEndpoint
{
    public class ListPagedRequest : BaseRequest
    {
        [Required]
        [Range(1,int.MaxValue)]
        public int PageIndex { get; set; }

        [Required]
        [Range(1,100)]
        public int PageSize { get; set; }
    }
}
