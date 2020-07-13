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
        public static async Task<string> ConstructAsync(IReadOnlyCollection<string> templatePaths)
        {
            var baseCardString = @"
{{
  ""type"": ""AdaptiveCard"",
  ""$schema"": ""http://adaptivecards.io/schemas/adaptive-card.json"",
  ""version"": ""1.2""
}}";

            var adaptiveCard = JObject.Parse(baseCardString);

            var body = new JArray();
            foreach (var templatePath in templatePaths)
            {
                using (var templateStream = new StreamReader(File.OpenRead(templatePath)))
                {
                    body.Merge(JArray.Parse(await templateStream.ReadToEndAsync()));
                }
            }

            adaptiveCard["body"] = body;

            return JsonConvert.SerializeObject(adaptiveCard);
        }

        public static string ExpandTemplate(string cardTemplateString, JObject evaluationContextRoot)
        {
            var cardTemplate = new AdaptiveCardTemplate(cardTemplateString);
            return cardTemplate.Expand(new EvaluationContext(evaluationContextRoot));
        }
    }
}
