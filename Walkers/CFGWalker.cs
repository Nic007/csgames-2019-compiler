using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace Compiler.Walkers
{
    internal class CFGWalker : CSharpSyntaxWalker
    {
        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var operation = (IMethodBodyOperation)Program.Instance.Model.GetOperation(node);
            string methodName = node.Identifier.ToString();
            Console.WriteLine(methodName);
            
            var cfg = ControlFlowGraph.Create(operation);
            Console.WriteLine(cfg);
        }       
    }
}
