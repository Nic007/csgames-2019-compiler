// Be careful with this file, you might want to add new functions here,
// but changing existing functions might break things.

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Linq;
using Compiler.Walkers;
using Microsoft.CodeAnalysis.Operations;

namespace Compiler
{
    internal class Helper
    {
        internal static (string filepath, int lineNumber) ExtractPosition(SyntaxNode node)
        {
            return (filepath: node.SyntaxTree.FilePath, 
                lineNumber: node.SyntaxTree.GetLineSpan(node.Span).StartLinePosition.Line + 1);
        }

        internal static (string filepath, int lineNumber) ExtractPosition(SyntaxTrivia trivia)
        {
            return (filepath: trivia.SyntaxTree.FilePath, 
                lineNumber: trivia.SyntaxTree.GetLineSpan(trivia.Span).StartLinePosition.Line + 1);
        }

        internal static void AnalyzeWalker(Project project, DefaultWalker walker)
        {
            walker.PreExecute();
            foreach(var doc in project.Documents.Where(x => !x.FilePath.Contains("Debug")))
            {
                var tree = doc.GetSyntaxTreeAsync().Result.GetRoot();
                Program.Instance.Model = doc.GetSemanticModelAsync().Result;
                
                walker.Visit(tree);
            }
            walker.PostExecute();
        }

        internal static void AnalyzeWalkers(Project project, IEnumerable<DefaultWalker> walkers)
        {
            foreach(var walker in walkers) {
                AnalyzeWalker(project, walker);
            }
        }

        internal static void AnalyzeWalker(IEnumerable<Project> projects, DefaultWalker walker)
        {
            foreach(var project in projects) {
                AnalyzeWalker(project, walker);
            }
        }

        internal static void AnalyzeWalkers(IEnumerable<Project> projects, IEnumerable<DefaultWalker> walkers)
        {
            foreach(var project in projects) {
                AnalyzeWalkers(project, walkers);
            }
        }
    }
}
