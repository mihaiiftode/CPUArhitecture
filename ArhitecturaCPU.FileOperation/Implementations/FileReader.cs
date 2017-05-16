using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArhitecturaCPU.FileOperation.Interfaces;

namespace ArhitecturaCPU.FileOperation.Implementations
{
    public class FileReader : IText
    {
        public string FilePath { get; }

        public FileReader(string filePath)
        {
            FilePath = filePath;
        }

        public string[] GetAllContent()
        {
            return File.ReadAllLines(FilePath);
        }

        public List<string> GetAllContentList()
        {
            return File.ReadLines(FilePath).ToList();
        }
    }
}
