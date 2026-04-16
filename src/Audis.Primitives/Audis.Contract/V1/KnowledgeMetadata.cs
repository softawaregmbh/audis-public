namespace Audis.Contract.V1;

public class KnowledgeMetadata
{
    public KnowledgeMetadata()
    {
    }

    public KnowledgeMetadata(KnowledgeFlags flags)
    {
        this.Flags = flags;
    }

    public int Priority { get; init; }

    public KnowledgeFlags Flags { get; init; }

    public bool IsUnknownAnswerKnowledge => this.Flags.HasFlag(KnowledgeFlags.UnknownAnswerKnowledge);

    public bool IsKnowledgeSummaryAnswerKnowledge => this.Flags.HasFlag(KnowledgeFlags.KnowledgeSummaryAnswerKnowledge);

    public bool ShouldIgnoreInTimeLine => this.Flags.HasFlag(KnowledgeFlags.IgnoreInTimeLine);

    public bool ShouldIgnoreInSummary => this.Flags.HasFlag(KnowledgeFlags.IgnoreInSummary);

    public bool IsInputAnswerKnowledge => this.Flags.HasFlag(KnowledgeFlags.InputAnswerKnowledge);

    public bool IsStandaloneKnowledge => this.Flags.HasFlag(KnowledgeFlags.StandaloneKnowledge);

    public bool IsProtectedFromVolatileSource => this.Flags.HasFlag(KnowledgeFlags.ProtectedFromVolatileSource);

    public KnowledgeMetadata Clone() =>
        new()
        {
            Flags = this.Flags,
            Priority = this.Priority
        };
}
