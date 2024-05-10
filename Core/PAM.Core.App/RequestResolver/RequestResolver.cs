using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PAM.Core.App.Validation;

namespace PAM.Core.App.RequestResolver
{
    public class RequestResolver
    {
        private static readonly Dictionary<Type, IRequestHandler> Handlers = new Dictionary<Type, IRequestHandler>();

        private static readonly Dictionary<Type, List<IValidator>> Validators =
            new Dictionary<Type, List<IValidator>>();

        public async Task<TResponse> Resolve<TRequest, TResponse>(TRequest request)
        {
            if (!Handlers.TryGetValue(typeof(TRequest), out IRequestHandler handler))
            {
                throw new Exception($"Handler for {nameof(TRequest)} request doesn't exist.");
            }
            
            if (Validators.TryGetValue(typeof(TRequest), out List<IValidator> validators))
            {
                request = validators.Aggregate(request, (current, validator) => (validator as IValidator<TRequest>)!.Validate(current));
            }
            
            return await (handler as IRequestHandler<TRequest, TResponse>)!.Handle(request, new CancellationToken());
        }
        
        public static void RegisterHandler(Type requestType, IRequestHandler handler)
        {
            Handlers.Add(requestType, handler);
        }

        public static void RegisterValidator(Type requestType, IValidator validator)
        {
            if (!Validators.ContainsKey(requestType))
            {
                Validators.Add(requestType, new List<IValidator>());
            }
            
            Validators[requestType].Add(validator);
        }
    }
}