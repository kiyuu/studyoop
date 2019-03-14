namespace StudyOOP
{
    public class ConvertedRecordInfo
    {
        public ConvertedRecordInfo(string fileName, string tsvLine)
        {
            this.FileName = fileName;
            this.Line = tsvLine;
        }

        public string FileName { get; }

        public string Line { get; }
    }
}
