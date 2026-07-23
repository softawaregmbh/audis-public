namespace Audis.ContextSearch.Contract.Logics;

public interface IPreconfiguredSearchLogic<T> : ISearchLogic<T>
{
    Task<IEnumerable<SearchResult<T>>> Search(
        string searchText,
        IEnumerable<SearchItem<T>> searchableItems,
        IEnumerable<PreconfiguredApiSearchSetting> preconfiguredSearchSettings,
        int takeCount = 5);
}
