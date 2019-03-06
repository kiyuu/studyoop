namespace StudyOOP.Common
{
    using System;
    using System.Collections.Generic;

    public struct FileSetting
    {
        public FileSetting(string fileName, int lineSize, Func<string, List<OutputFile>> convertFunc)
        {
            this.InputFileName = fileName;
            this.LineSize = lineSize;
            this.ConvertFunc = convertFunc;
        }

        public string InputFileName { get; }

        public int LineSize { get; }

        /// <summary>
        /// Gets (TextLine,OutputFileCollection)
        /// </summary>
        public Func<string, List<OutputFile>> ConvertFunc { get; }
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