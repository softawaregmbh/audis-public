namespace Audis.Analyzer.Common.Utils
{
    /// <summary>
    /// A instance of this class symbolizes a concrete adaptive card, constructed
    /// by applying the evaluation context to a certain template identified by
    /// a certain path.
    /// </summary>
    public class AbstractedAdaptiveCard
    {
        /// <summary>
        /// Gets or sets the template path.
        /// </summary>
        public string TemplatePath { get; set; }

        /// <summary>
        /// Gets or sets the evaluation context used to expand the adaptive card template.
        /// </summary>
        public object EvaluationContext { get; set; }
    }
}
