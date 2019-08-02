using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyOOP.Common
{
    /// <summary>
    /// InstanceIDクラスをインスタンス化して値を入れたクラス
    /// </summary>
    public static class InstanceIDs
    {
        /// <summary>
        /// EmployeeConverterクラス情報
        /// </summary>
        public static InstanceID EmployeeConverter => new InstanceID(nameof(EmployeeConverter), "StudyOOP.WXXX5555ToEmployeeConverter");

        /// <summary>
        /// ItemConverterクラス情報
        /// </summary>
        public static InstanceID ItemConverter => new InstanceID(nameof(ItemConverter), "StudyOOP.WXXX6666ToItemConverter");
    }
}
