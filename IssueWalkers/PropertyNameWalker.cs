using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace Compiler.IssueWalkers
{
    internal class PropertyNameWalker : CSharpSyntaxWalker
    {
        public int field; // @trap - PropertyName - Do not report PropertName on fields
        public int GoodProperty { get; } // @trap - PropertyName - GoodProperty does start with an uppercase      
        public int badProperty { get; } // @issue - PropertyName - badProperty does not start with an uppercase
        
        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            if(char.IsLower(node.Identifier.ToString()[0]))
            {
                var pos = Helper.ExtractPosition(node);
                var issue = new Issue("PropertyName", string.Format("{0} does not start with an uppercase", node.Identifier), pos.filepath, pos.lineNumber);
                IssueReporter.Instance.AddIssue(issue);
            }

            base.VisitPropertyDeclaration(node);
        }       
    }
}
