namespace Ext.Shared.DataAccess
{
    public abstract class RFBaseService
    {
        protected Result Succeed()
        {
            return new Result();
        }

        protected Result<T> Succeed<T>(T data)
        {
            return new Result<T>(data);
        }

        protected Result Error(string errorCode, string errorMsg = "")
        {
            return new Result(errorCode, errorMsg);
        }

        protected Result<T> Error<T>(string errorCode, string errorMsg = "")
        {
            return new Result<T>(errorCode, errorMsg, default(T));
        }
    }
}
