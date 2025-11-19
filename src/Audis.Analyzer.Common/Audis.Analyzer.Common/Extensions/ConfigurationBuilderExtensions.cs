using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Audis.Analyzer.Common.Extensions;

public static class ConfigurationBuilderExtensions
{
    /// <summary>
    ///     Adds implicit tenant configuration stored in appsettings.tenant.json.
    /// </summary>
    /// <param name="builder">The Microsoft.Extensions.Configuration.IConfigurationBuilder to add to.</param>
    /// <param name="filePath">Optional path relative to the base path of the builder.</param>
    /// <returns>The Microsoft.Extensions.Configuration.IConfigurationBuilder.</returns>
    public static IConfigurationBuilder AddTenantSpecificSettings(
        this IConfigurationBuilder builder,
        string filePath = "../../appsettings.tenant.json")
    {
        var assemblyName = Assembly.GetEntryAssembly().GetName().Name;
        var analyzerJsonPath = $"$.Analyzers[?(@.Name == '{assemblyName}')].Settings";
        var globalJsonPath = "$.Global";
        return builder
            .AddJsonSection(filePath, globalJsonPath, true)
            .AddJsonSection(filePath, analyzerJsonPath, true);
    }

    /// <summary>
    ///     Adds a section of a JSON configuration file at filePath to builder.
    /// </summary>
    /// <param name="builder">The Microsoft.Extensions.Configuration.IConfigurationBuilder to add to.</param>
    /// <param name="filePath">Path relative to the base path of the executing assembly.</param>
    /// <param name="jsonPath">JSONPath query for section.</param>
    /// <param name="optional">Whether the file is optional.</param>
    /// <returns>The Microsoft.Extensions.Configuration.IConfigurationBuilder.</returns>
    public static IConfigurationBuilder AddJsonSection(
        this IConfigurationBuilder builder, 
        string filePath,
        string jsonPath, 
        bool optional)
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

        if (!exists && optional)
        {
            return builder;
        }

        using var document = JsonDocument.Parse(File.ReadAllText(filePath));
        var section = SelectToken(document.RootElement, jsonPath);

        var sectionJson = section.HasValue
            ? JsonSerializer.Serialize(section.Value)
            : "{}";

        var stream = new MemoryStream(Encoding.UTF8.GetBytes(sectionJson));
        return builder.AddJsonStream(stream);
    }

    private static JsonElement? SelectToken(JsonElement root, string jsonPath)
    {
        // Simple JSONPath parser for the specific patterns used:
        // "$.Global" -> root.GetProperty("Global")
        // "$.Analyzers[?(@.Name == 'X')].Settings" -> root.GetProperty("Analyzers").EnumerateArray().FirstOrDefault(a => a.GetProperty("Name").GetString() == "X").GetProperty("Settings")

        if (jsonPath == "$.Global")
        {
            return root.TryGetProperty("Global", out var globalElement) ? globalElement : null;
        }

        // Handle pattern: $.Analyzers[?(@.Name == 'X')].Settings
        if (jsonPath.StartsWith("$.Analyzers[?(@.Name == '") && jsonPath.EndsWith("')].Settings"))
        {
            var nameStart = jsonPath.IndexOf("'") + 1;
            var nameEnd = jsonPath.LastIndexOf("'");
            var nameToFind = jsonPath.Substring(nameStart, nameEnd - nameStart);

            if (root.TryGetProperty("Analyzers", out var analyzersElement) &&
                analyzersElement.ValueKind == JsonValueKind.Array)
            {
                var analyzer = analyzersElement.EnumerateArray()
                    .FirstOrDefault(a => a.TryGetProperty("Name", out var nameElement) &&
                                         nameElement.GetString() == nameToFind);

                if (analyzer.ValueKind != JsonValueKind.Undefined &&
                    analyzer.TryGetProperty("Settings", out var settingsElement))
                {
                    return settingsElement;
                }
            }
        }

        return null;
    }
}