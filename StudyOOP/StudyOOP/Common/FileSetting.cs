namespace StudyOOP.Common
{
    using System.Collections.Generic;
    using System.IO;

    public struct FileSetting
    {
        public string Name;
        public int LineSize;
    }

    public struct OutputFile
    {
        public OutputFile(string line, string fileName)
        {
            this.Line = line;
            this.FileName = fileName;
        }

        public string Line { get; }

        public string FileName { get; }
    }
}