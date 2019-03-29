namespace StudyOOP
{
    using System;
    using System.Collections.Generic;
    using StudyOOP.Convert;

    using System.IO;
    using System.Xml.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            /////var converters = Factory.CreateInstances(InstanceGroupIDs.FlatFileToTsvConverters);

            var converters = Factory.CreateInstances("FlatFileToTsvConverters");

            foreach (var converter in converters)
            {
                converter.Execute();
            }

            ////Factory.CreateInstance(InstanceIDs.EmployeeConverter).Execute();
            ////Factory.CreateInstance(InstanceIDs.ItemConverter).Execute();
            Factory.CreateInstance("EmployeeConverter").Execute();
            Factory.CreateInstance("ItemConverter").Execute();
        }

        public class Factory
        {
            public static FlatFileToTsvConverterBase[] CreateInstances(string groupid)
            {
                FlatFileToTsvConverterBase[] flatFileToTsvConverters = new FlatFileToTsvConverterBase[0];

                string sFile = Path.Combine(Directory.GetCurrentDirectory(), "Setting", "InstanceInfo.xml");

                var xdoc = XDocument.Load(sFile);

                var xelements = xdoc.Elements("Extension");

                foreach (var row in xelements)
                {
                    foreach (var item in row.Elements("Group"))
                    {
                        if (item.Attribute("Id").Value == groupid)
                        {
                            foreach (var row2 in row.Elements("Instance"))
                            {
                                Array.Resize(ref flatFileToTsvConverters, flatFileToTsvConverters.Length + 1);

                                // インスタンス配列を生成
                                var type = Type.GetType(row2.Attribute("ClassName").Value);
                                flatFileToTsvConverters[flatFileToTsvConverters.Length - 1] = (FlatFileToTsvConverterBase)Activator.CreateInstance(type);
                            }
                        }
                    }
                }

                return flatFileToTsvConverters;
            }

            public static FlatFileToTsvConverterBase CreateInstance(string instanceid)
            {
                FlatFileToTsvConverterBase flatFileToTsvConverter = null;

                string sFile = Path.Combine(Directory.GetCurrentDirectory(), "Setting", "InstanceInfo.xml");

                var xdoc = XDocument.Load(sFile);

                var xelements = xdoc.Elements("Extension");

                foreach (var row in xelements)
                {
                    foreach (var item in row.Elements("Instance"))
                    {
                        if (item.Attribute("Id").Value == instanceid)
                        {
                            // インスタンスを生成
                            var type = Type.GetType(item.Attribute("ClassName").Value);
                            flatFileToTsvConverter = (FlatFileToTsvConverterBase)Activator.CreateInstance(type);
                            break;
                        }
                    }

                    if (flatFileToTsvConverter != null)
                    {
                        break;
                    }
                }

                return flatFileToTsvConverter;
            }
        }
    }
}