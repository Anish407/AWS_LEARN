namespace AWS_CloudCore.Core.Common.Exceptions
{
    public class DatabaseException : BaseException
    {
        public DatabaseException(string message) : base(message)
        {

        }

        public DatabaseException(string message, string details, string query = null) : this(message)
        {
            Details = details;
            Query = query ?? string.Empty;
        }

        public string Query { get; }
    }
}
