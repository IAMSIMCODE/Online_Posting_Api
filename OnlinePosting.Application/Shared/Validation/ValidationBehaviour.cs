using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace OnlinePosting.Application.Shared.Validation
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResult = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResult 
                    .Where(r => r.Errors.Count != 0)
                    .SelectMany(r => r.Errors).ToList();

                if (failures.Count != 0) 
                { 
                    throw new ValidationException(failures); 
                }
            }

            return await next();
        }
    }


    public sealed class ValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> where TResponse : notnull
    {
        private readonly IValidator<TRequest> _validators;

        public ValidationPipelineBehaviour(IValidator<TRequest> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //Code before the handler is executed
            if (_validators is null)
                await next();

            var validationResult = await _validators.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid)
                return await next();


            var errors = validationResult.Errors.ConvertAll(validationFailure => new ProblemDetails()
            {
                Status = Convert.ToInt32(validationFailure.ErrorCode),
                Detail = validationFailure.ErrorMessage
            });

            var respons = JsonSerializer.Serialize(errors);
            //httpContext.Response.ContentType = "application/json";

            //await httpContext.Response.WriteAsync(respons, cancellationToken);



            //var result = await next();


            //Code after the handler is executed
            return (dynamic)respons;
        }
    }

    public class ValidationBehaviour2<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour2(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators is null)
                await next();

            var context = new ValidationContext<TRequest>(request);

            var validationResult = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResult
                .Where(r => r.Errors.Count != 0)
                .SelectMany(r => r.Errors).ToList();

            if (failures.Count > 0)
            {
                throw new ValidationException(failures);
            }






            //if (_validators.Any())
            //{
            //    var context = new ValidationContext<TRequest>(request);

            //    var validationResult = await Task.WhenAll(
            //        _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            //    var failures = validationResult
            //        .Where(r => r.Errors.Count != 0)
            //        .SelectMany(r => r.Errors).ToList();

            //    if (failures.Count != 0)
            //    {
            //        throw new ValidationException(failures);
            //    }
            //}

            return await next();
        }
    }
}
