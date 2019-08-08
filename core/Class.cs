using System.Collections.Generic;

namespace rosinator.core
{
    public class Class
    {
        public Class(string name)
        {
            Name = name;
        }

        public IEnumerable<Method> Methods => MethodList;

        protected List<Method> MethodList { get; } = new List<Method>();

        public void Add(Method method)
        {
            MethodList.Add(method);
        }

        public void AddRange(IEnumerable<Method> methods)
        {
            MethodList.AddRange(methods);
        }

        public string Name { get; }
    }
}