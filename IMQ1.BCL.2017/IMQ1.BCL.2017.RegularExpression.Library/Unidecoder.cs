using IMQ1.BCL._2017.Infrastructure.Common;
using IMQ1.BCL._2017.Infrastructure.Speakers.Interface;
using IMQ1.BCL._2017.Infrastructure.Writer;
using IMQ1.BCL._2017.Infrastructure.Writer.Interface;
using IMQ1.BCL._2017.RegularExpression.Library.AppConfig;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnidecodeSharpFork;

namespace IMQ1.BCL._2017.RegularExpression.Library
{
    public static class Unidecoder
    {
        private const string RussianLettersPattern = "[А-я]+";
        private const string Backup = "Backup";
        private static HashSet<string> Extensions = new HashSet<string>() { ".txt" };

        public static void DecodeData(IEnumerable<string> fileSystemEntities, IWriter writer = null, ISpeaker speaker = null)
        {
            var files = fileSystemEntities
                    .Where(fileSystemEntity => fileSystemEntity.FileOrDirectoryExists() ? File.GetAttributes(fileSystemEntity).HasFlag(FileAttributes.Directory) : false)
                        .Select(folder => Directory.GetFiles(folder))
                        .SelectMany(file => file)
                        .Union(fileSystemEntities
                            .Where(fileSystemEntity => fileSystemEntity.FileOrDirectoryExists() ? !File.GetAttributes(fileSystemEntity).HasFlag(FileAttributes.Directory) : false));
            
            if (writer == null)
            {
                writer = new ConsoleWriter();
            }

            // Can be moved to class - Configurator or so,
            // but it can be used there, because this configuration was made only for this method.
            var section = (FormatSection)ConfigurationManager.GetSection("FormatSection");

            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (!Extensions.Contains(fileInfo.Extension)) continue;

                writer.Write(string.Format(section.File.Format, fileInfo.Name, fileInfo.Extension, fileInfo.Length));
                Directory.CreateDirectory($"{fileInfo.DirectoryName}//{Backup}");

                try
                {
                    fileInfo.MoveTo($"{fileInfo.DirectoryName}//{Backup}//{fileInfo.Name}");
                }
                catch (Exception ex)
                {
                    writer.Write($"File can't be used. Exception: {ex.Message}");
                    continue;
                }

                using (var streamReader = new StreamReader($"{fileInfo.DirectoryName}//{fileInfo.Name}"))
                {
                    using (var streamWriter = new StreamWriter(file))
                    {
                        var lineCount = 0;
                        var fileMathchesCount = 0;
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            Regex.Matches(line, RussianLettersPattern).Cast<Match>()
                               .ToList().ForEach(match =>
                               {
                                   writer.Write(string.Format(section.Substring.Format, lineCount + 1, match.Index, match.Value));
                                   fileMathchesCount++;
                               });
                            lineCount++;
                            streamWriter.WriteLine(line.Unidecode());
                        }

                        writer.Write(string.Format(section.Result.Format, fileMathchesCount));
                    }                  
                }

                if (speaker != null)
                {
                    speaker.Speak(file);
                }
            }
        }
    }
}
