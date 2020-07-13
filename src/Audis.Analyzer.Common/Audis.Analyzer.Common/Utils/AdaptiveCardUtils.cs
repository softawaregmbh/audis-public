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
        public static async Task<string> ConstructAsync(IEnumerable<(string, JObject)> cardTemplates)
        {
            var body = new JArray();
            foreach (var (templatePath, evaluationContext) in cardTemplates)
            {
                string cardString;
                using (var templateStream = new StreamReader(File.OpenRead(templatePath)))
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
