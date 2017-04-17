using System.IO;

namespace IMQ1.BCL._2017.Infrastructure.Common
{
    public static class FileOrDirectoryExistsExtension
    {
        public static bool FileOrDirectoryExists(this string name)
        {
            return (Directory.Exists(name) || File.Exists(name));
        }
    }
}
