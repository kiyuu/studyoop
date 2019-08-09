namespace StudyOOP.Common
{
    /// <summary>
    /// InstanceID型のクラスに紐づくIDを持つクラス
    /// </summary>
    /// <typeparam name="T">インスタンスの型</typeparam>
    public class InstanceGroupID<T> : IDBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstanceGroupID{T}"/> class.
        /// コンストラクタ
        /// </summary>
        /// <param name="id">インスタンス化させたいクラスのID</param>
        public InstanceGroupID(string id)
            : base(id)
        {
        }
    }
}
