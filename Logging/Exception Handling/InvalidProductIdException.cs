namespace Logging_And_Exceptions_Handling.Exception_Handling
{
    public class InvalidProductIdException : ArgumentException
    {
        public InvalidProductIdException()
        {

        }
        public InvalidProductIdException(string? message) : base(message)
        {

        }
        public InvalidProductIdException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
