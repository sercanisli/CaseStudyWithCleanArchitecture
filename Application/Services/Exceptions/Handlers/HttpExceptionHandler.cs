using Application.Services.Exceptions.Extensions;
using Application.Services.Exceptions.HttpProblemDetails;
using Application.Services.Exceptions.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Application.Services.Exceptions.Handlers
{
    public class HttpExceptionHandler : ExceptionHandler
    {
        private HttpResponse? _response;
        public HttpResponse Response
        {
            get => _response ?? throw new ArgumentNullException(nameof(_response));
            set => _response = value;
        }
        protected override Task HandlerException(BusinessException businessException)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            string details = new BusinessProblemDetails(businessException.Message).AsJson();
            return Response.WriteAsync(details);
        }

        protected override Task HandlerException(Exception exception)
        {
            Response.StatusCode = StatusCodes.Status500InternalServerError;
            string details = new InternalServerErrorProblemDetails(exception.Message).AsJson();
            return Response.WriteAsync(details);
        }

        protected override Task HandlerException(ValidationException validationException)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            string details = new HttpProblemDetails.ValidationProblemDetails(validationException.Errors).AsJson();
            return Response.WriteAsync(details);
        }
    }
}
