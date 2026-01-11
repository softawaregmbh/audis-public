using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Audis.Analyzer.Common.Utils;
using NUnit.Framework;

namespace Audis.Analyzer.Common.Tests.Utils;

[TestFixture]
public class AdaptiveCardUtilsTests
{
    private string testDirectory;

    [SetUp]
    public void SetUp()
    {
        testDirectory = Path.Combine(Path.GetTempPath(), $"AudisTests_{Guid.NewGuid()}");
        Directory.CreateDirectory(testDirectory);
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
    public async Task ConstructAsync_WithSingleTemplate_ShouldReturnValidAdaptiveCard()
    {
        // Arrange
        var templatePath = Path.Combine(testDirectory, "template1.json");
        var templateContent = @"[
                {
                    ""type"": ""TextBlock"",
                    ""text"": ""${title}""
                }
            ]";
        File.WriteAllText(templatePath, templateContent);

        var context = new { title = "Test Title" };
        var cardTemplates = new List<AbstractedAdaptiveCard>
        {
            new(templatePath, context)
        };

        // Act
        var result = await AdaptiveCardUtils.ConstructAsync(cardTemplates);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Not.Empty);

        var doc = JsonDocument.Parse(result);
        var root = doc.RootElement;

        Assert.That(root.GetProperty("type").GetString(), Is.EqualTo("AdaptiveCard"));
        Assert.That(root.GetProperty("$schema").GetString(),
            Is.EqualTo("http://adaptivecards.io/schemas/adaptive-card.json"));
        Assert.That(root.GetProperty("version").GetString(), Is.EqualTo("1.2"));
        Assert.That(root.GetProperty("body").GetArrayLength(), Is.EqualTo(1));
        Assert.That(root.GetProperty("body")[0].GetProperty("type").GetString(), Is.EqualTo("TextBlock"));
        Assert.That(root.GetProperty("body")[0].GetProperty("text").GetString(), Is.EqualTo("Test Title"));
    }

    [Test]
    public async Task ConstructAsync_WithMultipleTemplates_ShouldMergeBodyElements()
    {
        // Arrange
        var templatePath1 = Path.Combine(testDirectory, "template1.json");
        var templateContent1 = @"[
                {
                    ""type"": ""TextBlock"",
                    ""text"": ""${title}""
                }
            ]";
        File.WriteAllText(templatePath1, templateContent1);

        var templatePath2 = Path.Combine(testDirectory, "template2.json");
        var templateContent2 = @"[
                {
                    ""type"": ""TextBlock"",
                    ""text"": ""${description}""
                }
            ]";
        File.WriteAllText(templatePath2, templateContent2);

        var context1 = new { title = "First Title" };
        var context2 = new { description = "Second Description" };
        var cardTemplates = new List<AbstractedAdaptiveCard>
        {
            new(templatePath1, context1),
            new(templatePath2, context2)
        };

        // Act
        var result = await AdaptiveCardUtils.ConstructAsync(cardTemplates);

        // Assert
        var doc = JsonDocument.Parse(result);
        var body = doc.RootElement.GetProperty("body");

        Assert.That(body.GetArrayLength(), Is.EqualTo(2));
        Assert.That(body[0].GetProperty("text").GetString(), Is.EqualTo("First Title"));
        Assert.That(body[1].GetProperty("text").GetString(), Is.EqualTo("Second Description"));
    }

    [Test]
    public async Task ConstructAsync_WithEmptyTemplate_ShouldReturnCardWithEmptyBody()
    {
        // Arrange
        var templatePath = Path.Combine(testDirectory, "empty.json");
        var templateContent = @"[]";
        File.WriteAllText(templatePath, templateContent);

        var cardTemplates = new List<AbstractedAdaptiveCard>
        {
            new(templatePath, new { })
        };

        // Act
        var result = await AdaptiveCardUtils.ConstructAsync(cardTemplates);

        // Assert
        var doc = JsonDocument.Parse(result);
        var body = doc.RootElement.GetProperty("body");

        Assert.That(body.GetArrayLength(), Is.EqualTo(0));
    }

    [Test]
    public async Task ConstructAsync_WithComplexNestedElements_ShouldPreserveStructure()
    {
        // Arrange
        var templatePath = Path.Combine(testDirectory, "complex.json");
        var templateContent = @"[
                {
                    ""type"": ""Container"",
                    ""items"": [
                        {
                            ""type"": ""TextBlock"",
                            ""text"": ""${header}""
                        },
                        {
                            ""type"": ""ColumnSet"",
                            ""columns"": [
                                {
                                    ""type"": ""Column"",
                                    ""items"": [
                                        {
                                            ""type"": ""TextBlock"",
                                            ""text"": ""${value1}""
                                        }
                                    ]
                                },
                                {
                                    ""type"": ""Column"",
                                    ""items"": [
                                        {
                                            ""type"": ""TextBlock"",
                                            ""text"": ""${value2}""
                                        }
                                    ]
                                }
                            ]
                        }
                    ]
                }
            ]";
        File.WriteAllText(templatePath, templateContent);

        var context = new { header = "Header Text", value1 = "Value 1", value2 = "Value 2" };
        var cardTemplates = new List<AbstractedAdaptiveCard>
        {
            new(templatePath, context)
        };

        // Act
        var result = await AdaptiveCardUtils.ConstructAsync(cardTemplates);

        // Assert
        var doc = JsonDocument.Parse(result);
        var body = doc.RootElement.GetProperty("body");

        Assert.That(body.GetArrayLength(), Is.EqualTo(1));
        Assert.That(body[0].GetProperty("type").GetString(), Is.EqualTo("Container"));
        Assert.That(body[0].GetProperty("items").GetArrayLength(), Is.EqualTo(2));
        Assert.That(body[0].GetProperty("items")[0].GetProperty("text").GetString(), Is.EqualTo("Header Text"));

        var columnSet = body[0].GetProperty("items")[1];
        Assert.That(columnSet.GetProperty("type").GetString(), Is.EqualTo("ColumnSet"));
        Assert.That(columnSet.GetProperty("columns").GetArrayLength(), Is.EqualTo(2));
    }

    [Test]
    public async Task ConstructAsync_OutputShouldBeValidJson()
    {
        // Arrange
        var templatePath = Path.Combine(testDirectory, "template.json");
        var templateContent = @"[
                {
                    ""type"": ""TextBlock"",
                    ""text"": ""Test with special chars: \""quotes\"", \\backslash\\, /slash/""
                }
            ]";
        File.WriteAllText(templatePath, templateContent);

        var cardTemplates = new List<AbstractedAdaptiveCard>
        {
            new(templatePath, new { })
        };

        // Act
        var result = await AdaptiveCardUtils.ConstructAsync(cardTemplates);

        // Assert - Should not throw
        Assert.DoesNotThrow(() => JsonDocument.Parse(result));
    }

    [Test]
    public async Task ConstructAsync_WithMultipleElementsInSingleTemplate_ShouldIncludeAllElements()
    {
        // Arrange
        var templatePath = Path.Combine(testDirectory, "multi-element.json");
        var templateContent = @"[
                {
                    ""type"": ""TextBlock"",
                    ""text"": ""First Block""
                },
                {
                    ""type"": ""TextBlock"",
                    ""text"": ""Second Block""
                },
                {
                    ""type"": ""TextBlock"",
                    ""text"": ""Third Block""
                }
            ]";
        File.WriteAllText(templatePath, templateContent);

        var cardTemplates = new List<AbstractedAdaptiveCard>
        {
            new(templatePath, new { })
        };

        // Act
        var result = await AdaptiveCardUtils.ConstructAsync(cardTemplates);

        // Assert
        var doc = JsonDocument.Parse(result);
        var body = doc.RootElement.GetProperty("body");

        Assert.That(body.GetArrayLength(), Is.EqualTo(3));
        Assert.That(body[0].GetProperty("text").GetString(), Is.EqualTo("First Block"));
        Assert.That(body[1].GetProperty("text").GetString(), Is.EqualTo("Second Block"));
        Assert.That(body[2].GetProperty("text").GetString(), Is.EqualTo("Third Block"));
    }

    [Test]
    public async Task ConstructAsync_WithDataBinding_ShouldReplaceVariables()
    {
        // Arrange
        var templatePath = Path.Combine(testDirectory, "databinding.json");
        var templateContent = @"[
                {
                    ""type"": ""TextBlock"",
                    ""text"": ""Hello ${name}, you have ${count} items""
                }
            ]";
        File.WriteAllText(templatePath, templateContent);

        var context = new { name = "John", count = 42 };
        var cardTemplates = new List<AbstractedAdaptiveCard>
        {
            new(templatePath, context)
        };

        // Act
        var result = await AdaptiveCardUtils.ConstructAsync(cardTemplates);

        // Assert
        var doc = JsonDocument.Parse(result);
        var text = doc.RootElement.GetProperty("body")[0].GetProperty("text").GetString();

        Assert.That(text, Is.EqualTo("Hello John, you have 42 items"));
    }
}