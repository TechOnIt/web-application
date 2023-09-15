using System.ComponentModel.DataAnnotations;

namespace TechOnIt.Application.Common.Frameworks.ApiResultFrameWork;

public enum ApiResultStatusCode
{
    Success = 200,

    [Display(Name = "Internal server error has occured.")]
    ServerError = 500,

    [Display(Name = "Not valid data")]
    BadRequest = 400,

    [Display(Name = "Not found any content.")]
    NotFound = 404,

    [Display(Name = "List is empty")]
    ListEmpty = 411,//Length Required

    [Display(Name = "Somthing went wrong during processing.")]
    LogicError = 444,//No Response

    [Display(Name = "You are not authorized to access!")]
    UnAuthorized = 401,

    [Display(Name = "You dont have permission to access!")]
    Forbiden = 403,
}