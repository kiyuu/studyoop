namespace StudyOOP.Common
{
    using System.Collections.Generic;
    using System.IO;

    public class FileSetting
    {
        public FileSetting(string name, int linesize)
        {
            this.Name = name;
            this.LineSize = linesize;
        }

        public string Name { get; }

        public int LineSize { get; }
    }
}