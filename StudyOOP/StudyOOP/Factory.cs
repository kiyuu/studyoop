namespace StudyOOP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// インスタンスの生成を行うメソッドが入ったクラス
    /// </summary>
    public class Factory
    {
        /// <summary>
        /// CreateInstancesメソッドから受け取った引数に応じたクラスをインスタンス化して返すメソッド
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <returns>インスタンス化したクラス</returns>
        public static FlatFileToTsvConverterBase CreateInstance(string fileName)
        {
            switch (fileName)
            {
                case "WXXX5555":
                    return new ConvertWXXX5555ToTsv();

                case "WXXX6666":
                    return new ConvertWXXX6666ToTsv();

                default:
                    throw new ArgumentException();
            }
        }

        /// <summary>
        /// CreateInstance(string fileName)を呼び出してインスタンス化したクラスを配列に入れて返すメソッド
        /// </summary>
        /// <returns>インスタンス化したクラスが入った配列</returns>
        public static FlatFileToTsvConverterBase[] CreateInstances()
        {
            var files = new FlatFileToTsvConverterBase[]
            {
                CreateInstance("WXXX5555"),
                CreateInstance("WXXX6666"),
            };
            return files;
        }
    }
}
