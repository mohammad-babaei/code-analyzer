using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RoslynAnalyzer.CleanCodeAnalyzers.Data;
using SuccincT.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoslynAnalyzer.CleanCodeAnalyzers
{
    abstract class BaseAnalyzer
    {
        public SyntaxTree syntaxTree;

        public BaseAnalyzer(SyntaxTree syntaxTree)
        {
            this.syntaxTree = syntaxTree;
        }

        abstract public AnalyzeResult analyze();

        protected static bool DoNotDescendIntoTypeDeclarations(SyntaxNode node)
        {
            var kind = node?.Kind();
            return kind != SyntaxKind.ClassDeclaration &&
                   kind != SyntaxKind.StructDeclaration;
        }

        protected static bool NodeIsPublicClassDeclaration(SyntaxNode node)
        {
            var kind = node?.Kind();
            return kind == SyntaxKind.ClassDeclaration &&
                   SyntaxNodeIsPublic(((BaseTypeDeclarationSyntax)node).Modifiers);
        }

        protected static bool NodeIsPropertyDeclaration(SyntaxNode node)
        {
            var kind = node?.Kind();
            return kind == SyntaxKind.PropertyDeclaration;
        }

        protected static bool SyntaxNodeIsPublic(SyntaxTokenList modifiers) =>
            modifiers.Count(t => t.Kind() == SyntaxKind.PublicKeyword) > 0;

        protected static bool NodeIsInterfaceDeclaration(SyntaxNode node)
        {
            var kind = node?.Kind();
            return kind == SyntaxKind.InterfaceDeclaration;
        }

        protected static bool PropertyHasIgnoreRuleAttribute(PropertyDeclarationSyntax property,
                                                          IEnumerable<Type> attributes) =>
                property.AttributeLists
                    .SelectMany(l => l.Attributes, (l, a) => a.Name.GetText().ToString())
                    .Any(name => attributes.TryFirst(t => MatchAttributeName(t, name)).HasValue);

        protected static bool MatchAttributeName(Type attributeType, string name) =>
            attributeType.Name.Replace("Attribute", "") == name || attributeType.Name == name;

        public static int GetLineNumber(SyntaxTree tree, SyntaxNode syntaxNode)
        {
            return tree.GetLineSpan(syntaxNode.Span).StartLinePosition.Line + 1;
        }
    }
}
