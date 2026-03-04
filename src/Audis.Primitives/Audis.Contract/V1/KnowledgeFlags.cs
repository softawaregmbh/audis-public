using System;

namespace Audis.Contract.V1;

[Flags]
public enum KnowledgeFlags : long
{
    UnknownAnswerKnowledge = 1,
    KnowledgeSummaryAnswerKnowledge = 2,
    IgnoreInTimeLine = 4,
    IgnoreInSummary = 8,
    InputAnswerKnowledge = 16
}