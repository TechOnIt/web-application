using System.ComponentModel.DataAnnotations;

namespace iot.Application.Common.Frameworks.ApiResultFrameWork;

public enum ApiResultStatusCode
{
    Success = 0,

    [Display(Name = "Internal server error has occured.")]
    ServerError = 1,

    [Display(Name = "Not valid data")]
    BadRequest = 2,

    [Display(Name = "Not found any content.")]
    NotFound = 3,

    [Display(Name = "List is empty")]
    ListEmpty = 4,

    [Display(Name = "Somthing went wrong during processing.")]
    LogicError = 5,

    [Display(Name = "You are not authorized to access!")]
    UnAuthorized = 6,

    [Display(Name = "You dont have permission to access!")]
    Forbiden = 403,
}