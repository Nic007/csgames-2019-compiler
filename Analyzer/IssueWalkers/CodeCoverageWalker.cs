using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.Walkers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace Compiler.IssueWalkers
{
    internal class CodeCoverageWalker : DefaultWalker
    {
        internal static IEnumerable<ISymbol> FindInvokedMethods(SyntaxNode node, SemanticModel model) {
            var expressions = node.DescendantNodesAndSelf().OfType<InvocationExpressionSyntax>();
            return expressions.SelectMany(x => model.GetMemberGroup(x.Expression));
        }

        HashSet<StatementSyntax> allStatements = new HashSet<StatementSyntax>();
        HashSet<StatementSyntax> coveredStatements = new HashSet<StatementSyntax>();

        Dictionary<ISymbol, SemanticModel> models = new Dictionary<ISymbol, SemanticModel>();
        Dictionary<ISymbol, MethodDeclarationSyntax> methods = new Dictionary<ISymbol, MethodDeclarationSyntax>();
        Dictionary<ISymbol, MethodDeclarationSyntax> tests = new Dictionary<ISymbol, MethodDeclarationSyntax>();

        private bool isNodeInTestNamespace(SyntaxNode node) {
            var namespaceNode = node.Ancestors().OfType<NamespaceDeclarationSyntax>().First();
            return namespaceNode.Name.ToString().Contains("Tests");
        }

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

            // Detect if it is a test
            var isInTestNameSpace = isNodeInTestNamespace(node);
            var hasFactAttribute = node.AttributeLists.Any(x => x.Attributes.Any(y => y.Name.ToString() == "Fact"));
            var isTest = isInTestNameSpace && hasFactAttribute;

            if(isTest) {
                tests[symbol] = node;
            }
            // If it is not in the test namespace, collect all the statements
            else if (!isInTestNameSpace) {
                allStatements.UnionWith(node.Body.DescendantNodes().OfType<StatementSyntax>());
                return;
            }         
        }

        internal override void PostExecute() {
            // Start visiting every function reachable from the tests
            var visitedFunctions = new HashSet<ISymbol>();

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

            var notCoveredStatements = allStatements.Except(coveredStatements);
            foreach(var statement in notCoveredStatements) {
                var issue = new Issue(IssueType.TestCoverageMissing, statement);
                IssueReporter.Instance.AddIssue(issue);
            }
        }
        
        internal IEnumerable<ISymbol> ExploreMethod(MethodDeclarationSyntax node, SemanticModel model){          
            var possiblyCalledMethods = new List<ISymbol>();

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
                    if(!isNodeInTestNamespace(op.Syntax)) {
                        coveredStatements.Add(op.Syntax as StatementSyntax);
                    }

                    // Look at all the functions we might call in this operation, if it is an operation in the program, add it
                    var invokedMethods = FindInvokedMethods(op.Syntax, model);
                    possiblyCalledMethods.AddRange(invokedMethods.Where(x => methods.ContainsKey(x)));
                }

                // Push the nitra-procedural successors
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

        internal override void PreExecute() {
            // You want to call the following line! It will tell the correction system
            // that you want to get a a score for this kind of issue. Without it 
            // you won't be evaluated!
            IssueReporter.Instance.EnableIssueType(IssueType.TestCoverageMissing);


            // If you need to execute more code before visiting the AST
        }     
    }
}
