namespace CleaningCompany.Results.Implementations
{
    public class NotFoundResult : ErrorResult
    {
        public NotFoundResult(string message) : base(message, null)
        {

        }
    }

    public class NotFoundResult<T> : ErrorResult<T>
    {
        public NotFoundResult(string message) : base(message, null)
        {

        }
    }
}
