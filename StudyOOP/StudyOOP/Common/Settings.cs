namespace StudyOOP.Common
{
    using System.IO;

    internal static class Settings
    {
        internal static string InputDirectory => Path.Combine(".", "input");

        internal static string OutputDirectory => Path.Combine(".", "output");

        internal static string InputFileExtension => ".dat";

        internal static string OutputFileExtension => ".tsv";

        internal static string TsvSeparater => "\t";
    }
}
