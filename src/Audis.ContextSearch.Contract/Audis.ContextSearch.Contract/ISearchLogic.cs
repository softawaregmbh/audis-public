namespace Audis.ContextSearch.Contract;

public interface ISearchLogic<T>
{
    Task<IEnumerable<SearchResult<T>>> Search(
        string searchText,
        IEnumerable<SearchItem<T>> searchableItems,
        int takeCount = 5);
}
