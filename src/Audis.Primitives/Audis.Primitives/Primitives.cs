#pragma warning disable SA1649 // File name should match first type name
using System;
using System.Text.RegularExpressions;

namespace Audis.Primitives
{
    public record TenantId(string Value)
        : CaseInsensitiveStringPrimitive(Value);

    public record KnowledgeValue(string Value)
        : CaseInsensitiveStringPrimitive(Value);

    public record RevisionId(string Value)
        : CaseInsensitiveStringPrimitive(Value);

    public record QuestionCatalogName(string Value)
        : CaseInsensitiveStringPrimitive(Value);

    public record Operator(string Value)
        : Primitive<string>(Value)
    {
        public static readonly Operator AndOperator = new Operator("&&");
        public static readonly Operator OrOperator = new Operator("||");
        public static readonly Operator EqualsOperator = new Operator("=");
        public static readonly Operator UnequalsOperator = new Operator("!=");
    }

    public record KnowledgeIdentifier : CaseInsensitiveStringPrimitive
    {
        public KnowledgeIdentifier(string value)
            : base(value)
        {
            if (!this.Value.StartsWith("#"))
            {
                throw new ArgumentException($"The {nameof(KnowledgeIdentifier)} has an invalid format: \"{this.Value}\", Expected starting with #.");
            }
        }
    }

    /// <summary>
    /// Identifies a question with <see cref="QuestionCatalogName"/> and <see cref="LineNumber"/> ("questionCatalogName:LineNumber").
    /// </summary>
    public record QuestionId
        : CaseInsensitiveStringPrimitive
    {
        public QuestionId(string value)
            : base(value)
        {
            if (!Regex.IsMatch(this.Value, @"[\wäüöß-]+:[1-9]\d*")) // configurationName:anyNonZeroDigit
            {
                throw new ArgumentException($"The {nameof(QuestionId)} has an invalid format: \"{this.Value}\", Expected \"<question-catalog-name>:<lineNumber>\".");
            }

            var split = this.Value.Split(':');
            this.QuestionCatalogName = new QuestionCatalogName(split[0]);
            this.LineNumber = int.Parse(split[1]);
        }

        public QuestionId(QuestionCatalogName questionCatalogName, int lineNumber)
            : this($"{questionCatalogName.Value}:{lineNumber}")
        {
        }

        public int LineNumber { get; }
        public QuestionCatalogName QuestionCatalogName { get; }
    }

    /// <summary>
    /// Identifies an answer with <see cref="QuestionId" /> and <see cref="LineNumber"/> ("questionCatalogName:questionLineNumber/answerLineNumber").
    /// </summary>
    public record AnswerId
        : CaseInsensitiveStringPrimitive
    {
        public AnswerId(string value)
            : base(value)
        {
            if (!Regex.IsMatch(this.Value, @"[\wäüöß-]+:[1-9]\d*\/\d+")) // e.g. "abcde:10/3
            {
                throw new ArgumentException($"The {nameof(AnswerId)} has an invalid format: \"{this.Value}\", Expected \"<question-catalog-name>:<questionLineNumber>/<answerLineNumber>\".");
            }

            var split = this.Value.Split('/');
            this.QuestionId = new QuestionId(split[0]);
            this.LineNumber = int.Parse(split[1]);
        }

        public AnswerId(QuestionId questionId, int lineNumber)
            : this($"{questionId.Value}/{lineNumber}") 
        {
        }

        public QuestionId QuestionId { get; }
        public int LineNumber { get; }
    }

    public record DispositionCode
        : CaseInsensitiveStringPrimitive
    {
        public DispositionCode(string value)
            : base(value)
        {
            if (value[0] == '@')
            {
                if (value.Length == 1)
                {
                    throw new ArgumentNullException("value must not be empty.");
                }

                this.Value = value.Substring(1);
            }
        }
    }

    public record ScenarioIdentifier
        : CaseInsensitiveStringPrimitive
    {
        public ScenarioIdentifier(string value)
            : base(value)
        {
            if (value[0] == '@')
            {
                if (value.Length == 1)
                {
                    throw new ArgumentNullException("value must not be empty.");
                }

                this.Value = value.Substring(1);
            }
        }
    }
}
