using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyOOP.Common
{
    /// <summary>
    /// InstanceID
    /// </summary>
    /// <typeparam name="T">id</typeparam>
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
