namespace StudyOOP.Common
{
    using StudyOOP.Convert;

    /// <summary>
    /// シンプルファクトリーパターン
    /// </summary>
    public static class Factory
    {
        public static FlatFileToTsvConverterBase CreateInstance(string id)
        {
            if (id == FuctoryIds.EmployeeConverterId)
            {
                return new WXXX6666ToItemTSVConverter();
            }
            else if (id == FuctoryIds.ItemConverterId)
            {
                return new WXXX6666ToItemTSVConverter();
            }
            else
            {
                return null;
            }
        }
    }
}
