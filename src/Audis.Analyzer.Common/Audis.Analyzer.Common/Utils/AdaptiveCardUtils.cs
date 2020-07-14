using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AdaptiveCards.Templating;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Audis.Analyzer.Common.Utils
{
    public class AdaptiveCardUtils
    {
        /// <summary>
        /// Constructs a new adaptive card based on at least one adaptive card template.
        /// </summary>
        /// <param name="cardTemplates">
        /// A <see cref="IEnumerable{T}"/> containing at least one <see cref="System.ValueTuple"/> bag,
        /// symbolizing one adaptive card instance used to construct the new adaptive card.
        /// The first bag-element is the path to the adaptive card template, the second element is
        /// the data object that should be applied on this template.
        /// </param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> returning the constructed adaptive card as serialized JSON string.
        /// </returns>
        public static async Task<string> ConstructAsync(IEnumerable<(string templatePath, object data)> cardTemplates)
        {
            var body = new JArray();
            foreach (var (templatePath, evaluationContext) in cardTemplates)
            {
                string cardString;
                using (var templateStream = File.OpenText(templatePath))
                {
                    var templateString = await templateStream.ReadToEndAsync();
                    cardString = new AdaptiveCardTemplate(templateString)
                        .Expand(evaluationContext);
                }

                body.Merge(JArray.Parse(cardString));
            }

            var baseCardString = @"
{
  ""type"": ""AdaptiveCard"",
  ""$schema"": ""http://adaptivecards.io/schemas/adaptive-card.json"",
  ""version"": ""1.2""
}";

            var adaptiveCard = JObject.Parse(baseCardString);
            adaptiveCard["body"] = body;
            return JsonConvert.SerializeObject(adaptiveCard);
        }
    }
}
