namespace StudyOOP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// ファイル名と出力時の1行分の文字列を保持する
    /// </summary>
    public class ConvertedRecordInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConvertedRecordInfo"/> class.
        /// コンストラクタ
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <param name="outputLine">出力時の1行分の文字列</param>
        public ConvertedRecordInfo(string fileName, string outputLine)
        {
            this.FileName = fileName;

            this.OutputLine = outputLine;
        }

        /// <summary>
        /// ファイル名を取得する
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// 出力時の1行分の文字列を取得する
        /// </summary>
        public string OutputLine { get; }
    }
}
