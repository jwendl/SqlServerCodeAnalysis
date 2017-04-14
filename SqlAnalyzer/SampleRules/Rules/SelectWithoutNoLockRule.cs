using Microsoft.SqlServer.Dac.CodeAnalysis;
using Microsoft.SqlServer.Dac.Model;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using SampleRules.Attributes;
using SampleRules.Visitors;
using System.Collections.Generic;

namespace SampleRules.Rules
{
    [LocalizedExportCodeAnalysisRule(RuleId,
        RuleConstants.ResourceBaseName,
        RuleConstants.SelectWithoutNoLockRule_RuleName,
        RuleConstants.SelectWithoutNoLockRule_ProblemDescription,
        Category = RuleConstants.CategoryPerformance,
        RuleScope = SqlRuleScope.Element)]
    public sealed class SelectWithoutNoLockRule
        : BaseSqlCodeAnalysisRule<SelectWithoutNoLockVisitor, SelectStatement>
    {
        /// <summary>  
        /// The Rule ID should resemble a fully-qualified class name. In the Visual Studio UI  
        /// rules are grouped by "Namespace + Category", and each rule is shown using "Short ID: DisplayName".  
        /// For this rule, that means the grouping will be "Public.Dac.Samples.Performance", with the rule  
        /// shown as "SR1004: Avoid using WaitFor Delay statements in stored procedures, functions and triggers."  
        /// </summary>  
        public const string RuleId = "RuleSamples.SR1010";

        public SelectWithoutNoLockRule()
        {
            // This rule supports Procedures, Functions and Triggers. Only those objects will be passed to the Analyze method  
            SupportedElementTypes = new[]
            {  
                // Note: can use the ModelSchema definitions, or access the TypeClass for any of these types  
                ModelSchema.ExtendedProcedure,
                ModelSchema.Procedure,
                ModelSchema.TableValuedFunction,
                ModelSchema.ScalarFunction,

                ModelSchema.DatabaseDdlTrigger,
                ModelSchema.DmlTrigger,
                ModelSchema.ServerDdlTrigger
            };
        }

        /// <summary>  
        /// For element-scoped rules the Analyze method is executed once for every matching   
        /// object in the model.   
        /// </summary>  
        /// <param name="ruleExecutionContext">The context object contains the TSqlObject being   
        /// analyzed, a TSqlFragment  
        /// that's the AST representation of the object, the current rule's descriptor, and a   
        /// reference to the model being  
        /// analyzed.  
        /// </param>  
        /// <returns>A list of problems should be returned. These will be displayed in the Visual   
        /// Studio error list</returns>  
        public override IList<SqlRuleProblem> Analyze(SqlRuleExecutionContext ruleExecutionContext)
        {
            return AnalyzeImplementation(ruleExecutionContext);
        }
    }
}
