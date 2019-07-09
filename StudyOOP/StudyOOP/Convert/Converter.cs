namespace StudyOOP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 処理の分岐に使用するファイル名
    /// </summary>
    internal enum FlatFileType
    {
        WXXX5555,
        WXXX6666,
    }

    /// <summary>
    /// WXXX5555.datとWXXX6666.datをそれぞれ.tsv形式に変換するメソッドを持つクラス
    /// </summary>
    internal class Converter
    {
        private static readonly Encoding Encode = Encoding.UTF8;

        private static readonly int InputEmployeeFileLength = 18;

        private static readonly string OutputEmployeeFileName = "Employee";

        private static readonly string OutputEmployAuthorityFileName = "Authority";

        private static readonly string OutputFileNamePrefix = "delete_";

        private static readonly int InputItemFileLength = 35;

        private static readonly string OutputItemFileName = "Item";

        /// <summary>
        /// 該当の.dat形式のファイルを.tsv形式に変換するメソッド
        /// </summary>
        /// <param name="flatFileType">ファイル名</param>
        public static void ConvertToTsv(FlatFileType flatFileType)
        {
            if (!Directory.Exists(Settings.InputDirectory))
            {
                return;
            }

            if (!Directory.Exists(Settings.OutputDirectory))
            {
                Directory.CreateDirectory(Settings.OutputDirectory);
            }

            try
            {
                foreach (var filePath in Directory.EnumerateFiles(Settings.InputDirectory))
                {
                    if (!IsFileNameValid(filePath, flatFileType))
                    {
                        continue;
                    }

                    var lines = File.ReadLines(filePath, Encode);
                    var headerLine = lines.FirstOrDefault();

                    if (!IsHeaderLineValid(headerLine, filePath))
                    {
                        continue;
                    }

                    foreach (var dataLine in lines.Skip(1))
                    {
                        if (!IsLengthValid(dataLine.Length, flatFileType))
                        {
                            continue;
                        }

                        foreach (var s in SendFiletoConverter(dataLine, flatFileType))
                        {
                            File.AppendAllText(Path.Combine(Settings.OutputDirectory, s.FileName), s.DataLine, Encode);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// ファイル名が正しいか確認するメソッド
        /// </summary>
        /// <param name="filePath">拡張子付きファイル名</param>
        /// <param name="flatFileType">ファイル名</param>
        /// <returns>bool</returns>
        private static bool IsFileNameValid(string filePath, FlatFileType flatFileType)
        {
            if (!Path.GetExtension(filePath).Equals(Settings.InputExtension, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (!Path.GetFileNameWithoutExtension(filePath).Equals(flatFileType.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// ファイルの1行目とファイル名が合っているか確認するメソッド
        /// </summary>
        /// <param name="headerLine">ファイル内の最初の1行</param>
        /// <param name="fileName">ファイル名</param>
        /// <returns>bool</returns>
        private static bool IsHeaderLineValid(string headerLine, string fileName)
        {
            if (headerLine == null)
            {
                return false;
            }

            if (!headerLine.Trim().Equals(Path.GetFileNameWithoutExtension(fileName), StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// ファイルごとに文字数を比較して正しいかどうかを返すメソッド
        /// </summary>
        /// <param name="dataLineLength">ファイルの文字数</param>
        /// <param name="flatFlieType">ファイル名</param>
        /// <returns>bool</returns>
        private static bool IsLengthValid(int dataLineLength, FlatFileType flatFlieType)
        {
            switch (flatFlieType)
            {
                case FlatFileType.WXXX5555:
                    if (dataLineLength != InputEmployeeFileLength)
                    {
                        return false;
                    }

                    break;
                case FlatFileType.WXXX6666:
                    if (dataLineLength != InputItemFileLength)
                    {
                        return false;
                    }

                    break;
            }

            return true;
        }

        /// <summary>
        /// ファイル名を読み取ってそれぞれに合ったメソッドに処理を分岐させるメソッド
        /// </summary>
        /// <param name="dataLine">ファイル内のデータ</param>
        /// <param name="flatFileType">ファイル名</param>
        private static List<OutputDataAndFileName> SendFiletoConverter(string dataLine, FlatFileType flatFileType)
        {
            switch (flatFileType)
            {
                case FlatFileType.WXXX5555:
                    return ConvertEmployeeToTsv(dataLine);
                case FlatFileType.WXXX6666:
                    return ConvertItemToTsv(dataLine);
                default:
                    Exception e = new Exception();
                    throw e;
            }
        }

        /// <summary>
        /// SendFileToConverterメソッドから呼び出される、Employeeデータの文字列を分解してリストに格納して返すメソッド
        /// </summary>
        /// <param name="dataLine">ファイル内のデータ</param>
        private static List<OutputDataAndFileName> ConvertEmployeeToTsv(string dataLine)
        {
            var index = 0;
            var length = 2;
            var functionType = dataLine.Substring(index, length);

            index += length;
            length = 5;

            int code;
            int.TryParse(dataLine.Substring(index, length), out code);

            index += length;
            length = 10;

            var name = dataLine.Substring(index, length);

            index += length;
            length = 1;

            string authority = dataLine.Substring(index, length);

            var employeeLine = string.Join(Settings.TabSeparator, code.ToString().PadLeft(5, '0'), name.Trim()) + Environment.NewLine;
            var employAuthorityLine = string.Join(Settings.TabSeparator, code.ToString().PadLeft(5, '0'), authority) + Environment.NewLine;
            var outputEmployeeFileName = OutputEmployeeFileName + Settings.OutputExtension;
            var outputEmployAuthorityFileName = OutputEmployeeFileName + OutputEmployAuthorityFileName + Settings.OutputExtension;

            if (functionType != "10")
            {
                outputEmployeeFileName = OutputFileNamePrefix + outputEmployeeFileName;
                outputEmployAuthorityFileName = OutputFileNamePrefix + outputEmployAuthorityFileName;
            }

            var outputFileInfo = default(OutputDataAndFileName);
            outputFileInfo.DataLine = employeeLine;
            outputFileInfo.FileName = outputEmployeeFileName;

            var result = new List<OutputDataAndFileName>();
            result.Add(outputFileInfo);

            outputFileInfo = default(OutputDataAndFileName);
            outputFileInfo.DataLine = employAuthorityLine;
            outputFileInfo.FileName = outputEmployAuthorityFileName;
            result.Add(outputFileInfo);

            return result;
        }

        /// <summary>
        /// SendFileToConverterメソッドから呼び出される、Itemデータの文字列を分解してリストに格納して返すメソッド
        /// </summary>
        /// <param name="dataLine">ファイル内のデータ</param>
        private static List<OutputDataAndFileName> ConvertItemToTsv(string dataLine)
        {
            int index = 0;
            int length = 2;
            var functionType = dataLine.Substring(index, length);

            index += length;
            length = 13;
            long code;
            long.TryParse(dataLine.Substring(index, length), out code);

            index += length;
            length = 10;
            var name = dataLine.Substring(index, length);

            index += length;
            decimal unitPrice;
            decimal.TryParse(dataLine.Substring(index, length), out unitPrice);

            var itemLine = string.Join(Settings.TabSeparator, code.ToString().PadLeft(13, '0'), name.Trim(), unitPrice) + Environment.NewLine;
            var outputItemFileName = OutputItemFileName + Settings.OutputExtension;

            if (functionType != "10")
            {
                outputItemFileName = OutputFileNamePrefix + outputItemFileName;
            }

            var outputFileInfo = default(OutputDataAndFileName);
            outputFileInfo.DataLine = itemLine;
            outputFileInfo.FileName = outputItemFileName;

            List<OutputDataAndFileName> result = new List<OutputDataAndFileName>();
            result.Add(outputFileInfo);
            return result;
        }

        private struct OutputDataAndFileName
        {
            internal string DataLine;

            internal string FileName;
        }
    }
}
