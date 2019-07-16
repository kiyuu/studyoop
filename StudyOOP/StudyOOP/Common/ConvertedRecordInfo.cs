using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyOOP
{
    /// <summary>
    /// 変換の情報のクラス
    /// </summary>
    public class ConvertedRecordInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConvertedRecordInfo"/> class.
        /// 変換する情報を取得
        /// </summary>
        /// <param name="fileName">fileName</param>
        /// <param name="fileLine">filenline</param>
        public ConvertedRecordInfo(string fileName, string fileLine)
        {
            this.FileName = fileName;
            this.FileLine = fileLine;
        }

        /// <summary>
        /// ファイル名取得
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// ファイル行取得
        /// </summary>
        public string FileLine { get; }
    }
}
