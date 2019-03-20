namespace StudyOOP
{
    using StudyOOP.Common;
    using StudyOOP.Convert;

    public class Program
    {
        public static void Main(string[] args)
        {
            ConverterBase convertEmployee = new ConvertEmployee();
            convertEmployee.ConvertDatToTSV();
            ConverterBase convertItem = new ConvertItem();
            convertItem.ConvertDatToTSV();
        }
    }
}