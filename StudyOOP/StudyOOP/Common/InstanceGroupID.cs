using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyOOP.Common
{
    /// <summary>
    ///  InstanceGroupIDクラス
    /// </summary>
    public class InstanceGroupID : IDBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstanceGroupID"/> class.
        /// コンストラクタ
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="instanceIds">instanceId情報の配列</param>
        public InstanceGroupID(string id, InstanceID[] instanceIds)
           : base(id)
        {
            this.InstanceIds = instanceIds;
        }

        /// <summary>
        /// 読み取り専用InstanceIDの配列
        /// </summary>
        public InstanceID[] InstanceIds { get; }
    }
}
