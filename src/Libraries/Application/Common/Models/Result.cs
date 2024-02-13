namespace TechOnIt.Application.Common.Models
{
    public partial class Result
    {
        public bool IsSuccess { get; set; } = false;
        public List<string> Messages { get; private set; } = new();

        #region Ctor

        public Result(bool isSuccess = false, List<string> messages = null)
        {
            IsSuccess = isSuccess;
            if (messages != null && messages.Count > 0)
                Messages = messages;
        }

        #endregion

        #region Methods

        public static Result Ok() => new(true, null);
        public static Result Ok<TData>(TData data)
            => new Result<TData>(true, null, data);

        public static Result Fail(string message)
            => new(false, [message]);
        public static Result Fail(List<string> messages = null)
            => new(false, messages);

        #endregion
    }
    public partial class Result<TData>
    {
        public bool IsSuccess { get; set; } = false;
        public List<string> Messages { get; set; } = new();
        public TData Data { get; set; } = default;

        #region Ctor

        public Result(bool isSuccess = false, List<string> messages = null, TData data = default)
        {
            IsSuccess = isSuccess;
            if (messages != null && messages.Count > 0)
                Messages = messages;
            Data = data;
        }

        #endregion

        #region Methods

        public static Result<TData> Ok(TData data = default)
            => new(true, null, data);
        public static Result<TData> Fail(string message)
            => new(false, [message]);
        public static Result<TData> Fail(List<string> messages = null)
            => new(false, messages);

        #endregion

        #region Implicit

        public static implicit operator Result(Result<TData> result)
            => new(result.IsSuccess, result.Messages);
        public static implicit operator Result<TData>(Result result)
            => new(result.IsSuccess, result.Messages);
        public static implicit operator Result<TData>(TData result)
            => Ok(result);

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
    }
}
