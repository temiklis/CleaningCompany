namespace CleaningCompany.Results
{
    public class Error
    {
        public Error(string details) : this(null, details)
        {

        }

        public Error(string propertyName, string details)
        {
            PropertyName = propertyName;
            Details = details;
        }

        public string PropertyName { get; }
        public string Details { get; }
    }
}
