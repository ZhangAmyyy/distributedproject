using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Ordering.Application.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogError(ex, "Application Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);
                throw;
            }
        }

    }
}

//namespace Ordering.Application.Behaviours
//{
//    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> {
//        private readonly IEnumerable<IValidator<TRequest>> _validators;
//        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators) {
//            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
//        }

//        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
//        {
//            if (_validators.Any())
//            {
//                var context = new ValidationContext<TRequest>(request);

//                var validationResult = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
//            }
//        }
//    }
//}



//namespace Ordering.Application.Behaviours
//{
//    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
//        where TRequest : IRequest<TResponse>
//    {
//        private readonly ILogger<TRequest> _logger;

//        public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
//        {
//            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//        }

//        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
//            RequestHandlerDelegate<TResponse> next)
//        {
//            try
//            {
//                return await next();
//            }
//            catch (Exception ex)
//            {
//                var requestName = typeof(TRequest).Name;
//                _logger.LogError(ex, "Application Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);
//                throw;
//            }
//        }

//        Task<TResponse> IPipelineBehavior<TRequest, TResponse>.Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
