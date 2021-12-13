using CleaningCompany.Result.Implementations;
using CleaningCompany.Result;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleaningCompany.API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected void AddPaginationHeader<T>(Result<PagedList<T>> pagedListResult)
        {
            if (!pagedListResult.Success)
                return;

            var metadata = new
            {
                pagedListResult.Data.TotalCount,
                pagedListResult.Data.PageSize,
                pagedListResult.Data.CurrentPage,
                pagedListResult.Data.TotalPages,
                pagedListResult.Data.HasNext,
                pagedListResult.Data.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
        }

        protected ActionResult CreateResponseFromResult<T>(Result.Result result)
        {
            return result switch
            {
                SuccessResult<T> successResult => Ok(successResult.Data),
                ValidationErrorResult<T> validationErrorResult => BadRequest(new { validationErrorResult.Message, validationErrorResult.Errors }),
                NotFoundResult<T> notFoundResult => NotFound(notFoundResult.Message),
                ErrorResult<T> errorResult => BadRequest(errorResult.Message),
                _ => StatusCode(500)
            };
        }

        protected ActionResult CreateResponseFromResult(Result.Result result)
        {
            return result switch
            {
                SuccessResult => Ok(),
                ValidationErrorResult validationResult => BadRequest(new { validationResult.Message, validationResult.Errors }),
                _ => new StatusCodeResult(500)
            };
        }
    }
}
