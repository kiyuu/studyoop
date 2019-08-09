namespace StudyOOP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// WXXX5555.datを.tsv形式に変換するためのクラス
    /// </summary>
    internal class ConvertWXXX5555ToTsv : FlatFileToTsvConverterBase
    {
        private static readonly string _inputFileName = "WXXX5555";

        private static readonly int _inputEmployeeLineLength = 18;

        private static readonly string OutputEmployeeFileName = "Employee";

        private static readonly string OutputEmployAuthorityFileName = "EmployeeAuthority";

        /// <summary>
        /// 従業員用ファイル(変換前)の文字列の長さを取得する
        /// </summary>
        protected override int InputLineLength
        {
            get
            {
                return _inputEmployeeLineLength;
            }
        }

        /// <summary>
        /// 従業員用ファイル名(変換前)を取得する
        /// </summary>
        protected override string InputFileName
        {
            get
            {
                return _inputFileName;
            }
        }

        /// <summary>
        /// リストに各ファイル名とそれぞれの1行分の文字列を格納したインスタンスを入れて返す
        /// </summary>
        /// <param name="inputLine">1行分の文字列(変換前)</param>
        /// <param name="functionType">各行の先頭の2文字</param>
        /// <param name="index">文字列を分解する時の開始点</param>
        /// <returns>各ファイル名とそれぞれの1行分の文字列が入ったインスタンスのリスト</returns>
        protected override List<ConvertedRecordInfo> ConvertToTsv(string inputLine, string functionType, int index)
        {
            var length = 5;

            int code;
            int.TryParse(inputLine.Substring(index, length), out code);

            index += length;
            length = 10;

            var name = inputLine.Substring(index, length);

            index += length;
            length = 1;

            var authority = inputLine.Substring(index, length);

            var employeeLine = string.Join(Settings.TabSeparator, code.ToString().PadLeft(5, '0'), name.Trim()) + Environment.NewLine;
            var employAuthorityLine = string.Join(Settings.TabSeparator, code.ToString().PadLeft(5, '0'), authority) + Environment.NewLine;
            var outputEmployeeFileName = OutputEmployeeFileName + Settings.OutputExtension;
            var outputEmployAuthorityFileName = OutputEmployAuthorityFileName + Settings.OutputExtension;

            var result = new List<ConvertedRecordInfo>();
            result.Add(new ConvertedRecordInfo(this.GenerateOutputFileName(functionType, outputEmployeeFileName), employeeLine));
            result.Add(new ConvertedRecordInfo(this.GenerateOutputFileName(functionType, outputEmployAuthorityFileName), employAuthorityLine));

            return result;
        }
    }
}
