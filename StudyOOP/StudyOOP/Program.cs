namespace StudyOOP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using StudyOOP.Common;

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
            var converter = Factory.CreateInstances(InstanceGroupIDs.FlatFileToTsvConverters);
            foreach (var c in converter)
            {
                c.Excute();
            }
        }
    }
}