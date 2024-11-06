using System;
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
            Assert.That(new DispositionCode(input).Value, Is.EqualTo(expected), string.Empty);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        [TestCase("@")]
        public void TestEmptyDispositionCodeThrows(string? input)
        {
            Assert.Throws<ArgumentNullException>(() => new DispositionCode(input));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void TestEmptyKnowledgeIdentifierThrows(string? input)
        {
            Assert.Throws<ArgumentNullException>(() => new KnowledgeIdentifier(input));
        }

        [Test]
        public void TestKnowledgeIdentifierInvalidFormat()
        {
            var ex = Assert.Throws<ArgumentException>(() => new KnowledgeIdentifier("no-leading-hashtag"));
            Assert.That(ex.Message, Is.EqualTo("The KnowledgeIdentifier has an invalid format: \"no-leading-hashtag\", Expected starting with #."));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void TestEmptyKnowledgeValueThrows(string? input)
        {
            Assert.Throws<ArgumentNullException>(() => new KnowledgeValue(input));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void TestEmptyQuestionIdThrows(string? input)
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
                Assert.That(ex.Message, Is.EqualTo($"The QuestionId has an invalid format: \"{input}\", Expected \"<question-catalog-name>:<lineNumber>\"."));
            }
            else
            {
                var questionId = new QuestionId(input);
                Assert.That(questionId.Value, Is.EqualTo(input));
                Assert.That(questionId.QuestionCatalogName, Is.EqualTo(new QuestionCatalogName(input.Split(':')[0])));
            }
        }

        [Test]
        public void TestQuestionIdQuestionCatalogAndLineNumberConstructor()
        {
            var questionId = new QuestionId(new QuestionCatalogName("question-catalog"), 10);
            Assert.That(questionId.Value, Is.EqualTo("question-catalog:10"));
            Assert.That(questionId.QuestionCatalogName, Is.EqualTo(new QuestionCatalogName(questionId.Value.Split(':')[0])));
            Assert.That(questionId.LineNumber, Is.EqualTo(int.Parse(questionId.Value.Split(':')[1])));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void TestEmptyAnswerIdThrows(string? input)
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
                Assert.That(ex.Message, Is.EqualTo($"The AnswerId has an invalid format: \"{input}\", Expected \"<question-catalog-name>:<questionLineNumber>/<answerLineNumber>\"."));
            }
            else
            {
                var answerId = new AnswerId(input);
                Assert.That(answerId.Value, Is.EqualTo(input));
                var split = input.Split('/');
                Assert.That(answerId.QuestionId.Value, Is.EqualTo(split[0]));
            }
        }

        [Test]
        public void TestAnswerIdQuestionIdAndLineNumberConstructor()
        {
            var answerId = new AnswerId(new QuestionId(new QuestionCatalogName("question-catalog"), 10), 12);
            Assert.That(answerId.Value, Is.EqualTo("question-catalog:10/12"));
            Assert.That(answerId.QuestionId, Is.EqualTo(new QuestionId(answerId.Value.Split('/')[0])));
            Assert.That(answerId.LineNumber, Is.EqualTo(int.Parse(answerId.Value.Split('/')[1])));
        }

        [TestCase("#audis.schmerzen", "#audis.schmerzen", true)]
        [TestCase("#audis.SCHMERZEN", "#audis.schmerzen", true)]
        [TestCase("#audis.schmerzen", "#audis.SCHMERZEN", true)]
        [TestCase("#audis.value1", "#audis.value2", false)]
        public void AssertKnowledgeIdentifierCaseInsensitiveEqualsAndGetHashCode(string input1, string input2, bool equals)
        {
            var value1 = new KnowledgeIdentifier(input1);
            var value2 = new KnowledgeIdentifier(input2);

            Assert.That(value1.Equals(value2), Is.EqualTo(equals));
            Assert.That(value1.GetHashCode() == value2.GetHashCode(), Is.EqualTo(equals));
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

            Assert.That(value1.Equals(value2), Is.EqualTo(equals));
            Assert.That(value1.GetHashCode() == value2.GetHashCode(), Is.EqualTo(equals));
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

            Assert.That(value1.Equals(value2), Is.EqualTo(equals));
            Assert.That(value1.GetHashCode() == value2.GetHashCode(), Is.EqualTo(equals));
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

            Assert.That(value1.Equals(value2), Is.EqualTo(equals));
            Assert.That(value1.GetHashCode() == value2.GetHashCode(), Is.EqualTo(equals));
        }

        [Test]
        public void AssertToStringReturnsValue()
        {
            var name = new QuestionCatalogName("test");
            Assert.That(name.ToString(), Is.EqualTo(name.Value));
        }
    }
}