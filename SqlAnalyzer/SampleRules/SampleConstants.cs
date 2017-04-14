namespace SampleRules
{
    internal static class RuleConstants
    {
        /// <summary>  
        /// The name of the resources file to use when looking up rule resources  
        /// </summary>  
        public const string ResourceBaseName = "SampleRules.RuleResources";

        /// <summary>  
        /// Lookup name inside the resources file for the select asterisk rule name  
        /// </summary>  
        public const string AvoidWaitForDelay_RuleName = "AvoidWaitForDelay_RuleName";

        /// <summary>  
        /// Lookup ID inside the resources file for the select asterisk description  
        /// </summary>  
        public const string AvoidWaitForDelay_ProblemDescription = "AvoidWaitForDelay_ProblemDescription";

        public const string SelectStarRule_RuleName = "SelectStarRule_RuleName";

        public const string SelectStarRule_ProblemDescription = "SelectStarRule_ProblemDescription";

        public const string SelectLikeWildcardRule_RuleName = "SelectLikeWildcardRule_RuleName";

        public const string SelectLikeWildcardRule_ProblemDescription = "SelectLikeWildcardRule_ProblemDescription";

        public const string SelectIntoRule_RuleName = "SelectIntoRule_RuleName";

        public const string SelectIntoRule_ProblemDescription = "SelectIntoRule_ProblemDescription";

        public const string SelectWithoutNoLockRule_RuleName = "SelectWithoutNoLockRule_RuleName";

        public const string SelectWithoutNoLockRule_ProblemDescription = "SelectWithoutNoLockRule_ProblemDescription";

        public const string DeleteWithNoPredicateRule_RuleName = "DeleteWithNoPredicateRule_RuleName";

        public const string DeleteWithNoPredicateRule_ProblemDescription = "DeleteWithNoPredicateRule_ProblemDescription";

        /// <summary>  
        /// The design category (should not be localized)  
        /// </summary>  
        public const string CategoryDesign = "Design";

        /// <summary>  
        /// The performance category (should not be localized)  
        /// </summary>  
        public const string CategoryPerformance = "Design";
    }
}
