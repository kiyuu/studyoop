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
        /// XMLを読み取ってインスタンス化したものを配列にするメソッド
        /// </summary>
        /// <typeparam name="T">CreateInstances</typeparam>
        /// <param name="instanceGroupID">instanceGroupID</param>
        /// <returns>インスタンス化した配列</returns>
        public static T[] CreateInstances<T>(InstanceGroupID<T> instanceGroupID)
           where T : class
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(@"instanceInfo.xml");
            var instance = xmlDoc.SelectNodes($"Extension/Group[@Id='{instanceGroupID.Id}']/Instance");

            var converter = new List<T> { };

            foreach (XmlNode instances in instance)
            {
                var className = instances.Attributes["ClassName"].InnerText;
                var obj = CreateInstance<T>(className);
                converter.Add(obj);
            }

            T[] array = converter.ToArray();
            return array;
        }

        /// <summary>
        /// XMLを読み取ってインスタンス化するメソッド
        /// </summary>
        /// <typeparam name="T"> CreateInstance</typeparam>
        /// <param name="instanceID">instanceID</param>
        /// <returns>キャスト結果</returns>
        public static T CreateInstance<T>(InstanceID<T> instanceID)
            where T : class
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(@"./instanceInfo.xml");
            var instance = xmlDoc.SelectNodes($"Extension/Instance[@Id='{instanceID.Id}']");

            if (instance.Count == 0)
            {
                throw new Exception("instanceId not found");
            }

            var className = instance.Item(0).Attributes["ClassName"].InnerText;
            return CreateInstance<T>(className);
        }

        /// <summary>
        /// インスタンスを生成するメソッド
        /// </summary>
        /// <param name="className">クラス名</param>
        /// <returns>生成したオブジェクト</returns>
        private static T CreateInstance<T>(string className)
            where T : class
        {
            Type typeInfo = Type.GetType(className);
            object obj = Activator.CreateInstance(typeInfo);
            return (T)obj;
        }
    }
}
