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

        protected string OutputDeletePrefix => "delete_";

        protected abstract FileSetting FileSetting { get; }

        public void ConvertFixedLengthFileToTSVFile()
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
                    if (!Path.GetExtension(filePath).Equals(Settings.InputFileExtension, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    if (string.IsNullOrEmpty(this.FileSetting.Name))
                    {
                        continue;
                    }

                    // 取込ファイル名が「WXXX5555」もしくは「WXXX6666」なら処理続行
                    if (!this.IsFileNameMatchWithoutExtension(filePath))
                    {
                        continue;
                    }

                    var lines = File.ReadLines(filePath, this._inputEncode);

                    var headerLine = lines.FirstOrDefault();
                    if (headerLine == null)
                    {
                        continue;
                    }

                    // 取込ファイルの1行目がファイル名と一致していれば処理続行
                    if (!this.IsHeaderLineMatchFileName(headerLine))
                    {
                        continue;
                    }

                    foreach (var line in lines.Skip(1))
                    {
                        // 全角文字ない前提
                        if (!this.IsLineLengthMatchBodyLineSize(line))
                        {
                            continue;
                        }

                        // ファイル出力処理
                        if (!this.OutputFiles(line))
                        {
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected abstract bool GetPrintContent(string line, List<OutputFile> outputfiles);

        protected virtual bool IsFileNameMatchWithoutExtension(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath).Equals(this.FileSetting.Name, StringComparison.OrdinalIgnoreCase);
        }

        protected virtual string GetOutputFileName(string line)
        {
            if (this.GetFunctionType(line) != "10")
            {
                return this.OutputDeletePrefix;
            }

            return string.Empty;
        }

        private bool IsHeaderLineMatchFileName(string headerLine)
        {
            return headerLine.Trim().Equals(this.FileSetting.Name, StringComparison.OrdinalIgnoreCase);
        }

        private bool IsLineLengthMatchBodyLineSize(string line)
        {
            return line.Length == this.FileSetting.LineSize;
        }

        private bool OutputFiles(string line)
        {
            var outputfiles = new List<OutputFile>();

            //// 出力するファイルによって出力内容を変える
            if (!this.GetPrintContent(line, outputfiles))
            {
                return false;
            }

            foreach (var outputfile in outputfiles)
            {
                var outputFile = outputfile.FileName + Settings.OutputFileExtension;

                outputFile = this.GetOutputFileName(line) + outputFile;

                var outputline = outputfile.Line;
                File.AppendAllText(Path.Combine(Settings.OutputDirectory, outputFile), outputline, this._outputEncode);
            }

            return true;
        }

        private string GetFunctionType(string line)
        {
            var index = 0;
            var length = 2;
            return line.Substring(index, length);
        }
    }
}
