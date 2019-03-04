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
    internal class PropertyNameWalker : DefaultWalker
    {
        internal override void PreExecute() {
            // You want to call the following line! It will tell the correction system
            // that you want to get a a score for this kind of issue. Without it 
            // you won't be evaluated!
            IssueReporter.Instance.EnableIssueType(IssueType.PropertyStartUppercase);


            // If you need to execute more code before visiting the AST
        }

        public int field; // @trap@I01
        public int GoodProperty { get; } // @trap@I01
        public int badProperty { get; } // @issue@I01
        
        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            if(!char.IsUpper(node.Identifier.ToString()[0]))
            {
                var issue = new Issue(IssueType.PropertyStartUppercase, node);
                IssueReporter.Instance.AddIssue(issue);
            }

            base.VisitPropertyDeclaration(node);
        }       
    }
}
