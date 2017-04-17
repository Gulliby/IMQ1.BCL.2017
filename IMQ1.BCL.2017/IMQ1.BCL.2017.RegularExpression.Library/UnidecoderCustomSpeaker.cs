using IMQ1.BCL._2017.Infrastructure.Reader.Interface;
using IMQ1.BCL._2017.Infrastructure.Speakers.Interface;
using IMQ1.BCL._2017.Infrastructure.Writer.Interface;
using System;
using System.IO;

namespace IMQ1.BCL._2017.RegularExpression.Library
{
    public class UnidecoderCustomSpeaker : ISpeaker
    {
        private static string Backup = "Backup";

        private readonly IWriter _writer;
        private readonly IReader _reader;

        public UnidecoderCustomSpeaker(IReader reader, IWriter writer)
        {
            _writer = writer;
            _reader = reader;
        }

        public void Speak<T>(T data)
        {
            _writer.Write("Would you like to save unidecoded data?");
            if (_reader.Read() == "y")
            {
                return;
            }

            if (!(data is string))
            {
                return;
            }

            var file = data as string;
            var fileInfo = new FileInfo(file);
            try
            {
                fileInfo.Delete();
                File.Move($"{fileInfo.DirectoryName}\\{Backup}\\{fileInfo.Name}", fileInfo.FullName);
            }
            catch (Exception ex)
            {
                _writer.Write($"File can't be used. Exception: {ex.Message}");
            }            
        }
    }
}
