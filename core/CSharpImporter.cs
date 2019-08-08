using System.Collections.Generic;
using Microsoft.CodeAnalysis.MSBuild;

namespace rosinator.core
{
    public class CSharpImporter
    {
        public IEnumerable<string> RetrieveClasses(string path)
        {
            var msWorkspace = MSBuildWorkspace.Create();

            var project = msWorkspace.OpenProjectAsync(path).Result;
            var classes = new List<string>();
            foreach (var document in project.Documents)
            {
                var semanticModel = document.GetSemanticModelAsync().Result;
                var classVisitor = new ClassVisitor();
                classVisitor.Visit(semanticModel.SyntaxTree.GetRoot());
                classes.AddRange(classVisitor.Classes);
            }

            return classes;
        }
    }
}