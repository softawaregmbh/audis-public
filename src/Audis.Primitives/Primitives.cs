#pragma warning disable SA1649 // File name should match first type name
#pragma warning disable SA1402 // File may only contain a single type
#pragma warning disable SA1502 // Element should not be on a single line
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace Audis.Primitives
{
    [TypeConverter(typeof(ValueOfTypeConverter<string, KnowledgeValue>))]
    public class KnowledgeValue : CaseInsensitiveValueOfString<KnowledgeValue> { }

    [TypeConverter(typeof(ValueOfTypeConverter<string, TenantId>))]
    public class TenantId : CaseInsensitiveValueOfString<TenantId> { }

    [TypeConverter(typeof(ValueOfTypeConverter<string, RevisionId>))]
    public class RevisionId : CaseInsensitiveValueOfString<RevisionId> { }

    [TypeConverter(typeof(ValueOfTypeConverter<string, QuestionCatalogName>))]
    public class QuestionCatalogName : CaseInsensitiveValueOfString<QuestionCatalogName> { }

    [TypeConverter(typeof(ValueOfTypeConverter<string, Operator>))]
    public class Operator : ValueOf<string, Operator>
    {
        public static Operator AndOperator = Operator.From("&&");
        public static Operator OrOperator = Operator.From("||");
        public static Operator EqualsOperator = Operator.From("=");
        public static Operator UnequalsOperator = Operator.From("!=");
    }

    [TypeConverter(typeof(ValueOfTypeConverter<string, KnowledgeIdentifier>))]
    public class KnowledgeIdentifier : CaseInsensitiveValueOfString<KnowledgeIdentifier>
    {
        protected override void Validate()
        {
            base.Validate();

            if (!this.Value.StartsWith("#"))
            {
                throw new ArgumentException($"The {nameof(KnowledgeIdentifier)} has an invalid format: \"{this.Value}\", Expected starting with #.");
            }
        }
    }

    /// <summary>
    /// Identifies a question with <see cref="QuestionConfiguration.ConfigurationName"/> and <see cref="QuestionConfiguration.LineNumber"/> ("questionConfigurationName:LineNumber").
    /// </summary>
    [TypeConverter(typeof(ValueOfTypeConverter<string, QuestionId>))]
    public class QuestionId : CaseInsensitiveValueOfString<QuestionId>
    {
        protected override void Validate()
        {
            base.Validate();

            if (!Regex.IsMatch(this.Value, @"[\wäüöß-]+:[1-9]\d*")) // configurationName:anyNonZeroDigit
            {
                throw new ArgumentException($"The {nameof(QuestionId)} has an invalid format: \"{this.Value}\", Expected \"<configuration-name>:<lineNumber>\".");
            }

            this.ConfigurationName = QuestionCatalogName.From(this.Value.Split(':')[0]);
        }

        public QuestionCatalogName ConfigurationName { get; private set; }

        public static QuestionId From(string configurationName, int lineNumber)
        {
            return QuestionId.From($"{configurationName}:{lineNumber}");
        }
    }

    /// <summary>
    /// Identifies an answer with <see cref="QuestionConfiguration.ConfigurationName"/>, <see cref="QuestionConfiguration.LineNumber"/>
    /// and <see cref="AnswerConfiguration.KnowledgeValue"/> ("questionConfigurationName:LineNumber/KnowledgeValue").
    /// </summary>
    [TypeConverter(typeof(ValueOfTypeConverter<string, AnswerId>))]
    public class AnswerId : CaseInsensitiveValueOfString<AnswerId>
    {
        protected override void Validate()
        {
            base.Validate();

            if (!Regex.IsMatch(this.Value, @"[\wäüöß-]+:[1-9]\d*\/[\wäüöß ]+")) // e.g. "abcde:10/ja
            {
                throw new ArgumentException($"The {nameof(AnswerId)} has an invalid format: \"{this.Value}\", Expected \"<configuration-name>:<lineNumber>/<knowledge-value>\".");
            }

            var split = this.Value.Split('/');
            this.QuestionId = QuestionId.From(split[0]);
            this.KnowledgeValue = KnowledgeValue.From(split[1]);
        }

        public QuestionId QuestionId { get; private set; }
        public KnowledgeValue KnowledgeValue { get; private set; }

        public static AnswerId From(QuestionId questionId, KnowledgeValue knowledgeValue)
        {
            return AnswerId.From($"{questionId.Value}/{knowledgeValue.Value}");
        }
    }

    [TypeConverter(typeof(ValueOfTypeConverter<string, DispositionCode>))]
    public class DispositionCode : CaseInsensitiveValueOfString<DispositionCode>
    {
        protected override string Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value[0] == '@')
            {
                value = value.Substring(1);
            }

            return value;
        }
    }

    [TypeConverter(typeof(ValueOfTypeConverter<string, ScenarioIdentifier>))]
    public class ScenarioIdentifier : CaseInsensitiveValueOfString<ScenarioIdentifier>
    {
        protected override string Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value[0] == '@')
            {
                value = value.Substring(1);
            }

            return value;
        }
    }
}
