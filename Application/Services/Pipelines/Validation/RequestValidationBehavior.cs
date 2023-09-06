﻿using Application.Services.Exceptions.Types;
using FluentValidation;
using MediatR;

namespace Application.Services.Pipelines.Validation
{
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            ValidationContext<object> context = new(request);

            IEnumerable<ValidationExceptionModel> errors = _validators
                .Select(validator => validator.Validate(context)) //context'i validate et.
                .SelectMany(result => result.Errors) //birden fazla validation hatası alınırsa eğer.
                .Where(failure => failure != null) //bir fail var ise
                .GroupBy(
                    keySelector: p=>p.PropertyName,
                    resultSelector: (propertyName,errors)=>
                        new ValidationExceptionModel
                        {
                            Property=propertyName,
                            Errors=errors.Select(e=>e.ErrorMessage)
                        }
                ).ToList();

            if (errors.Any())
            {
                throw new Exceptions.Types.ValidationException(errors);
            }
            TResponse response = await next();
            return response;
        }
    }
}