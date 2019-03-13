namespace StudyOOP.Convert
{
    using System;
    using System.Collections.Generic;
    using StudyOOP.Common;

    public class WXXX6666ToItemTSVConverter : FlatFileToTsvConverterBase
    {
        private static readonly string _outputItemFileName = "Item";

        protected override string InputFileName => "WXXX6666";

        protected override int BodyLineSizeWithoutFunctionType => 33;

        protected override List<ConvertedRecordInfo> ConvertFlatLineToTsvs(string bodyLine, string functionType, int index)
        {
            var result = new List<ConvertedRecordInfo>();

            var length = 13;
            if (!long.TryParse(bodyLine.Substring(index, length), out long code))
            {
                return result;
            }

            index += length;

            length = 10;
            var name = bodyLine.Substring(index, length);
            index += length;

            length = 10;
            if (!decimal.TryParse(bodyLine.Substring(index, length), out decimal unitPrice))
            {
                return result;
            }

            index += length;

            var itemLine = string.Join(Settings.TsvSeparater, code.ToString().PadLeft(13, '0'), name.Trim(), unitPrice) + Environment.NewLine;
            result.Add(new ConvertedRecordInfo(this.MakeOutputFileName(functionType, _outputItemFileName), itemLine));
            return result;
        }
    }
}
