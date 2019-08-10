namespace rosinator.core
{
    public class Modifier
    {
        public Modifier(string name)
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