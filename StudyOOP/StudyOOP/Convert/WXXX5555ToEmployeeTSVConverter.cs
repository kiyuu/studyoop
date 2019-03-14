namespace StudyOOP.Convert
{
    using System;
    using System.Collections.Generic;
    using StudyOOP.Common;

    public class WXXX5555ToEmployeeTSVConverter : FlatFileToTsvConverterBase
    {
        private readonly string _outputEmployeeFileName = "Employee";

        private readonly string _outputEmployeeAuthorityFileName = "EmployeeAuthority";

        protected override string InputFileName => "WXXX5555";

        protected override int BodyLineSizeWithoutFunctionType => 16;

        protected override int FunctionTypeLength => 3;

        protected override string MakeOutputFileName(string functionType, string fileNameWithoutExtension)
        {
            var result = fileNameWithoutExtension + Settings.OutputFileExtension;
            if (functionType == "100")
            {
                return result;
            }
            else if (functionType == "200")
            {
                return this.OutputDeletePrefix + result;
            }

            return "update_" + result;
        }

        protected override List<ConvertedRecordInfo> ConvertFlatLineToTsvs(string bodyLine, string functionType, int index)
        {
            var result = new List<ConvertedRecordInfo>();
            var length = 5;
            if (!int.TryParse(bodyLine.Substring(index, length), out int code))
            {
                return result;
            }

            index += length;

            length = 10;
            var name = bodyLine.Substring(index, length);
            index += length;

            length = 1;
            var authority = bodyLine.Substring(index, length);
            index += length;

            var employeeLine = string.Join(Settings.TsvSeparater, code.ToString().PadLeft(5, '0'), name.Trim()) + Environment.NewLine;
            var employeeAuthorityLine = string.Join(Settings.TsvSeparater, code.ToString().PadLeft(5, '0'), authority) + Environment.NewLine;

            result.Add(new ConvertedRecordInfo(this.MakeOutputFileName(functionType, this._outputEmployeeFileName), employeeLine));
            result.Add(new ConvertedRecordInfo(this.MakeOutputFileName(functionType, this._outputEmployeeAuthorityFileName), employeeAuthorityLine));
            return result;
        }
    }
}
