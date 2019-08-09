namespace StudyOOP.Common
{
    /// <summary>
    /// インスタンス化させたいクラスに紐づくIDを持つクラス
    /// </summary>
    /// <typeparam name="T">インスタンスの型</typeparam>
    public class InstanceID<T> : IDBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstanceID{T}"/> class.
        /// コンストラクタ
        /// </summary>
        /// <param name="id">ID</param>
        public InstanceID(string id)
            : base(id)
        {
        }
    }
}
