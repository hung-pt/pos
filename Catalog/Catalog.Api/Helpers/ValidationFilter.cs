using FluentValidation;
using System.Net;
using System.Reflection;

namespace Catalog.Api.Helpers;

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
public class ValidateAttribute : Attribute { }

public static class ValidationFilter {
    public static EndpointFilterDelegate ValidationFilterFactory(EndpointFilterFactoryContext factoryContext, EndpointFilterDelegate next) {
        IEnumerable<ValidationDescriptor> descriptors = GetValidators(
            factoryContext.MethodInfo,
            factoryContext.ApplicationServices
        );

        return descriptors.Any() ?
            invocationContext => Validate(descriptors, invocationContext, next) :
            invocationContext => next(invocationContext);
    }

    private static async ValueTask<object?> Validate(
        IEnumerable<ValidationDescriptor> descriptors,
        EndpointFilterInvocationContext invocationContext,
        EndpointFilterDelegate next
        ) {
        foreach (ValidationDescriptor des in descriptors) {
            var arg = invocationContext.Arguments[des.ArgumentIndex];

            if (arg is not null) {
                var validationResult = await des.Validator.ValidateAsync(
                    new ValidationContext<object>(arg)
                );

                if (!validationResult.IsValid) {
                    return Results.ValidationProblem(
                        validationResult.ToDictionary(),
                        statusCode: (int)HttpStatusCode.UnprocessableEntity);
                }
            }
        }

        return await next.Invoke(invocationContext);
    }

    private static IEnumerable<ValidationDescriptor> GetValidators(MethodInfo methodInfo, IServiceProvider sp) {
        IEnumerable<ParameterInfo> validateParameters =
            methodInfo.GetParameters().Where(p => p.GetCustomAttribute<ValidateAttribute>() is not null);
        var enumerator = validateParameters.GetEnumerator();

        for (int i = 0; enumerator.MoveNext(); i++) {
            ParameterInfo param = enumerator.Current;
            Type validatorType = typeof(IValidator<>).MakeGenericType(param.ParameterType);

            // Note that FluentValidation validators needs to be registered as singleton
            IValidator? validator = sp.GetService(validatorType) as IValidator;

            if (validator is not null) {
                yield return new ValidationDescriptor {
                    ArgumentIndex = i,
                    ArgumentType = param.ParameterType,
                    Validator = validator
                };
            }

        }
    }

    private class ValidationDescriptor {
        public required int ArgumentIndex { get; init; }
        public required Type ArgumentType { get; init; }
        public required IValidator Validator { get; init; }
    }
}
