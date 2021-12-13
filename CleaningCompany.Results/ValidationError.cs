namespace CleaningCompany.Result
{
    public class ValidationError : Error
    {
        public ValidationError(string propertyName, string details) : base(propertyName, details)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; }
    }
}
