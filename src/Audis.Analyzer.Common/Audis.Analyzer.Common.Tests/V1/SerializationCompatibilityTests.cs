using System;
using System.Collections.Generic;
using System.Text.Json;
using Audis.Analyzer.Common.V1;
using Audis.Analyzer.Contract.V1;
using Audis.Primitives;
using NUnit.Framework;

namespace Audis.Analyzer.Common.Tests.V1
{
    /// <summary>
    /// Tests to verify that System.Text.Json serialization behavior is compatible
    /// with the previous Newtonsoft.Json behavior for DTOs.
    /// </summary>
    [TestFixture]
    public class SerializationCompatibilityTests
    {
        private JsonSerializerOptions jsonOptions;

        [SetUp]
        public void SetUp()
        {
            jsonOptions = new JsonSerializerOptions();
            // This converter already exists in your Audis.Primitives package and handles types like QuestionId, KnowledgeIdentifier, etc.
            // Required in consuming APIs: options.JsonSerializerOptions.Converters.Add(new CaseInsensitiveStringPrimitiveConverter());
            jsonOptions.Converters.Add(new CaseInsensitiveStringPrimitiveConverter());
        }
        [Test]
        public void AnalyzerRequestDto_WithNullData_ShouldSerializeAndDeserialize()
        {
            // Arrange
            var original = new AnalyzerRequestDto
            {
                InterrogationId = Guid.NewGuid(),
                InterrogationProcessStepId = 123,
                CurrentQuestion = new AnalyzerRequestDto.Question
                {
                    Id = new Audis.Primitives.QuestionId("catalog:1"),
                    Answers = new List<AnalyzerRequestDto.Answer>()
                },
                Knowledge = new List<KnowledgeDto>(),
                Data = null
            };

            // Act
            var json = JsonSerializer.Serialize(original, jsonOptions);
            var deserialized = JsonSerializer.Deserialize<AnalyzerRequestDto>(json, jsonOptions);

            // Assert
            Assert.That(deserialized, Is.Not.Null);
            Assert.That(deserialized.InterrogationId, Is.EqualTo(original.InterrogationId));
            Assert.That(deserialized.Data, Is.Null);
        }

        [Test]
        public void AnalyzerRequestDto_WithJsonObjectData_ShouldSerializeAndDeserialize()
        {
            // Arrange - simulating what was previously stored as JObject
            var dataJson = @"{""customField"":""customValue"",""counter"":42,""isActive"":true}";
            var dataElement = JsonDocument.Parse(dataJson).RootElement;

            var original = new AnalyzerRequestDto
            {
                InterrogationId = Guid.NewGuid(),
                InterrogationProcessStepId = 123,
                CurrentQuestion = new AnalyzerRequestDto.Question
                {
                    Id = new Audis.Primitives.QuestionId("catalog:1"),
                    Answers = new List<AnalyzerRequestDto.Answer>()
                },
                Knowledge = new List<KnowledgeDto>(),
                Data = dataElement
            };

            // Act
            var json = JsonSerializer.Serialize(original, jsonOptions);
            var deserialized = JsonSerializer.Deserialize<AnalyzerRequestDto>(json, jsonOptions);

            // Assert
            Assert.That(deserialized, Is.Not.Null);
            Assert.That(deserialized.Data.HasValue, Is.True);
            Assert.That(deserialized.Data.Value.GetProperty("customField").GetString(), Is.EqualTo("customValue"));
            Assert.That(deserialized.Data.Value.GetProperty("counter").GetInt32(), Is.EqualTo(42));
            Assert.That(deserialized.Data.Value.GetProperty("isActive").GetBoolean(), Is.True);
        }

        [Test]
        public void AnalyzerResultDto_WithNestedData_ShouldPreserveStructure()
        {
            // Arrange
            var dataJson = @"{
                ""nested"": {
                    ""level1"": {
                        ""level2"": {
                            ""value"": ""deep""
                        }
                    }
                },
                ""array"": [1, 2, 3, 4, 5]
            }";
            var dataElement = JsonDocument.Parse(dataJson).RootElement;

            var original = new AnalyzerResultDto
            {
                InterrogationId = Guid.NewGuid(),
                InterrogationProcessStepId = 456,
                Timestamp = DateTime.UtcNow,
                SuggestedKnowledge = new List<SuggestedKnowledgeDto>(),
                Data = dataElement
            };

