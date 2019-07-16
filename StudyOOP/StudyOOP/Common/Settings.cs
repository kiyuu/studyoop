namespace StudyOOP
{
    using System.IO;

    /// <summary>
    /// Settingクラス
    /// </summary>
    internal static class Settings
    {
        /// <summary>
        /// inputパス作成
        /// </summary>
        internal static string InputDirectory { get; } = Path.Combine(".", "input");

        /// <summary>
        /// outputパス作成
        /// </summary>
        internal static string OutputDirectory { get; } = Path.Combine(".", "output");

        /// <summary>
        /// 拡張子.dat
        /// </summary>
        internal static string InputExtension { get; } = ".dat";

        /// <summary>
        /// 拡張子.tsv
        /// </summary>
        internal static string OutputExtension { get; } = ".tsv";

        /// <summary>
        /// タブ区切り
        /// </summary>
        internal static string Separation { get; } = "\t";
    }
}
