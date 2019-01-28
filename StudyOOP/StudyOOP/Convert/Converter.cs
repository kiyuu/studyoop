namespace StudyOOP.Convert
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using StudyOOP.Common;

    public static class Converter
    {
        private static readonly Encoding _useEncode = Encoding.UTF8;

        private static readonly string _outputDeletePrefix = "delete_";

        private static readonly string _inputEmployeeFileName = "WXXX5555";

        private static readonly string _outputEmployeeFileName = "Employee";

        private static readonly string _outputEmployeeAuthorityFileName = "EmployeeAuthority";

        private static readonly int _employeeBodyLineSize = 18;

        private static readonly string _inputItemFileName = "WXXX6666";

        private static readonly string _outputItemFileName = "Item";

        private static readonly int _itemBodyLineSize = 35;

        private static int _fileFlg;
        private static int _filelineNameLen;

        public static void ConvertDatFileToTSVFile()
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

                    // 取込ファイル名が「WXXX5555」もしくは「WXXX6666」なら処理続行
                    if (Path.GetFileNameWithoutExtension(filePath).Equals(_inputEmployeeFileName, StringComparison.OrdinalIgnoreCase))
                    {
                        _fileFlg = 0;
                    }
                    else if (Path.GetFileNameWithoutExtension(filePath).Equals(_inputItemFileName, StringComparison.OrdinalIgnoreCase))
                    {
                        _fileFlg = 1;
                    }
                    else
                    {
                        continue;
                    }

                    var lines = File.ReadLines(filePath, _useEncode);

                    var headerLine = lines.FirstOrDefault();
                    if (headerLine == null)
                    {
                        continue;
                    }

                    // 取込ファイルの1行目がファイル名と一致していれば処理続行
                    if (_fileFlg == 0)
                    {
                        if (!headerLine.Trim().Equals(_inputEmployeeFileName, StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (!headerLine.Trim().Equals(_inputItemFileName, StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                    }

                    foreach (var line in lines.Skip(1))
                    {
                        // 全角文字ない前提
                        if ((line.Length != _employeeBodyLineSize) && (line.Length != _itemBodyLineSize))
                        {
                            continue;
                        }

                        var index = 0;
                        var length = 2;
                        var functionType = line.Substring(index, length);
                        index += length;

                        
                        if (_fileFlg == 0)
                        {
                            var code = 0;
                            length = 5;
                            if (!int.TryParse(line.Substring(index, length), out code))
                            {
                                continue;
                            }
                        }
                        else
                        {
                            long code = 0;
                            length = 13;
                            if (!long.TryParse(line.Substring(index, length), out code))
                            {
                                continue;
                            }
                        }

                        index += length;
                        _filelineNameLen = length;

                        length = 10;
                        var name = line.Substring(index, length);
                        index += length;

                        var authority = string.Empty;
                        if (_fileFlg == 0)
                        {
                            length = 1;
                            authority = line.Substring(index, length);
                        }
                        else
                        {
                            length = 10;
                            if (!decimal.TryParse(line.Substring(index, length), out decimal unitPrice))
                            {
                                continue;
                            }
                        }

                        index += length;

                        var s = string.Empty;
                        if (_fileFlg == 0)
                        {
                            s = name.Trim();
                            s = authority;
                        }
                        else
                        {
                            s = name.Trim(), unitPrice
                        }

                        var outputLine = string.Join(Settings.TsvSeparater, code.ToString().PadLeft(_filelineNameLen, '0'), s) + Environment.NewLine;

                        var outputFileName = string.Empty;
                        if (_fileFlg == 0)
                        {
                            outputFileName = _outputEmployeeFileName;
                            outputFileName = _outputEmployeeAuthorityFileName;
                        }
                        else
                        {
                            outputFileName = _outputItemFileName;
                        }

                        outputFileName = outputFileName + Settings.OutputFileExtension;
                        if (functionType != "10")
                        {
                            outputFileName = _outputDeletePrefix + outputFileName;
                        }

                        File.AppendAllText(Path.Combine(Settings.OutputDirectory, outputFileName), outputLine, _useEncode);
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