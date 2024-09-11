
namespace ClinicalBackend.Domain.Exceptions
{
    public abstract class BadRequestException : Exception
    {
        protected BadRequestException(string message)
            : base(message)
        {
        }

        protected BadRequestException()
        {
        }

        protected BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}