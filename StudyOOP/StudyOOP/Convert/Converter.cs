namespace StudyOOP.Convert
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using StudyOOP.Common;

    public static class Converter
    {
        private static readonly Encoding _inputEncode = Encoding.UTF8;

        private static readonly Encoding _outputEncode = Encoding.UTF8;

        private static readonly string _outputDeletePrefix = "delete_";

        private static readonly string _inputEmployeeFileName = "WXXX5555";

        private static readonly string _outputEmployeeFileName = "Employee";

        private static readonly string _outputEmployeeAuthorityFileName = "EmployeeAuthority";

        private static readonly int _employeeBodyLineSize = 18;

        private static readonly string _inputItemFileName = "WXXX6666";

        private static readonly string _outputItemFileName = "Item";

        private static readonly int _itemBodyLineSize = 35;

        private static FileSetting fileSetting;

        public static void ConvertFixedLengthFileToTSVFile(ConvertMode mode)
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

                    // モードによってファイル名、行の長さを取得
                    if (!SetInputFileSetting(mode))
                    {
                        continue;
                    }

                    if (fileSetting.Name == string.Empty)
                    {
                        continue;
                    }

                    // 取込ファイル名が「WXXX5555」もしくは「WXXX6666」なら処理続行
                    if (!IsFileNameMatchWithoutExtension(filePath))
                    {
                        continue;
                    }

                    var lines = File.ReadLines(filePath, _inputEncode);

                    var headerLine = lines.FirstOrDefault();
                    if (headerLine == null)
                    {
                        continue;
                    }

                    // 取込ファイルの1行目がファイル名と一致していれば処理続行
                    if (!IsHeaderLineMatchFilename(headerLine))
                    {
                        continue;
                    }

                    foreach (var line in lines.Skip(1))
                    {
                        // 全角文字ない前提
                        if (!IsLineLengthMatchBodyLineSize(line))
                        {
                            continue;
                        }

                        // ファイル出力処理
                        if (!OutputFiles(line))
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

        private static bool IsFileNameMatchWithoutExtension(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath).Equals(fileSetting.Name, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsHeaderLineMatchFilename(string headerLine)
        {
            return headerLine.Trim().Equals(fileSetting.Name, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsLineLengthMatchBodyLineSize(string line)
        {
            return line.Length == fileSetting.LineSize;
        }

        private static bool SetInputFileSetting(ConvertMode mode)
        {
            switch (mode)
            {
                case ConvertMode.ModeEmployee:
                    fileSetting.Name = _inputEmployeeFileName;
                    fileSetting.LineSize = _employeeBodyLineSize;
                    break;

                case ConvertMode.ModeItem:
                    fileSetting.Name = _inputItemFileName;
                    fileSetting.LineSize = _itemBodyLineSize;
                    break;

                default:
                    return false;
            }

            return true;
        }

        private static bool OutputFiles(string line)
        {
            var outputfiles = new List<OutputFile>();

            // 出力するファイルによって出力内容を変える
            if (!OutputFileContents(line, outputfiles))
            {
                return false;
            }

            foreach (var outputfile in outputfiles)
            {
                var outputFile = outputfile.FileName + Settings.OutputFileExtension;
                if (GetFunctionType(line) != "10")
                {
                    outputFile = _outputDeletePrefix + outputFile;
                }

                var outputline = outputfile.Line;
                File.AppendAllText(Path.Combine(Settings.OutputDirectory, outputFile), outputline, _outputEncode);
            }

            return true;
        }

        private static string GetFunctionType(string line)
        {
            var index = 0;
            var length = 2;
            return line.Substring(index, length);
        }

        private static bool OutputFileContents(string line, List<OutputFile> outputfiles)
        {
            if (fileSetting.Name == _inputEmployeeFileName)
            {
                return GetPrintContentModeEmployee(line, outputfiles);
            }

            if (fileSetting.Name == _inputItemFileName)
            {
                return GetPrintContentModeItem(line, outputfiles);
            }

            return false;
        }

        private static bool GetPrintContentModeEmployee(string line, List<OutputFile> outputfiles)
        {
            var index = 0;
            var length = 2;

            index += length;
            var authority = string.Empty;

            length = 5;
            if (!int.TryParse(line.Substring(index, length), out int code))
            {
                return false;
            }

            index += length;

            length = 10;
            var name = line.Substring(index, length);
            index += length;

            length = 1;
            authority = line.Substring(index, length);
            index += length;

            outputfiles.Add(new OutputFile(string.Join(Settings.TsvSeparater, code.ToString().PadLeft(5, '0'), name.Trim()) + Environment.NewLine, _outputEmployeeFileName));
            outputfiles.Add(new OutputFile(string.Join(Settings.TsvSeparater, code.ToString().PadLeft(5, '0'), authority) + Environment.NewLine, _outputEmployeeAuthorityFileName));

            return true;
        }

        private static bool GetPrintContentModeItem(string line, List<OutputFile> outputfiles)
        {
            var index = 0;
            var length = 2;

            index += length;
            length = 13;
            if (!long.TryParse(line.Substring(index, length), out long code))
            {
                return false;
            }

            index += length;

            length = 10;
            var name = line.Substring(index, length);
            index += length;

            length = 10;
            if (!decimal.TryParse(line.Substring(index, length), out decimal unitPrice))
            {
                return false;
            }

            index += length;

            outputfiles.Add(new OutputFile(string.Join(Settings.TsvSeparater, code.ToString().PadLeft(13, '0'), name.Trim(), unitPrice) + Environment.NewLine, _outputItemFileName));

            return true;
        }
    }
}
