using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using StudyOOP.Common;

namespace StudyOOP
{
    /// <summary>
    /// Factoryクラス
    /// </summary>
    public class Factory
    {
        /// <summary>
        /// XMLを読み取ってインスタンス化して配列にするメソッド
        /// </summary>
        /// <param name="instanceGroupID">instanceGroupID</param>
        /// <returns>インスタンス化したものの配列</returns>
        public static Converter[] CreateInstances(InstanceGroupID instanceGroupID)
        {         
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(@"instanceInfo.xml");
            var instance = xmlDoc.SelectNodes($"Extension/Group[@Id='{instanceGroupID.Id}']/Instance");

            var converter = new List<Converter> { };
            
            foreach (XmlNode emp in instance)
            {
                var className = emp.Attributes["ClassName"].InnerText;
                var obj = CreateInstance(className);
                Converter instanceClass = (Converter)obj;
                converter.Add(instanceClass);
            }

            Converter[] array = converter.ToArray();
            return array;
        }

        /// <summary>
        /// 生成したオブジェクトをキャストするメソッド
        /// </summary>
        /// <param name="instanceID">instanceID</param>
        /// <returns>キャスト結果</returns>
        public static Converter CreateInstance(InstanceID instanceID)
        {
            var obj = CreateInstance(instanceID.Id);
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
