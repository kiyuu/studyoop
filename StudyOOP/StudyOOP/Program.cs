namespace StudyOOP
{
    using StudyOOP.Convert;

    public class Program
    {
        public static void Main(string[] args)
        {
            Converter.ConvertFlatFileToTsv(FlatFileType.WXXX5555);
            Converter.ConvertFlatFileToTsv(FlatFileType.WXXX6666);
        }
    }
}