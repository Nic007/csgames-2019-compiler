// This file is an example, it might be helpful to you

using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.Walkers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace Compiler.Walkers
{
    internal class CFGWalker : DefaultWalker
    {
        internal static IEnumerable<ISymbol> FindInvokedMethods(SyntaxNode node, SemanticModel model) {
            var expressions = node.DescendantNodesAndSelf().OfType<InvocationExpressionSyntax>();
            return expressions.SelectMany(x => model.GetMemberGroup(x.Expression));
        }

        Dictionary<ISymbol, SemanticModel> models = new Dictionary<ISymbol, SemanticModel>();
        Dictionary<ISymbol, MethodDeclarationSyntax> methods = new Dictionary<ISymbol, MethodDeclarationSyntax>();
        Dictionary<ISymbol, MethodDeclarationSyntax> tests = new Dictionary<ISymbol, MethodDeclarationSyntax>();

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            // If the method does not have a body, ignore it's something like an interface
            if(node.Body == null) {
                return;
            }

            // First gather all the methods

            var symbol = Program.Instance.Model.GetDeclaredSymbol(node);
            methods[symbol] = node;
            models[symbol] = Program.Instance.Model;
        }

        internal override void PostExecute() {
            // Start visiting every function reachable from the tests
            var visitedFunctions = new HashSet<ISymbol>();

            // This piece of code handle inter-procedural logic
            // Be aware that it is very very basic and possibly incomplete (However it is better than nothing)

            var q = new Queue<ISymbol>();
            foreach(var test in tests) {
                if (!visitedFunctions.Contains(test.Key)) {
                    q.Enqueue(test.Key);
                    visitedFunctions.Add(test.Key);
                }
            }

            while(q.Any()) {
                var methodSymbol = q.Dequeue();
                var methodNode = methods[methodSymbol];
                var methodModel = models[methodSymbol];

                var methodsCalled = ExploreMethod(methodNode, methodModel);

                foreach(var methodCalled in methodsCalled) {
                    if (!visitedFunctions.Contains(methodCalled)) {
                        q.Enqueue(methodCalled);
                        visitedFunctions.Add(methodCalled);
                    }
                }
            }
        }
        
        internal IEnumerable<ISymbol> ExploreMethod(MethodDeclarationSyntax node, SemanticModel model){          
            var possiblyCalledMethods = new List<ISymbol>();

            // This method will use the control flow graph and visit the basic blocks of a function
            // and find what it can calls

            var cfg = ControlFlowGraph.Create(node, model);
            var FirstBlockExecuted = cfg.Blocks.First();

            var visited = new HashSet<BasicBlock>();
            var q = new Queue<BasicBlock>();
            q.Enqueue(FirstBlockExecuted);
            while(q.Count() > 0)
            {
                var currentBlock = q.Dequeue();
                
                // Iterate over all operations executed in this block
                foreach (var op in currentBlock.Operations.Where(x => x.Syntax is StatementSyntax))
                {
                    // Here you could add some logic on a statement

                    // Look at all the functions we might call in this operation, if it is an operation in the program, add it
                    var invokedMethods = FindInvokedMethods(op.Syntax, model);
                    possiblyCalledMethods.AddRange(invokedMethods.Where(x => methods.ContainsKey(x)));
                }

                // Push the intra-procedural successors
                if(currentBlock.FallThroughSuccessor?.Destination != null 
                    && !visited.Contains(currentBlock.FallThroughSuccessor.Destination))
                {
                    q.Enqueue(currentBlock.FallThroughSuccessor.Destination);
                    visited.Add(currentBlock.FallThroughSuccessor.Destination);
                }
                if(currentBlock.ConditionalSuccessor?.Destination != null
                    && !visited.Contains(currentBlock.ConditionalSuccessor.Destination))
                {
                    q.Enqueue(currentBlock.ConditionalSuccessor.Destination);
                    visited.Add(currentBlock.ConditionalSuccessor.Destination);
                }
            }

            return possiblyCalledMethods;
        }         
    }
}
