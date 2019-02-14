namespace StudyOOP
{
    using StudyOOP.Convert;

    public class Program
    {
        public static void Main(string[] args)
        {
            Converter.ConvertDatFileToTSVFile((int)Converter.ConvertMode.ModeEmployee);
            Converter.ConvertDatFileToTSVFile((int)Converter.ConvertMode.ModeItem);
        }
    }
}