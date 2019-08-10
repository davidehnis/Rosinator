using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Reflection;

namespace rosinator.core.tests
{
    [TestClass]
    public class Importer
    {
        [TestMethod]
        public void Importer_Returns_Valid_Classes()
        {
            // Arrange
            var assembly = Assembly.GetExecutingAssembly().Location;
            var root = Path.GetFullPath(Path.Combine(assembly, @"..\..\..\..\"));
            var project = Path.GetFullPath(Path.Combine(root, @"core\"));
            var importer = new CSharpImporter();

            // Act
            var classes = importer.RetrieveClasses(project);

            // Assert
            Assert.IsTrue(classes.Any());
        }

        [TestMethod]
        public void Importer_Convert_Returns_Valid_Class()
        {
            // Arrange
            var importer = new CSharpImporter();
            var source = CreateClassSourceCode();

            // Act
            var @class = importer.Convert(source);

            // Assert
            Assert.IsNotNull(@class);
            Assert.AreEqual(5, @class.Usings.Count());
            Assert.AreEqual("Microsoft.CodeAnalysis", @class.Usings.First().Text);
            Assert.AreEqual("rosinator.core", @class.Namespace);
            Assert.IsNotNull(@class.Methods.FirstOrDefault(m => m.Name == "Convert"));
        }

        private string CreateClassSourceCode()
        {
            var sb = new System.Text.StringBuilder(1448);
            sb.AppendLine(@"using Microsoft.CodeAnalysis;");
            sb.AppendLine(@"using Microsoft.CodeAnalysis.CSharp;");
            sb.AppendLine(@"using System.Collections.Generic;");
            sb.AppendLine(@"using System.IO;");
            sb.AppendLine(@"using System.Linq;");
            sb.AppendLine(@"");
            sb.AppendLine(@"namespace rosinator.core");
            sb.AppendLine(@"{");
            sb.AppendLine(@"    public class CSharpImporter");
            sb.AppendLine(@"    {");
            sb.AppendLine(@"        public Class Convert(string text)");
            sb.AppendLine(@"        {");
            sb.AppendLine(@"            var tree = CSharpSyntaxTree.ParseText(text);");
            sb.AppendLine(@"            var root = tree.GetCompilationUnitRoot();");
            sb.AppendLine(@"            var classVisitor = new ClassVisitor();");
            sb.AppendLine(@"            classVisitor.Visit(root);");
            sb.AppendLine(@"");
            sb.AppendLine(@"            return classVisitor.Classes.Any()");
            sb.AppendLine(@"                ? classVisitor.Classes.First()");
            sb.AppendLine(@"                : null;");
            sb.AppendLine(@"        }");
            sb.AppendLine(@"");
            sb.AppendLine(@"        public IEnumerable<Class> RetrieveClasses(string path)");
            sb.AppendLine(@"        {");
            sb.AppendLine(@"            var classes = new List<Class>();");
            sb.AppendLine(@"            foreach (var file in Directory.GetFiles(path))");
            sb.AppendLine(@"            {");
            sb.AppendLine(@"                var content = File.ReadAllText(file);");
            sb.AppendLine(@"                var tree = CSharpSyntaxTree.ParseText(content);");
            sb.AppendLine(@"                var root = tree.GetCompilationUnitRoot();");
            sb.AppendLine(@"                var classVisitor = new ClassVisitor();");
            sb.AppendLine(@"                classVisitor.Visit(root);");
            sb.AppendLine(@"                classes.AddRange(classVisitor.Classes);");
            sb.AppendLine(@"            }");
            sb.AppendLine(@"");
            sb.AppendLine(@"            return classes;");
            sb.AppendLine(@"        }");
            sb.AppendLine(@"");
            sb.AppendLine(@"        public string Failure { get; protected set; }");
            sb.AppendLine(@"");
            sb.AppendLine(@"        private void MsWorkspace_WorkspaceFailed(object sender, Microsoft.CodeAnalysis.WorkspaceDiagnosticEventArgs e)");
            sb.AppendLine(@"        {");
            sb.AppendLine(@"            Failure = e.Diagnostic.Message;");
            sb.AppendLine(@"        }");
            sb.AppendLine(@"    }");
            sb.AppendLine(@"}");

            return sb.ToString();
        }
    }
}