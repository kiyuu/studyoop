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
            Converter converter1 = Factory.CreateInstance("WXXX5555");
            Converter converter2 = Factory.CreateInstance("WXXX6666");
            converter1.Excute();
            converter2.Excute();
        }
    }
}