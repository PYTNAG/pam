using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PAM.Core.App.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace PAM.Core.App.RequestResolver
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRequestResolver(this IServiceCollection services)
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.IsAbstract)
                {
                    continue;
                }
                
                var interfaces = type.GetInterfaces();

                var handlerInterface = interfaces.FirstOrDefault(@interface =>
                    @interface.IsGenericType &&
                    @interface.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));

                if (handlerInterface is not null)
                {
                    AddHandler(services, type, handlerInterface.GetGenericArguments()[0]);
                    continue;
                }

                var validatorInterface = interfaces.FirstOrDefault(@interface =>
                    @interface.IsGenericType &&
                    @interface.GetGenericTypeDefinition() == typeof(IValidator<>));

                if (validatorInterface is not null)
                {
                    RequestResolver.RegisterValidator(
                        validatorInterface.GetGenericArguments()[0], Activator.CreateInstance(type) as IValidator);
                }
            }

            services.AddSingleton<RequestResolver>();

            return services;
        }

        private static void AddHandler(IServiceCollection services, Type handlerType, Type requestType)
        {
            int constructorsCount = handlerType.GetConstructors().Length;
            if (constructorsCount != 1)
            {
                throw new Exception(
                        $"{handlerType.Name} must has 1 public constructor, but there is {constructorsCount}.");
            }

            var parameters = new List<object>();
            ParameterInfo[] parametersInfos = handlerType.GetConstructors()[0].GetParameters();

            ServiceProvider servicesBuild = services.BuildServiceProvider();
            foreach (var parameterInfo in parametersInfos)
            {
                parameters.Add(servicesBuild.GetRequiredService(parameterInfo.ParameterType));
            }
                    
            RequestResolver.RegisterHandler(requestType, Activator.CreateInstance(handlerType, parameters.ToArray()) as IRequestHandler);
        }
    }
}