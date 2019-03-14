namespace StudyOOP
{
    using System.Collections.Generic;
    using StudyOOP.Common;
    using StudyOOP.Convert;

    public class Program
    {
        public static void Main(string[] args)
        {
            var converters = Factory.CreateInstances(InstanceGroupIDs.FlatFileToTsvConverters);

            foreach (var converter in converters)
            {
                converter.Execute();
            }
        }
    }
}