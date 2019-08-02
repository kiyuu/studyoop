namespace StudyOOP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// WXXX5555.datとWXXX6666.datをそれぞれ.tsv形式に変換するメソッドを持つクラス
    /// </summary>
    public abstract class FlatFileToTsvConverterBase
    {
        /// <summary>
        /// 子クラスで2回目から文字列を分解する時の開始点(1回目は共通)
        /// </summary>
        protected static readonly int SecondIndex = 2;

        private static readonly Encoding Encode = Encoding.UTF8;

        private static readonly string OutputFileNamePrefix = "delete_";

        /// <summary>
        /// 従業員用ファイル(変換前)の文字列の長さを取得するプロパティ(抽象メソッド)
        /// </summary>
        protected abstract int InputLineLength { get; }

        /// <summary>
        /// 従業員用ファイル名(変換前)を取得するプロパティ(抽象メソッド)
        /// </summary>
        protected abstract string InputFileName { get; }

        /// <summary>
        /// 該当の.dat形式のファイルを.tsv形式に変換するメソッド
        /// </summary>
        public void Execute()
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

                    var lines = File.ReadLines(filePath, Encode);
                    var headerLine = lines.FirstOrDefault();

                    if (!this.IsHeaderLineValid(headerLine, filePath))
                    {
                        continue;
                    }

                    foreach (var inputLine in lines.Skip(1))
                    {
                        if (!this.IsLengthValid(inputLine.Length))
                        {
                            continue;
                        }

                        var functionType = this.GetFunctionType(inputLine);
                        foreach (var s in this.ConvertToTsv(inputLine, functionType, SecondIndex))
                        {
                            File.AppendAllText(Path.Combine(Settings.OutputDirectory, s.FileName), s.OutputLine, Encode);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// ファイル名が正しいか確認するメソッド
        /// </summary>
        /// <param name="filePath">拡張子付きファイル名</param>
        /// <returns>bool</returns>
        protected virtual bool IsFileNameValid(string filePath)
        {
            if (!Path.GetExtension(filePath).Equals(Settings.InputExtension, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (!Path.GetFileNameWithoutExtension(filePath).Equals(this.InputFileName.ToString(), StringComparison.OrdinalIgnoreCase))
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
        protected virtual bool IsHeaderLineValid(string headerLine, string fileName)
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
        /// <param name="inputLineLength">ファイルの1行あたりの文字数</param>
        /// <returns>bool</returns>
        protected virtual bool IsLengthValid(int inputLineLength)
        {
            if (inputLineLength != this.InputLineLength)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 行の先頭から2文字を取得するメソッド
        /// </summary>
        /// <param name="inputLine">一行分の文字列</param>
        /// <returns>functionType</returns>
        protected virtual string GetFunctionType(string inputLine)
        {
            var index = 0;
            var functionType = inputLine.Substring(index, SecondIndex);
            return functionType;
        }

        /// <summary>
        /// ファイル名を作成するメソッド
        /// </summary>
        /// <param name="functionType">行の先頭2文字</param>
        /// <param name="outputFileName">作成されるファイル名</param>
        /// <returns>ファイル名</returns>
        protected virtual string GenerateOutputFileName(string functionType, string outputFileName)
        {
            var fileName = outputFileName;
            if (functionType != "10")
            {
                foreach (var s in outputFileName)
                {
                    fileName = OutputFileNamePrefix + outputFileName;
                }
            }

            return fileName;
        }

        /// <summary>
        /// リストに各ファイル名とそれぞれの1行分の文字列を格納したクラスを入れて返す抽象メソッド
        /// </summary>
        /// <param name="inputLine">1行分の文字列(変換前)</param>
        /// <param name="functionType">各行の先頭の2文字</param>
        /// <param name="index">文字列を分解する時の開始点</param>
        /// <returns>各ファイル名とそれぞれの1行分の文字列が入ったクラスのリスト</returns>
        protected abstract List<ConvertedRecordInfo> ConvertToTsv(string inputLine, string functionType, int index);
    }
}
