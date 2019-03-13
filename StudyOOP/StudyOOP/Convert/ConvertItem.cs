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

        public void ExecuteConvertDatToTSV()
        {
            this.ConvertDatToTSV();
        }

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

            convertedfileInformations.Add(new ConvertedFileInformation() { FileName = this.CreateDeleteFileName(outputItemFileName, functionType), Line = itemLine });

            return true;
        }
    }
}
