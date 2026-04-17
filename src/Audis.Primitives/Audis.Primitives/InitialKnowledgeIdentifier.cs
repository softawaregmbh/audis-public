using System;
using System.Text.RegularExpressions;

namespace Audis.Primitives;

public record InitialKnowledgeIdentifier : KnowledgeIdentifier
{
    public InitialKnowledgeIdentifier(string suffix)
        : base($"{Constants.InitialKnowledgePrefix}{suffix}")
    {
        if (string.IsNullOrWhiteSpace(suffix))
            throw new ArgumentNullException(nameof(suffix));

        if (!Regex.IsMatch(suffix, @"^[\w][\w.\-]*$"))
            throw new ArgumentException(
                $"The {nameof(InitialKnowledgeIdentifier)} suffix has an invalid format: \"{suffix}\", Expected alphanumeric characters, dots, or hyphens without leading #.");
    }
}
