using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;

internal sealed class IssueReporter
{
    private static readonly Lazy<IssueReporter> lazy =
        new Lazy<IssueReporter>(() => new IssueReporter());
    
    public static IssueReporter Instance { get { return lazy.Value; } }

    private bool expectMode = true;

    private ConcurrentDictionary<Issue, bool> ExpectedIssues = new ConcurrentDictionary<Issue, bool>();
    private ConcurrentDictionary<Issue, bool> ExpectedTraps = new ConcurrentDictionary<Issue, bool>();
    private ConcurrentDictionary<Issue, bool> ReportedIssues = new ConcurrentDictionary<Issue, bool>();

    private IssueReporter()
    {
    }

    public void AddExpectedIssue(Issue issue)
    {
        if(!expectMode)
        {
            throw new AccessViolationException("DO NOT CALL THIS FUNCTION, use only AddIssue()");
        }

        if(issue.IsRealIssue)
        {
            ExpectedIssues[issue] = true;
        }
        else
        {
            ExpectedTraps[issue] = true;
        }

    }

    public void AddIssue(Issue issue)
    {
        if(expectMode)
        {
            throw new AccessViolationException("This is not the moment to call this function!");
        }

        if(!issue.IsRealIssue)
        {
            throw new AccessViolationException("You don't have to report traps! Just make sure your analysis ignore them");
        }

        ReportedIssues[issue] = true;
    }

    public void EndExpectMode()
    {
        expectMode = false;
    }

    public void Report(StreamWriter writer)
    {
        foreach (IssueType issueType in (IssueType[]) Enum.GetValues(typeof(IssueType)))
        {
            var expectedIssues = ExpectedIssues.Keys.Where(x => x.IssueType == issueType).ToImmutableHashSet();
            var expectedTraps  = ExpectedTraps.Keys.Where(x => x.IssueType == issueType).ToImmutableHashSet();
            var reportedIssues = ReportedIssues.Keys.Where(x => x.IssueType == issueType).ToImmutableHashSet();

            var truePositives  = reportedIssues.Intersect(expectedIssues);
            var falsePositives = reportedIssues.Except(expectedIssues);
            var trueNegatives  = expectedTraps.Except(reportedIssues);
            var falseNegatives = expectedIssues.Except(reportedIssues);

            var TP = truePositives.Count;
            var FP = falsePositives.Count;
            var TN = trueNegatives.Count;
            var FN = falseNegatives.Count;

            var TPR = ((float)TP) / ( TP + FN );
            var FPR = ((float)FP) / ( FP + TN );

            var score = (TPR - FPR) * 100;

            writer.WriteLine("\nReporting Results for issue: " + issueType);
            writer.WriteLine("");

            writer.WriteLine("Reported issues:");
            foreach (var issue in reportedIssues)
            {
                writer.WriteLine(issue.ToFullString());
            }
            writer.WriteLine("");

            writer.WriteLine("Expected issues:");
            foreach (var issue in expectedIssues)
            {
                writer.WriteLine(issue.ToFullString());
            }
            writer.WriteLine("");

            writer.WriteLine("Expected traps:");
            foreach (var issue in expectedTraps)
            {
                writer.WriteLine(issue.ToFullString());
            }
            writer.WriteLine("");

            writer.WriteLine("False positives:");
            foreach (var issue in falsePositives)
            {
                writer.WriteLine(issue.ToFullString());
            }
            writer.WriteLine("");

            writer.WriteLine("False negatives:");
            foreach (var issue in falseNegatives)
            {
                writer.WriteLine(issue.ToFullString());
            }
            writer.WriteLine("");

            writer.WriteLine("Expected Issues Count: {0}", expectedIssues.Count);
            writer.WriteLine("Expected Traps Count: {0}", expectedTraps.Count);
            writer.WriteLine("Reported Issues Count: {0}", reportedIssues.Count);
            writer.WriteLine("");

            writer.WriteLine("True Positives Count: {0}", TP);
            writer.WriteLine("False Positives Count: {0}", FP);
            writer.WriteLine("True Negatives Count: {0}", TN);
            writer.WriteLine("False Negatives Count: {0}", FN);
            writer.WriteLine("");

            writer.WriteLine("True Positive Rate: {0}", TPR);
            writer.WriteLine("False Positive Rate: {0}", FPR);
            writer.WriteLine("");

            writer.WriteLine("Final Score: {0}", score);
        }
    }

}