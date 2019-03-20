namespace StudyOOP.Convert
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using StudyOOP.Common;

    public class ConvertEmployee : ConverterBase
    {
        private readonly string _inputEmployeeFileName = "WXXX5555";

        private readonly string _outputEmployeeFileName = "Employee";

        private readonly string _outputEmployeeAuthorityFileName = "EmployeeAuthority";

        private readonly int _employeeBodyLineSize = 19;

        private readonly string _outputUpdate_Prefix = "update_";

        protected override string GetFileName()
        {
            return this._inputEmployeeFileName;
        }

        protected override int GetBodyLineSize()
        {
            return this._employeeBodyLineSize;
        }

        protected override bool IsFileDetailValid(string line, List<ConvertedFileInformation> convertedfileInformations, string functionType, int index)
        {
            var length = 5;
            if (!int.TryParse(line.Substring(index, length), out int code))
            {
                return false;
            }

            index += length;

            length = 10;
            var name = line.Substring(index, length);

            index += length;

            length = 1;
            var authority = line.Substring(index, length);

            index += length;

            var employeeLine = string.Join(Settings.TsvSeparater, code.ToString().PadLeft(5, '0'), name.Trim()) + Environment.NewLine;
            var employeeAuthority = string.Join(Settings.TsvSeparater, code.ToString().PadLeft(5, '0'), authority) + Environment.NewLine;

            var outputEmployeeFileName = this._outputEmployeeFileName + Settings.OutputFileExtension;
            var outputEmployeeAuthorityFileName = this._outputEmployeeAuthorityFileName + Settings.OutputFileExtension;

            convertedfileInformations.Add(new ConvertedFileInformation(this.MakeDeleteFileName(outputEmployeeFileName, functionType), employeeLine));
            convertedfileInformations.Add(new ConvertedFileInformation(this.MakeDeleteFileName(outputEmployeeAuthorityFileName, functionType), employeeAuthority));

            return true;
        }

        protected override string GetFunctionType(string line, out int index)
        {
            index = 0;
            var length = 3;
            var functionType = line.Substring(index, length);
            index += length;
            return functionType;
        }

        protected override string MakeDeleteFileName(string outPutFileName, string functionType)
        {
            var fileName = outPutFileName;

            switch (functionType)
            {
                case "100":
                    break;
                case "200":
                    fileName = this.OutputDeletePrefix + outPutFileName;
                    break;
                default:
                    fileName = this._outputUpdate_Prefix + outPutFileName;
                    break;
            }

            return fileName;
        }
    }
}
