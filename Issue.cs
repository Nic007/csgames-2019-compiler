using System;
using Compiler;
using Microsoft.CodeAnalysis;

internal enum IssueType {
    PropertyStartUppercase = 1,
    TestCoverageMissing,
    NeverUserField
};

internal sealed class Issue
{

    public Issue(IssueType issueType, SyntaxNode node, bool isRealIssue = true) {
        var pos = Helper.ExtractPosition(node);

        if (string.IsNullOrWhiteSpace(pos.filepath))
        {
            throw new System.ArgumentException("Filepath cannot be empty", nameof(pos.filepath));
        }

        if (pos.lineNumber < 1)
        {
            throw new System.ArgumentException("Linenumber cannot be inferior to 1", nameof(pos.lineNumber));
        }

        IssueType = issueType;
        FilePath = pos.filepath;
        LineNumber = pos.lineNumber;
        IsRealIssue = isRealIssue;
    }

    public Issue(IssueType issueType, string filepath, int lineNumber, bool isRealIssue = true)
    {
        if (string.IsNullOrWhiteSpace(filepath))
        {
            throw new System.ArgumentException("Filepath cannot be empty", nameof(filepath));
        }

        if (lineNumber < 1)
        {
            throw new System.ArgumentException("Linenumber cannot be inferior to 1", nameof(lineNumber));
        }

        IssueType = issueType;
        FilePath = filepath;
        LineNumber = lineNumber;
        IsRealIssue = isRealIssue;
    }

    public IssueType IssueType { get; }
    public string FilePath { get; }
    public int LineNumber { get; }
    public bool IsRealIssue { get; }

    public string ToFullString()
    {
        return string.Format("{0}:{1} I{2:X2}", FilePath, LineNumber, (int) IssueType);
    }

    public override string ToString()
    {
        return string.Format("{0}:{1} I{2:X2}", FilePath, LineNumber, (int) IssueType);
    }

    public override int GetHashCode() {
        return ToString().GetHashCode();
    }

    public override bool Equals(object other)
    {
        return ToString() == other.ToString();
    }
}