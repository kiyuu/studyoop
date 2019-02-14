namespace StudyOOP.Convert
{
    using System;
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

        public enum ConvertMode : int
        {
            ModeEmployee,
            ModeItem
        }

        public static void ConvertDatFileToTSVFile(int mode)
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

                    // モードによって取込ファイル名を設定
                    string sfilename = SetInputFileName(mode);
                    if (sfilename == string.Empty)
                    {
                        continue;
                    }

                    // 取込ファイル名が「WXXX5555」もしくは「WXXX6666」なら処理続行
                    if (!IsFileNameMatchWithoutExtension(filePath, sfilename))
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
                    if (!IsHeaderLineMatchFilename(headerLine, sfilename))
                    {
                        continue;
                    }

                    foreach (var line in lines.Skip(1))
                    {
                        // 全角文字ない前提
                        if (!IsLineLengthMatchBodyLineSize(line, mode))
                        {
                            continue;
                        }

                        var index = 0;
                        var length = 2;
                        var functionType = line.Substring(index, length);
                        index += length;

                        var authority = string.Empty;
                        var outputLineList = new System.Collections.Generic.List<string>();
                        var outputFileNameList = new System.Collections.Generic.List<string>();

                        if (mode == (int)ConvertMode.ModeEmployee)
                        {
                            length = 5;
                            if (!int.TryParse(line.Substring(index, length), out int code))
                            {
                                continue;
                            }

                            index += length;

                            length = 10;
                            var name = line.Substring(index, length);
                            index += length;

                            length = 1;
                            authority = line.Substring(index, length);
                            index += length;

                            outputLineList.Add(string.Join(Settings.TsvSeparater, code.ToString().PadLeft(5, '0'), name.Trim()) + Environment.NewLine);
                            outputLineList.Add(string.Join(Settings.TsvSeparater, code.ToString().PadLeft(5, '0'), authority) + Environment.NewLine);

                            outputFileNameList.Add(_outputEmployeeFileName);
                            outputFileNameList.Add(_outputEmployeeAuthorityFileName);
                        }
                        else if (mode == (int)ConvertMode.ModeItem)
                        {
                            length = 13;
                            if (!long.TryParse(line.Substring(index, length), out long code))
                            {
                                continue;
                            }

                            index += length;

                            length = 10;
                            var name = line.Substring(index, length);
                            index += length;

                            length = 10;
                            if (!decimal.TryParse(line.Substring(index, length), out decimal unitPrice))
                            {
                                continue;
                            }

                            index += length;

                            outputLineList.Add(string.Join(Settings.TsvSeparater, code.ToString().PadLeft(13, '0'), name.Trim(), unitPrice) + Environment.NewLine);
                            outputFileNameList.Add(_outputItemFileName);
                        }

                        var iCnt = 0;
                        foreach (string outputFileName in outputFileNameList)
                        {
                            var outputFile = outputFileName + Settings.OutputFileExtension;
                            if (functionType != "10")
                            {
                                outputFile = _outputDeletePrefix + outputFile;
                            }

                            var outputline = outputLineList[iCnt];
                            File.AppendAllText(Path.Combine(Settings.OutputDirectory, outputFile), outputline, _outputEncode);

                            iCnt += 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static string SetInputFileName(int mode)
        {
            switch (mode)
            {
                case (int)ConvertMode.ModeEmployee:
                    return _inputEmployeeFileName;

                case (int)ConvertMode.ModeItem:
                   return _inputItemFileName;

                default:
                    return string.Empty;
            }
        }

        private static bool IsFileNameMatchWithoutExtension(string filePath, string sfileName)
        {
            if (!Path.GetFileNameWithoutExtension(filePath).Equals(sfileName, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        private static bool IsHeaderLineMatchFilename(string headerLine, string sfileName)
        {
            if (!headerLine.Trim().Equals(sfileName, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        private static bool IsLineLengthMatchBodyLineSize(string line, int mode)
        {
            int iLineSize = 0;

            switch (mode)
            {
                case (int)ConvertMode.ModeEmployee:
                    iLineSize = _employeeBodyLineSize;
                    break;

                case (int)ConvertMode.ModeItem:
                    iLineSize = _itemBodyLineSize;
                    break;

                default:
                    return false;
            }

            if (line.Length != iLineSize)
            {
                return false;
            }

            return true;
        }
    }
}
