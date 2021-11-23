using CleaningCompany.Results.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningCompany.Results.Implementations
{
    public class ErrorResult : Result, IErrorResult
    {
        public ErrorResult(string message) : this(message, Array.Empty<Error>())
        {

        }

        public ErrorResult(string message, ICollection<Error> errors)
        {
            Message = message;
            Success = false;
            Errors = errors ?? Array.Empty<Error>();
        }

        public string Message { get; }
        public ICollection<Error> Errors { get; protected set; }
    }

    public class ErrorResult<T> : Result<T>, IErrorResult
    {
        public ErrorResult(string message) : this(message, Array.Empty<Error>())
        {
        }

        public ErrorResult(string message, ICollection<Error> errors) : base(default)
        {
            Message = message;
            Success = false;
            Errors = errors ?? Array.Empty<Error>();
        }

        public string Message { get; set; }
        public ICollection<Error> Errors { get; protected set; }
    }
}
