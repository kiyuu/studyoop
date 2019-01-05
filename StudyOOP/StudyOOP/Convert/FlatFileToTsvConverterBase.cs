namespace StudyOOP.Convert
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using StudyOOP.Common;

    public abstract class FlatFileToTsvConverterBase
    {
        private readonly Encoding _inputEncode = Encoding.UTF8;

        private readonly Encoding _outputEncode = Encoding.UTF8;

        private readonly int _headerLineCount = 1;

        private readonly string _outputDeletePrefix = "delete_";

        protected abstract string InputFileName { get; }

        protected abstract int BodyLineSize { get; }

        public void Execute()
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

                    var headerLine = lines.FirstOrDefault();
                    if (!this.IsHeaderLineValid(headerLine))
                    {
                        continue;
                    }

                    foreach (var bodyLine in lines.Skip(this._headerLineCount))
                    {
                        if (!this.IsBodyLineSizeValid(bodyLine))
                        {
                            continue;
                        }

                        this.ConvertFlatLineToTsvs(bodyLine)
                            .ForEach((c) => File.AppendAllText(Path.Combine(Settings.OutputDirectory, c.FileName), c.Line, this._outputEncode));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected bool IsFileNameValid(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return false;
            }

            if (!this.IsExtensionValid(filePath))
            {
                return false;
            }

            if (!this.IsFileNameWithoutExtensionValid(filePath))
            {
                return false;
            }

            return true;
        }

        protected virtual bool IsExtensionValid(string filePath)
        {
            return Path.GetExtension(filePath).Equals(Settings.InputFileExtension, StringComparison.OrdinalIgnoreCase);
        }

        protected virtual bool IsFileNameWithoutExtensionValid(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath).Equals(this.InputFileName, StringComparison.OrdinalIgnoreCase);
        }

        protected virtual bool IsHeaderLineValid(string headerLine)
        {
            if (string.IsNullOrEmpty(headerLine))
            {
                return false;
            }

            if (!headerLine.Trim().Equals(this.InputFileName, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        protected virtual string GetFunctionType(string bodyLine, out int nextStartIndex)
        {
            nextStartIndex = 0;
            var length = 2;
            var resultFunctionType = bodyLine.Substring(nextStartIndex, length);
            nextStartIndex += length;
            return resultFunctionType;
        }

        protected abstract List<ConvertedRecordInfo> ConvertFlatLineToTsvs(string bodyLine, string functionType, int index);

        protected string MakeOutputFileName(string functionType, string fileNameWithoutExtension)
        {
            var result = fileNameWithoutExtension + Settings.OutputFileExtension;
            if (functionType != "10")
            {
                result = this._outputDeletePrefix + result;
            }

            return result;
        }

        private bool IsBodyLineSizeValid(string bodyLine)
        {
            if (string.IsNullOrEmpty(bodyLine))
            {
                return false;
            }

            // 全角文字ない前提
            if (bodyLine.Length != this.BodyLineSize)
            {
                return false;
            }

            return true;
        }

        private List<ConvertedRecordInfo> ConvertFlatLineToTsvs(string bodyLine)
        {
            var functionType = this.GetFunctionType(bodyLine, out int index);
            return this.ConvertFlatLineToTsvs(bodyLine, functionType, index);
        }
    }
}
