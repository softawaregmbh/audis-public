namespace Audis.ContextSearch.Contract;

public interface ISearchProcessor
{
    string Process(string inputString, IEnumerable<string>? synonyms = null);
}
