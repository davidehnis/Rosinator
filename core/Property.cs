using System.Collections.Generic;

namespace rosinator.core
{
    public class Property
    {
        public Property(string name, string type)
        {
            Name = name;
            Type = type;
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

        public string Name { get; }

        public string Type { get; }

        public override string ToString()
        {
            return $"{Type} {Name}";
        }
    }
}