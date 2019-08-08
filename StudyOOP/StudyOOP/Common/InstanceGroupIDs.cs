using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyOOP.Common
{
    /// <summary>
    /// InstanceGroupIDを配列にしたクラス
    /// </summary>
    public static class InstanceGroupIDs
    {
        /// <summary>
        /// 配列作成
        /// </summary>
        public static InstanceGroupID FlatFileToTsvConverters => new InstanceGroupID(nameof(FlatFileToTsvConverters));               
    }
}
