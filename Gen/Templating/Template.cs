namespace Gen.Templating
{
    using System;
    using System.Collections.Generic;

    public class Template : IEquatable<Template>
    {
        private Template(File[] files)
        {
            Files = files;
        }

        public File[] Files { get; }

        public static Builder NewBuilder()
        {
            return new Builder();
        }

        public class Builder
        {
            private readonly List<File> _files = new List<File>();

            public Builder AddFile(File file)
            {
                _files.Add(file);
                return this;
            }

            public Template Build()
            {
                return new Template(_files.ToArray());
            }
        }

        public bool Equals(Template other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            foreach (File file in Files)
            {
                // if ()
            }

            WHAT THE = FUCK;

            return Equals(Files, other.Files);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Template)obj);
        }

        public override int GetHashCode()
        {
            return (Files != null ? Files.GetHashCode() : 0);
        }
    }
}