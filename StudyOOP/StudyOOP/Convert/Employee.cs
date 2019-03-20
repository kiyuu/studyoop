namespace StudyOOP.Convert
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using StudyOOP.Common;

    public class Employee : ConverterBase
    {
        private readonly string _inputEmployeeFileName = "WXXX5555";

        private readonly string _outputEmployeeFileName = "Employee";

        private readonly string _outputEmployeeAuthorityFileName = "EmployeeAuthority";

        private readonly string _outputUpdatePrefix = "update_";

        private readonly int _employeeBodyLineSize = 19;

        protected override FileSetting FileSetting
        {
            get
            {
                return new FileSetting(this._inputEmployeeFileName, this._employeeBodyLineSize);
            }
        }

        protected override bool GetPrintContent(string line, List<OutputFile> outputfiles)
        {
            var index = 0;
            var length = 3;

            index += length;
            var authority = string.Empty;

            length = 5;
            if (!int.TryParse(line.Substring(index, length), out int code))
            {
                return false;
            }

            index += length;

            length = 10;
            var name = line.Substring(index, length);
            index += length;

            length = 1;
            authority = line.Substring(index, length);
            index += length;

            outputfiles.Add(new OutputFile(string.Join(Settings.TsvSeparater, code.ToString().PadLeft(5, '0'), name.Trim()) + Environment.NewLine, this._outputEmployeeFileName));
            outputfiles.Add(new OutputFile(string.Join(Settings.TsvSeparater, code.ToString().PadLeft(5, '0'), authority) + Environment.NewLine, this._outputEmployeeAuthorityFileName));

            return true;
        }

        protected override string GetOutputFileName(string line)
        {
            switch (line.Substring(0, 3))
            {
                case "100":
                    return string.Empty;

                case "200":
                    return this.OutputDeletePrefix;

                default:
                    return this._outputUpdatePrefix;
            }
        }
    }
}
