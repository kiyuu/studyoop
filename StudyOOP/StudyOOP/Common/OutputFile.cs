namespace StudyOOP.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class OutputFile
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
