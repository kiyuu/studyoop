namespace StudyOOP
{
    using System.IO;

    /// <summary>
    /// Converter.csで呼び出すプロパティが入ったクラス
    /// </summary>
    internal class Settings
    {
        private static readonly string _inputExtension = ".dat";
        private static string outputExtension = ".tsv";
        private static string tabSeparator = "\t";

        /// <summary>
        /// inputディレクトリへのパス
        /// </summary>
        internal static string InputDirectory => Path.Combine(".", "input");

        /// <summary>
        /// outputディレクトリへのパス
        /// </summary>
        internal static string OutputDirectory => Path.Combine(".", "output");

        /// <summary>
        /// 読み込むファイルの拡張子(.dat)
        /// </summary>
        internal static string InputExtension
        {
            get
            {
                return _inputExtension;
            }
        }

        /// <summary>
        /// 書き出すファイルの拡張子(.tsv)
        /// </summary>
        internal static string OutputExtension
        {
            get
            {
                return outputExtension;
            }
        }

        /// <summary>
        /// 書き出すファイル用の区切り文字(tab)
        /// </summary>
        internal static string TabSeparator
        {
            get
            {
                return tabSeparator;
            }
        }
    }
}