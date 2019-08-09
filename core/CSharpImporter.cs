using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace rosinator.core
{
    public class CSharpImporter
    {
        public Class Convert(string text)
        {
            var tree = CSharpSyntaxTree.ParseText(text);
            var root = tree.GetCompilationUnitRoot();
            var classVisitor = new ClassVisitor();
            classVisitor.Visit(root);

            return classVisitor.Classes.Any()
                ? classVisitor.Classes.First()
                : null;
        }

        public IEnumerable<Class> RetrieveClasses(string path)
        {
            var classes = new List<Class>();
            foreach (var file in Directory.GetFiles(path))
            {
                var content = File.ReadAllText(file);
                var tree = CSharpSyntaxTree.ParseText(content);
                var root = tree.GetCompilationUnitRoot();
                var classVisitor = new ClassVisitor();
                classVisitor.Visit(root);
                classes.AddRange(classVisitor.Classes);
            }

            return classes;
        }
    }
}