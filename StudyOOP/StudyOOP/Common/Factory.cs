namespace StudyOOP.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using StudyOOP.Convert;

    internal static class Factory
    {
        public static FlatFileToTsvConverterBase CreateInstance(string fileType)
        {
            if (fileType == ID.IDEmployee)
            {
                return new WXXX5555ToEmployeeTSVConverter();
            }
            else if (fileType == ID.IDItem)
            {
                return new WXXX6666ToItemTSVConverter();
            }
            else
            {
                return null;
            }
        }
    }
}
