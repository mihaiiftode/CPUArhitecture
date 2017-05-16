using System.Collections.Generic;

namespace ArhitecturaCPU.FileOperation.Interfaces
{
    interface IText
    {
        string FilePath { get; }

        string[] GetAllContent();

        List<string> GetAllContentList();
    }
}
