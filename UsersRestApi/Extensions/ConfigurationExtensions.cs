// **********************************************************************************
// Filename					- ConfigurationExtensions.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

using Microsoft.Extensions.Configuration;
using System;

namespace UsersRestApi.Extensions
{
    public static class ConfigurationExtensions
    {
        private const string OPTIONS_SUFFIX = "Options";

        /// <summary>
        /// Permits strongly typed calls to reference sections of Configuration objects.
        /// </summary>
        /// <typeparam name="T">The type of options to get.</typeparam>
        /// <param name="configuration">This configuration to use.</param>
        /// <returns>The configuration section to use for the specified type.</returns>
        public static IConfigurationSection GetSection<T>(this IConfiguration configuration)
        {
            var typeName = typeof(T).Name;
            var sectionName = typeName.Substring(0, typeName.IndexOf(OPTIONS_SUFFIX, StringComparison.Ordinal));
            return configuration.GetSection(sectionName);
        }
    }
}
