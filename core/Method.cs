namespace rosinator.core
{
    public class Method
    {
        public Method(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public string Body { get; protected set; }

        public void Update(string body)
        {
            Body = body;
        }

        public void Add(Parameter parameter)
        {
        }
    }
}