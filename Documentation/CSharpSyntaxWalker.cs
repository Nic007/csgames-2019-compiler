namespace Microsoft.CodeAnalysis.CSharp
{
    //
    // Summary:
    //     Represents a Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor that descends
    //     an entire Microsoft.CodeAnalysis.CSharp.CSharpSyntaxNode graph visiting each
    //     CSharpSyntaxNode and its child SyntaxNodes and Microsoft.CodeAnalysis.SyntaxTokens
    //     in depth-first order.
    public abstract class CSharpSyntaxWalker : CSharpSyntaxVisitor
    {
        protected CSharpSyntaxWalker(SyntaxWalkerDepth depth = SyntaxWalkerDepth.Node);

        protected SyntaxWalkerDepth Depth { get; }

        public override void DefaultVisit(SyntaxNode node);
        public override void Visit(SyntaxNode node);
        public virtual void VisitLeadingTrivia(SyntaxToken token);
        public virtual void VisitToken(SyntaxToken token);
        public virtual void VisitTrailingTrivia(SyntaxToken token);
        public virtual void VisitTrivia(SyntaxTrivia trivia);
    }
}