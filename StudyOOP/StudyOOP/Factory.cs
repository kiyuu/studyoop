namespace StudyOOP
{
    using System;
    using System.Xml;
    using StudyOOP.Common;

    /// <summary>
    /// インスタンスの生成を行うメソッドが入ったクラス
    /// </summary>
    public class Factory
    {
        /// <summary>
        /// xmlから読み取ったGroupIDに入っているclassNameと同じ名前のクラスをインスタンス化させて配列に格納して返すメソッド
        /// </summary>
        /// <param name="instanceGroupId">InstanceGroupIDクラスのプロパティ</param>
        /// <returns>インスタンス化させたクラスが入った配列</returns>
        public static FlatFileToTsvConverterBase[] CreateInstances(InstanceGroupID instanceGroupId)
        {
            var xml = new XmlDocument();
            xml.Load(@"./instanceInfo.xml");
            var instances = xml.SelectNodes($"Extension/Group[@Id='{instanceGroupId.Id}']/Instance");

            var converters = new FlatFileToTsvConverterBase[instances.Count];
            var i = 0;

            foreach (XmlNode instance in instances)
            {
                var className = instance.Attributes["ClassName"].InnerText;
                var obj = CreateInstance(className);
                converters[i] = (FlatFileToTsvConverterBase)obj;
                i++;
            }

            return converters;
        }

        /// <summary>
        /// xmlから読み取ったInstanceIDに入っているclassNameと同じ名前のクラスをインスタンス化返すメソッド
        /// </summary>
        /// <param name="instanceId">InstanceGroupIDクラスのプロパティ</param>
        /// <returns>インスタンス化させたクラスが入った配列</returns>
        public static FlatFileToTsvConverterBase CreateInstance(InstanceID instanceId)
        {
            var xml = new XmlDocument();
            xml.Load(@"./instanceInfo.xml");
            var instanceInfo = xml.SelectNodes($"Extension/Instance[@Id='{instanceId.Id}']");

            if (instanceInfo.Count == 0)
            {
                throw new Exception("instanceId not found");
            }

            var className = instanceInfo.Item(0).Attributes["ClassName"].InnerText;

            return (FlatFileToTsvConverterBase)CreateInstance(className);
        }

        /// <summary>
        /// クラス名を渡してobject型でインスタンスを生成するメソッド
        /// </summary>
        /// <param name="className">インスタンス化させるクラス名</param>
        /// <returns>object型のインスタンス</returns>
        private static object CreateInstance(string className)
        {
            Type myType = Type.GetType(className);
            object obj = Activator.CreateInstance(myType);
            return obj;
        }
    }
}
