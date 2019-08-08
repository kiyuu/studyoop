namespace StudyOOP
{
    using Common;
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
        /// CreateInstance(string fileName)を呼び出してインスタンス化したクラスを配列に入れて返すメソッド
        /// </summary>
        /// <param name="instanceGroupID">インスタンスが入った配列と紐づくID</param>
        /// <returns>インスタンス化したクラスが入った配列</returns>
        public static FlatFileToTsvConverterBase[] CreateInstances(InstanceGroupID instanceGroupID)
        {
            var files = new FlatFileToTsvConverterBase[instanceGroupID.InstanceIds.Length];

            int i = 0;
            foreach (var id in instanceGroupID.InstanceIds)
            {
                files[i] = CreateInstance(id);
                i++;
            }

            return files;
        }

        /// <summary>
        /// CreateInstance(string className)で生成したインスタンスをFlatFileToTsvConverterBase型にキャストするメソッド
        /// </summary>
        /// <param name="instanceID">キャストさせるインスタンスと紐づくID</param>
        /// <returns>キャスト後のインスタンス</returns>
        public static FlatFileToTsvConverterBase CreateInstance(InstanceID instanceID)
        {
            var flatFile = (FlatFileToTsvConverterBase)CreateInstance(instanceID.ClassName);
            return flatFile;
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
