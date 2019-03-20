namespace StudyOOP.Convert
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using StudyOOP.Common;

    public abstract class ConverterBase
    {
        private readonly Encoding _inputEncode = Encoding.UTF8;

        private readonly Encoding _outputEncode = Encoding.UTF8;

        private string _outputDeletePrefix = "delete_";

        protected string OutputDeletePrefix
        {
            get
            {
                return this._outputDeletePrefix;
            }
        }

        public void ConvertDatToTSV()
        {
            if (!Directory.Exists(Settings.InputDirectory))
            {
                return;
            }

            try
            {
                if (!Directory.Exists(Settings.OutputDirectory))
                {
                    Directory.CreateDirectory(Settings.OutputDirectory);
                }

                foreach (var filePath in Directory.EnumerateFiles(Settings.InputDirectory))
                {
                    if (!this.IsFileNameValid(filePath))
                    {
                        continue;
                    }

                    var lines = File.ReadLines(filePath, this._inputEncode);
                    var fileName = this.GetFileName();
                    if (string.IsNullOrEmpty(fileName))
                    {
                        continue;
                    }

                    if (!this.IsHeaderLineValid(lines, fileName))
                    {
                        continue;
                    }

                    this.DoAppendAllText(lines);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void DoAppendAllText(IEnumerable<string> lines)
        {
            foreach (var line in lines.Skip(1))
            {
                // 全角文字ない前提
                var lineSize = this.GetBodyLineSize();
                if (lineSize == 0)
                {
                    continue;
                }

                if (!this.IsLengthValid(line, lineSize))
                {
                    continue;
                }

                var functionType = this.GetFunctionType(line, out int index);
                List<ConvertedFileInformation> convertedfileInformations = new List<ConvertedFileInformation>();

                if (!this.IsFileDetailValid(line, convertedfileInformations, functionType, index))
                {
                    continue;
                }

                foreach (ConvertedFileInformation convertedFileInformation in convertedfileInformations)
                {
                    File.AppendAllText(Path.Combine(Settings.OutputDirectory, convertedFileInformation.FileName), convertedFileInformation.Line, this._outputEncode);
                }
            }
        }

        protected abstract string GetFileName();

        protected abstract int GetBodyLineSize();

        protected abstract bool IsFileDetailValid(string line, List<ConvertedFileInformation> convertedfileInformations, string functionType, int index);

        protected virtual string GetFunctionType(string line, out int index)
        {
            index = 0;
            var length = 2;
            var functionType = line.Substring(index, length);
            index += length;
            return functionType;
        }

        protected virtual string MakeDeleteFileName(string outPutFileName, string functionType)
        {
            var fileName = outPutFileName;
            if (functionType != "10")
            {
                fileName = this.OutputDeletePrefix + outPutFileName;
            }

            return fileName;
        }

        protected virtual bool IsFileNameWithoutExtensionValid(string filePath, string fileName)
        {
            return Path.GetFileNameWithoutExtension(filePath).Equals(fileName, StringComparison.OrdinalIgnoreCase);
        }

        private bool IsExtensionValid(string filePath)
        {
            return Path.GetExtension(filePath).Equals(Settings.InputFileExtension, StringComparison.OrdinalIgnoreCase);
        }

        private bool IsHeaderLineValid(IEnumerable<string> lines, string fileName)
        {
            var headerLine = lines.FirstOrDefault();
            if (headerLine == null || !headerLine.Trim().Equals(fileName, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        private bool IsLengthValid(string line, int size)
        {
            return line.Length == size;
        }

        private bool IsFileNameValid(string filePath)
        {
            if (!this.IsExtensionValid(filePath))
            {
                return false;
            }

            var fileName = this.GetFileName();
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            if (!this.IsFileNameWithoutExtensionValid(filePath, fileName))
            {
                return false;
            }

            return true;
        }
    }
}