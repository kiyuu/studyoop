namespace StudyOOP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Factory
    {
        public static FlatFileToTsvConverterBase CreateInstance(string fileName)
        {
            switch (fileName)
            {
                case "WXXX5555":
                    return new ConvertWXXX5555ToTsv();

                case "WXXX6666":
                    return new ConvertWXXX6666ToTsv();

                default:
                    throw new ArgumentException();
            }
        }
    }
}
