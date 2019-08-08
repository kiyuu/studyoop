namespace StudyOOP.Common
{
    /// <summary>
    /// InstanceID型のクラスが入った配列とそれに紐づくIDを持つクラス
    /// </summary>
    public class InstanceGroupID : IDBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstanceGroupID"/> class.
        /// コンストラクタ
        /// </summary>
        /// <param name="id">インスタンス化させたいクラスのID</param>
        public InstanceGroupID(string id)
            : base(id)
        {
        }
    }
}
