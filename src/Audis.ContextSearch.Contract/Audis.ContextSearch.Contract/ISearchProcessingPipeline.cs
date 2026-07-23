namespace Audis.ContextSearch.Contract;

public interface ISearchProcessingPipeline
{
    string Process(string inputString, IEnumerable<string>? synonyms = null);

    IEnumerable<string> Process(IEnumerable<string> inputStrings, IEnumerable<string>? synonyms = null);

    void AddProcessor(ISearchProcessor processor);

    void RemoveProcessor(ISearchProcessor processor);

    IEnumerable<ISearchProcessor> GetProcessors();

    void ClearProcessors();
}
