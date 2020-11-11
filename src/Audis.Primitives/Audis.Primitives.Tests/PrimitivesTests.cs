using System;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Audis.Primitives.Tests
{
    [TestFixture]
    public class PrimitivesTests
    {
        [TestCase("RD4", "RD4")]
        [TestCase("@RD4", "RD4")]
        [TestCase("@rd4", "rd4")]
        public void TestDispositionCode(string input, string expected)
        {
            Assert.AreEqual(expected, new DispositionCode(input).Value);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        [TestCase("@")]
        public void TestEmptyDispositionCodeThrows(string input)
        {
            Assert.Throws<ArgumentNullException>(() => new DispositionCode(input));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void TestEmptyKnowledgeIdentifierThrows(string input)
        {
            Assert.Throws<ArgumentNullException>(() => new KnowledgeIdentifier(input));
        }

        [Test]
        public void TestKnowledgeIdentifierInvalidFormat()
        {
            var ex = Assert.Throws<ArgumentException>(() => new KnowledgeIdentifier("no-leading-hashtag"));
            Assert.AreEqual("The KnowledgeIdentifier has an invalid format: \"no-leading-hashtag\", Expected starting with #.", ex.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void TestEmptyKnowledgeValueThrows(string input)
        {
            Assert.Throws<ArgumentNullException>(() => new KnowledgeValue(input));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void TestEmptyQuestionIdThrows(string input)
        {
            Assert.Throws<ArgumentNullException>(() => new QuestionId(input));
        }

        [TestCase("invalid-format", true)]
        [TestCase("abcde:", true)]
        [TestCase(":1", true)]
        [TestCase("abcde:0", true)]
        [TestCase("abcde:7", false)]
        [TestCase("abcde:10", false)]
        public void TestQuestionIdInvalidFormat(string input, bool throws)
        {
            if (throws)
            {
                var ex = Assert.Throws<ArgumentException>(() => new QuestionId(input));
                Assert.AreEqual($"The QuestionId has an invalid format: \"{input}\", Expected \"<question-catalog-name>:<lineNumber>\".", ex.Message);
            }
            else
            {
                var questionId = new QuestionId(input);
                Assert.AreEqual(input, questionId.Value);
                Assert.AreEqual(new QuestionCatalogName(input.Split(':')[0]), questionId.QuestionCatalogName);
            }
        }

        [Test]
        public void TestQuestionIdQuestionCatalogAndLineNumberConstructor()
        {
            var questionId = new QuestionId(new QuestionCatalogName("question-catalog"), 10);
            Assert.That(questionId.Value, Is.EqualTo("question-catalog:10"));
            Assert.AreEqual(new QuestionCatalogName(questionId.Value.Split(':')[0]), questionId.QuestionCatalogName);
            Assert.AreEqual(int.Parse(questionId.Value.Split(':')[1]), questionId.LineNumber);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void TestEmptyAnswerIdThrows(string input)
        {
            Assert.Throws<ArgumentNullException>(() => new AnswerId(input));
        }

        [TestCase("invalid-format", true)]
        [TestCase("abcde:", true)]
        [TestCase(":1", true)]
        [TestCase("abcde:0", true)]
        [TestCase("abcde:7", true)]
        [TestCase("abcde:7/", true)]
        [TestCase("abcde:7/1", false)]
        [TestCase("abcde:7/3", false)]
        [TestCase("abcde:10/3", false)]
        public void TestAnswerIdInvalidFormat(string input, bool throws)
        {
            if (throws)
            {
                var ex = Assert.Throws<ArgumentException>(() => new AnswerId(input));
                Assert.AreEqual($"The AnswerId has an invalid format: \"{input}\", Expected \"<question-catalog-name>:<questionLineNumber>/<answerLineNumber>\".", ex.Message);
            }
            else
            {
                var answerId = new AnswerId(input);
                Assert.AreEqual(input, answerId.Value);
                var split = input.Split('/');
                Assert.AreEqual(split[0], answerId.QuestionId.Value);
            }
        }

        [Test]
        public void TestAnswerIdQuestionIdAndLineNumberConstructor()
        {
            var answerId = new AnswerId(new QuestionId(new QuestionCatalogName("question-catalog"), 10), 12);
            Assert.That(answerId.Value, Is.EqualTo("question-catalog:10/12"));
            Assert.AreEqual(new QuestionId(answerId.Value.Split('/')[0]), answerId.QuestionId);
            Assert.AreEqual(int.Parse(answerId.Value.Split('/')[1]), answerId.LineNumber);
        }

        [TestCase("#audis.schmerzen", "#audis.schmerzen", true)]
        [TestCase("#audis.SCHMERZEN", "#audis.schmerzen", true)]
        [TestCase("#audis.schmerzen", "#audis.SCHMERZEN", true)]
        [TestCase("#audis.value1", "#audis.value2", false)]
        public void AssertKnowledgeIdentifierCaseInsensitiveEqualsAndGetHashCode(string input1, string input2, bool equals)
        {
            var value1 = new KnowledgeIdentifier(input1);
            var value2 = new KnowledgeIdentifier(input2);

            Assert.AreEqual(equals, value1.Equals(value2));
            Assert.AreEqual(equals, value1.GetHashCode() == value2.GetHashCode());
        }

        [TestCase("Ja", "Ja", true)]
        [TestCase("ja", "Ja", true)]
        [TestCase("Ja", "ja", true)]
        [TestCase("ja", "ja", true)]
        [TestCase("ja", "nein", false)]
        public void AssertKnowledgeValueCaseInsensitiveEqualsAndGetHashCode(string input1, string input2, bool equals)
        {
            var v = new KnowledgeValue("asdf");
            var value1 = new KnowledgeValue(input1);
            var value2 = new KnowledgeValue(input2);

            Assert.AreEqual(equals, value1.Equals(value2));
            Assert.AreEqual(equals, value1.GetHashCode() == value2.GetHashCode());
        }

        [TestCase("@RD1", "@RD1", true)]
        [TestCase("@RD1", "RD1", true)]
        [TestCase("RD1", "@RD1", true)]
        [TestCase("RD1", "RD1", true)]
        [TestCase("rd1", "RD1", true)]
        [TestCase("RD1", "rd1", true)]
        [TestCase("@RD1", "@RD2", false)]
        public void AssertDispositionCodeCaseInsensitiveEqualsAndGetHashCode(string input1, string input2, bool equals)
        {
            var value1 = new DispositionCode(input1);
            var value2 = new DispositionCode(input2);

            Assert.AreEqual(equals, value1.Equals(value2));
            Assert.AreEqual(equals, value1.GetHashCode() == value2.GetHashCode());
        }

        [TestCase("@Scenario", "@Scenario", true)]
        [TestCase("@Scenario", "Scenario", true)]
        [TestCase("Scenario", "@Scenario", true)]
        [TestCase("Scenario", "Scenario", true)]
        [TestCase("scenario", "SCENARIO", true)]
        [TestCase("SCENARIO", "scenario", true)]
        [TestCase("@Scenario1", "@Scenario2", false)]
        public void AssertScenarioCaseInsensitiveEqualsAndGetHashCode(string input1, string input2, bool equals)
        {
            var value1 = new ScenarioIdentifier(input1);
            var value2 = new ScenarioIdentifier(input2);

            Assert.AreEqual(equals, value1.Equals(value2));
            Assert.AreEqual(equals, value1.GetHashCode() == value2.GetHashCode());
        }

        [Test]
        public void KnowledgeValueWithDateTimeCanBeSerializedAndDeserialized()
        {
            var knowledgeValue = KnowledgeValue.From(DateTime.Now.ToString("o"));
            var json = JsonConvert.SerializeObject(knowledgeValue);
            var deserializedKnowledegeValue = JsonConvert.DeserializeObject<KnowledgeValue>(json);            
            Assert.AreEqual(knowledgeValue, deserializedKnowledegeValue);
        }
    }
}