namespace StudyOOP
{
    using System.Collections.Generic;
    using StudyOOP.Common;
    using StudyOOP.Convert;

    public class Program
    {
        public static void Main(string[] args)
        {
            var converters = new List<FlatFileToTsvConverterBase>()
                {
                    Factory.CreateInstance(ID.IDEmployee),
                    Factory.CreateInstance(ID.IDItem)
                };
            converters.ForEach(c => c.Execute());
        }
    }
}