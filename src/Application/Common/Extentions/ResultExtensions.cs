namespace iot.Application.Common.Extentions;

public static class ResultExtensions
{
    public static ApiResult? MapToApiResult(this Result? result)
    {
        if (result == null)
            return null;

        ApiResult apiResult = new(ApiResultStatusCode.Success);

        if (result.Errors != null)
        {
            foreach (var item in result.Errors)
            {
                // TODO
                //apiResult.AddErrorMessage(message: item.Message);
            }
        }

        if (result.Successes != null)
        {
            foreach (var item in result.Successes)
            {
                // TODO
                //apiResult.AddSuccessMessage(message: item.Message);
            }
        }

        return apiResult;
    }

    //public static ApiResult<TData> MapToApiResult<TData>(this Result<TData> result)
    //where TData : class
    //{
    //    // TODO
    //    ApiResult<TData> apiResult = new ApiResult<TData>();

    //    if (result.IsFailed == false)
    //    {
    //        apiResult.data = result.Value;
    //    }

    //    if (result.Errors != null)
    //    {
    //        foreach (var item in result.Errors)
    //        {
    //            // TODO
    //            //apiResult.AddErrorMessage(message: item.Message);
    //        }
    //    }

    //    if (result.Successes != null)
    //    {
    //        foreach (var item in result.Successes)
    //        {
    //            apiResult.AddSuccessMessage(message: item.Message);
    //        }
    //    }

    //    return apiResult;
    //}
}