namespace StudyOOP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// datファイルをtsvに変換
    /// </summary>
    internal static class Converter
    {
        private static readonly Encoding InputEncode = Encoding.UTF8;
        private static readonly Encoding OutputEncode = Encoding.UTF8;
        private static readonly string OutputDeletePrefix = "delete_";
        private static readonly string InputEmployeeFileName = "WXXX5555";
        private static readonly string InputItemFileName = "WXXX6666";
        private static readonly int EmployeeBodySize = 18;
        private static readonly int ItemBodySize = 35;
        private static readonly string OutputEmployeeName = "Employee";
        private static readonly string OutputEnployeeAuthorityName = "EmployeeAuthority";
        private static readonly string OutputItemName = "Item";

        /// <summary>
        /// 入力するファイルの種類
        /// </summary>
        public enum FlatFileType
        {
            /// <summary>
            /// WXXXX5555.datファイル
            /// </summary>
            WXXX5555,

            /// <summary>
            /// WXXXX6666.datファイル
            /// </summary>
            WXXX6666,
        }

        ////public struct Sample
        ////{
        ////    public string FileName;
        ////    public string FileLine;

        ////    public Sample(string fileName, string fileLine)
        ////    {
        ////        this.FileName = fileName;
        ////        this.FileLine = fileLine;
        ////    }
        ////}

        /// <summary>
        /// tsv変換
        /// </summary>
        /// <param name="flatfiletype">flatfiletype</param>
        public static void ConvertToTsv(FlatFileType flatfiletype)
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
                    if (!IsFileNameValid(filePath, flatfiletype))
                    {
                        continue;
                    }

                    var lines = File.ReadAllLines(filePath, InputEncode);
                    var headerLines = lines.FirstOrDefault();

                    if (!IsFirstElementValid(headerLines, flatfiletype))
                    {
                        continue;
                    }

                    foreach (var line in lines.Skip(1))
                    {
                        if (!ValidateLength(line, flatfiletype))
                        {
                            continue;
                        }

                        var result = ConvertFile(line, flatfiletype);

                        foreach (var key in result)
                        {
                            File.AppendAllText(Path.Combine(Settings.OutputDirectory, key.Key), key.Value, OutputEncode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// ファイル名が正しいか確認する
        /// </summary>
        /// <param name="file">ファイル名</param>
        /// <param name="flatfile">ファイルの種類</param>
        /// <returns>判定結果</returns>
        public static bool IsFileNameValid(string file, FlatFileType flatfile)
        {
            var fileName = GetInputFileName(flatfile);

            if (file == null)
            {
                return false;
            }

            if (!Path.GetExtension(file).Equals(Settings.InputExtension, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (!Path.GetFileNameWithoutExtension(file).Equals(fileName, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// ファイルの最初の要素が正しいか
        /// </summary>
        /// <param name="lines">ファイルの最初の行</param>
        /// <param name="flatfile">ファイルの種類</param>
        /// <returns>判定結果</returns>
        public static bool IsFirstElementValid(string lines, FlatFileType flatfile)
        {
            var inputFileName = GetInputFileName(flatfile);

            if (lines == null)
            {
                return false;
            }

            if (!lines.Trim().Equals(inputFileName, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// inputファイル名取得
        /// </summary>
        /// <param name="flatfile">ファイルの種類</param>
        /// <returns>inputFile</returns>
        public static string GetInputFileName(FlatFileType flatfile)
        {
            var inputFile = Path.GetFileName(flatfile.ToString());

            if (flatfile.Equals(InputEmployeeFileName))
            {
                inputFile = InputEmployeeFileName;
            }
            else if (flatfile.Equals(InputItemFileName))
            {
                inputFile = InputItemFileName;
            }

            return inputFile;
        }

        /// <summary>
        /// 行の長さが正しいか
        /// </summary>
        /// <param name="line">ファイルの２行目以降</param>
        /// <param name="flatfile">ファイルの種類</param>
        /// <returns>文字列の長さの判定</returns>
        public static bool ValidateLength(string line, FlatFileType flatfile)
        {
            var bodySize = 0;

            if (flatfile == FlatFileType.WXXX5555)
            {
                bodySize = EmployeeBodySize;
            }
            else if (flatfile == FlatFileType.WXXX6666)
            {
                bodySize = ItemBodySize;
            }

            if (line.Length != bodySize)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// WXXX5555とWXXX6666にsそれぞれ変換する
        /// </summary>
        /// <param name="line">２行目以降</param>
        /// <param name="flatfile">ファイルの種類</param>
        /// <returns>変換結果</returns>
        public static Dictionary<string, string> ConvertFile(string line, FlatFileType flatfile)
        {
            var result = new Dictionary<string, string>();

            switch (flatfile)
            {
                case FlatFileType.WXXX5555:
                    result = ConvertWXXX5555(line);
                    break;
                case FlatFileType.WXXX6666:
                    result = ConvertWXXX6666(line);
                    break;
            }

            return result;
        }

        /// <summary>
        /// WXXXX5555をemployeeに変換
        /// </summary>
        /// <param name="bodyLine">行</param>
        /// <returns>変換後ファイル名、行</returns>
        public static Dictionary<string, string> ConvertWXXX5555(string bodyLine)
        {
            var result = new Dictionary<string, string>();

            var index = 0;
            var length = 2;
            var functionType = bodyLine.Substring(index, length);
            index += length;

            length = 5;
            int code;
            if (!int.TryParse(bodyLine.Substring(index, length), out code))
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

            var employeeLine = string.Join(Settings.Separation, code.ToString().PadLeft(5, '0'), name.Trim()) + Environment.NewLine;
            var employeeAuthorityline = string.Join(Settings.Separation, code.ToString().PadLeft(5, '0'), authority) + Environment.NewLine;

            var outputEmployeeFileName = OutputEmployeeName + Settings.OutputExtension;
            var outputEmployeeAuthorityFileName = OutputEnployeeAuthorityName + Settings.OutputExtension;
            if (functionType != "10")
            {
                outputEmployeeFileName = OutputDeletePrefix + outputEmployeeFileName;
                outputEmployeeAuthorityFileName = OutputDeletePrefix + outputEmployeeAuthorityFileName;
            }

            result.Add(outputEmployeeFileName, employeeLine);
            result.Add(outputEmployeeAuthorityFileName, employeeAuthorityline);

            ////Sample[] listArray = new Sample[2];
            ////Sample sample = new Sample();
            ////sample.FileLine = employeeAuthorityline;
            ////sample.FileName = outputEmployeeAuthorityFileName;

            ////listArray[0] = sample;

            ////var list = new List<Sample>();

            ////list.Add(sample);
            return result;
        }

        /// <summary>
        /// WXXXX6666をitemに変換
        /// </summary>
        /// <param name="line">行</param>
        /// <returns>itemへ変換後ファイル名、要素</returns>
        public static Dictionary<string, string> ConvertWXXX6666(string line)
        {
            var result = new Dictionary<string, string>();

            var index = 0;
            var length = 2;
            var function = line.Substring(index, length);
            index += length;

            length = 13;
            long code;
            if (!long.TryParse(line.Substring(index, length), out code))
            {
                return result;
            }

            index += length;

            length = 10;
            var name = line.Substring(index, length);
            index += length;

            length = 10;
            decimal price;
            if (!decimal.TryParse(line.Substring(index, length), out price))
            {
                return result;
            }

            index += length;

            var itemLine = string.Join(Settings.Separation, code.ToString().PadLeft(13, '0'), name.Trim(), price) + Environment.NewLine;
            var outputItemFileName = OutputItemName + Settings.OutputExtension;
            if (function != "10")
            {
                outputItemFileName = OutputDeletePrefix + outputItemFileName;
            }

            result.Add(outputItemFileName, itemLine);
            return result;
        }
    }
}