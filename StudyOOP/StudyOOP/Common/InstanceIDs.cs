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
        public static InstanceID<Converter> EmployeeConverter => new InstanceID<Converter>(nameof(EmployeeConverter));

        /// <summary>
        /// ItemConverterクラス情報
        /// </summary>
        public static InstanceID<Converter> ItemConverter => new InstanceID<Converter>(nameof(ItemConverter));
    }
}
