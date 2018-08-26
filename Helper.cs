using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Compiler
{
    internal class Helper
    {
        internal static (string filepath, int lineNumber) ExtractPosition(SyntaxNode node)
        {
            return (filepath: node.SyntaxTree.FilePath, 
                lineNumber: node.SyntaxTree.GetLineSpan(node.Span).StartLinePosition.Line);
        }

        internal static (string filepath, int lineNumber) ExtractPosition(SyntaxTrivia trivia)
        {
            return (filepath: trivia.SyntaxTree.FilePath, 
                lineNumber: trivia.SyntaxTree.GetLineSpan(trivia.Span).StartLinePosition.Line);
        }

        internal static void AnalyzeWalker(Project project, CSharpSyntaxWalker walker)
        {
            foreach(var doc in project.Documents)
            {
                var tree = doc.GetSyntaxTreeAsync().Result.GetRoot();
                Program.Instance.Model = doc.GetSemanticModelAsync().Result;

                walker.Visit(tree);
            }
        }

        internal static void AnalyzeWalkers(Project project, IEnumerable<CSharpSyntaxWalker> walkers)
        {
            Parallel.ForEach(walkers, (walker) => AnalyzeWalker(project, walker));
        }
    }
}
