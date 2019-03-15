namespace StudyOOP
{
    using StudyOOP.Common;

    public class Program
    {
        public static void Main(string[] args)
        {
            var converters = Factory.CreateInstances(InstanceGroupIDs.FlatFileToTsvConverters);

            foreach (var converter in converters)
            {
                converter.Execute();
            }

            ////Factory.CreateInstance(InstanceIDs.EmployeeConverter).Execute();
            ////Factory.CreateInstance(InstanceIDs.ItemConverter).Execute();
        }
    }
}