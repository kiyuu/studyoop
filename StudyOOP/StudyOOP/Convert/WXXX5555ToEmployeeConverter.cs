using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyOOP
{
    /// <summary>
    /// WXXX5555をemployeeに変換
    /// </summary>
    internal class WXXX5555ToEmployeeConverter : Converter
    {
        private static string outputEmployeeName = "Employee";
        private static string outputEmployeeAuthorityName = "EmployeeAuthority";

        /// <summary>
        /// WXXX5555ファイル名取得
        /// </summary>
        protected override string InputFileName { get; } = "WXXX5555";

        /// <summary>
        /// 行の長さ取得
        /// </summary>
        protected override int BodySize { get; } = 18;

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

            var length = 5;
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
            var employeeAuthorityLine = string.Join(Settings.Separation, code.ToString().PadLeft(5, '0'), authority) + Environment.NewLine;

            var outputEmployeeFileName = this.GetOutputFileName(outputEmployeeName, functionType);
            var outputEmployeeAuthorityFileName = this.GetOutputFileName(outputEmployeeAuthorityName, functionType);

            ConvertedRecordInfo convertedRecordInfo = new ConvertedRecordInfo(outputEmployeeFileName, employeeLine);
            result.Add(convertedRecordInfo);

            convertedRecordInfo = new ConvertedRecordInfo(outputEmployeeAuthorityFileName, employeeAuthorityLine);
            result.Add(convertedRecordInfo);

            return result;
        }
    }
}
