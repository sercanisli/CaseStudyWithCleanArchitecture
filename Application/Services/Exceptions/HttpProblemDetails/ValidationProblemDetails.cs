﻿using Application.Services.Exceptions.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Exceptions.HttpProblemDetails
{
    public class ValidationProblemDetails:ProblemDetails
    {
        public IEnumerable<ValidationExceptionModel> Errors { get; init; }

        public ValidationProblemDetails(IEnumerable<ValidationExceptionModel> errors)
        {
            Title = "Validation Error";
            Detail = "One or more validation errors occured";
            Errors = errors;
            Status = StatusCodes.Status400BadRequest;
            Type = "https://example.com/problems/validation";
        }
    }
}
