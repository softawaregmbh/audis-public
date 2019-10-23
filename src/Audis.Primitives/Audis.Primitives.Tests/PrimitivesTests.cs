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
            Assert.AreEqual(expected, DispositionCode.From(input).Value);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        [TestCase("@")]
        public void TestEmptyDispositionCodeThrows(string input)
        {
            Assert.Throws<ArgumentNullException>(() => DispositionCode.From(input));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void TestEmptyKnowledgeIdentifierThrows(string input)
        {
            Assert.Throws<ArgumentNullException>(() => KnowledgeIdentifier.From(input));
        }

        [Test]
        public void TestKnowledgeIdentifierInvalidFormat()
        {
            var ex = Assert.Throws<ArgumentException>(() => KnowledgeIdentifier.From("no-leading-hashtag"));
            Assert.AreEqual("The KnowledgeIdentifier has an invalid format: \"no-leading-hashtag\", Expected starting with #.", ex.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void TestEmptyKnowledgeValueThrows(string input)
        {
            Assert.Throws<ArgumentNullException>(() => KnowledgeValue.From(input));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void TestEmptyQuestionIdThrows(string input)
        {
            Assert.Throws<ArgumentNullException>(() => QuestionId.From(input));
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
                var ex = Assert.Throws<ArgumentException>(() => QuestionId.From(input));
                Assert.AreEqual($"The QuestionId has an invalid format: \"{input}\", Expected \"<configuration-name>:<lineNumber>\".", ex.Message);
            }
            else
            {
                var questionId = QuestionId.From(input);
                Assert.AreEqual(input, questionId.Value);
                Assert.AreEqual(QuestionCatalogName.From(input.Split(':')[0]), questionId.ConfigurationName);
            }
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void TestEmptyAnswerIdThrows(string input)
        {
            Assert.Throws<ArgumentNullException>(() => AnswerId.From(input));
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
                var ex = Assert.Throws<ArgumentException>(() => AnswerId.From(input));
                Assert.AreEqual($"The AnswerId has an invalid format: \"{input}\", Expected \"<configuration-name>:<questionLineNumber>/<answerLineNumber>\".", ex.Message);
            }
            else
            {
                var answerId = AnswerId.From(input);
                Assert.AreEqual(input, answerId.Value);
                var split = input.Split('/');
                Assert.AreEqual(split[0], answerId.QuestionId.Value);
            }
        }

        [TestCase("#audis.schmerzen", "#audis.schmerzen", true)]
        [TestCase("#audis.SCHMERZEN", "#audis.schmerzen", true)]
        [TestCase("#audis.schmerzen", "#audis.SCHMERZEN", true)]
        [TestCase("#audis.value1", "#audis.value2", false)]
        public void AssertKnowledgeIdentifierCaseInsensitiveEqualsAndGetHashCode(string input1, string input2, bool equals)
        {
            var value1 = KnowledgeIdentifier.From(input1);
            var value2 = KnowledgeIdentifier.From(input2);

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
            var value1 = KnowledgeValue.From(input1);
            var value2 = KnowledgeValue.From(input2);

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
            var value1 = DispositionCode.From(input1);
            var value2 = DispositionCode.From(input2);

            Assert.AreEqual(equals, value1.Equals(value2));
            Assert.AreEqual(equals, value1.GetHashCode() == value2.GetHashCode());
        }
    }
}
