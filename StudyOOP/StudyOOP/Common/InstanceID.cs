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
        public InstanceID(string id)
            : base(id)
        {           
        }         
    }
}
