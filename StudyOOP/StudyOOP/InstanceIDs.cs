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
        public static InstanceID EmployeeConverter => new InstanceID(nameof(EmployeeConverter), "StudyOOP.ConvertWXXX5555ToTsv");

        /// <summary>
        /// 商品用ファイルの情報が入ったクラスを指定して取得するプロパティ
        /// </summary>
        public static InstanceID ItemConverter => new InstanceID(nameof(ItemConverter), "StudyOOP.ConvertWXXX6666ToTsv");
    }
}
