using System;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Audis.Primitives.Tests
{
    [TestFixture]
    public class PrimitiveTypeConverterTests
    {
        [Test]
        public void StringSerialization()
        {
            var knowledgeValue = new KnowledgeValue("asdf");
            var dto = new StringDto { Primitive = knowledgeValue };

            var json = JsonConvert.SerializeObject(dto);
            var newDto = JsonConvert.DeserializeObject<StringDto>(json);

            Assert.That(json, Is.EqualTo("{\"Primitive\":\"asdf\"}"));
            Assert.That(newDto.Primitive, Is.EqualTo(dto.Primitive));
            Assert.That(newDto.Primitive!.Value, Is.EqualTo(knowledgeValue.Value));
        }

        [Test]
        public void KnowledgeValueWithDateTimeCanBeSerializedAndDeserialized()
        {
            var date = new DateTime(2020, 11, 11, 11, 11, 11);
            var knowledgeValue = new KnowledgeValue(date.ToString("o"));
            var json = JsonConvert.SerializeObject(knowledgeValue);

            var deserializedKnowledegeValue = JsonConvert.DeserializeObject<KnowledgeValue>(json);

            Assert.That(json, Is.EqualTo("\"2020-11-11T11:11:11.0000000\""));
            Assert.That(deserializedKnowledegeValue, Is.EqualTo(knowledgeValue));
            Assert.That(deserializedKnowledegeValue.Value, Is.EqualTo(date.ToString("o")));
        }

        public class StringDto
        {
            public KnowledgeValue? Primitive { get; set; }
        }
    }
}
