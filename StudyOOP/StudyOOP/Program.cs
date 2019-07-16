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
            var converters = new List<FlatFileToTsvConverterBase>()
            {
                new ConvertWXXX5555ToTsv(),
                new ConvertWXXX6666ToTsv(),
            };
            foreach (var s in converters)
            {
                s.Execute();
            }
        }
    }
}
