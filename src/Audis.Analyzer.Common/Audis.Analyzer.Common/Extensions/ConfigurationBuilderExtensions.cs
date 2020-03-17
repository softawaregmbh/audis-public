using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace Audis.Analyzer.Common.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        /// <summary>
        /// Adds implicit tenant configuration stored in appsettings.tenant.json. 
        /// </summary>
        /// <param name="builder">The Microsoft.Extensions.Configuration.IConfigurationBuilder to add to.</param>
        /// <param name="filePath">Optional path relative to the base path of the builder.</param>
        /// <returns>The Microsoft.Extensions.Configuration.IConfigurationBuilder.</returns>
        public static IConfigurationBuilder AddTenantSpecificSettings(this IConfigurationBuilder builder, string filePath = "../../appsettings.tenant.json")
        {
            var assemblyName = Assembly.GetEntryAssembly().GetName().Name;
            var analyzerJsonPath = $"$.Analyzers[?(@.Name == '{assemblyName}')].Settings";
            var globalJsonPath = "$.Global";
            return builder
                .AddJsonSection(filePath, globalJsonPath, true)
                .AddJsonSection(filePath, analyzerJsonPath, true);
        }

        /// <summary>
        /// Adds a section of a JSON configuration file at filePath to builder.
        /// </summary>
        /// <param name="builder">The Microsoft.Extensions.Configuration.IConfigurationBuilder to add to.</param>
        /// <param name="filePath">Path relative to the base path of the executing assembly.</param>
        /// <param name="jsonPath">JSONPath query for section.</param>
        /// <param name="optional">Whether the file is optional.</param>
        /// <returns>The Microsoft.Extensions.Configuration.IConfigurationBuilder.</returns>
        public static IConfigurationBuilder AddJsonSection(this IConfigurationBuilder builder, string filePath, string jsonPath, bool optional)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            var exists = File.Exists(filePath);
            if (string.IsNullOrEmpty(filePath) || (!optional && !exists))
            {
                throw new ArgumentException("Invalid File Path", nameof(filePath));
            }
            else if (!exists && optional)
            {
                return builder;
            }

            var content = JObject.Parse(File.ReadAllText(filePath));
            var section = content.SelectToken(jsonPath)?.ToString() ?? "{}";
            var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(section));
            return builder.AddJsonStream(stream);
        }
    }
}
