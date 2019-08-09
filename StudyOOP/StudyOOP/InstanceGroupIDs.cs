namespace StudyOOP.Common
{
    /// <summary>
    /// InstanceGroupID型の配列に紐づくID
    /// </summary>
    public static class InstanceGroupIDs
    {
        /// <summary>
        /// インスタンスが入った配列に紐づくIDを取得する
        /// </summary>
        public static InstanceGroupID<FlatFileToTsvConverterBase> FlatFileToTsvConverters =>
            new InstanceGroupID<FlatFileToTsvConverterBase>(nameof(FlatFileToTsvConverters));
    }
}
