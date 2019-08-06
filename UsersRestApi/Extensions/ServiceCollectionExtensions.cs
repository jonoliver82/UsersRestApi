// **********************************************************************************
// Filename					- ServiceCollectionExtensions.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

using Microsoft.Extensions.DependencyInjection;
using UsersRestApi.Factories;
using UsersRestApi.Interfaces;
using UsersRestApi.Repositories;
using UsersRestApi.Services;
using UsersRestApi.Validaters;

namespace UsersRestApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterFactories(this IServiceCollection services)
        {
            services.AddScoped<IUserFactory, UserFactory>();

            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUsersFinderService, UsersFinderService>();

            return services;
        }

        public static IServiceCollection RegisterValidaters(this IServiceCollection services)
        {
            services.AddScoped<IValidationExceptionHandler, AggregatingValidationExceptionHandler>();

            return services;
        }
    }
}
