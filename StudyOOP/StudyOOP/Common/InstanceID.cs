using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyOOP.Common
{
    /// <summary>
    /// InstanceIDクラス
    /// </summary>
    public class InstanceID : IDBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstanceID"/> class.
        /// コンストラクタ
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="className">cクラス名</param>
        public InstanceID(string id, string className)
            : base(id)
        {
            this.ClassName = className;
        }

        /// <summary>
        /// 読み取り専用クラス名
        /// </summary>
        public string ClassName { get; }
    }
}
