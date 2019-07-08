namespace StudyOOP
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// WXXX5555.datとWXXX6666.datをそれぞれ.tsv形式に変換するメソッドを持つクラス
    /// </summary>
    internal class Converter
    {
        private static readonly Encoding Encode = Encoding.UTF8;

        private static readonly string InputFileName = "WXXX5555";

        private static readonly int InputEmployeeFileLength = 18;

        private static readonly string OutputEmployeeFileName = "Employee";

        private static readonly string OutputEmployAuthorityFileName = "Authority";

        private static readonly string OutputFileNamePrefix = "delete_";

        private static readonly string InputItemFileName = "WXXX6666";

        private static readonly int InputItemFileLength = 35;

        private static readonly string OutputItemFileName = "Item";

        /// <summary>
        /// WXXX5555.datを.tsv形式に変換するメソッド
        /// </summary>
        public static void WXXX5555DatToTsvConverter()
        {
            if (!Directory.Exists(Settings.InputDirectory))
            {
                return;
            }

            if (!Directory.Exists(Settings.OutputDirectory))
            {
                Directory.CreateDirectory("output");
            }

            try
            {
                foreach (var filePath in Directory.EnumerateFiles(Settings.InputDirectory))
                {
                    if (!Path.GetExtension(filePath).Equals(Settings.InputExtension, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    if (!Path.GetFileNameWithoutExtension(filePath).Equals(InputFileName, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    var lines = File.ReadLines(filePath, Encode);
                    var headerLine = lines.FirstOrDefault();
                    if (headerLine == null)
                    {
                        continue;
                    }

                    if (headerLine != Path.GetFileNameWithoutExtension(filePath))
                    {
                        continue;
                    }

                    foreach (var inputData in lines.Skip(1))
                    {
                        if (inputData.Length != InputEmployeeFileLength)
                        {
                            continue;
                        }

                        var index = 0;
                        var length = 2;
                        var functionType = inputData.Substring(index, length);

                        index += length;
                        length = 5;

                        int code;
                        int.TryParse(inputData.Substring(index, length), out code);

                        index += length;
                        length = 10;

                        var name = inputData.Substring(index, length);

                        index += length;
                        length = 1;

                        string authority = inputData.Substring(index, length);

                        var employeeLine = string.Join(Settings.TabSeparator, code.ToString().PadLeft(5, '0'), name.Trim()) + Environment.NewLine;
                        var employAuthorityLine = string.Join(Settings.TabSeparator, code.ToString().PadLeft(5, '0'), authority) + Environment.NewLine;
                        var outputEmployeeFileName = OutputEmployeeFileName + Settings.OutputExtension;
                        var outputEmployAuthorityFileName = OutputEmployeeFileName + OutputEmployAuthorityFileName + Settings.OutputExtension;

                        if (functionType != "10")
                        {
                            outputEmployeeFileName = OutputFileNamePrefix + outputEmployeeFileName;
                            outputEmployAuthorityFileName = OutputFileNamePrefix + outputEmployAuthorityFileName;
                        }

                        File.AppendAllText(Path.Combine(Settings.OutputDirectory, outputEmployeeFileName), employeeLine, Encode);
                        File.AppendAllText(Path.Combine(Settings.OutputDirectory, outputEmployAuthorityFileName), employAuthorityLine, Encode);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// WXXX6666.datを.tsv形式に変換するメソッド
        /// </summary>
        internal static void WXXX6666DatToTsvConverter()
        {
            if (!Directory.Exists(Settings.InputDirectory))
            {
                return;
            }

            if (!Directory.Exists(Settings.OutputDirectory))
            {
                Directory.CreateDirectory("output");
            }

            try
            {
                foreach (var filePath in Directory.EnumerateFiles(Settings.InputDirectory))
                {
                    if (!Path.GetExtension(filePath).Equals(Settings.InputExtension, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    if (!Path.GetFileNameWithoutExtension(filePath).Equals(InputItemFileName, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    var lines = File.ReadLines(filePath, Encode);
                    var headerLine = lines.FirstOrDefault();
                    if (headerLine == null)
                    {
                        continue;
                    }

                    if (headerLine != Path.GetFileNameWithoutExtension(filePath))
                    {
                        continue;
                    }

                    foreach (var inputData in lines.Skip(1))
                    {
                        if (inputData.Length != InputItemFileLength)
                        {
                            continue;
                        }

                        int index = 0;
                        int length = 2;
                        var functionType = inputData.Substring(index, length);

                        index += length;
                        length = 13;
                        long code;
                        long.TryParse(inputData.Substring(index, length), out code);

                        index += length;
                        length = 10;
                        var name = inputData.Substring(index, length);

                        index += length;
                        decimal unitPrice;
                        decimal.TryParse(inputData.Substring(index, length), out unitPrice);

                        var itemLine = string.Join(Settings.TabSeparator, code.ToString().PadLeft(13, '0'), name.Trim(), unitPrice) + Environment.NewLine;
                        var outputItemFileName = OutputItemFileName + Settings.OutputExtension;

                        if (functionType != "10")
                        {
                            outputItemFileName = OutputFileNamePrefix + outputItemFileName;
                        }

                        File.AppendAllText(Path.Combine(Settings.OutputDirectory, outputItemFileName), itemLine, Encode);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
