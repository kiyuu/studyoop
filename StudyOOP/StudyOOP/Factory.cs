using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyOOP
{
    public class Factory
    {
        public static Converter[] CreateInstance()
        {
            Converter[] converter =
            {
                new WXXX5555ToEmployeeConverter(),
                new WXXX6666ToItemConverter(),
            };

            return converter;
        }
    }
}
