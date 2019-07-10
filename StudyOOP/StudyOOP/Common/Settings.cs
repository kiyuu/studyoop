namespace StudyOOP
{
    using System.IO;

    /// <summary>
    /// Settingクラス
    /// </summary>
    internal static class Settings
    {
        /// <summary>
        /// Gets inputパス作成
        /// </summary>
        internal static string InputDirectory { get; } = Path.Combine(".", "input");

        /// <summary>
        /// Gets outputパス作成
        /// </summary>
        internal static string OutputDirectory { get; } = Path.Combine(".", "output");

        /// <summary>
        /// Gets 拡張子.dat
        /// </summary>
        internal static string InputExtension { get; } = ".dat";

        /// <summary>
        /// Gets 拡張子.tsv
        /// </summary>
        internal static string OutputExtension { get; } = ".tsv";

        /// <summary>
        /// Gets タブ区切り
        /// </summary>
        internal static string Separation { get; } = "\t";
    }
}
