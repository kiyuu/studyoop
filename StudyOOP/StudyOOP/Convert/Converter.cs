namespace StudyOOP.Convert
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using StudyOOP.Common;

    public enum FlatFileType
    {
        WXXX5555,
        WXXX6666
    }

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

        public static void ConvertFlatFileToTsv(FlatFileType flatFileType)
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
                    if (!IsFileNameValid(filePath, flatFileType))
                    {
                        continue;
                    }

                    var lines = File.ReadLines(filePath, _inputEncode);

                    var headerLine = lines.FirstOrDefault();
                    if (!IsHeaderValid(headerLine, flatFileType))
                    {
                        continue;
                    }

                    foreach (var bodyLine in lines.Skip(1))
                    {
                        if (!IsBodyLineSizeValid(bodyLine, flatFileType))
                        {
                            continue;
                        }

                        ConvertFlatLineToTsvs(bodyLine, flatFileType)
                            .ForEach((p) => File.AppendAllText(Path.Combine(Settings.OutputDirectory, p.Key), p.Value, _outputEncode));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static bool IsFileNameValid(string filePath, FlatFileType flatFileType)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return false;
            }

            if (!Path.GetExtension(filePath).Equals(Settings.InputFileExtension, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (!Path.GetFileNameWithoutExtension(filePath).Equals(GetInputFileName(flatFileType), StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        private static bool IsHeaderValid(string headerLine, FlatFileType flatFileType)
        {
            if (string.IsNullOrEmpty(headerLine))
            {
                return false;
            }

            if (!headerLine.Trim().Equals(GetInputFileName(flatFileType), StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        private static bool IsBodyLineSizeValid(string bodyLine, FlatFileType flatFileType)
        {
            if (string.IsNullOrEmpty(bodyLine))
            {
                return false;
            }

            // 全角文字ない前提
            if (bodyLine.Length != GetBodyLineSize(flatFileType))
            {
                return false;
            }

            return true;
        }

        private static string GetInputFileName(FlatFileType flatFileType)
        {
            var result = string.Empty;

            switch (flatFileType)
            {
                case FlatFileType.WXXX5555:
                    result = _inputEmployeeFileName;
                    break;
                case FlatFileType.WXXX6666:
                    result = _inputItemFileName;
                    break;
                default:
                    throw new Exception($"argument error");
            }

            return result;
        }

        private static int GetBodyLineSize(FlatFileType flatFileType)
        {
            var result = 0;

            switch (flatFileType)
            {
                case FlatFileType.WXXX5555:
                    result = _employeeBodyLineSize;
                    break;
                case FlatFileType.WXXX6666:
                    result = _itemBodyLineSize;
                    break;
                default:
                    throw new Exception($"argument error");
            }

            return result;
        }

        private static List<KeyValuePair<string, string>> ConvertFlatLineToTsvs(string bodyLine, FlatFileType flatFileType)
        {
            var result = new List<KeyValuePair<string, string>>();
            switch (flatFileType)
            {
                case FlatFileType.WXXX5555:
                    result.AddRange(ConvertWXXX5555ToEmployeeTSV(bodyLine));
                    break;

                case FlatFileType.WXXX6666:
                    result.AddRange(ConvertWXXX6666ToItemTSV(bodyLine));
                    break;
                default:
                    throw new Exception($"argument error");
            }

            return result;
        }

        private static List<KeyValuePair<string, string>> ConvertWXXX5555ToEmployeeTSV(string bodyLine)
        {
            var result = new List<KeyValuePair<string, string>>();
            var index = 0;
            var length = 2;
            var functionType = bodyLine.Substring(index, length);
            index += length;

            length = 5;
            if (!int.TryParse(bodyLine.Substring(index, length), out int code))
            {
                return result;
            }

            index += length;

            length = 10;
            var name = bodyLine.Substring(index, length);
            index += length;

            length = 1;
            var authority = bodyLine.Substring(index, length);
            index += length;

            var employeeLine = string.Join(Settings.TsvSeparater, code.ToString().PadLeft(5, '0'), name.Trim()) + Environment.NewLine;
            var employeeAuthorityLine = string.Join(Settings.TsvSeparater, code.ToString().PadLeft(5, '0'), authority) + Environment.NewLine;

            result.Add(new KeyValuePair<string, string>(MakeOutputFileName(functionType, _outputEmployeeFileName), employeeLine));
            result.Add(new KeyValuePair<string, string>(MakeOutputFileName(functionType, _outputEmployeeAuthorityFileName), employeeAuthorityLine));
            return result;
        }

        private static List<KeyValuePair<string, string>> ConvertWXXX6666ToItemTSV(string bodyLine)
        {
            var result = new List<KeyValuePair<string, string>>();
            var index = 0;
            var length = 2;
            var functionType = bodyLine.Substring(index, length);
            index += length;

            length = 13;
            if (!long.TryParse(bodyLine.Substring(index, length), out long code))
            {
                return result;
            }

            index += length;

            length = 10;
            var name = bodyLine.Substring(index, length);
            index += length;

            length = 10;
            if (!decimal.TryParse(bodyLine.Substring(index, length), out decimal unitPrice))
            {
                return result;
            }

            index += length;

            var itemLine = string.Join(Settings.TsvSeparater, code.ToString().PadLeft(13, '0'), name.Trim(), unitPrice) + Environment.NewLine;
            result.Add(new KeyValuePair<string, string>(MakeOutputFileName(functionType, _outputItemFileName), itemLine));
            return result;
        }

        private static string MakeOutputFileName(string functionType, string fileNameWithoutExtension)
        {
            var result = fileNameWithoutExtension + Settings.OutputFileExtension;
            if (functionType != "10")
            {
                result = _outputDeletePrefix + result;
            }

            return result;
        }
    }
}
