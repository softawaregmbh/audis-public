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

    public KnowledgeFlags Flags { get; init; }

    public bool IsUnknownAnswerKnowledge => this.Flags.HasFlag(KnowledgeFlags.UnknownAnswerKnowledge);

    public bool IsKnowledgeSummaryAnswerKnowledge => this.Flags.HasFlag(KnowledgeFlags.KnowledgeSummaryAnswerKnowledge);
    public bool ShouldIgnoreInTimeLine => this.Flags.HasFlag(KnowledgeFlags.IgnoreInTimeLine);
    public bool ShouldIgnoreInSummary => this.Flags.HasFlag(KnowledgeFlags.IgnoreInSummary);
    public bool IsInputAnswerKnowledge => this.Flags.HasFlag(KnowledgeFlags.InputAnswerKnowledge);

    public int Priority { get; init; }

    public KnowledgeMetadata Clone() =>
        new()
        {
            Flags = this.Flags,
            Priority = this.Priority
        };
}