namespace StudyOOP.Convert
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using StudyOOP.Common;

    public static class Converter
    {
        private static readonly ReadOnlyDictionary<ConvertMode, FileSetting> _fileSettingDic;

        private static readonly Encoding _inputEncode = Encoding.UTF8;

        private static readonly Encoding _outputEncode = Encoding.UTF8;

        private static readonly string _outputDeletePrefix = "delete_";

        static Converter()
        {
            var dic = new Dictionary<ConvertMode, FileSetting>();

            // 従業員設定
            Func<string, List<OutputFile>> convertFunc;
            convertFunc = line =>
            {
                var outputFiles = new List<OutputFile>();
                int index;
                int length;
                var functionType = GetFunctionType(line, out index, out length);
                var outputEmployeeFileName = MakeOutputFileName("Employee", functionType);
                var outputEmployeeAuthorityFileName = MakeOutputFileName("EmployeeAuthority", functionType);

                index += length;
                var authority = string.Empty;

                length = 5;
                if (!int.TryParse(line.Substring(index, length), out int code))
                {
                    return outputFiles;
                }

                index += length;

                length = 10;
                var name = line.Substring(index, length);
                index += length;

                length = 1;
                authority = line.Substring(index, length);
                index += length;

                outputFiles.Add(new OutputFile(string.Join(Settings.TsvSeparater, code.ToString().PadLeft(5, '0'), name.Trim()) + Environment.NewLine, outputEmployeeFileName));
                outputFiles.Add(new OutputFile(string.Join(Settings.TsvSeparater, code.ToString().PadLeft(5, '0'), authority) + Environment.NewLine, outputEmployeeAuthorityFileName));

                return outputFiles;
            };

            dic.Add(ConvertMode.ModeEmployee, new FileSetting("WXXX5555", 18, convertFunc));

            // 商品マスタ設定
            convertFunc = line =>
            {
                var outputFiles = new List<OutputFile>();
                int index;
                int length;
                var outputItemFileName = MakeOutputFileName("Item", GetFunctionType(line, out index, out length));

                index += length;
                length = 13;
                if (!long.TryParse(line.Substring(index, length), out long code))
                {
                    return outputFiles;
                }

                index += length;

                length = 10;
                var name = line.Substring(index, length);
                index += length;

                length = 10;
                if (!decimal.TryParse(line.Substring(index, length), out decimal unitPrice))
                {
                    return outputFiles;
                }

                index += length;

                outputFiles.Add(new OutputFile(string.Join(Settings.TsvSeparater, code.ToString().PadLeft(13, '0'), name.Trim(), unitPrice) + Environment.NewLine, outputItemFileName));

                return outputFiles;
            };

            dic.Add(ConvertMode.ModeItem, new FileSetting("WXXX6666", 35, convertFunc));

            _fileSettingDic = new ReadOnlyDictionary<ConvertMode, FileSetting>(dic);
        }

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

                var fileSetting = GetFileSetting(mode);

                foreach (var filePath in Directory.EnumerateFiles(Settings.InputDirectory))
                {
                    if (!IsFileNameValid(filePath, ref fileSetting))
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
                    if (!IsHeaderLineMatchFilename(headerLine, ref fileSetting))
                    {
                        continue;
                    }

                    foreach (var line in lines.Skip(1))
                    {
                        // 全角文字ない前提
                        if (!IsLineLengthMatchBodyLineSize(line, ref fileSetting))
                        {
                            continue;
                        }

                        // ファイル出力処理
                        OutputFiles(line, ref fileSetting);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static bool IsFileNameValid(string filePath, ref FileSetting fileSetting)
        {
            if (!IsFileExtensionValid(filePath))
            {
                return false;
            }

            if (!IsFileNameMatchWithoutExtension(filePath, ref fileSetting))
            {
                return false;
            }

            return true;
        }

        private static bool IsFileExtensionValid(string filePath)
        {
            return Path.GetExtension(filePath).Equals(Settings.InputFileExtension, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsFileNameMatchWithoutExtension(string filePath, ref FileSetting fileSetting)
        {
            return Path.GetFileNameWithoutExtension(filePath).Equals(fileSetting.InputFileName, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsHeaderLineMatchFilename(string headerLine, ref FileSetting fileSetting)
        {
            return headerLine.Trim().Equals(fileSetting.InputFileName, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsLineLengthMatchBodyLineSize(string line, ref FileSetting fileSetting)
        {
            return line.Length == fileSetting.LineSize;
        }

        private static FileSetting GetFileSetting(ConvertMode mode)
        {
            if (!_fileSettingDic.TryGetValue(mode, out FileSetting fileSetting))
            {
                throw new ApplicationException("Arguments error");
            }

            if (string.IsNullOrEmpty(fileSetting.InputFileName)
                || fileSetting.LineSize == 0
                || fileSetting.ConvertFunc == null)
            {
                throw new ApplicationException("Arguments error");
            }

            return fileSetting;
        }

        private static void OutputFiles(string line, ref FileSetting fileSetting)
        {
            fileSetting
                .ConvertFunc(line)
                .ForEach(o => File.AppendAllText(Path.Combine(Settings.OutputDirectory, o.FileName), o.Line, _outputEncode));
        }

        private static string GetFunctionType(string line, out int index, out int length)
        {
            index = 0;
            length = 2;
            return line.Substring(index, length);
        }

        private static string MakeOutputFileName(string fileName, string functionType)
        {
            var fileNameResult = fileName + Settings.OutputFileExtension;
            if (functionType != "10")
            {
                fileNameResult = _outputDeletePrefix + fileNameResult;
            }

            return fileNameResult;
        }
    }
}
