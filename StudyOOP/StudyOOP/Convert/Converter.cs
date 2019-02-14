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

        public static void ConvertToTSV(int fileType)
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

                    if (!IsHeaderLineValid(lines, Get_FileName(fileType)))
                    {
                        continue;
                    }

                    AppendAllText_Main(lines, fileType);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static string Get_FileName(int filetype)
        {
            var returnFileName = string.Empty;
            switch (filetype)
            {
                case Constants.FileTypeEmployee:
                    returnFileName = "WXXX5555";
                    break;
                case Constants.FileTypeItem:
                    returnFileName = "WXXX6666";
                    break;
            }

            return returnFileName;
        }

        private static int Get_BodyLineSize(int filetype)
        {
            var returnLineSize = 0;
            switch (filetype)
            {
                case Constants.FileTypeEmployee:
                    returnLineSize = _employeeBodyLineSize;
                    break;
                case Constants.FileTypeItem:
                    returnLineSize = _itemBodyLineSize;
                    break;
            }

            return returnLineSize;
        }

        private static void AppendAllText_Main(IEnumerable<string> lines, int fileType)
        {
            foreach (var line in lines.Skip(1))
            {
                // 全角文字ない前提
                if (!IsLengthValid(line, Get_BodyLineSize(fileType)))
                {
                    continue;
                }

                string functiontype = string.Empty;
                var lineList = new List<string>();
                var fileDictionary = new Dictionary<string, string>();

                if (!IsFileDetailValid(line, fileType, ref functiontype, lineList))
                {
                    continue;
                }

                fileDictionary = CreateFileDictionary(Create_outputFileName_Main(fileType, functiontype), lineList);

                foreach (KeyValuePair<string, string> kvp in fileDictionary)
                {
                    File.AppendAllText(Path.Combine(Settings.OutputDirectory, kvp.Key), kvp.Value, _outputEncode);
                }
            }
        }

        private static bool IsFileNameValid(string filePath, int fileType)
        {
            if (!IsExtensionValid(filePath))
            {
                return false;
            }

            if (!IsFileNameWithoutExtensionValid(filePath, Get_FileName(fileType)))
            {
                return false;
            }

            return true;
        }

        private static bool IsExtensionValid(string filePath)
        {
            return Path.GetExtension(filePath).Equals(Settings.InputFileExtension, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsFileNameWithoutExtensionValid(string filePath, string ofilename)
        {
            return Path.GetFileNameWithoutExtension(filePath).Equals(ofilename, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsHeaderLineValid(IEnumerable<string> lines, string filename)
        {
            var headerLine = lines.FirstOrDefault();
            if (headerLine == null || !headerLine.Trim().Equals(filename, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        private static bool IsLengthValid(string line, int size)
        {
            if (line.Length != size)
            {
                return false;
            }

            return true;
        }

        private static bool IsFileDetailValid(string line, int fileType, ref string functiontype, List<string> returnlineList)
        {
            var lineList = new List<string>();
            int icode = 0;
            long lcode = 0;
            decimal unitPrice = 0;

            switch (fileType)
            {
                case Constants.FileTypeEmployee:
                    if (!IsFileDetailValid_Employee(line, lineList, ref functiontype, ref icode))
                    {
                        return false;
                    }

                    returnlineList = EditLine_Employee(lineList, icode, returnlineList);
                    break;
                case Constants.FileTypeItem:
                     if (!IsFileDetailValid_Item(line, lineList, ref functiontype,  ref lcode, ref unitPrice))
                    {
                        return false;
                    }

                    returnlineList = EditLine_Item(lineList, lcode, unitPrice, returnlineList);
                    break;
            }

                return true;
        }

        private static Dictionary<string, string> CreateFileDictionary(List<string> names, List<string> lines)
        {
            var fileDictionary = new Dictionary<string, string>();
            for (int i = 0; i <= names.Count - 1; i++)
            {
                fileDictionary.Add(names[i], lines[i]);
            }

            return fileDictionary;
        }

        private static List<string> EditLine_Employee(List<string> names, int code, List<string> lines)
        {
            string returnLine = string.Empty;
            foreach (var name in names)
            {
                lines.Add(string.Join(Settings.TsvSeparater, code.ToString().PadLeft(5, '0'), name.Trim()) + Environment.NewLine);
            }

            return lines;
        }

        private static List<string> EditLine_Item(List<string> names, long code, decimal unitPrice, List<string> lines)
        {
            string returnLine = string.Empty;
            foreach (var name in names)
            {
                lines.Add(string.Join(Settings.TsvSeparater, code.ToString().PadLeft(13, '0'), name.Trim(), unitPrice) + Environment.NewLine);
            }

            return lines;
        }

        private static bool IsFileDetailValid_Employee(string line, List<string> names, ref string sfunctionType, ref int icode)
        {
            var index = 0;
            var length = 2;
            sfunctionType = line.Substring(index, length);
            index += length;

            length = 5;
                if (!int.TryParse(line.Substring(index, length), out icode))
            {
                return false;
            }

            index += length;

            length = 10;
            var name = line.Substring(index, length);
            names.Add(name);
            index += length;

            length = 1;
            var authority = line.Substring(index, length);
            names.Add(authority);
            index += length;

            return true;
        }

        private static bool IsFileDetailValid_Item(string line, List<string> names, ref string sfunctionType, ref long lcode, ref decimal dunitPrice)
        {
            var index = 0;
            var length = 2;
            sfunctionType = line.Substring(index, length);
            index += length;

            length = 13;
                if (!long.TryParse(line.Substring(index, length), out lcode))
                {
                return false;
            }

            index += length;

            length = 10;
            var name = line.Substring(index, length);
            names.Add(name);
            index += length;

            length = 10;
                if (!decimal.TryParse(line.Substring(index, length), out dunitPrice))
                {
                return false;
            }

            index += length;

            return true;
        }

     private static List<string> Create_outputFileName_Main(int fileType, string functionType)
        {
            var fileNameList = GetoutputFileName(fileType);

            var return_fileNameList = new List<string>();

            foreach (var list in fileNameList)
            {
                var outputFileName = list + Settings.OutputFileExtension;
                if (functionType != "10")
                {
                    outputFileName = _outputDeletePrefix + outputFileName;
                }

                return_fileNameList.Add(outputFileName);
            }

            return return_fileNameList;
        }

        private static List<string> GetoutputFileName(int fileType)
        {
            var fileNameList = new List<string>();

            switch (fileType)
            {
                case Constants.FileTypeEmployee:
                    fileNameList.Add(_outputEmployeeFileName);
                    fileNameList.Add(_outputEmployeeAuthorityFileName);
                    break;
                case Constants.FileTypeItem:
                    fileNameList.Add(_outputItemFileName);
                    break;
            }

            return fileNameList;
        }
    }
}