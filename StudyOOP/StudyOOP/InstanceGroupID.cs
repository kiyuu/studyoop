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
        /// <param name="instanceIds">インスタンス化させたいクラス名の配列</param>
        public InstanceGroupID(string id, InstanceID[] instanceIds)
            : base(id)
        {
            this.InstanceIds = instanceIds;
        }

        /// <summary>
        /// プロパティ
        /// </summary>
        public InstanceID[] InstanceIds { get; }
    }
}
