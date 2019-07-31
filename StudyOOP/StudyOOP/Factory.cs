using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyOOP

{/// <summary>
/// Factoryクラス
/// </summary>
    public class Factory
    {
        /// <summary>
        /// インスタンスを生成するメソッド
        /// </summary>
        /// <param name="id">クラスの指定</param>
        /// <returns>生成したクラス</returns>
        public static Converter CreateInstance(string id)
        {
            switch (id)
            {
                case "WXXX5555":
                    return new WXXX5555ToEmployeeConverter();
                case "WXXX6666":
                    return new WXXX6666ToItemConverter();
                default:
                    throw new ArgumentException();
            }
        }
        /// <summary>
        ///  複数の変換クラスのインスタンスを生成するメソッド
        /// </summary>
        /// <returns>生成したクラス</returns>
        public static Converter[] CreateInstances()
        {
            Converter[] converter =
            {
                new WXXX5555ToEmployeeConverter(),
                new WXXX6666ToItemConverter(),
            };

            return converter;
        }
    }
}
