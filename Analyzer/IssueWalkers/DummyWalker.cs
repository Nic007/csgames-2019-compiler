using System;
using System.Linq;
using Compiler.Walkers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace Compiler.IssueWalkers
{
    // This is the skeleton of what a walker might look like
    internal class DummyWalker : DefaultWalker
    {
        // This is you want to override a node for the AST, go look at the Documentation
        // folder to see which methods you can overrride
        public override void VisitCaseSwitchLabel(CaseSwitchLabelSyntax node)
        {
            // This will be executed for every CaseSwitchLabel

            // When you detect an issue, you can report it by doing this :
            // var issue = new Issue(IssueType.TheIssueYouCareAbout, node);
            // IssueReporter.Instance.AddIssue(issue);

            base.VisitCaseSwitchLabel(node);
        }
        internal override void PreExecute() {
            // You want to call the following line! It will tell the correction system
            // that you want to get a a score for this kind of issue. Without it 
            // you won't be evaluated!
            // IssueReporter.Instance.EnableIssueType(IssueType.TheIssueYouCareAbout);


            // If you need to execute more code before visiting the AST
        }

        internal override void PostExecute() {
            
            // If you need to execute more code after visiting the AST 

            // For example you stored some nodes during the visit and now you
            // want to do some kind of logic on them and finally report your 
            // issues
        }
    }
}
