using System.Collections.Generic;

namespace CleaningCompany.Results.Interfaces
{
    internal interface IErrorResult
    {
        string Message { get;}
        ICollection<Error> Errors { get; }
    }
}
