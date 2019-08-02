namespace StudyOOP.Common
{
    /// <summary>
    /// インスタンス化させたいクラスと紐づくIDを持つクラス。実際にインスタンス化させるクラスの名前は子クラスで設定する
    /// </summary>
    public abstract class IDBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IDBase"/> class.
        /// コンストラクタ
        /// </summary>
        /// <param name="id">ID</param>
        protected IDBase(string id)
        {
            this.Id = id;
        }

        /// <summary>
        /// プロパティ
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// IDを返すようにオーバーライド
        /// </summary>
        /// <returns>ID名</returns>
        public override string ToString() => this.Id;

        /// <summary>
        /// インスタンス化したクラス同士が等価と判定される条件を設定
        /// </summary>
        /// <param name="obj">インスタンス</param>
        /// <returns>bool値</returns>
        public override bool Equals(object obj) => obj != null && this.Id == obj.ToString();

        /// <summary>
        /// ハッシュコード
        /// </summary>
        /// <returns>idのハッシュコード</returns>
        public override int GetHashCode() => this.Id.GetHashCode();
    }
}
