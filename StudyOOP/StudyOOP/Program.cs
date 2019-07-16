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
            var convertlist = new List<Converter>()
            {
                new WXXX5555ToEmployeeConverter(),
                new WXXX6666ToItemConverter(),
            };

            foreach (var result in convertlist)
            {
                result.Excute();
            }
        }
    }
}