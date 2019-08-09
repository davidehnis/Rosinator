using System.Collections.Generic;

namespace rosinator.core
{
    public class Class
    {
        public Class(string name)
        {
            Name = name;
        }

        public string Namespace { get; set; } = "";

        public IEnumerable<Method> Methods => MethodList;

        public IEnumerable<Using> Usings => UsingList;

        protected List<Method> MethodList { get; } = new List<Method>();

        protected List<Using> UsingList { get; } = new List<Using>();

        public void Add(Method method)
        {
            MethodList.Add(method);
        }

        public void Add(Using @using)
        {
            UsingList.Add(@using);
        }

        public void AddRange(IEnumerable<Method> methods)
        {
            MethodList.AddRange(methods);
        }

        public void AddRange(IEnumerable<Using> usings)
        {
            UsingList.AddRange(usings);
        }

        public string Name { get; }
    }
}