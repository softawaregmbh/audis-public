namespace Audis.ContextSearch.Contract;

#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
public record SearchItem<T>(string Text, string Context, string[]? Synonyms = null, T? Item = default)
{
    public IEnumerable<(string SearchText, string ResultText)> GetSearchTexts(bool includeSynonymCombinations = true)
    {
        yield return (this.Text, this.Text);
        yield return (this.Context, this.Text);

        for (var i = 0; i < (this.Synonyms?.Length ?? 0); i++)
        {
            yield return (this.Synonyms![i] !, this.Text);

            if (includeSynonymCombinations)
            {
                for (var j = i + 1; j < this.Synonyms.Length; j++)
                {
                    yield return ($"{this.Synonyms[i]} {this.Synonyms[j]}", this.Text);
                }
            }
        }
    }
}

public record SearchItem(string Text, string Context, string[]? Synonyms = null)
    : SearchItem<object>(Text, Context, Synonyms);
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
