using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace rosinator.core
{
    public class ClassVisitor : CSharpSyntaxRewriter
    {
        public List<Class> Classes { get; set; } = new List<Class>();

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            node = (ClassDeclarationSyntax)base.VisitClassDeclaration(node);

            var methodVisitor = new MethodVisitor();
            methodVisitor.Visit(node.SyntaxTree.GetRoot());

            var namespaceNode = node.SyntaxTree.GetRoot()
                .DescendantNodes()
                .OfType<NamespaceDeclarationSyntax>()
                .FirstOrDefault();
            var usingStatements = node.SyntaxTree.GetRoot()
                .DescendantNodes()
                .OfType<UsingDirectiveSyntax>()
                .ToList();
            var cls = new Class(node.Identifier.ValueText);
            cls.AddRange(methodVisitor.Methods);
            cls.Namespace = namespaceNode?.Name.ToString();

            foreach (var st in usingStatements)
            {
                var statement = st.Name.GetText().ToString();
                cls.Add(new Using(statement));
            }

            Classes.Add(cls);

            return node;
        }
    }
}