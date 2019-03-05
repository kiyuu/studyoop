namespace DecoratorStudy
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// ファイル読み取り
    /// </summary>
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.Write(ReadAllText(Path.Combine(@".\", "input", "rss.xml"), Encoding.UTF8));
            Console.Write("何かキーを入力してください。");
            Console.ReadKey();
        }

        public static string ReadAllText(string filePath, Encoding encoding)
        {
            using (var sr = new MyStreamReader(filePath, encoding))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
