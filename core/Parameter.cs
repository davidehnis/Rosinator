﻿namespace rosinator.core
{
    public class Parameter
    {
        public Parameter(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }

        public string Type { get; }
    }
}