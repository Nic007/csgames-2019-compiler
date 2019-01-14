using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace Compiler.Walkers
{
    internal abstract class DefaultWalker : CSharpSyntaxWalker
    {
        internal DefaultWalker() : base() 
        {
        }
        
        internal DefaultWalker(SyntaxWalkerDepth param) : base(param) 
        {
        }

        internal virtual void PreExecute() {
            // If you need to execute more code before visiting the node you can override 
        }

        internal virtual void PostExecute() {
            // If you need to execute more code after visiting the node you can override 
        }
    }
}
