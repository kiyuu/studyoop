namespace StudyOOP.Common
{
    /// <summary>
    /// InstanceID型のIDを渡してInstanceGroupID型の配列とそれに紐づくIDを持つクラス
    /// </summary>
    public static class InstanceGroupIDs
    {
        /// <summary>
        /// インスタンス化させたいクラスのIDが入った配列とそれに紐づくIDを持つクラスを取得するプロパティ
        /// </summary>
        public static InstanceGroupID FlatFileToTsvConverters => new InstanceGroupID(nameof(FlatFileToTsvConverters));
    }
}