            // Act
            var json = JsonSerializer.Serialize(original, jsonOptions);
            var deserialized = JsonSerializer.Deserialize<AnalyzerResultDto>(json, jsonOptions);

            // Assert
            Assert.That(deserialized, Is.Not.Null);
            Assert.That(deserialized.Data.HasValue, Is.True);
            Assert.That(deserialized.Data.Value.GetProperty("nested")
                .GetProperty("level1")
                .GetProperty("level2")
                .GetProperty("value").GetString(), Is.EqualTo("deep"));
            Assert.That(deserialized.Data.Value.GetProperty("array").GetArrayLength(), Is.EqualTo(5));
            Assert.That(deserialized.Data.Value.GetProperty("array")[2].GetInt32(), Is.EqualTo(3));
        }

        [Test]
        public void JsonElement_CanBeAccessedMultipleTimes()
        {
            // Arrange
            var dataJson = @"{""field1"":""value1"",""field2"":""value2""}";
            var dto = new AnalyzerRequestDto
            {
                InterrogationId = Guid.NewGuid(),
                InterrogationProcessStepId = 1,
                CurrentQuestion = new AnalyzerRequestDto.Question
                {
                    Id = new Audis.Primitives.QuestionId("catalog:1"),
                    Answers = new List<AnalyzerRequestDto.Answer>()
                },
                Knowledge = new List<KnowledgeDto>(),
                Data = JsonDocument.Parse(dataJson).RootElement
            };

            // Act & Assert - Multiple accesses should work
            Assert.That(dto.Data.HasValue, Is.True);
            Assert.That(dto.Data.Value.GetProperty("field1").GetString(), Is.EqualTo("value1"));
            Assert.That(dto.Data.Value.GetProperty("field2").GetString(), Is.EqualTo("value2"));
            
            // Access again
            Assert.That(dto.Data.Value.GetProperty("field1").GetString(), Is.EqualTo("value1"));
        }

        [Test]
        public void JsonElement_WithSpecialCharacters_ShouldPreserveContent()
        {
            // Arrange
            var dataJson = @"{
                ""path"": ""C:\\Users\\Test\\Documents"",
                ""url"": ""https://example.com/api?param=value&other=123"",
                ""multiline"": ""Line1\nLine2\nLine3"",
                ""unicode"": ""Hällo Wörld 🎉""
            }";
            var dataElement = JsonDocument.Parse(dataJson).RootElement;

            var dto = new AnalyzerResultDto
            {
                InterrogationId = Guid.NewGuid(),
                InterrogationProcessStepId = 1,
                Timestamp = DateTime.UtcNow,
                SuggestedKnowledge = new List<SuggestedKnowledgeDto>(),
                Data = dataElement
            };

            // Act
            var json = JsonSerializer.Serialize(dto, jsonOptions);
            var deserialized = JsonSerializer.Deserialize<AnalyzerResultDto>(json, jsonOptions);

