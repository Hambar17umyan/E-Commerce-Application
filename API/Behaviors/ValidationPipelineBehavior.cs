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
            if (!_validators.Any())
                return await next();

            foreach (var validator in _validators)
            {
                var res = validator.Validate(request);
                if (!res.IsValid)
                {
                    return Result.Fail(res.Errors.First().ToString()) as TResponse;
                }
            }

            return await next();
        }

        private static TResponse CreateValidationResult(IEnumerable<ValidationFailure> fails)
        {
            return new ValidationResult(fails) as TResponse;
        }
    }
}
