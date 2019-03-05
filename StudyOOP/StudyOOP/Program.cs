namespace StudyOOP
{
    using StudyOOP.Common;
    using StudyOOP.Convert;

    public class Program
    {
        public static void Main(string[] args)
        {
            Converter.ConvertDatToTSV(FileType.FileTypeEmployee);
            Converter.ConvertDatToTSV(FileType.FileTypeItem);
        }
    }
}