namespace Audis.Analyzer.Common.V1
{
    public class AdaptiveCardResultDto : AnalyzerResultDto
    {
        public string Title { get; set; }

        public AdaptiveCardType Type { get; set; }

        /// <summary>
        /// Set to <see langword="true"/>, if the next process step should remove this adaptive card.
        /// </summary>
        public bool RemoveWithNextProcessStep { get; set; }

        public string AdaptiveCardJson { get; set; }
    }
}
