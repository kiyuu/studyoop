namespace StudyOOP
{
    using System.IO;

    /// <summary>
    /// Setting
    /// </summary>
    internal static class Setting
    {
        /// <summary>
        /// GEtsinputパス作成
        /// </summary>
        internal static string InputDirectory { get; private set; } = Path.Combine(".", "input");

        /// <summary>
        /// GEtsoutputパス作成
        /// </summary>
        internal static string OutputDirectory { get; private set; } = Path.Combine(".", "output");

        /// <summary>
        /// GEts拡張子.dat
        /// </summary>
        internal static string InputExtension { get; private set; } = ".dat";

        /// <summary>
        /// GEts拡張子.tsv
        /// </summary>
        internal static string OutputExtension { get; private set; } = ".tsv";

        /// <summary>
        /// GEtsタブ区切り
        /// </summary>
        internal static string Separation { get; private set; } = "\t";
    }
}
