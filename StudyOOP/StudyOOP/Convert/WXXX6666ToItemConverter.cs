using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyOOP
{
    /// <summary>
    /// WXXX6666をItemに変換
    /// </summary>
    internal class WXXX6666ToItemConverter : Converter
    {
        private static string outputItemName = "Item";

        /// <summary>
        /// WXXX6666ファイル名取得
        /// </summary>
        protected override string InputFileName { get; } = "WXXX6666";

        /// <summary>
        /// 行の長さ取得
        /// </summary>
        protected override int BodySize { get; } = 35;

        /// <summary>
        /// WXXX6666をItemに変換
        /// </summary>
        /// <param name="bodyLine">行</param>
        /// <param name="index">index</param>
        /// <param name="functionType">職能タイプ</param>
        /// <returns>出力するファイル名、ファイル行</returns>
        protected override List<ConvertedRecordInfo> ConvertToTsv(string bodyLine, int index, string functionType)
        {
            var result = new List<ConvertedRecordInfo>();
            var length = 13;
            long code = 0;
            if (!long.TryParse(bodyLine.Substring(index, length), out code))
            {
                return result;
            }

            index += length;

            length = 10;
            var name = bodyLine.Substring(index, length);
            index += length;

            length = 10;
            decimal price = 0;
            if (!decimal.TryParse(bodyLine.Substring(index, length), out price))
            {
                return result;
            }

            index += length;

            var itemLine = string.Join(Settings.Separation, code.ToString().PadLeft(13, '0'), name.Trim(), price) + Environment.NewLine;
            var outputItemFileName = this.GetOutputFileName(outputItemName, functionType);

            ConvertedRecordInfo convertedRecordInfo = new ConvertedRecordInfo(outputItemFileName, itemLine);

            result.Add(convertedRecordInfo);

            return result;
        }
    }
}
