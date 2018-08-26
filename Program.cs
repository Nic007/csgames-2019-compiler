using Buildalyzer;
using Buildalyzer.Workspaces;
using Compiler.Walkers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FlowAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Compiler
{
    internal class Program
    {
        private static readonly Lazy<Program> lazy = new Lazy<Program>(() => new Program());

        public static Program Instance { get { return lazy.Value; } }

        public SemanticModel Model { get; internal set; }

        private Program()
        {

        }
        
        static void Main() 
        {
            var projectPath = @"Compiler.csproj";

            var manager = new AnalyzerManager();
            manager.SetGlobalProperty("features", "Flow-Analysis");
            var analyzer = manager.GetProject(projectPath);
            var workspace = analyzer.GetWorkspace();
            var project = workspace.CurrentSolution.Projects.First(x => x.FilePath.Contains(projectPath));

            Helper.AnalyzeWalker(project, new ExpectedIssueWalkers());
            IssueReporter.Instance.EndExpectMode();

            var walkersNames = Assembly.GetExecutingAssembly().GetTypes()
              .Where(t => String.Equals(t.Namespace, "Compiler.IssueWalkers", StringComparison.Ordinal));

            var walkers = walkersNames.Select(x => Activator.CreateInstance(x) as CSharpSyntaxWalker);
            Helper.AnalyzeWalkers(project, walkers);

            var sw = new StreamWriter(Console.OpenStandardOutput());
            sw.AutoFlush = true;
            Console.SetOut(sw);


            IssueReporter.Instance.Report(sw);
        }
    }
}
