namespace StudyOOP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Factory
    {
        public static FlatFileToTsvConverterBase[] CreateInstances()
        {
            FlatFileToTsvConverterBase[] files = new FlatFileToTsvConverterBase[]
                {
                    new ConvertWXXX5555ToTsv(),
                    new ConvertWXXX6666ToTsv()
                };
            return files;
        }
    }
}
