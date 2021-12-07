namespace CleaningCompany.Results.Implementations
{
    public class UnauthorizedAccessResult : Result
    {
        public UnauthorizedAccessResult()
        {
            Success = false;
        }
    }

    public class UnauthorizedAccessResult<T> : Result<T>
    {
        public UnauthorizedAccessResult(T data) : base(data)
        {
            Success = false;
        }
    }
}
