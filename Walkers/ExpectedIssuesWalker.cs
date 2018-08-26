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
    internal sealed class ExpectedIssueWalkers : CSharpSyntaxWalker
    {
        private Regex defectRegex = new Regex("^// @issue - [a-zA-Z]* - .*$");
        private Regex trapRegex = new Regex("^// @trap - [a-zA-Z]* - .*$");

        public ExpectedIssueWalkers() : base(SyntaxWalkerDepth.Trivia)
        {
        }

        public override void VisitTrivia(SyntaxTrivia trivia)
        {
            if(trivia.Kind() == SyntaxKind.SingleLineCommentTrivia)
            {
                var defectComment = trivia.ToString();
                var match = defectRegex.Match(defectComment);
                if(match.Success) {
                    var issueType = defectComment.Split(" - ")[1];
                    var additionnalMisc = defectComment.Split(" - ")[2];
                    var position = Helper.ExtractPosition(trivia);

                    var expectedIssue = new Issue(issueType, additionnalMisc, position.filepath, position.lineNumber);
                    IssueReporter.Instance.AddExpectedIssue(expectedIssue);
                }

                match = trapRegex.Match(defectComment);
                if(match.Success) {
                    var issueType = defectComment.Split(" - ")[1];
                    var additionnalMisc = defectComment.Split(" - ")[2];
                    var position = Helper.ExtractPosition(trivia);

                    var expectedTrap = new Issue(issueType, additionnalMisc, position.filepath, position.lineNumber, false);
                    IssueReporter.Instance.AddExpectedIssue(expectedTrap);
                }
            }

            base.VisitTrivia(trivia);
        }
    }
}
