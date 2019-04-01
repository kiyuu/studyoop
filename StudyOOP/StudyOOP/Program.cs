namespace StudyOOP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using StudyOOP.Convert;

    public class Program
    {
        private static Dictionary<string, string> _dicClassInfo = new Dictionary<string, string>();

        public static void Main(string[] args)
        {
            var converters = Factory.CreateInstances("FlatFileToTsvConverters");

            foreach (var converter in converters)
            {
                converter.Execute();
            }

            Factory.CreateInstance("EmployeeConverter").Execute();
            Factory.CreateInstance("ItemConverter").Execute();
        }

        public class Factory
        {
            public static List<FlatFileToTsvConverterBase> CreateInstances(string groupid)
            {
                var flatFileToTsvConverters = new List<FlatFileToTsvConverterBase>();

                string sFile = Path.Combine(Directory.GetCurrentDirectory(), "Setting", "InstanceInfo.xml");

                var xdoc = XDocument.Load(sFile);
                var xelements = xdoc.Elements("Extension").Elements("Group");
                {
                    foreach (var row2 in xelements.Elements("Instance"))
                    {
                        // インスタンスを生成
                        var type = Type.GetType(row2.Attribute("ClassName").Value);
                        flatFileToTsvConverters.Add((FlatFileToTsvConverterBase)Activator.CreateInstance(type));
                    }
                }

                return flatFileToTsvConverters;
            }

            public static FlatFileToTsvConverterBase CreateInstance(string instanceid)
            {
                if (_dicClassInfo.Count == 0)
                {
                    string sFile = Path.Combine(Directory.GetCurrentDirectory(), "Setting", "InstanceInfo.xml");

                    var xdoc = XDocument.Load(sFile);

                    var xelements = xdoc.Elements("Extension").Elements("Instance");

                    foreach (var row in xelements)
                    {
                        _dicClassInfo.Add(row.Attribute("Id").Value, row.Attribute("ClassName").Value);
                    }
                }

                _dicClassInfo.TryGetValue(instanceid, out string classname);

                // インスタンスを生成
                var type = Type.GetType(classname);
                return (FlatFileToTsvConverterBase)Activator.CreateInstance(type);
            }
        }
    }
}