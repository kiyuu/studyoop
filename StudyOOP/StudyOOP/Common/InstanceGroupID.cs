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
        public InstanceGroupID(string id)
           : base(id)
        {
        }
    }
}
