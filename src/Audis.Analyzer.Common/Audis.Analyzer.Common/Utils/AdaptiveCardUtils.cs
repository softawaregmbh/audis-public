using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using AdaptiveCards.Templating;

namespace Audis.Analyzer.Common.Utils;

public class AdaptiveCardUtils
{
    /// <summary>
    ///     Constructs a new adaptive card based on at least one adaptive card template.
    /// </summary>
    /// <param name="cardTemplates">
    ///     A <see cref="IEnumerable{T}" /> containing at least one <see cref="AbstractedAdaptiveCard" />,
    ///     symbolizing one adaptive card instance used to construct the new adaptive card.
    /// </param>
    /// <returns>
    ///     A <see cref="Task{TResult}" /> returning the constructed adaptive card as serialized JSON string.
    /// </returns>
    public static async Task<string> ConstructAsync(IEnumerable<AbstractedAdaptiveCard> cardTemplates)
    {
        var body = new JsonArray();
        foreach (var abstractedCard in cardTemplates)
        {
            string cardString;
            using (var templateStream = File.OpenText(abstractedCard.TemplatePath))
            {
                var templateString = await templateStream.ReadToEndAsync();
                cardString = new AdaptiveCardTemplate(templateString)
                    .Expand(abstractedCard.EvaluationContext);
            }

            var cardArray = JsonNode.Parse(cardString) as JsonArray;
            if (cardArray != null)
            {
                foreach (var item in cardArray)
                {
                    body.Add(item?.DeepClone());
                }
            }
        }

        var adaptiveCard = new JsonObject
        {
            ["type"] = "AdaptiveCard",
            ["$schema"] = "http://adaptivecards.io/schemas/adaptive-card.json",
            ["version"] = "1.2",
            ["body"] = body
        };

        return JsonSerializer.Serialize(adaptiveCard);
    }
}