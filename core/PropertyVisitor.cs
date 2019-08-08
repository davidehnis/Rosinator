using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace rosinator.core
{
    public class PropertyVisitor : CSharpSyntaxRewriter
    {
        public List<Property> Properties { get; } = new List<Property>();

        public override SyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            node = (PropertyDeclarationSyntax)base.VisitPropertyDeclaration(node);
            var property = new Property(node.Identifier.ValueText, node.Type.ToString());

            foreach (var accessor in node.AccessorList.Accessors)
            {
                property.Add(new Accessor(accessor.ToString()));
            }

            foreach (var modifier in node.Modifiers)
            {
                property.Add(new Modifier(modifier.ValueText));
            }

            Properties.Add(property);
            return node;
        }
    }
}