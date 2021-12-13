using System.Collections.Generic;

namespace CleaningCompany.Result.Interfaces
{
    internal interface IErrorResult
    {
        string Message { get;}
        ICollection<Error> Errors { get; }
    }
}
