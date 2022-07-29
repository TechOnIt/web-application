﻿using FluentValidation.AspNetCore;
using iot.Application.Commands.Users;
using iot.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace iot.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddFluentValidation(fv =>  // Fluent Validation
        {
                fv.RegisterValidatorsFromAssemblyContaining<BaseFluentValidator<object>>();
        });

        services.AddTransient
            (typeof(IPipelineBehavior<UserCreateCommand, Guid>),
            typeof(UserCreateCommandValidationBehavior<UserCreateCommand, Guid>));

        return services;
    }
}