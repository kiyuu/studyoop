namespace StudyOOP.Convert
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using StudyOOP.Common;

    public class ConvertItem : ConverterBase
    {
        private readonly string _inputItemFileName = "WXXX6666";

        private readonly string _outputItemFileName = "Item";

        private readonly int _itemBodyLineSize = 35;

        protected override string GetFileName()
        {
            return this._inputItemFileName;
        }

        protected override int GetBodyLineSize()
        {
            return this._itemBodyLineSize;
        }

        protected override bool IsFileDetailValid(string line, List<ConvertedFileInformation> convertedfileInformations, string functionType, int index)
        {
            var length = 13;
            if (!long.TryParse(line.Substring(index, length), out long code))
            {
                return false;
            }

            index += length;

            length = 10;
            var name = line.Substring(index, length);
            index += length;

            length = 10;
            if (!decimal.TryParse(line.Substring(index, length), out decimal unitPrice))
            {
                return false;
            }

            index += length;
            var itemLine = string.Join(Settings.TsvSeparater, code.ToString().PadLeft(13, '0'), name.Trim(), unitPrice) + Environment.NewLine;
            var outputItemFileName = this._outputItemFileName + Settings.OutputFileExtension;

            convertedfileInformations.Add(new ConvertedFileInformation(this.MakeDeleteFileName(outputItemFileName, functionType), itemLine));

            return true;
        }

        protected override bool IsFileNameWithoutExtensionValid(string filePath, string fileName)
        {
            var dateFormat = "yyyyMMdd";
            var dateFormatLength = dateFormat.Length;
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);

            if (!this.IsFileNameDateValid(fileNameWithoutExtension, dateFormat, dateFormatLength))
            {
                return false;
            }

            return this.IsFileNameWithoutDateValid(fileNameWithoutExtension, fileName, dateFormatLength);
        }

        private bool IsFileNameDateValid(string fileName, string dateFormat, int dateFormatLength)
        {
            var index = 0;
            if (string.IsNullOrEmpty(fileName) || fileName.Length < dateFormatLength)
            {
                return false;
            }

            var datePart = fileName.Substring(index, dateFormatLength);
            DateTime datetime;
            return DateTime.TryParseExact(datePart, dateFormat, System.Globalization.CultureInfo.InstalledUICulture, System.Globalization.DateTimeStyles.None, out datetime);
        }

        private bool IsFileNameWithoutDateValid(string fileNameWithoutExtension, string fileName, int dateFormatLength)
        {
            var fileNamePart = fileNameWithoutExtension.Substring(dateFormatLength);
            return fileNamePart.Equals(fileName, StringComparison.OrdinalIgnoreCase);
        }
    }
}
