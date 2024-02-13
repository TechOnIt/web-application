namespace TechOnIt.Application.Common.Models
{
    public partial class Result
    {
        public bool IsSuccess { get; set; } = false;
        public string[] Messages { get; set; } = Array.Empty<string>();

        #region Ctor

        public Result(bool isSuccess = false, string[] messages = null)
        {
            IsSuccess = isSuccess;
            Messages = messages;
        }

        #endregion

        #region Methods

        public static Result Success() => new(true, null);
        public static Result Fail(string message)
            => new(false, [message]);
        public static Result Fail(string[] messages = null)
            => new(false, messages);

        #endregion
    }
    public partial class Result<TData>
    {
        public bool IsSuccess { get; set; } = false;
        public string[] Messages { get; set; } = Array.Empty<string>();
        public TData Data { get; set; } = default;

        #region Ctor

        public Result(bool isSuccess = false, string[] messages = null, TData data = default)
        {
            IsSuccess = isSuccess;
            Messages = messages;
            Data = data;
        }

        #endregion

        #region Methods

        public static Result<TData> Success(TData data = default)
            => new(true, null, data);
        public static Result<TData> Fail(string message)
            => new(false, [message]);
        public static Result<TData> Fail(string[] messages = null)
            => new(false, messages);

        #endregion

        #region Implicit

        public static implicit operator Result(Result<TData> result)
            => new(result.IsSuccess, result.Messages);
        public static implicit operator Result<TData>(Result result)
            => new(result.IsSuccess, result.Messages);

        #endregion
    }
}
