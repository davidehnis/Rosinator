using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace rosinator.core
{
    public class ClassVisitor : CSharpSyntaxRewriter
    {
        public List<Class> Classes { get; set; } = new List<Class>();

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            node = (ClassDeclarationSyntax)base.VisitClassDeclaration(node);

            var className = node.Identifier.ValueText;

            var methodVisitor = new MethodVisitor();
            methodVisitor.Visit(node.SyntaxTree.GetRoot());

            var cls = new Class(className);
            cls.AddRange(methodVisitor.Methods);

            Classes.Add(cls);

            return node;
        }
    }
}