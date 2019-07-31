using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyOOP
{
    public class Factory
    {
        public static Converter CreateInstance(string id)
        {
            switch (id)
            {
                case "WXXX5555":
                    return new WXXX5555ToEmployeeConverter();
                case "WXXX6666":
                    return new WXXX6666ToItemConverter();
                default:
                    throw new ArgumentException();
            }
        }
    }
}
