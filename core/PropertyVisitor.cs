using System.Collections.Generic;
using System.Linq;
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

            if (node.AccessorList != null)
            {
                var accessors = node.AccessorList.Accessors.ToList();
                foreach (var accessor in accessors)
                {
                    property.Add(new Accessor(accessor.ToString()));
                }
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