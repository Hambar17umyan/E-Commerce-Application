using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace API.Behaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
                return await next();

            var fails = _validators
                .Select(x => x.Validate(request))
                .SelectMany(x => x.Errors)
                .Where(x => x is not null)
                .Distinct()
                .ToArray();
                
            if(fails.Any())
            {
                return CreateValidationResult(fails);
            }

            return await next();
        }

        private static TResponse CreateValidationResult(IEnumerable<ValidationFailure> fails)
        {
            return new ValidationResult(fails) as TResponse;
        }
    }
}
