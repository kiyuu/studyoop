namespace StudyOOP
{
    using System;
    using System.Xml;
    using StudyOOP.Common;

    /// <summary>
    /// インスタンスの生成と配列への格納を行う
    /// </summary>
    public class Factory
    {
        /// <summary>
        /// xmlから読み取ったGroupIDに入っているclassNameと同じ名前のクラスをインスタンス化させて配列に格納して返す
        /// </summary>
        /// <typeparam name="T">インスタンスを入れる配列の型</typeparam>
        /// <param name="instanceGroupId">InstanceGroupIDクラスのID</param>
        /// <returns>インスタンスが入った配列</returns>
        public static T[] CreateInstances<T>(InstanceGroupID<T> instanceGroupId)
            where T : class
        {
            var xml = new XmlDocument();
            xml.Load(@"./instanceInfo.xml");
            var instances = xml.SelectNodes($"Extension/Group[@Id='{instanceGroupId.Id}']/Instance");

            var converters = new T[instances.Count];
            var i = 0;

            foreach (XmlNode instance in instances)
            {
                var className = instance.Attributes["ClassName"].InnerText;
                var obj = CreateInstance<T>(className);
                converters[i] = obj;
                i++;
            }

            return converters;
        }

        /// <summary>
        /// xmlから受け取ったInstanceIDに入っているclassNameと同じ名前のクラスをインスタンス化させる
        /// </summary>
        /// <typeparam name="T">インスタンスの型</typeparam>
        /// <param name="instanceID">InstanceIDクラスのID</param>
        /// <returns>インスタンス</returns>
        public static T CreateInstance<T>(InstanceID<T> instanceID)
            where T : class
        {
            var xml = new XmlDocument();
            xml.Load(@"./instanceInfo.xml");
            var instanceInfo = xml.SelectNodes($"Extension/Instance[@Id='{instanceID.Id}']");

            if (instanceInfo.Count == 0)
            {
                throw new Exception("instanceId not found");
            }

            var className = instanceInfo.Item(0).Attributes["ClassName"].InnerText;

            return CreateInstance<T>(className);
        }

        /// <summary>
        /// クラス名と型を受け取りインスタンスを生成する
        /// </summary>
        /// <param name="className">インスタンス化させるクラス名</param>
        /// <returns>インスタンス</returns>
        private static T CreateInstance<T>(string className)
            where T : class
        {
            Type myType = Type.GetType(className);
            object obj = Activator.CreateInstance(myType);
            return (T)obj;
        }
    }
}
