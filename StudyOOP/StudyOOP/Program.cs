namespace StudyOOP
{
    using StudyOOP.Common;
    using StudyOOP.Convert;

    public class Program
    {
        public static void Main(string[] args)
        {
            var convertEmployee = new ConvertEmployee();
            convertEmployee.ExecuteConvertDatToTSV();
            var convertItem = new ConvertItem();
            convertItem.ExecuteConvertDatToTSV();
        }
    }
}