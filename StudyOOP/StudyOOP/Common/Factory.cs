namespace StudyOOP.Common
{
    using System;
    using System.Collections.Generic;
    using StudyOOP.Convert;

    /// <summary>
    /// シンプルファクトリーパターン
    /// </summary>
    public static class Factory
    {
        public static FlatFileToTsvConverterBase[] CreateInstances(InstanceGroupID instanceGroupID)
        {
            var instances = new List<FlatFileToTsvConverterBase>();
            foreach (var instanceID in instanceGroupID.InstanceIds)
            {
                instances.Add(CreateInstance(instanceID));
            }

            return instances.ToArray();
        }

        public static FlatFileToTsvConverterBase CreateInstance(InstanceID instancesID)
        {
            var instance = CreateInstance(instancesID.ClassName) as FlatFileToTsvConverterBase;
            if (instance == null)
            {
                throw new ApplicationException($"{instancesID.ClassName} can't create instance");
            }

            return instance;
        }

        private static object CreateInstance(string className)
        {
            var t = Type.GetType(className);
            return Activator.CreateInstance(t);
        }
    }
}
