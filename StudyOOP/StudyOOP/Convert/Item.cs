namespace StudyOOP.Convert
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using StudyOOP.Common;

    public class Item : ConverterBase
    {
        private readonly string _inputItemFileName = "WXXX6666";

        private readonly string _outputItemFileName = "Item";

        private readonly int _itemBodyLineSize = 35;

        protected override FileSetting FileSetting
        {
            get
            {
                return new FileSetting(this._inputItemFileName, this._itemBodyLineSize);
            }
        }

        // ↓同じ意味
        ////protected override FileSetting GetFileSetting()
        ////{
        ////    FileSetting fileSetting;
        ////    fileSetting.Name = this._inputItemFileName;
        ////    fileSetting.LineSize = this._itemBodyLineSize;
        ////    return fileSetting;
        ////}

        protected override bool IsFileNameMatchWithoutExtension(string filePath)
        {
            filePath = Path.GetFileNameWithoutExtension(filePath);
            if (filePath.Length < 8)
            {
                return false;
            }

            var filename = filePath.Substring(0, 8);
            var format = "yyyyMMdd";
            DateTime dt;
            if (!DateTime.TryParseExact(filename, format, CultureInfo.CurrentCulture, DateTimeStyles.None, out dt))
            {
                return false;
            }

            var comparefilename = filePath.Substring(8, filePath.Length - 8);
            return Path.GetFileNameWithoutExtension(comparefilename).Equals(this.FileSetting.Name, StringComparison.OrdinalIgnoreCase);
        }

        protected override bool GetPrintContent(string line, List<OutputFile> outputfiles)
        {
            var index = 0;
            var length = 2;

            index += length;
            length = 13;
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

            outputfiles.Add(new OutputFile(string.Join(Settings.TsvSeparater, code.ToString().PadLeft(13, '0'), name.Trim(), unitPrice) + Environment.NewLine, this._outputItemFileName));

            return true;
        }
    }
}
