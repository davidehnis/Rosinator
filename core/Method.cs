using System.Collections.Generic;

namespace rosinator.core
{
    public class Method
    {
        public Method(string name)
        {
            Name = name;
        }

        public string ReturnType { get; set; }

        public IEnumerable<Parameter> Parameters => ParameterList;

        protected List<Parameter> ParameterList { get; } = new List<Parameter>();

        public string Name { get; }

        public string Body { get; protected set; }

        public void Update(string body)
        {
            Body = body;
        }

        public IEnumerable<Accessor> Accessors => AccessorList;

        protected List<Accessor> AccessorList { get; } = new List<Accessor>();

        public IEnumerable<Modifier> Modifiers => ModifierList;

        protected List<Modifier> ModifierList { get; } = new List<Modifier>();

        public void Add(Accessor accessor)
        {
            AccessorList.Add(accessor);
        }

        public void Add(Modifier modifier)
        {
            ModifierList.Add(modifier);
        }

        public void Add(Parameter parameter)
        {
            ParameterList.Add(parameter);
        }

        public override string ToString()
        {
            return $"{ReturnType} {Name}";
        }
    }
}