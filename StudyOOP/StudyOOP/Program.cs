namespace StudyOOP
{
    using StudyOOP.Common;
    using StudyOOP.Convert;

    public class Program
    {
        public static void Main(string[] args)
        {
            Converter.ConvertToTSV(Constants.FileTypeEmployee);
            Converter.ConvertToTSV(Constants.FileTypeItem);
        }
    }
}