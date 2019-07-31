namespace StudyOOP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Mainメソッドの中身から実行される
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// エントリーポイント
        /// </summary>
        /// <param name="args">引数</param>
        private static void Main(string[] args)
        {

            FlatFileToTsvConverterBase file1 = Factory.CreateInstance("WXXX5555");
            FlatFileToTsvConverterBase file2 = Factory.CreateInstance("WXXX6666");

            file1.Execute();
            file2.Execute();
        }
    }
}
