namespace StudyOOP.Common
{
    /// <summary>
    /// インスタンス化させたいクラス名を渡して生成したInstanceID型のクラスを持つクラス
    /// </summary>
    public static class InstanceIDs
    {
        /// <summary>
        /// 従業員用ファイルの情報が入ったクラスを指定して取得するプロパティ
        /// </summary>
        public static InstanceID<FlatFileToTsvConverterBase> EmployeeConverter =>
            new InstanceID<FlatFileToTsvConverterBase>(nameof(EmployeeConverter));

        /// <summary>
        /// 商品用ファイルの情報が入ったクラスを指定して取得するプロパティ
        /// </summary>
        public static InstanceID<FlatFileToTsvConverterBase> ItemConverter =>
            new InstanceID<FlatFileToTsvConverterBase>(nameof(ItemConverter));
    }
}
