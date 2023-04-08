namespace Ext.Shared.DataAccess
{
    using System.Data.SqlClient;

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
        public Result(SqlParameter codeParam, SqlParameter msgParam)
        {
            ErrorCode = codeParam.Value.ToString();
            ErrorMessage = msgParam.Value.ToString();
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
        public Result(SqlParameter codeParam, SqlParameter msgParam, T data)
            : this(codeParam.Value.ToString(), msgParam.Value.ToString(), data)
        { }
        public Result(Result result, T data)
            : this(result.ErrorCode, result.ErrorMessage, data)
        { }
    }

    public static class SpExecutionResultExtensions
    {
        public static Result<T> AddData<T>(this Result result, T data)
        {
            return new Result<T>(result, data);
        }
    }
}
