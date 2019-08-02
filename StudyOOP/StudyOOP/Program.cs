namespace StudyOOP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using StudyOOP.Common;

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
            foreach (var files in Factory.CreateInstances(InstanceGroupIDs.FlatFileToTsvConverters))
            {
                files.Execute();
            }
        }
    }
}
