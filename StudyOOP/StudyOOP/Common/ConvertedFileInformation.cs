namespace StudyOOP.Common
{
    using System;
    using System.Collections.Generic;

    public class ConvertedFileInformation
    {
        private string _fileName;
        private string _line;

        public ConvertedFileInformation(string fileName, string line)
        {
            this._fileName = fileName;
            this._line = line;
        }

        public string FileName
        {
            get
            {
                return this._fileName;
            }
        }

        public string Line
        {
            get
            {
                return this._line;
            }
        }
    }
}
