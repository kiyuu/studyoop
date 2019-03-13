namespace StudyOOP
{
    using StudyOOP.Common;
    using StudyOOP.Convert;

    public class Program
    {
        public static void Main(string[] args)
        {
            ConverterBase employeeFile = new Employee();
            ConverterBase itemFile = new Item();

            employeeFile.ConvertFixedLengthFileToTSVFile();
            itemFile.ConvertFixedLengthFileToTSVFile();
        }
    }
}