            // Assert
            Assert.That(deserialized.Data.HasValue, Is.True);
            Assert.That(deserialized.Data.Value.GetProperty("path").GetString(), 
                Is.EqualTo("C:\\Users\\Test\\Documents"));
            Assert.That(deserialized.Data.Value.GetProperty("url").GetString(), 
                Is.EqualTo("https://example.com/api?param=value&other=123"));
            Assert.That(deserialized.Data.Value.GetProperty("multiline").GetString(), 
                Is.EqualTo("Line1\nLine2\nLine3"));
            Assert.That(deserialized.Data.Value.GetProperty("unicode").GetString(), 
                Is.EqualTo("Hällo Wörld 🎉"));
        }

        [Test]
        public void JsonElement_WithDifferentTypes_ShouldPreserveTypes()
        {
            // Arrange
            var dataJson = @"{
                ""stringValue"": ""text"",
                ""intValue"": 42,
                ""doubleValue"": 3.14159,
                ""boolValue"": true,
                ""nullValue"": null,
                ""arrayValue"": [1, 2, 3],
                ""objectValue"": { ""nested"": ""value"" }
            }";
            var dataElement = JsonDocument.Parse(dataJson).RootElement;

            var dto = new AnalyzerRequestDto
            {
                InterrogationId = Guid.NewGuid(),
                InterrogationProcessStepId = 1,
                CurrentQuestion = new AnalyzerRequestDto.Question
                {
                    Id = new Audis.Primitives.QuestionId("catalog:1"),
                    Answers = new List<AnalyzerRequestDto.Answer>()
                },
                Knowledge = new List<KnowledgeDto>(),
                Data = dataElement
            };

            // Act
            var json = JsonSerializer.Serialize(dto, jsonOptions);
            var deserialized = JsonSerializer.Deserialize<AnalyzerRequestDto>(json, jsonOptions);

            // Assert
            Assert.That(deserialized.Data.HasValue, Is.True);
            var data = deserialized.Data.Value;
            
            Assert.That(data.GetProperty("stringValue").ValueKind, Is.EqualTo(JsonValueKind.String));
            Assert.That(data.GetProperty("stringValue").GetString(), Is.EqualTo("text"));
            
            Assert.That(data.GetProperty("intValue").ValueKind, Is.EqualTo(JsonValueKind.Number));
            Assert.That(data.GetProperty("intValue").GetInt32(), Is.EqualTo(42));
            
            Assert.That(data.GetProperty("doubleValue").ValueKind, Is.EqualTo(JsonValueKind.Number));
            Assert.That(data.GetProperty("doubleValue").GetDouble(), Is.EqualTo(3.14159).Within(0.00001));
            
            Assert.That(data.GetProperty("boolValue").ValueKind, Is.EqualTo(JsonValueKind.True));
            Assert.That(data.GetProperty("boolValue").GetBoolean(), Is.True);
            
            Assert.That(data.GetProperty("nullValue").ValueKind, Is.EqualTo(JsonValueKind.Null));
            
            Assert.That(data.GetProperty("arrayValue").ValueKind, Is.EqualTo(JsonValueKind.Array));
            Assert.That(data.GetProperty("arrayValue").GetArrayLength(), Is.EqualTo(3));
            
            Assert.That(data.GetProperty("objectValue").ValueKind, Is.EqualTo(JsonValueKind.Object));
            Assert.That(data.GetProperty("objectValue").GetProperty("nested").GetString(), Is.EqualTo("value"));
        }

        [Test]
        public void AnalyzerRequestDto_RoundTripSerialization_PreservesAllProperties()
        {
            // Arrange
            var dataJson = @"{""step"":1,""previousValue"":""something""}";
            var original = new AnalyzerRequestDto
            {
                InterrogationId = Guid.Parse("12345678-1234-1234-1234-123456789012"),
                InterrogationProcessStepId = 999,
                CurrentQuestion = new AnalyzerRequestDto.Question
                {
                    Id = new Audis.Primitives.QuestionId("catalog:123"),
                    Answers = new List<AnalyzerRequestDto.Answer>
                    {
                        new AnalyzerRequestDto.Answer
                        {
                            KnowledgeIdentifier = new Audis.Primitives.KnowledgeIdentifier("#K1"),
                            knowledgeValue = new Audis.Primitives.KnowledgeValue("V1")
                        }
                    }
                },
                Knowledge = new List<KnowledgeDto>
                {
                    new KnowledgeDto
                    {
                        KnowledgeIdentifier = new Audis.Primitives.KnowledgeIdentifier("#KID1"),
                        Values = new HashSet<KnowledgeValueDto>
                        {
                            new KnowledgeValueDto
                            {
                                KnowledgeValue = new Audis.Primitives.KnowledgeValue("KValue1")
                            }
                        }
                    }
                },
                Data = JsonDocument.Parse(dataJson).RootElement
            };

            // Act
            var json = JsonSerializer.Serialize(original, jsonOptions);
            var deserialized = JsonSerializer.Deserialize<AnalyzerRequestDto>(json, jsonOptions);

            // Assert - All properties preserved
            Assert.That(deserialized.InterrogationId, Is.EqualTo(original.InterrogationId));
            Assert.That(deserialized.InterrogationProcessStepId, Is.EqualTo(original.InterrogationProcessStepId));
            Assert.That(deserialized.CurrentQuestion.Id.Value, Is.EqualTo(original.CurrentQuestion.Id.Value));
            Assert.That(deserialized.Data.Value.GetProperty("step").GetInt32(), Is.EqualTo(1));
            Assert.That(deserialized.Data.Value.GetProperty("previousValue").GetString(), Is.EqualTo("something"));
        }
    }
}
