namespace StudyOOP
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// datファイルをtsvに変換
    /// </summary>
    internal class Convert
    {
        private static readonly string InputEmployeeFileName = "WXXX5555";
        private static readonly string Prifix = "delete_";
        private static readonly string InputItemFileName = "WXXX6666";

        /// <summary>
        /// WXXX555をemployeeに変換
        /// </summary>
        public static void ConvertWXXX5555Employee()
        {
            if (!Directory.Exists(Setting.InputDirectory))
            {
                return;
            }

            try
            {
                if (!Directory.Exists(Setting.OutputDirectory))
                {
                    Directory.CreateDirectory(Setting.OutputDirectory);
                }

                foreach (var files in Directory.EnumerateFiles(Setting.InputDirectory))
                {
                    if (!Path.GetExtension(files).Equals(Setting.InputExtension, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    if (!Path.GetFileNameWithoutExtension(files).Equals(InputEmployeeFileName, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    var lines = File.ReadAllLines(files, Encoding.UTF8);
                    var headerline = lines.FirstOrDefault();
                    if (headerline == null)
                    {
                        continue;
                    }

                    if (headerline != InputEmployeeFileName.Trim())
                    {
                        continue;
                    }

                    foreach (var line in lines.Skip(1))
                    {
                        if (line.Length != 18)
                        {
                            continue;
                        }

                        var index = 0;
                        var length = 2;
                        var function = line.Substring(index, length);
                        index = index + length;

                        length = 5;
                        int code;
                        if (!int.TryParse(line.Substring(index, length), out code))
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

                        var employeeLine = string.Join(Setting.Separation, code.ToString().PadLeft(5, '0'), name.Trim()) + Environment.NewLine;
                        var employeeAuthorityline = string.Join(Setting.Separation, code.ToString().PadLeft(5, '0'), authority) + Environment.NewLine;

                        var outputEmployeeFileName = "Employee" + Setting.OutputExtension;
                        var outputEmployeeAuthorityFileName = "EmployeeAuthority" + Setting.OutputExtension;
                        if (function != "10")
                        {
                            outputEmployeeFileName = Prifix + outputEmployeeFileName;
                            outputEmployeeAuthorityFileName = Prifix + outputEmployeeAuthorityFileName;
                        }

                        File.AppendAllText(Path.Combine(Setting.OutputDirectory, outputEmployeeFileName), employeeLine, Encoding.UTF8);
                        File.AppendAllText(Path.Combine(Setting.OutputDirectory, outputEmployeeAuthorityFileName), employeeAuthorityline, Encoding.UTF8);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// WXXX666をitemに変換
        /// </summary>
        public static void ConvertWXXX6666item()
        {
            if (!Directory.Exists(Setting.InputDirectory))
            {
                return;
            }

            try
            {
                if (!Directory.Exists(Setting.OutputDirectory))
                {
                    Directory.CreateDirectory(Setting.OutputDirectory);
                }

                foreach (var files in Directory.EnumerateFiles(Setting.InputDirectory))
                {
                    if (!Path.GetExtension(files).Equals(Setting.InputExtension, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    if (!Path.GetFileNameWithoutExtension(files).Equals(InputItemFileName, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    var lines = File.ReadAllLines(files, Encoding.UTF8);
                    var headerline = lines.FirstOrDefault();
                    if (headerline == null)
                    {
                        continue;
                    }

                    if (headerline != InputItemFileName.Trim())
                    {
                        continue;
                    }

                    foreach (var line in lines.Skip(1))
                    {
                        if (line.Length != 35)
                        {
                            continue;
                        }

                        var index = 0;
                        var length = 2;
                        var function = line.Substring(index, length);
                        index += length;

                        length = 13;
                        long code;
                        if (!long.TryParse(line.Substring(index, length), out code))
                        {
                            continue;
                        }

                        index += length;

                        length = 10;
                        var name = line.Substring(index, length);
                        index += length;

                        length = 10;
                        decimal price;
                        if (!decimal.TryParse(line.Substring(index, length), out price))
                        {
                            continue;
                        }

                        index += length;

                        var itemLine = string.Join(Setting.Separation, code.ToString().PadLeft(13, '0'), name.Trim(), price) + Environment.NewLine;
                        var outputItemFileName = "Item" + Setting.OutputExtension;
                        if (function != "10")
                        {
                            outputItemFileName = Prifix + outputItemFileName;
                        }

                        File.AppendAllText(Path.Combine(Setting.OutputDirectory, outputItemFileName), itemLine, Encoding.UTF8);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}