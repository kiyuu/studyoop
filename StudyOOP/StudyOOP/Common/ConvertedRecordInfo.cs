namespace StudyOOP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 配列に格納して使う、2つのフィールドを戻り値として返すためのクラス
    /// </summary>
    internal class ConvertedRecordInfo
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
        /// ファイル名
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// 出力時の1行分の文字列
        /// </summary>
        public string OutputLine { get; }
    }
}
