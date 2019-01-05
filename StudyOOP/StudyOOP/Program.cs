namespace StudyOOP
{
    using System.Collections.Generic;
    using StudyOOP.Convert;

    public class Program
    {
        public static void Main(string[] args)
        {
            var converters = new List<FlatFileToTsvConverterBase>()
                {
                    new WXXX5555ToEmployeeTSVConverter(),
                    new WXXX6666ToItemTSVConverter()
                };

            converters.ForEach(c => c.Execute());
        }
    }
}