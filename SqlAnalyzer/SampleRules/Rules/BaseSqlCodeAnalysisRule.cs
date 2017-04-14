using Microsoft.SqlServer.Dac.CodeAnalysis;
using Microsoft.SqlServer.Dac.Model;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using SampleRules.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SampleRules.Rules
{
    public abstract class BaseSqlCodeAnalysisRule<TVisitor, TStatement>
        : SqlCodeAnalysisRule
        where TVisitor : TSqlConcreteFragmentVisitor, new()
        where TStatement : TSqlFragment
    {
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
        protected virtual IList<SqlRuleProblem> AnalyzeImplementation(SqlRuleExecutionContext ruleExecutionContext)
        {
            var problems = new List<SqlRuleProblem>();
            var modelElement = ruleExecutionContext.ModelElement;

            // this rule does not apply to inline table-valued function  
            // we simply do not return any problem in that case.  
            if (IsInlineTableValuedFunction(modelElement))
            {
                return problems;
            }

            var elementName = GetElementName(ruleExecutionContext, modelElement);

            // The rule execution context has all the objects we'll need, including the   
            // fragment representing the object,  
            // and a descriptor that lets us access rule metadata  
            var fragment = ruleExecutionContext.ScriptFragment;
            var ruleDescriptor = ruleExecutionContext.RuleDescriptor;

            // To process the fragment and identify WAITFOR DELAY statements we will use a   
            // visitor   
            var visitor = new TVisitor();
            fragment.Accept(visitor);
            var visitorWithStatements = visitor as IStatementVisitor<TStatement>;
            var selectStatements = visitorWithStatements.Statements;

            // Create problems for each WAITFOR DELAY statement found   
            // When creating a rule problem, always include the TSqlObject being analyzed. This   
            // is used to determine  
            // the name of the source this problem was found in and a best guess as to the   
            // line/column the problem was found at.  
            //  
            // In addition if you have a specific TSqlFragment that is related to the problem   
            //also include this  
            // since the most accurate source position information (start line and column) will   
            // be read from the fragment  
            foreach (var selectStatement in selectStatements)
            {
                var problem = new SqlRuleProblem(String.Format(CultureInfo.CurrentCulture, ruleDescriptor.DisplayDescription, elementName), modelElement, selectStatement);
                problems.Add(problem);
            }
            return problems;
        }

        protected static string GetElementName(SqlRuleExecutionContext ruleExecutionContext, TSqlObject modelElement)
        {
            // Get the element name using the built in DisplayServices. This provides a number of   
            // useful formatting options to  
            // make a name user-readable  
            var displayServices = ruleExecutionContext.SchemaModel.DisplayServices;
            var elementName = displayServices.GetElementName(modelElement, ElementNameStyle.EscapedFullyQualifiedName);
            return elementName;
        }

        protected static bool IsInlineTableValuedFunction(TSqlObject modelElement)
        {
            return TableValuedFunction.TypeClass.Equals(modelElement.ObjectType) && FunctionType.InlineTableValuedFunction == modelElement.GetMetadata<FunctionType>(TableValuedFunction.FunctionType);
        }
    }
}
