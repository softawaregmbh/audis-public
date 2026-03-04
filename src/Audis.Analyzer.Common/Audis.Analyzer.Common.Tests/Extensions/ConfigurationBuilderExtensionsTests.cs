using System;
using System.IO;
using System.Linq;
using Audis.Analyzer.Common.Extensions;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Audis.Analyzer.Common.Tests.Extensions;

[TestFixture]
public class ConfigurationBuilderExtensionsTests
{
    private string testDirectory;
    private string testFilePath;
    
    [SetUp]
    public void SetUp()
    {
        testDirectory = Path.Combine(Path.GetTempPath(), $"AudisTests_{Guid.NewGuid()}");
        Directory.CreateDirectory(testDirectory);
        testFilePath = Path.Combine(testDirectory, "appsettings.test.json");
    }

    [TearDown]
    public void TearDown()
    {
        if (Directory.Exists(testDirectory))
        {
            Directory.Delete(testDirectory, true);
        }
    }

    [Test]
    public void AddJsonSection_WithSimpleGlobalPath_ShouldExtractGlobalSection()
    {
        // Arrange
        var json = @"{
                ""Global"": {
                    ""Setting1"": ""Value1"",
                    ""Setting2"": 42
                },
                ""Other"": {
                    ""Setting3"": ""Value3""
                }
            }";
        File.WriteAllText(testFilePath, json);

        var builder = new ConfigurationBuilder();

        // Act
        builder.AddJsonSection(testFilePath, "$.Global", false);
        var config = builder.Build();

