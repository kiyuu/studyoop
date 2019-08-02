namespace StudyOOP.Common
{
    /// <summary>
    /// インスタンス化させたいクラスとそれに紐づくIDを持つクラス
    /// </summary>
    public class InstanceID : IDBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstanceID"/> class.
        /// コンストラクタ
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="className">クラス名</param>
        public InstanceID(string id, string className)
            : base(id)
        {
            this.ClassName = className;
        }

        /// <summary>
        /// プロパティ
        /// </summary>
        public string ClassName { get; }
    }
}
