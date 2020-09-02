using System;
using System.IO;

namespace DAN_LIX_Kristina_Garcia_Francisco.Models
{
    /// <summary>
    /// Reads and Writes to a file
    /// </summary>
    class ReadWriteFile
    {
        /// <summary>
        /// The file we are writing to
        /// </summary>
        private readonly string file = @"../../TextFile";

        /// <summary>
        /// Save wins to file
        /// </summary>
        public void WriteToFile(TimeSpan time)
        {
            // Create directory if it does not exist
            Directory.CreateDirectory(file);
            using (StreamWriter writer = new StreamWriter(file + @"/IgraPamcenja.txt", append: true))
            {
                writer.WriteLine($"Date and time: {DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}, Total time Played: {time}");
            }
        }
    }
}
