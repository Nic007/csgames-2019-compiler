using Buildalyzer;
using Buildalyzer.Workspaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FlowAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace Compiler
{
    class Program
    {
        static void Main() 
        {
            AnalyzerManager manager = new AnalyzerManager();
            ProjectAnalyzer analyzer = manager.GetProject(@"D:\Developpement\csgames-2019-competitions\Compiler\Compiler.csproj");
            AdhocWorkspace workspace = analyzer.GetWorkspace();
            var project = workspace.CurrentSolution.Projects.First();
            System.Console.WriteLine(project.ParseOptions.Features.Count);
            foreach(var proj in workspace.CurrentSolution.Projects)
            {
                foreach(var doc in proj.Documents)
                {
                    var tree = doc.GetSyntaxTreeAsync().Result.GetRoot();
                    var model = doc.GetSemanticModelAsync().Result;

                    var walker = new CFGWalker(model);
                    walker.Visit(tree);
                }
            }
        }
    }
}
