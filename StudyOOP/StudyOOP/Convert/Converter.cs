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

        public static void ConvertDatToTSV(FileType fileType)
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
                    if (!IsFileNameValid(filePath, fileType))
                    {
                        continue;
                    }

                    var lines = File.ReadLines(filePath, _inputEncode);
                    var fileName = GetFileName(fileType);
                    if (fileName == string.Empty)
                    {
                        continue;
                    }

                    if (!IsHeaderLineValid(lines, fileName))
                    {
                        continue;
                    }

                    DoAppendAllText(lines, fileType);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static string GetFileName(FileType fileType)
        {
            var returnFileName = string.Empty;
            switch (fileType)
            {
                case FileType.FileTypeEmployee:
                    returnFileName = _inputEmployeeFileName;
                    break;
                case FileType.FileTypeItem:
                    returnFileName = _inputItemFileName;
                    break;
            }

            return returnFileName;
        }

        private static int GetBodyLineSize(FileType fileType)
        {
            var returnLineSize = 0;
            switch (fileType)
            {
                case FileType.FileTypeEmployee:
                    returnLineSize = _employeeBodyLineSize;
                    break;
                case FileType.FileTypeItem:
                    returnLineSize = _itemBodyLineSize;
                    break;
            }

            return returnLineSize;
        }

        private static void DoAppendAllText(IEnumerable<string> lines, FileType fileType)
        {
            foreach (var line in lines.Skip(1))
            {
                // 全角文字ない前提
                var lineSize = GetBodyLineSize(fileType);
                if (lineSize == 0)
                {
                    continue;
                }

                if (!IsLengthValid(line, lineSize))
                {
                    continue;
                }

                List<ConvertedFileInformation> convertedfileInformations = new List<ConvertedFileInformation>();

                if (!IsFileDetailValid(line, fileType, convertedfileInformations))
                {
                    continue;
                }

                foreach (ConvertedFileInformation convertedFileInformation in convertedfileInformations)
                {
                    File.AppendAllText(Path.Combine(Settings.OutputDirectory, convertedFileInformation.FileNames), convertedFileInformation.Lines, _outputEncode);
                }
            }
        }

        private static bool IsFileNameValid(string filePath, FileType fileType)
        {
            if (!IsExtensionValid(filePath))
            {
                return false;
            }

            var fileName = GetFileName(fileType);
            if (fileName == string.Empty)
            {
                return false;
            }

            if (!IsFileNameWithoutExtensionValid(filePath, fileName))
            {
                return false;
            }

            return true;
        }

        private static bool IsExtensionValid(string filePath)
        {
            return Path.GetExtension(filePath).Equals(Settings.InputFileExtension, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsFileNameWithoutExtensionValid(string filePath, string fileName)
        {
            return Path.GetFileNameWithoutExtension(filePath).Equals(fileName, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsHeaderLineValid(IEnumerable<string> lines, string fileName)
        {
            var headerLine = lines.FirstOrDefault();
            if (headerLine == null || !headerLine.Trim().Equals(fileName, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        private static bool IsLengthValid(string line, int size)
        {
            return line.Length == size;
        }

        private static bool IsFileDetailValid(string line, FileType fileType, List<ConvertedFileInformation> convertedfileInformations)
        {
            switch (fileType)
            {
                case FileType.FileTypeEmployee:
                    if (!IsFileDetailValidEmployee(line, convertedfileInformations))
                    {
                        return false;
                    }

                    break;
                case FileType.FileTypeItem:
                    if (!IsFileDetailValidItem(line, convertedfileInformations))
                    {
                        return false;
                    }

                    break;
            }

            return true;
        }

        private static bool IsFileDetailValidEmployee(string line, List<ConvertedFileInformation> convertedfileInformations)
        {
            var index = 0;
            var length = 2;
            var functionType = line.Substring(index, length);
            index += length;

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

            convertedfileInformations.Add(new ConvertedFileInformation() { FileNames = outputEmployeeFileName, Lines = employeeLine });
            convertedfileInformations.Add(new ConvertedFileInformation() { FileNames = outputEmployeeAuthorityFileName, Lines = employeeAuthority });

            return true;
        }

        private static bool IsFileDetailValidItem(string line, List<ConvertedFileInformation> convertedfileInformations)
        {
            var index = 0;
            var length = 2;
            var functionType = line.Substring(index, length);
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
            var itemLine = string.Join(Settings.TsvSeparater, code.ToString().PadLeft(13, '0'), name.Trim(), unitPrice) + Environment.NewLine;

            var outputItemFileName = _outputItemFileName + Settings.OutputFileExtension;
            if (functionType != "10")
            {
                outputItemFileName = _outputDeletePrefix + outputItemFileName;
            }

            convertedfileInformations.Add(new ConvertedFileInformation() { FileNames = outputItemFileName, Lines = itemLine });

            return true;
        }
    }
}