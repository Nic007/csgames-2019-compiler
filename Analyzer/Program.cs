﻿// DO NOT EDIT THIS FILE: Your code for finding issues should be inserted
// in the folder IssueWalkers

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
            // However you might want to change this filepath for your testing
            var solutionFilePath = Directory.GetCurrentDirectory() + @"/Benchmarks/eShopOnWeb/eShopOnWeb.sln";
            Console.WriteLine("Parsing solution... " + solutionFilePath);
            if (!File.Exists(solutionFilePath)) {
                Console.WriteLine("File not found, the folder " + Directory.GetCurrentDirectory() 
                + " should be the one containing the Benchmarks folder, so the root directory of the git repo");
                return;
            }

            var manager = new AnalyzerManager(solutionFilePath);
            var workspace = manager.GetWorkspace();
            var projects = workspace.CurrentSolution.Projects;

            Console.WriteLine("Parsing source code for expected issues and traps...");
            Helper.AnalyzeWalker(projects, new ExpectedIssueWalker());
            IssueReporter.Instance.EndExpectMode();

            Console.WriteLine("Analyzing source code...");
            var walkersNames = Assembly.GetExecutingAssembly().GetTypes()
              .Where(t => String.Equals(t.Namespace, "Compiler.IssueWalkers", StringComparison.Ordinal));

            var walkers = walkersNames.Select(x => Activator.CreateInstance(x)).OfType<DefaultWalker>();
            Helper.AnalyzeWalkers(projects, walkers);

            // And reporting results
            var sw = new StreamWriter(Console.OpenStandardOutput());
            sw.AutoFlush = true;
            Console.SetOut(sw);

            IssueReporter.Instance.Report(sw);
        }
    }
}
