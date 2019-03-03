using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
    //
    // Summary:
    //     Represents a Microsoft.CodeAnalysis.CSharp.CSharpSyntaxNode visitor that visits
    //     only the single CSharpSyntaxNode passed into its Visit method.
    public abstract class CSharpSyntaxVisitor
    {
        protected CSharpSyntaxVisitor();

        public virtual void DefaultVisit(SyntaxNode node);
        public virtual void Visit(SyntaxNode node);
        //
        // Summary:
        //     Called when the visitor visits a AccessorDeclarationSyntax node.
        public virtual void VisitAccessorDeclaration(AccessorDeclarationSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a AccessorListSyntax node.
        public virtual void VisitAccessorList(AccessorListSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a AliasQualifiedNameSyntax node.
        public virtual void VisitAliasQualifiedName(AliasQualifiedNameSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a AnonymousMethodExpressionSyntax node.
        public virtual void VisitAnonymousMethodExpression(AnonymousMethodExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a AnonymousObjectCreationExpressionSyntax node.
        public virtual void VisitAnonymousObjectCreationExpression(AnonymousObjectCreationExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a AnonymousObjectMemberDeclaratorSyntax node.
        public virtual void VisitAnonymousObjectMemberDeclarator(AnonymousObjectMemberDeclaratorSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ArgumentSyntax node.
        public virtual void VisitArgument(ArgumentSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ArgumentListSyntax node.
        public virtual void VisitArgumentList(ArgumentListSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ArrayCreationExpressionSyntax node.
        public virtual void VisitArrayCreationExpression(ArrayCreationExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ArrayRankSpecifierSyntax node.
        public virtual void VisitArrayRankSpecifier(ArrayRankSpecifierSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ArrayTypeSyntax node.
        public virtual void VisitArrayType(ArrayTypeSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ArrowExpressionClauseSyntax node.
        public virtual void VisitArrowExpressionClause(ArrowExpressionClauseSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a AssignmentExpressionSyntax node.
        public virtual void VisitAssignmentExpression(AssignmentExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a AttributeSyntax node.
        public virtual void VisitAttribute(AttributeSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a AttributeArgumentSyntax node.
        public virtual void VisitAttributeArgument(AttributeArgumentSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a AttributeArgumentListSyntax node.
        public virtual void VisitAttributeArgumentList(AttributeArgumentListSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a AttributeListSyntax node.
        public virtual void VisitAttributeList(AttributeListSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a AttributeTargetSpecifierSyntax node.
        public virtual void VisitAttributeTargetSpecifier(AttributeTargetSpecifierSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a AwaitExpressionSyntax node.
        public virtual void VisitAwaitExpression(AwaitExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a BadDirectiveTriviaSyntax node.
        public virtual void VisitBadDirectiveTrivia(BadDirectiveTriviaSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a BaseExpressionSyntax node.
        public virtual void VisitBaseExpression(BaseExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a BaseListSyntax node.
        public virtual void VisitBaseList(BaseListSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a BinaryExpressionSyntax node.
        public virtual void VisitBinaryExpression(BinaryExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a BlockSyntax node.
        public virtual void VisitBlock(BlockSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a BracketedArgumentListSyntax node.
        public virtual void VisitBracketedArgumentList(BracketedArgumentListSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a BracketedParameterListSyntax node.
        public virtual void VisitBracketedParameterList(BracketedParameterListSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a BreakStatementSyntax node.
        public virtual void VisitBreakStatement(BreakStatementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a CasePatternSwitchLabelSyntax node.
        public virtual void VisitCasePatternSwitchLabel(CasePatternSwitchLabelSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a CaseSwitchLabelSyntax node.
        public virtual void VisitCaseSwitchLabel(CaseSwitchLabelSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a CastExpressionSyntax node.
        public virtual void VisitCastExpression(CastExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a CatchClauseSyntax node.
        public virtual void VisitCatchClause(CatchClauseSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a CatchDeclarationSyntax node.
        public virtual void VisitCatchDeclaration(CatchDeclarationSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a CatchFilterClauseSyntax node.
        public virtual void VisitCatchFilterClause(CatchFilterClauseSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a CheckedExpressionSyntax node.
        public virtual void VisitCheckedExpression(CheckedExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a CheckedStatementSyntax node.
        public virtual void VisitCheckedStatement(CheckedStatementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ClassDeclarationSyntax node.
        public virtual void VisitClassDeclaration(ClassDeclarationSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ClassOrStructConstraintSyntax node.
        public virtual void VisitClassOrStructConstraint(ClassOrStructConstraintSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a CompilationUnitSyntax node.
        public virtual void VisitCompilationUnit(CompilationUnitSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ConditionalAccessExpressionSyntax node.
        public virtual void VisitConditionalAccessExpression(ConditionalAccessExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ConditionalExpressionSyntax node.
        public virtual void VisitConditionalExpression(ConditionalExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ConstantPatternSyntax node.
        public virtual void VisitConstantPattern(ConstantPatternSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ConstructorConstraintSyntax node.
        public virtual void VisitConstructorConstraint(ConstructorConstraintSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ConstructorDeclarationSyntax node.
        public virtual void VisitConstructorDeclaration(ConstructorDeclarationSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ConstructorInitializerSyntax node.
        public virtual void VisitConstructorInitializer(ConstructorInitializerSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ContinueStatementSyntax node.
        public virtual void VisitContinueStatement(ContinueStatementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ConversionOperatorDeclarationSyntax node.
        public virtual void VisitConversionOperatorDeclaration(ConversionOperatorDeclarationSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ConversionOperatorMemberCrefSyntax node.
        public virtual void VisitConversionOperatorMemberCref(ConversionOperatorMemberCrefSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a CrefBracketedParameterListSyntax node.
        public virtual void VisitCrefBracketedParameterList(CrefBracketedParameterListSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a CrefParameterSyntax node.
        public virtual void VisitCrefParameter(CrefParameterSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a CrefParameterListSyntax node.
        public virtual void VisitCrefParameterList(CrefParameterListSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a DeclarationExpressionSyntax node.
        public virtual void VisitDeclarationExpression(DeclarationExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a DeclarationPatternSyntax node.
        public virtual void VisitDeclarationPattern(DeclarationPatternSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a DefaultExpressionSyntax node.
        public virtual void VisitDefaultExpression(DefaultExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a DefaultSwitchLabelSyntax node.
        public virtual void VisitDefaultSwitchLabel(DefaultSwitchLabelSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a DefineDirectiveTriviaSyntax node.
        public virtual void VisitDefineDirectiveTrivia(DefineDirectiveTriviaSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a DelegateDeclarationSyntax node.
        public virtual void VisitDelegateDeclaration(DelegateDeclarationSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a DestructorDeclarationSyntax node.
        public virtual void VisitDestructorDeclaration(DestructorDeclarationSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a DiscardDesignationSyntax node.
        public virtual void VisitDiscardDesignation(DiscardDesignationSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a DocumentationCommentTriviaSyntax node.
        public virtual void VisitDocumentationCommentTrivia(DocumentationCommentTriviaSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a DoStatementSyntax node.
        public virtual void VisitDoStatement(DoStatementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ElementAccessExpressionSyntax node.
        public virtual void VisitElementAccessExpression(ElementAccessExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ElementBindingExpressionSyntax node.
        public virtual void VisitElementBindingExpression(ElementBindingExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ElifDirectiveTriviaSyntax node.
        public virtual void VisitElifDirectiveTrivia(ElifDirectiveTriviaSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ElseClauseSyntax node.
        public virtual void VisitElseClause(ElseClauseSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ElseDirectiveTriviaSyntax node.
        public virtual void VisitElseDirectiveTrivia(ElseDirectiveTriviaSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a EmptyStatementSyntax node.
        public virtual void VisitEmptyStatement(EmptyStatementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a EndIfDirectiveTriviaSyntax node.
        public virtual void VisitEndIfDirectiveTrivia(EndIfDirectiveTriviaSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a EndRegionDirectiveTriviaSyntax node.
        public virtual void VisitEndRegionDirectiveTrivia(EndRegionDirectiveTriviaSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a EnumDeclarationSyntax node.
        public virtual void VisitEnumDeclaration(EnumDeclarationSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a EnumMemberDeclarationSyntax node.
        public virtual void VisitEnumMemberDeclaration(EnumMemberDeclarationSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a EqualsValueClauseSyntax node.
        public virtual void VisitEqualsValueClause(EqualsValueClauseSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ErrorDirectiveTriviaSyntax node.
        public virtual void VisitErrorDirectiveTrivia(ErrorDirectiveTriviaSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a EventDeclarationSyntax node.
        public virtual void VisitEventDeclaration(EventDeclarationSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a EventFieldDeclarationSyntax node.
        public virtual void VisitEventFieldDeclaration(EventFieldDeclarationSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ExplicitInterfaceSpecifierSyntax node.
        public virtual void VisitExplicitInterfaceSpecifier(ExplicitInterfaceSpecifierSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ExpressionStatementSyntax node.
        public virtual void VisitExpressionStatement(ExpressionStatementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ExternAliasDirectiveSyntax node.
        public virtual void VisitExternAliasDirective(ExternAliasDirectiveSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a FieldDeclarationSyntax node.
        public virtual void VisitFieldDeclaration(FieldDeclarationSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a FinallyClauseSyntax node.
        public virtual void VisitFinallyClause(FinallyClauseSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a FixedStatementSyntax node.
        public virtual void VisitFixedStatement(FixedStatementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ForEachStatementSyntax node.
        public virtual void VisitForEachStatement(ForEachStatementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ForEachVariableStatementSyntax node.
        public virtual void VisitForEachVariableStatement(ForEachVariableStatementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ForStatementSyntax node.
        public virtual void VisitForStatement(ForStatementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a FromClauseSyntax node.
        public virtual void VisitFromClause(FromClauseSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a GenericNameSyntax node.
        public virtual void VisitGenericName(GenericNameSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a GlobalStatementSyntax node.
        public virtual void VisitGlobalStatement(GlobalStatementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a GotoStatementSyntax node.
        public virtual void VisitGotoStatement(GotoStatementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a GroupClauseSyntax node.
        public virtual void VisitGroupClause(GroupClauseSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a IdentifierNameSyntax node.
        public virtual void VisitIdentifierName(IdentifierNameSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a IfDirectiveTriviaSyntax node.
        public virtual void VisitIfDirectiveTrivia(IfDirectiveTriviaSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a IfStatementSyntax node.
        public virtual void VisitIfStatement(IfStatementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ImplicitArrayCreationExpressionSyntax node.
        public virtual void VisitImplicitArrayCreationExpression(ImplicitArrayCreationExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ImplicitElementAccessSyntax node.
        public virtual void VisitImplicitElementAccess(ImplicitElementAccessSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ImplicitStackAllocArrayCreationExpressionSyntax
        //     node.
        public virtual void VisitImplicitStackAllocArrayCreationExpression(ImplicitStackAllocArrayCreationExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a IncompleteMemberSyntax node.
        public virtual void VisitIncompleteMember(IncompleteMemberSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a IndexerDeclarationSyntax node.
        public virtual void VisitIndexerDeclaration(IndexerDeclarationSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a IndexerMemberCrefSyntax node.
        public virtual void VisitIndexerMemberCref(IndexerMemberCrefSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a InitializerExpressionSyntax node.
        public virtual void VisitInitializerExpression(InitializerExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a InterfaceDeclarationSyntax node.
        public virtual void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a InterpolatedStringExpressionSyntax node.
        public virtual void VisitInterpolatedStringExpression(InterpolatedStringExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a InterpolatedStringTextSyntax node.
        public virtual void VisitInterpolatedStringText(InterpolatedStringTextSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a InterpolationSyntax node.
        public virtual void VisitInterpolation(InterpolationSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a InterpolationAlignmentClauseSyntax node.
        public virtual void VisitInterpolationAlignmentClause(InterpolationAlignmentClauseSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a InterpolationFormatClauseSyntax node.
        public virtual void VisitInterpolationFormatClause(InterpolationFormatClauseSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a InvocationExpressionSyntax node.
        public virtual void VisitInvocationExpression(InvocationExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a IsPatternExpressionSyntax node.
        public virtual void VisitIsPatternExpression(IsPatternExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a JoinClauseSyntax node.
        public virtual void VisitJoinClause(JoinClauseSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a JoinIntoClauseSyntax node.
        public virtual void VisitJoinIntoClause(JoinIntoClauseSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a LabeledStatementSyntax node.
        public virtual void VisitLabeledStatement(LabeledStatementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a LetClauseSyntax node.
        public virtual void VisitLetClause(LetClauseSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a LineDirectiveTriviaSyntax node.
        public virtual void VisitLineDirectiveTrivia(LineDirectiveTriviaSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a LiteralExpressionSyntax node.
        public virtual void VisitLiteralExpression(LiteralExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a LoadDirectiveTriviaSyntax node.
        public virtual void VisitLoadDirectiveTrivia(LoadDirectiveTriviaSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a LocalDeclarationStatementSyntax node.
        public virtual void VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a LocalFunctionStatementSyntax node.
        public virtual void VisitLocalFunctionStatement(LocalFunctionStatementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a LockStatementSyntax node.
        public virtual void VisitLockStatement(LockStatementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a MakeRefExpressionSyntax node.
        public virtual void VisitMakeRefExpression(MakeRefExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a MemberAccessExpressionSyntax node.
        public virtual void VisitMemberAccessExpression(MemberAccessExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a MemberBindingExpressionSyntax node.
        public virtual void VisitMemberBindingExpression(MemberBindingExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a MethodDeclarationSyntax node.
        public virtual void VisitMethodDeclaration(MethodDeclarationSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a NameColonSyntax node.
        public virtual void VisitNameColon(NameColonSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a NameEqualsSyntax node.
        public virtual void VisitNameEquals(NameEqualsSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a NameMemberCrefSyntax node.
        public virtual void VisitNameMemberCref(NameMemberCrefSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a NamespaceDeclarationSyntax node.
        public virtual void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a NullableDirectiveTriviaSyntax node.
        public virtual void VisitNullableDirectiveTrivia(NullableDirectiveTriviaSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a NullableTypeSyntax node.
        public virtual void VisitNullableType(NullableTypeSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ObjectCreationExpressionSyntax node.
        public virtual void VisitObjectCreationExpression(ObjectCreationExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a OmittedArraySizeExpressionSyntax node.
        public virtual void VisitOmittedArraySizeExpression(OmittedArraySizeExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a OmittedTypeArgumentSyntax node.
        public virtual void VisitOmittedTypeArgument(OmittedTypeArgumentSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a OperatorDeclarationSyntax node.
        public virtual void VisitOperatorDeclaration(OperatorDeclarationSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a OperatorMemberCrefSyntax node.
        public virtual void VisitOperatorMemberCref(OperatorMemberCrefSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a OrderByClauseSyntax node.
        public virtual void VisitOrderByClause(OrderByClauseSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a OrderingSyntax node.
        public virtual void VisitOrdering(OrderingSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ParameterSyntax node.
        public virtual void VisitParameter(ParameterSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ParameterListSyntax node.
        public virtual void VisitParameterList(ParameterListSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ParenthesizedExpressionSyntax node.
        public virtual void VisitParenthesizedExpression(ParenthesizedExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ParenthesizedLambdaExpressionSyntax node.
        public virtual void VisitParenthesizedLambdaExpression(ParenthesizedLambdaExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ParenthesizedVariableDesignationSyntax node.
        public virtual void VisitParenthesizedVariableDesignation(ParenthesizedVariableDesignationSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a PointerTypeSyntax node.
        public virtual void VisitPointerType(PointerTypeSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a PostfixUnaryExpressionSyntax node.
        public virtual void VisitPostfixUnaryExpression(PostfixUnaryExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a PragmaChecksumDirectiveTriviaSyntax node.
        public virtual void VisitPragmaChecksumDirectiveTrivia(PragmaChecksumDirectiveTriviaSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a PragmaWarningDirectiveTriviaSyntax node.
        public virtual void VisitPragmaWarningDirectiveTrivia(PragmaWarningDirectiveTriviaSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a PredefinedTypeSyntax node.
        public virtual void VisitPredefinedType(PredefinedTypeSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a PrefixUnaryExpressionSyntax node.
        public virtual void VisitPrefixUnaryExpression(PrefixUnaryExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a PropertyDeclarationSyntax node.
        public virtual void VisitPropertyDeclaration(PropertyDeclarationSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a QualifiedCrefSyntax node.
        public virtual void VisitQualifiedCref(QualifiedCrefSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a QualifiedNameSyntax node.
        public virtual void VisitQualifiedName(QualifiedNameSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a QueryBodySyntax node.
        public virtual void VisitQueryBody(QueryBodySyntax node);
        //
        // Summary:
        //     Called when the visitor visits a QueryContinuationSyntax node.
        public virtual void VisitQueryContinuation(QueryContinuationSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a QueryExpressionSyntax node.
        public virtual void VisitQueryExpression(QueryExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a RangeExpressionSyntax node.
        public virtual void VisitRangeExpression(RangeExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ReferenceDirectiveTriviaSyntax node.
        public virtual void VisitReferenceDirectiveTrivia(ReferenceDirectiveTriviaSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a RefExpressionSyntax node.
        public virtual void VisitRefExpression(RefExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a RefTypeSyntax node.
        public virtual void VisitRefType(RefTypeSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a RefTypeExpressionSyntax node.
        public virtual void VisitRefTypeExpression(RefTypeExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a RefValueExpressionSyntax node.
        public virtual void VisitRefValueExpression(RefValueExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a RegionDirectiveTriviaSyntax node.
        public virtual void VisitRegionDirectiveTrivia(RegionDirectiveTriviaSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ReturnStatementSyntax node.
        public virtual void VisitReturnStatement(ReturnStatementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a SelectClauseSyntax node.
        public virtual void VisitSelectClause(SelectClauseSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ShebangDirectiveTriviaSyntax node.
        public virtual void VisitShebangDirectiveTrivia(ShebangDirectiveTriviaSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a SimpleBaseTypeSyntax node.
        public virtual void VisitSimpleBaseType(SimpleBaseTypeSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a SimpleLambdaExpressionSyntax node.
        public virtual void VisitSimpleLambdaExpression(SimpleLambdaExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a SingleVariableDesignationSyntax node.
        public virtual void VisitSingleVariableDesignation(SingleVariableDesignationSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a SizeOfExpressionSyntax node.
        public virtual void VisitSizeOfExpression(SizeOfExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a SkippedTokensTriviaSyntax node.
        public virtual void VisitSkippedTokensTrivia(SkippedTokensTriviaSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a StackAllocArrayCreationExpressionSyntax node.
        public virtual void VisitStackAllocArrayCreationExpression(StackAllocArrayCreationExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a StructDeclarationSyntax node.
        public virtual void VisitStructDeclaration(StructDeclarationSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a SwitchSectionSyntax node.
        public virtual void VisitSwitchSection(SwitchSectionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a SwitchStatementSyntax node.
        public virtual void VisitSwitchStatement(SwitchStatementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ThisExpressionSyntax node.
        public virtual void VisitThisExpression(ThisExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ThrowExpressionSyntax node.
        public virtual void VisitThrowExpression(ThrowExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a ThrowStatementSyntax node.
        public virtual void VisitThrowStatement(ThrowStatementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a TryStatementSyntax node.
        public virtual void VisitTryStatement(TryStatementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a TupleElementSyntax node.
        public virtual void VisitTupleElement(TupleElementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a TupleExpressionSyntax node.
        public virtual void VisitTupleExpression(TupleExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a TupleTypeSyntax node.
        public virtual void VisitTupleType(TupleTypeSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a TypeArgumentListSyntax node.
        public virtual void VisitTypeArgumentList(TypeArgumentListSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a TypeConstraintSyntax node.
        public virtual void VisitTypeConstraint(TypeConstraintSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a TypeCrefSyntax node.
        public virtual void VisitTypeCref(TypeCrefSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a TypeOfExpressionSyntax node.
        public virtual void VisitTypeOfExpression(TypeOfExpressionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a TypeParameterSyntax node.
        public virtual void VisitTypeParameter(TypeParameterSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a TypeParameterConstraintClauseSyntax node.
        public virtual void VisitTypeParameterConstraintClause(TypeParameterConstraintClauseSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a TypeParameterListSyntax node.
        public virtual void VisitTypeParameterList(TypeParameterListSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a UndefDirectiveTriviaSyntax node.
        public virtual void VisitUndefDirectiveTrivia(UndefDirectiveTriviaSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a UnsafeStatementSyntax node.
        public virtual void VisitUnsafeStatement(UnsafeStatementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a UsingDirectiveSyntax node.
        public virtual void VisitUsingDirective(UsingDirectiveSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a UsingStatementSyntax node.
        public virtual void VisitUsingStatement(UsingStatementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a VariableDeclarationSyntax node.
        public virtual void VisitVariableDeclaration(VariableDeclarationSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a VariableDeclaratorSyntax node.
        public virtual void VisitVariableDeclarator(VariableDeclaratorSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a WarningDirectiveTriviaSyntax node.
        public virtual void VisitWarningDirectiveTrivia(WarningDirectiveTriviaSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a WhenClauseSyntax node.
        public virtual void VisitWhenClause(WhenClauseSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a WhereClauseSyntax node.
        public virtual void VisitWhereClause(WhereClauseSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a WhileStatementSyntax node.
        public virtual void VisitWhileStatement(WhileStatementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a XmlCDataSectionSyntax node.
        public virtual void VisitXmlCDataSection(XmlCDataSectionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a XmlCommentSyntax node.
        public virtual void VisitXmlComment(XmlCommentSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a XmlCrefAttributeSyntax node.
        public virtual void VisitXmlCrefAttribute(XmlCrefAttributeSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a XmlElementSyntax node.
        public virtual void VisitXmlElement(XmlElementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a XmlElementEndTagSyntax node.
        public virtual void VisitXmlElementEndTag(XmlElementEndTagSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a XmlElementStartTagSyntax node.
        public virtual void VisitXmlElementStartTag(XmlElementStartTagSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a XmlEmptyElementSyntax node.
        public virtual void VisitXmlEmptyElement(XmlEmptyElementSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a XmlNameSyntax node.
        public virtual void VisitXmlName(XmlNameSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a XmlNameAttributeSyntax node.
        public virtual void VisitXmlNameAttribute(XmlNameAttributeSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a XmlPrefixSyntax node.
        public virtual void VisitXmlPrefix(XmlPrefixSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a XmlProcessingInstructionSyntax node.
        public virtual void VisitXmlProcessingInstruction(XmlProcessingInstructionSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a XmlTextSyntax node.
        public virtual void VisitXmlText(XmlTextSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a XmlTextAttributeSyntax node.
        public virtual void VisitXmlTextAttribute(XmlTextAttributeSyntax node);
        //
        // Summary:
        //     Called when the visitor visits a YieldStatementSyntax node.
        public virtual void VisitYieldStatement(YieldStatementSyntax node);
    }
}