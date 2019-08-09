using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyOOP.Common
{
    /// <summary>
    /// InstanceGroupID
    /// </summary>
    /// <typeparam name="T">GroupID</typeparam>
    public class InstanceGroupID<T> : IDBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstanceGroupID{T}"/> class.
        /// コンストラクタ
        /// </summary>
        /// <param name="id">ID</param>
        public InstanceGroupID(string id)
           : base(id)
        {
        }
    }
}
