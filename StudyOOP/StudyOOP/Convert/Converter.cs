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

        private static FileSettings fileSettings;

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
                    if (!GetInputFileName(mode))
                    {
                        continue;
                    }

                    if (fileSettings.Name == string.Empty)
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
                        if (!(OutputFiles(line) == 0))
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
            return Path.GetFileNameWithoutExtension(filePath).Equals(fileSettings.Name, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsHeaderLineMatchFilename(string headerLine)
        {
            return headerLine.Trim().Equals(fileSettings.Name, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsLineLengthMatchBodyLineSize(string line)
        {
            return line.Length == fileSettings.LineSize;
        }

        private static bool GetInputFileName(ConvertMode mode)
        {
            switch (mode)
            {
                case ConvertMode.ModeEmployee:
                    fileSettings.Name = _inputEmployeeFileName;
                    fileSettings.LineSize = _employeeBodyLineSize;
                    break;

                case ConvertMode.ModeItem:
                    fileSettings.Name = _inputItemFileName;
                    fileSettings.LineSize = _itemBodyLineSize;
                    break;

                default:
                    return false;
            }

            return true;
        }

        private static int OutputFiles(string line)
        {
            var index = 0;
            var length = 2;
            var functionType = line.Substring(index, length);
            var cnt = 0;

            // 出力するファイルによって出力内容を変える
            if (!(OutputFileContents(index, length, line) == 0))
            {
                return -1;
            }

            foreach (var outputFileName in fileSettings.OutputFileName)
            {
                var outputFile = outputFileName + Settings.OutputFileExtension;
                if (functionType != "10")
                {
                    outputFile = _outputDeletePrefix + outputFile;
                }

                var outputline = fileSettings.OutputLine[cnt];
                File.AppendAllText(Path.Combine(Settings.OutputDirectory, outputFile), outputline, _outputEncode);
                cnt += 1;
            }

            return 0;
        }

        private static int OutputFileContents(int index, int length, string line)
        {
            fileSettings.OutputLine = new List<string>();
            fileSettings.OutputFileName = new List<string>();

            if (fileSettings.Name == _inputEmployeeFileName)
            {
                return GetPrintContentModeEmployee(index, length, line);
            }

            if (fileSettings.Name == _inputItemFileName)
            {
                return GetPrintContentModeItem(index, length, line);
            }

            return -1;
        }

        private static int GetPrintContentModeEmployee(int index, int length, string line)
        {
            index += length;
            var authority = string.Empty;

            length = 5;
            if (!int.TryParse(line.Substring(index, length), out int code))
            {
                return -1;
            }

            index += length;

            length = 10;
            var name = line.Substring(index, length);
            index += length;

            length = 1;
            authority = line.Substring(index, length);
            index += length;

            fileSettings.OutputLine.Add(string.Join(Settings.TsvSeparater, code.ToString().PadLeft(5, '0'), name.Trim()) + Environment.NewLine);
            fileSettings.OutputLine.Add(string.Join(Settings.TsvSeparater, code.ToString().PadLeft(5, '0'), authority) + Environment.NewLine);
            fileSettings.OutputFileName.Add(_outputEmployeeFileName);
            fileSettings.OutputFileName.Add(_outputEmployeeAuthorityFileName);

            return 0;
        }

        private static int GetPrintContentModeItem(int index, int length, string line)
        {
            index += length;
            length = 13;
            if (!long.TryParse(line.Substring(index, length), out long code))
            {
                return -1;
            }

            index += length;

            length = 10;
            var name = line.Substring(index, length);
            index += length;

            length = 10;
            if (!decimal.TryParse(line.Substring(index, length), out decimal unitPrice))
            {
                return -1;
            }

            index += length;

            fileSettings.OutputLine.Add(string.Join(Settings.TsvSeparater, code.ToString().PadLeft(13, '0'), name.Trim(), unitPrice) + Environment.NewLine);
            fileSettings.OutputFileName.Add(_outputItemFileName);

            return 0;
        }
    }
}