        // Assert
        Assert.That(config["Setting1"], Is.EqualTo("Value1"));
        Assert.That(config["Setting2"], Is.EqualTo("42"));
        Assert.That(config["Setting3"], Is.Null);
    }

    [Test]
    public void AddJsonSection_WithAnalyzerFilterPath_ShouldExtractMatchingAnalyzerSettings()
    {
        // Arrange
        var json = @"{
                ""Analyzers"": [
                    {
                        ""Name"": ""TestAnalyzer"",
                        ""Settings"": {
                            ""MaxRetries"": 3,
                            ""Timeout"": 5000
                        }
                    },
                    {
                        ""Name"": ""OtherAnalyzer"",
                        ""Settings"": {
                            ""MaxRetries"": 5
                        }
                    }
                ]
            }";
        File.WriteAllText(testFilePath, json);

        var builder = new ConfigurationBuilder();

        // Act
        builder.AddJsonSection(testFilePath, "$.Analyzers[?(@.Name == 'TestAnalyzer')].Settings", false);
        var config = builder.Build();

        // Assert
        Assert.That(config["MaxRetries"], Is.EqualTo("3"));
        Assert.That(config["Timeout"], Is.EqualTo("5000"));
    }

    [Test]
    public void AddJsonSection_WithNonMatchingAnalyzerName_ShouldReturnEmptyConfiguration()
    {
        // Arrange
        var json = @"{
                ""Analyzers"": [
                    {
                        ""Name"": ""TestAnalyzer"",
                        ""Settings"": {
                            ""MaxRetries"": 3
                        }
                    }
                ]
            }";
        File.WriteAllText(testFilePath, json);

        var builder = new ConfigurationBuilder();

        // Act
        builder.AddJsonSection(testFilePath, "$.Analyzers[?(@.Name == 'NonExistent')].Settings", false);
        var config = builder.Build();

        // Assert
        Assert.That(config.AsEnumerable().Count(), Is.EqualTo(0));
    }

    [Test]
    public void AddJsonSection_WithMissingGlobalSection_ShouldReturnEmptyConfiguration()
    {
        // Arrange
        var json = @"{
                ""Other"": {
                    ""Setting1"": ""Value1""
                }
            }";
        File.WriteAllText(testFilePath, json);

        var builder = new ConfigurationBuilder();

        // Act
        builder.AddJsonSection(testFilePath, "$.Global", false);
        var config = builder.Build();

        // Assert
        Assert.That(config.AsEnumerable().Count(), Is.EqualTo(0));
    }

    [Test]
    public void AddJsonSection_WithOptionalMissingFile_ShouldNotThrow()
    {
        // Arrange
        var builder = new ConfigurationBuilder();
        var nonExistentPath = Path.Combine(testDirectory, "nonexistent.json");

        // Act & Assert
        Assert.DoesNotThrow(() => builder.AddJsonSection(nonExistentPath, "$.Global", true));
    }

    [Test]
    public void AddJsonSection_WithRequiredMissingFile_ShouldThrow()
    {
        // Arrange
        var builder = new ConfigurationBuilder();
        var nonExistentPath = Path.Combine(testDirectory, "nonexistent.json");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => builder.AddJsonSection(nonExistentPath, "$.Global", false));
    }

    [Test]
    public void AddJsonSection_WithNullBuilder_ShouldThrowArgumentNullException()
    {
        // Arrange
        IConfigurationBuilder builder = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => builder.AddJsonSection(testFilePath, "$.Global", false));
    }

    [Test]
    public void AddJsonSection_WithComplexNestedSettings_ShouldPreserveStructure()
    {
        // Arrange
        var json = @"{
                ""Global"": {
                    ""Database"": {
                        ""ConnectionString"": ""Server=localhost"",
                        ""Timeout"": 30
                    },
                    ""Features"": {
                        ""EnableCache"": true,
                        ""CacheSize"": 100
                    }
                }
            }";
        File.WriteAllText(testFilePath, json);

        var builder = new ConfigurationBuilder();

        // Act
        builder.AddJsonSection(testFilePath, "$.Global", false);
        var config = builder.Build();

        // Assert
        Assert.That(config["Database:ConnectionString"], Is.EqualTo("Server=localhost"));
        Assert.That(config["Database:Timeout"], Is.EqualTo("30"));
        Assert.That(config["Features:EnableCache"], Is.EqualTo("True"));
        Assert.That(config["Features:CacheSize"], Is.EqualTo("100"));
    }

    [Test]
    public void AddJsonSection_WithAnalyzerArraySettings_ShouldPreserveArrays()
    {
        // Arrange
        var json = @"{
                ""Analyzers"": [
                    {
                        ""Name"": ""TestAnalyzer"",
                        ""Settings"": {
                            ""AllowedValues"": [""A"", ""B"", ""C""],
                            ""MaxItems"": 3
                        }
                    }
                ]
            }";
        File.WriteAllText(testFilePath, json);

        var builder = new ConfigurationBuilder();

        // Act
        builder.AddJsonSection(testFilePath, "$.Analyzers[?(@.Name == 'TestAnalyzer')].Settings", false);
        var config = builder.Build();

        // Assert
        Assert.That(config["AllowedValues:0"], Is.EqualTo("A"));
        Assert.That(config["AllowedValues:1"], Is.EqualTo("B"));
        Assert.That(config["AllowedValues:2"], Is.EqualTo("C"));
        Assert.That(config["MaxItems"], Is.EqualTo("3"));
    }

    [Test]
    public void AddJsonSection_WithEmptyAnalyzersArray_ShouldReturnEmptyConfiguration()
    {
        // Arrange
        var json = @"{
                ""Analyzers"": []
            }";
        File.WriteAllText(testFilePath, json);

        var builder = new ConfigurationBuilder();

        // Act
        builder.AddJsonSection(testFilePath, "$.Analyzers[?(@.Name == 'TestAnalyzer')].Settings", false);
        var config = builder.Build();

        // Assert
        Assert.That(config.AsEnumerable().Count(), Is.EqualTo(0));
    }

    [Test]
    public void AddJsonSection_WithAnalyzerMissingSettings_ShouldReturnEmptyConfiguration()
    {
        // Arrange
        var json = @"{
                ""Analyzers"": [
                    {
                        ""Name"": ""TestAnalyzer""
                    }
                ]
            }";
        File.WriteAllText(testFilePath, json);

        var builder = new ConfigurationBuilder();

        // Act
        builder.AddJsonSection(testFilePath, "$.Analyzers[?(@.Name == 'TestAnalyzer')].Settings", false);
        var config = builder.Build();

        // Assert
        Assert.That(config.AsEnumerable().Count(), Is.EqualTo(0));
    }

    [Test]
    public void AddJsonSection_WithSpecialCharactersInValues_ShouldPreserveValues()
    {
        // Arrange
        var json = @"{
                ""Global"": {
                    ""ConnectionString"": ""Server=localhost;Database=test;User=admin@test.com"",
                    ""Path"": ""C:\\Program Files\\Test"",
                    ""Message"": ""Hello\nWorld\t!""
                }
            }";
        File.WriteAllText(testFilePath, json);

        var builder = new ConfigurationBuilder();

        // Act
        builder.AddJsonSection(testFilePath, "$.Global", false);
        var config = builder.Build();

        // Assert
        Assert.That(config["ConnectionString"], Is.EqualTo("Server=localhost;Database=test;User=admin@test.com"));
        Assert.That(config["Path"], Is.EqualTo("C:\\Program Files\\Test"));
        Assert.That(config["Message"], Is.EqualTo("Hello\nWorld\t!"));
    }
}