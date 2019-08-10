using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace rosinator.core
{
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

            method.ReturnType = node.ReturnType.ToString();
            foreach (var modifier in node.Modifiers)
            {
                method.Add(new Modifier(modifier.ValueText));
            }

            Methods.Add(method);
            return node;
        }
    }
}