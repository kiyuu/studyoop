using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyOOP.Common;

namespace StudyOOP
{
    /// <summary>
    /// Factoryクラス
    /// </summary>
    public class Factory
    {
        /// <summary>
        /// キャストしたものを配列にするメソッド
        /// </summary>
        /// <param name = "instanceGroupID" > instanceGroupID </param >
        /// <returns > 作成した配列 </returns >
        public static Converter[] CreateInstances(InstanceGroupID instanceGroupID)
        {
            var instance = new List<Converter>();
            foreach (var c in instanceGroupID.InstanceIds)
            {
                var converter = CreateInstance(c);
                instance.Add(converter);
            }

            Converter[] array = instance.ToArray();
            return array;
        }

        /// <summary>
        /// 生成したオブジェクトをキャストするメソッド
        /// </summary>
        /// <param name="instancesID">instancesID</param>
        /// <returns>キャスト結果</returns>
        public static Converter CreateInstance(InstanceID instancesID)
        {
            var obj = CreateInstance(instancesID.ClassName);
            Converter converter = (Converter)obj;
            return converter;
        }

        /// <summary>
        /// インスタンスを生成するメソッド
        /// </summary>
        /// <param name="className">クラス名</param>
        /// <returns>生成したオブジェクト</returns>
        private static object CreateInstance(string className)
        {
            Type typeInfo = Type.GetType(className);
            object obj = Activator.CreateInstance(typeInfo);
            return obj;
        }
    }
}
