namespace StudyOOP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 変換するプログラム
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// メインメソッド
        /// </summary>
        /// <param name="args">main</param>
        internal static void Main(string[] args)
        {
            Converter.ConvertToTsv(Converter.FlatFileType.WXXX5555);
            Converter.ConvertToTsv(Converter.FlatFileType.WXXX6666);
        }
    }
}
