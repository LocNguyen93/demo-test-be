namespace Ext.Shared.DataAccess
{
    public class Result
    {
        public string ErrorCode { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public bool Succeeded => string.IsNullOrWhiteSpace(ErrorCode);

        public Result()
        {
        }
        public Result(string code, string msg)
        {
            ErrorCode = code;
            ErrorMessage = msg;
        }

        public Result<T> AddData<T>(T data)
        {
            return new Result<T>(this, data);
        }
    }

    public class Result<T> : Result
    {
        public T Data { get; set; }

        public Result(T data) : base()
        {
            Data = data;
        }
        public Result(string code, string msg, T data = default)
            : base(code, msg)
        {
            Data = data;
        }
        public Result(Result result, T data)
            : this(result.ErrorCode, result.ErrorMessage, data)
        { }
    }
}
