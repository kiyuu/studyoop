﻿namespace StudyOOP.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    /// シンプルファクトリーパターン
    /// </summary>
    public static class Factory
    {
        // <id ,className>
        private static readonly IReadOnlyDictionary<string, string> _instanceInfos;

        // <id ,<id, className>>
        private static readonly IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> _instanceGroupInfos;

        static Factory()
        {
            var rootPath = Path.Combine(".", "Settings");
            var fileName = "InstanceInfo.xml";

            // <id ,className>
            var instances = new Dictionary<string, string>();

            // <id ,<id, className>>
            var instanceGroups = new Dictionary<string, IReadOnlyDictionary<string, string>>();

            string filePath = Path.Combine(rootPath, fileName);

            foreach (var element in XElement.Load(filePath).Elements())
            {
                if (element.Name == "Instance")
                {
                    var instanceInfo = GetInstanceInfo(element);
                    instances.Add(instanceInfo.id, instanceInfo.className);
                }
                else if (element.Name == "Group")
                {
                    var id = element.Attribute("Id").Value;

                    var instanceInfos = element
                        .Elements("Instance")
                        .Select(e => GetInstanceInfo(e))
                        .ToDictionary(i => i.id, i => i.className);

                    instanceGroups.Add(id, instanceInfos);
                }
            }

            _instanceInfos = new Dictionary<string, string>(instances);
            _instanceGroupInfos = new Dictionary<string, IReadOnlyDictionary<string, string>>(instanceGroups);
        }

        public static T[] CreateInstances<T>(InstanceGroupID<T> instanceGroupID)
            where T : class
        {
            if (!_instanceGroupInfos.TryGetValue(instanceGroupID.Id, out IReadOnlyDictionary<string, string> value))
            {
                throw new ApplicationException($"{instanceGroupID.Id} is not found");
            }

            var instances = new List<T>();
            foreach (var className in value.Select(v => v.Value))
            {
                instances.Add(CreateInstance<T>(className));
            }

            return instances.ToArray();
        }

        public static T CreateInstance<T>(InstanceID<T> instancesID)
            where T : class
        {
            if (!_instanceInfos.TryGetValue(instancesID.Id, out string className))
            {
                throw new ApplicationException($"{instancesID.Id} is not found");
            }

            return CreateInstance<T>(className);
        }

        private static (string id, string className) GetInstanceInfo(XElement element)
        {
            return (id: element.Attribute("Id").Value, className: element.Attribute("ClassName").Value);
        }

        private static T CreateInstance<T>(string className)
            where T : class
        {
            var t = Type.GetType(className);
            var instance = Activator.CreateInstance(t) as T;
            if (instance == null)
            {
                throw new ApplicationException($"{className} can't create instance");
            }

            return instance;
        }
    }
}
