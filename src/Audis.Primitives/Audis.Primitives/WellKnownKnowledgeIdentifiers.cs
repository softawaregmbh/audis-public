namespace Audis.Primitives;

public static class WellKnownKnowledgeIdentifiers
{
    // Meta-Knowledge
    public static readonly KnowledgeIdentifier Dispo = new("#audis.dispo");

    public static readonly KnowledgeIdentifier IntermediateDisposition = new("#audis.intermediate-disposition");

    public static readonly KnowledgeIdentifier Scenarios = new("#audis.scenarios");

    public static readonly KnowledgeIdentifier UserId = new("#audis.userId");

    public static readonly KnowledgeIdentifier Summary = new("#audis.summary");

    public static readonly KnowledgeIdentifier CurrentQuestion = new("#audis.current-question");

    // Initial Knowledge
    public static readonly KnowledgeIdentifier User = new InitialKnowledgeIdentifier("user");
}
