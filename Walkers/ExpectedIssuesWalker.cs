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
    internal sealed class ExpectedIssueWalker : DefaultWalker
    {
        private Regex defectRegex = new Regex(@"\@issue\@I(\d+)");
        private Regex trapRegex = new Regex(@"\@trap\@I(\d+)");


        public ExpectedIssueWalker() : base(SyntaxWalkerDepth.Trivia)
        {
        }

        public override void VisitTrivia(SyntaxTrivia trivia)
        {
            if(trivia.Kind() == SyntaxKind.SingleLineCommentTrivia)
            {
                var defectComment = trivia.ToString();
                foreach (Match match in defectRegex.Matches(defectComment)) {
                    var issueType = (IssueType) int.Parse(match.Groups[1].Value);
                    var position = Helper.ExtractPosition(trivia);

                    var expectedIssue = new Issue(issueType, position.filepath, position.lineNumber);
                    IssueReporter.Instance.AddExpectedIssue(expectedIssue);
                }

                foreach (Match match in trapRegex.Matches(defectComment)) {
                    var issueType = (IssueType) int.Parse(match.Groups[1].Value);
                    var position = Helper.ExtractPosition(trivia);

                    var expectedTrap = new Issue(issueType, position.filepath, position.lineNumber, false);
                    IssueReporter.Instance.AddExpectedIssue(expectedTrap);
                }
            }

            base.VisitTrivia(trivia);
        }
    }
}
