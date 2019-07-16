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
    public abstract class Converter
    {
        private static readonly Encoding InputEncode = Encoding.UTF8;
        private static readonly Encoding OutputEncode = Encoding.UTF8;
        private static readonly string OutputDeletePrefix = "delete_";

        /// <summary>
        /// inputファイル名取得
        /// </summary>
        protected abstract string InputFileName { get; }

        /// <summary>
        /// それぞれの行の長さ取得
        /// </summary>
        protected abstract int BodySize { get; }

        /// <summary>
        /// tsv変換実行メソッド
        /// </summary>
        public void Excute()
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
                    if (!this.IsFileNameValid(filePath))
                    {
                        continue;
                    }

                    var lines = File.ReadAllLines(filePath, InputEncode);
                    var headerLines = lines.FirstOrDefault();

                    if (!this.IsFirstElementValid(headerLines))
                    {
                        continue;
                    }

                    foreach (var line in lines.Skip(1))
                    {
                        if (!this.ValidateLength(line))
                        {
                            continue;
                        }

                        var result = this.ConvertToTsv(line);

                        foreach (var key in result)
                        {
                            File.AppendAllText(Path.Combine(Settings.OutputDirectory, key.FileName), key.FileLine, OutputEncode);
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
        /// <param name="filePath">ファイル名</param>
        /// <returns>判定結果</returns>
        protected bool IsFileNameValid(string filePath)
        {
            var fileName = this.InputFileName;

            if (filePath == null)
            {
                return false;
            }

            if (!Path.GetExtension(filePath).Equals(Settings.InputExtension, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (!Path.GetFileNameWithoutExtension(filePath).Equals(fileName, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// ファイルの最初の要素が正しいか
        /// </summary>
        /// <param name="lines">ファイルの最初の行</param>
        /// <returns>判定結果</returns>
        protected bool IsFirstElementValid(string lines)
        {
            var inputFileName = this.InputFileName;

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
        /// 行の長さが正しいか
        /// </summary>
        /// <param name="line">ファイルの２行目以降</param>
        /// <returns>文字列の長さの判定</returns>
        protected bool ValidateLength(string line)
        {
            if (line == null)
            {
                return false;
            }

            if (line.Length != this.BodySize)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// FunctionType取得
        /// </summary>
        /// <param name="line">行</param>
        /// <param name="index">index</param>
        /// <returns>FunctionType</returns>
        protected string GetFunctionType(string line, out int index)
        {
            index = 0;
            var length = 2;
            var functionType = line.Substring(index, length);
            index += length;
            return functionType;
        }

        /// <summary>
        /// 出力ファイル名取得
        /// </summary>
        /// <param name="outputFileName">ファイル名</param>
        /// <param name="functionType">functionType</param>
        /// <returns>出力ファイル名</returns>
        protected string GetOutputFileName(string outputFileName, string functionType)
        {
            outputFileName = outputFileName + Settings.OutputExtension;

            if (functionType != "10")
            {
                outputFileName = OutputDeletePrefix + outputFileName;
            }

            return outputFileName;
        }

        /// <summary>
        /// tsvに変換
        /// </summary>
        /// <param name="bodyLIne">行</param>
        /// <param name="index">index</param>
        /// <param name="functionType">functionType</param>
        /// <returns>tsvに変換されたファイル名、行</returns>
        protected abstract List<ConvertedRecordInfo> ConvertToTsv(string bodyLIne, int index, string functionType);

        private List<ConvertedRecordInfo> ConvertToTsv(string bodyLine)
        {
            int index;
            var functionType = this.GetFunctionType(bodyLine, out index);
            return this.ConvertToTsv(bodyLine, index, functionType);
        }
    }
}