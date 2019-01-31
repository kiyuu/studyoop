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

        public static bool Chk_Exists_Directory(string inputDirectory)
        {
            if (!Directory.Exists(inputDirectory))
            {
                return false;
            }

            return true;
        }

        public static bool Chk_Extension(string filePath)
        {
            if (!Path.GetExtension(filePath).Equals(Settings.InputFileExtension, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        public static bool Chk_FileNameWithoutExtension(string filePath, string ofilename)
        {
            if (!Path.GetFileNameWithoutExtension(filePath).Equals(ofilename, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        public static bool Chk_HeaderLine(IEnumerable<string> lines, string filename)
        {
            var headerLine = lines.FirstOrDefault();
            if (headerLine == null)
            {
                return false;
            }

            if (!headerLine.Trim().Equals(filename, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        public static bool Chk_Length(string line, int size)
        {
            if (line.Length != size)
            {
                return false;
            }

            return true;
        }

        public static void AppendAllText_Employee(IEnumerable<string> lines)
        {
            foreach (var line in lines.Skip(1))
            {
                // 全角文字ない前提
                if (!Chk_Length(line, _employeeBodyLineSize))
                {
                    continue;
                }

                var index = 0;
                var length = 2;
                var functionType = line.Substring(index, length);
                index += length;

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
                var authority = line.Substring(index, length);
                index += length;

                var employeeLine = string.Join(Settings.TsvSeparater, code.ToString().PadLeft(5, '0'), name.Trim()) + Environment.NewLine;
                var employeeAuthority = string.Join(Settings.TsvSeparater, code.ToString().PadLeft(5, '0'), authority) + Environment.NewLine;

                var outputEmployeeFileName = _outputEmployeeFileName + Settings.OutputFileExtension;
                var outputEmployeeAuthorityFileName = _outputEmployeeAuthorityFileName + Settings.OutputFileExtension;
                if (functionType != "10")
                {
                    outputEmployeeFileName = _outputDeletePrefix + outputEmployeeFileName;
                    outputEmployeeAuthorityFileName = _outputDeletePrefix + outputEmployeeAuthorityFileName;
                }

                File.AppendAllText(Path.Combine(Settings.OutputDirectory, outputEmployeeFileName), employeeLine, _outputEncode);
                File.AppendAllText(Path.Combine(Settings.OutputDirectory, outputEmployeeAuthorityFileName), employeeAuthority, _outputEncode);
            }
        }

        public static void AppendAllText_Item(IEnumerable<string> lines)
        {
            foreach (var line in lines.Skip(1))
            {
                // 全角文字ない前提
                if (!Chk_Length(line, _itemBodyLineSize))
                {
                    continue;
                }

                var index = 0;
                var length = 2;
                var functionType = line.Substring(index, length);
                index += length;

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

                var itemLine = string.Join(Settings.TsvSeparater, code.ToString().PadLeft(13, '0'), name.Trim(), unitPrice) + Environment.NewLine;

                var outputItemFileName = _outputItemFileName + Settings.OutputFileExtension;
                if (functionType != "10")
                {
                    outputItemFileName = _outputDeletePrefix + outputItemFileName;
                }

                File.AppendAllText(Path.Combine(Settings.OutputDirectory, outputItemFileName), itemLine, _outputEncode);
            }
        }

        public static void ConvertWXXX5555ToEmployeeTSV()
        {
            if (!Chk_Exists_Directory(Settings.InputDirectory))
            {
                return;
            }

            try
            {
                if (!Chk_Exists_Directory(Settings.OutputDirectory))
                {
                    Directory.CreateDirectory(Settings.OutputDirectory);
                }

                foreach (var filePath in Directory.EnumerateFiles(Settings.InputDirectory))
                {
                    if (!Chk_Extension(filePath))
                    {
                        continue;
                    }

                    if (!Chk_FileNameWithoutExtension(filePath, _inputEmployeeFileName))
                    {
                        continue;
                    }

                    var lines = File.ReadLines(filePath, _inputEncode);

                    if (!Chk_HeaderLine(lines, _inputEmployeeFileName))
                    {
                        continue;
                    }

                    AppendAllText_Item(lines);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void ConvertWXXX6666ToItemTSV()
        {
            if (!Chk_Exists_Directory(Settings.InputDirectory))
            {
                return;
            }

            try
            {
                if (!Chk_Exists_Directory(Settings.OutputDirectory))
                {
                    Directory.CreateDirectory(Settings.OutputDirectory);
                }

                foreach (var filePath in Directory.EnumerateFiles(Settings.InputDirectory))
                {
                    if (!Chk_Extension(filePath))
                    {
                        continue;
                    }

                    if (!Chk_FileNameWithoutExtension(filePath, _inputItemFileName))
                    {
                        continue;
                    }

                    var lines = File.ReadLines(filePath, _inputEncode);

                    if (!Chk_HeaderLine(lines, _inputItemFileName))
                    {
                        continue;
                    }

                    AppendAllText_Employee(lines);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}