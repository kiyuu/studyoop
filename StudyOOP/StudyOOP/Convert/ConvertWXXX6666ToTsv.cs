using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyOOP
{
    /// <summary>
    /// WXXX6666.datを.tsv形式に変換するためのクラス
    /// </summary>
    internal class ConvertWXXX6666ToTsv : FlatFileToTsvConverterBase
    {
        private static readonly string _inputFileName = "WXXX6666";

        private static readonly int _inputItemLineLength = 35;

        private static readonly string OutputItemFileName = "Item";

        /// <summary>
        /// 商品用ファイル(変換前)の文字列の長さを取得するプロパティ
        /// </summary>
        protected override int InputLineLength
        {
            get
            {
                return _inputItemLineLength;
            }
        }

        /// <summary>
        /// 商品用ファイル名(変換前)を取得するプロパティ
        /// </summary>
        protected override string InputFileName
        {
            get
            {
                return _inputFileName;
            }
        }

        /// <summary>
        /// リストに各ファイル名とそれぞれの1行分の文字列を格納したクラスを入れて返すメソッド
        /// </summary>
        /// <param name="inputLine">1行分の文字列(変換前)</param>
        /// <param name="functionType">各行の先頭の2文字</param>
        /// <param name="index">文字列を分解する時の開始点</param>
        /// <returns>各ファイル名とそれぞれの1行分の文字列が入ったクラスのリスト</returns>
        protected override List<ConvertedRecordInfo> ConvertToTsv(string inputLine, string functionType, int index)
        {
            int length = 13;
            long code;
            long.TryParse(inputLine.Substring(index, length), out code);

            index += length;
            length = 10;
            var name = inputLine.Substring(index, length);

            index += length;
            decimal unitPrice;
            decimal.TryParse(inputLine.Substring(index, length), out unitPrice);

            var itemLine = string.Join(Settings.TabSeparator, code.ToString().PadLeft(13, '0'), name.Trim(), unitPrice) + Environment.NewLine;
            var outputItemFileName = OutputItemFileName + Settings.OutputExtension;

            var result = new List<ConvertedRecordInfo>();
            result.Add(new ConvertedRecordInfo(this.GenerateOutputFileName(functionType, outputItemFileName), itemLine));
            return result;
        }
    }
}
