namespace Gen.Templating
{
    using System;

    public class File : IEquatable<File>
    {
        private File(string name, string content)
        {
            Name = name;
            Content = content;
        }

        public string Name { get; }

        public string Content { get; }

        public static Builder NewBuilder()
        {
            return new Builder();
        }

        public static Builder WithName(string name)
        {
            return NewBuilder().SetName(name);
        }

        public class Builder
        {
            private string _name;

            private string _content;

            public Builder SetName(string name)
            {
                _name = name;
                return this;
            }

            public Builder SetContent(string content)
            {
                _content = content;
                return this;
            }

            public File Build()
            {
                return new File(_name, _content);
            }

            public File WithContent(string content)
            {
                return SetContent(content).Build();
            }
        }

        public bool Equals(File other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Content == other.Content;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((File)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Content);
        }
    }
}