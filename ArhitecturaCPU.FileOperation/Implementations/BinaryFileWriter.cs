using System;
using System.IO;
using ArhitecturaCPU.FileOperation.Interfaces;

namespace ArhitecturaCPU.FileOperation.Implementations
{
    public class BinaryFileWriter : IBinaryWriter
    {
        private BinaryWriter _binaryWriter;

        public BinaryFileWriter(string fileName)
        {
            _binaryWriter = new BinaryWriter(File.Open(fileName, FileMode.Create));
        }

        public void Write(string item)
        {

            var valueToWrite = Convert.ToInt16(item,2);
            Console.WriteLine("Bin:" + item);
            Console.WriteLine("Dec:" + valueToWrite);
            Console.WriteLine("Hex:" + valueToWrite.ToString("X"));
            _binaryWriter.Write(valueToWrite);
        }

        public void Close()
        {
            _binaryWriter.Close();
        }

        public void Write()
        {
            throw new NotImplementedException();
        }
    }
}
