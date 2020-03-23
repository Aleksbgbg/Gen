namespace Gen.Templating
{
    public class Project
    {
        public Project(string name, Template template)
        {
            Name = name;
            Template = template;
        }

        public string Name { get; }

        public Template Template { get; }
    }
}