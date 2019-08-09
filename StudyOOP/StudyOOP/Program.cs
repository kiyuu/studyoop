namespace StudyOOP
{
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
            foreach (var fileConverters in Factory.CreateInstances(InstanceGroupIDs.FlatFileToTsvConverters))
            {
                fileConverters.Execute();
            }
        }
    }
}
