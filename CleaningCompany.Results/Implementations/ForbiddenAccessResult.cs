using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningCompany.Result.Implementations
{
    public class ForbiddenAccessResult : Result
    {
        public ForbiddenAccessResult()
        {
            Success = false;
        }
    }

    public class ForbiddenAccessResult<T> : Result<T>
    {
        public ForbiddenAccessResult(T data) : base(data)
        {
            Success = false;
        }
    }
}
