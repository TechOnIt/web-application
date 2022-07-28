using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot.Application.Common.Models
{
    public enum ApiResultStatusCode
    {
        [Display(Name = "Succeded")]
        Success = 0,

        [Display(Name = "server error")]
        ServerError = 1,

        [Display(Name = "not valid data")]
        BadRequest = 2,

        [Display(Name = "not found")]
        NotFound = 3,

        [Display(Name = "list is empty")]
        ListEmpty = 4,

        [Display(Name = "somthing went wrong during processing")]
        LogicError = 5,

        [Display(Name = "unAuthorized")]
        UnAuthorized = 6
    }
}
