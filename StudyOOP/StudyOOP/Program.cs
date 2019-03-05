namespace StudyOOP
{
    using StudyOOP.Common;
    using StudyOOP.Convert;

    public class Program
    {
        public static void Main(string[] args)
        {
            Converter.ConvertFixedLengthFileToTSVFile(ConvertMode.ModeEmployee);
            Converter.ConvertFixedLengthFileToTSVFile(ConvertMode.ModeItem);
        }
    }
}