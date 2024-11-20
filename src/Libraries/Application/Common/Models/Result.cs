namespace TechOnIt.Application.Common.Models
{
    public class BaseResult
    {
        public BaseResult(bool isSuccess, List<string>? messages = default)
        {
            IsSuccess = isSuccess;
            if (messages != null && messages.Count > 0)
                Messages = messages;
        }
        public bool IsSuccess { get; private set; }
        public List<string> Messages { get; private set; } = new();
    }
    public class Result : BaseResult
    {
        #region Ctor

        public Result(bool isSuccess, List<string>? messages = default)
            : base(isSuccess, messages) { }

        #endregion
        #region Methods

        public static Result Ok()
            => new(true, []);
        public static Result<TData> Ok<TData>(TData? data = default)
            => new Result<TData>(true, data, []);
        public static Result<TData, TStatus> Ok<TData, TStatus>(TData? data, TStatus status)
            where TStatus : Enum
            => new Result<TData, TStatus>(true, data, status, []);

        public static Result Fail(string message)
            => new(false, [message]);
        public static Result Fail(List<string>? messages = null)
            => new(false, messages);
        public static Result<TData> Fail<TData>(List<string>? messages = null, TData? data = default)
            => new Result<TData>(false, data, messages);
        public static Result<TData> Fail<TData>(string? error = null, TData? data = default)
            => new Result<TData>(false, data, [error!]);
        public static Result<TData, TStatus> Fail<TData,TStatus>(TStatus status, string? error = null, TData? data = default)
            where TStatus : Enum
            => new Result<TData, TStatus>(false, data, status, [error!]);
        public static Result<TData, TStatus> Fail<TData,TStatus>(TStatus status, List<string>? errors = null, TData? data = default)
            where TStatus : Enum
            => new Result<TData, TStatus>(false, data, status, errors);

        /// <summary>
        /// Map extension message to result message.
        /// </summary>
        public static Result Fail(Exception exception)
            => new(false, [exception.Message]);

        #endregion
        #region Implicit

        public static implicit operator Result(bool isSuccess)
            => isSuccess ? Ok() : Fail();

        #endregion
    }
    public class Result<TData> : BaseResult
    {
        public TData? Data { get; set; } = default;

        #region Ctor

        public Result(bool isSuccess, TData? data = default, List<string>? messages = default)
            : base(isSuccess, messages)
        {
            Data = data;
        }

        #endregion
        #region Implicit

        public static implicit operator Result(Result<TData> result)
            => new Result(result.IsSuccess, result.Messages);
        public static implicit operator Result<TData>(Result result)
            => new Result<TData>(result.IsSuccess, default, result.Messages);

        #endregion
    }

    public partial class Result<TData, TStatus> : Result<TData>
        where TStatus : Enum
    {
        public TStatus Status { get; set; }

        #region Ctor

        public Result(bool isSuccess, TData? data = default, TStatus status = default, List<string>? messages = default)
            : base(isSuccess, data, messages)
        {
            Status = status;
        }

        #endregion
        #region Implicit

        public static implicit operator Result(Result<TData, TStatus> result)
            => new Result(result.IsSuccess, result.Messages);
        public static implicit operator Result<TData, TStatus>(Result result)
            => new Result<TData, TStatus>(result.IsSuccess, default, default, result.Messages);

        public static implicit operator Result<TData, TStatus>(Result<TStatus> result)
            => new Result<TData, TStatus>(result.IsSuccess, default, default, result.Messages);


        #endregion
    }

    public static class ResultExtensions
    {
        public static Result WithMessage(this Result result, string message)
        {
            result.Messages.Add(message);
            return result;
        }
        public static Result<TData> WithMessage<TData>(this Result<TData> result, string message)
        {
            result.Messages.Add(message);
            return result;
        }

        public static Result<TData, TStatus> WithStatus<TData, TStatus>(this Result<TData, TStatus> result, TStatus status)
            where TStatus : Enum
        {
            result.Status = status;
            return result;
        }

        public static List<TData> CollectSuccesfulData<TData>(this List<Result<TData>> results)
        {
            return results
                .Where(x => x.IsSuccess == true)
                .Select(x => x.Data!)
                .ToList();
        }

        public static List<TData> CollectSuccesfulData<TData, TStatus>(this List<Result<TData, TStatus>> results)
            where TStatus : Enum
        {
            return results
                .Where (x => x.IsSuccess == true)
                .Select(x => x.Data!)
                .ToList();
        }
        public static List<TData> CollectFailedData<TData>(this List<Result<TData>> results)
        {
            return results
                .Where(x => x.IsSuccess == false)
                .Select(x => x.Data!)
                .ToList();
        }
    }

    public interface IStatus { }
}
