using System;

internal sealed class Issue
{
    public Issue(string issueType, string additionnalMisc, string filepath, int lineNumber, bool isRealIssue = true)
    {
        if (string.IsNullOrWhiteSpace(issueType))
        {
            throw new System.ArgumentException("Issue Type cannot be empty", nameof(issueType));
        }

        if (string.IsNullOrWhiteSpace(filepath))
        {
            throw new System.ArgumentException("Filepath cannot be empty", nameof(filepath));
        }

        if (lineNumber < 1)
        {
            throw new System.ArgumentException("Linenumber cannot be inferior to 1", nameof(lineNumber));
        }

        IssueType = issueType;
        AdditionnalMisc = additionnalMisc;
        FilePath = filepath;
        LineNumber = lineNumber;
        IsRealIssue = isRealIssue;
    }

    public string IssueType { get; }
    public string AdditionnalMisc { get; }
    public string FilePath { get; }
    public int LineNumber { get; }
    public bool IsRealIssue { get; }

    public string ToFullString()
    {
        return string.Format("{0}:{1} {2} - {3}", FilePath, LineNumber, IssueType, AdditionnalMisc);
    }

    public override string ToString()
    {
        return string.Format("{0}:{1} {2}", FilePath, LineNumber, IssueType);
    }

    public override int GetHashCode() {
        return ToString().GetHashCode();
    }

    public override bool Equals(object other)
    {
        return ToString() == other.ToString();
    }
}