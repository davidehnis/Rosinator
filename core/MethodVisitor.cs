using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace rosinator.core
{
    public class Accessor
    {
        public Accessor(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

    public class Modifier
    {
        public Modifier(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

    public class MethodVisitor : CSharpSyntaxRewriter
    {
        public List<Method> Methods { get; set; } = new List<Method>();

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            node = (MethodDeclarationSyntax)base.VisitMethodDeclaration(node);
            var method = new Method(node.Identifier.ValueText);
            method.Update(node.Body.ToString());

            foreach (var parameter in node.ParameterList.Parameters)
            {
                var para = new Parameter(parameter.Identifier.ValueText, parameter.Type.ToString());
                method.Add(para);
            }

            Methods.Add(method);
            return node;
        }
    }
}