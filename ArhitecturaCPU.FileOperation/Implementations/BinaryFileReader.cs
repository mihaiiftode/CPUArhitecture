using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ArhitecturaCPU.FileOperation.Interfaces;

namespace ArhitecturaCPU.FileOperation.Implementations
{
    public class BinaryFileReader : IBinaryReader
    {
        private readonly BinaryReader _binaryReader;

        public List<short> Instructions { get; } = new List<short>();

        public BinaryFileReader(string fileName)
        {
            _binaryReader = new BinaryReader(File.Open(fileName, FileMode.Open));
        }

        public void ReadAll()
        {
            try
            {
                while (_binaryReader.BaseStream.Position != _binaryReader.BaseStream.Length)
                {
                    Instructions.Add(Read());
                }
                Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Binary Reader exception: {0}", ex.Data);
            }
        }

        public short Read()
        {
            return _binaryReader.BaseStream.Position != _binaryReader.BaseStream.Length ? _binaryReader.ReadInt16() : (short)0;
        }

        public void Close()
        {
            _binaryReader.Close();
        }
    }
}
