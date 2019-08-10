namespace rosinator.core
{
    public class Accessor
    {
        public Accessor(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}