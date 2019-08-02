using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyOOP.Common
{
    /// <summary>
    /// ID基本クラス
    /// </summary>
    public abstract class IDBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IDBase"/> class.
        /// コンストラクタ
        /// </summary>
        /// <param name="id">id</param>
        protected IDBase(string id)
        {
            this.Id = id;
        }

        /// <summary>
        /// 読み取り専用strign型Id
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// string型に変換
        /// </summary>
        /// <returns>string型のId</returns>
        public override string ToString() => this.Id;

        /// <summary>
        /// オブジェクトの判定
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns>判定結果</returns>
        public override bool Equals(object obj) => obj != null && this.Id == obj.ToString();

        /// <summary>
        /// HashCode取得
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() => this.Id.GetHashCode();
    }
}