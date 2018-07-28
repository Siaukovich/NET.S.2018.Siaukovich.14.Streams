using System;
using System.Configuration;
using static StreamsDemo.StreamsExtension;

namespace ConsoleClient
{
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            var source = ConfigurationManager.AppSettings["sourceFilePath"];

            var destination = ConfigurationManager.AppSettings["destinationFilePath"];

            Console.WriteLine($"ByteCopy() done. Total bytes: {ByByteCopy(source, destination)}");
            Console.WriteLine($"ContentEquals() done. Files equal: {IsContentEquals(source, destination)}");

            Console.WriteLine($"BlockCopy() done. Total bytes: {ByBlockCopy(source, destination)}");
            Console.WriteLine($"ContentEquals() done. Files equal: {IsContentEquals(source, destination)}");

            Console.WriteLine($"LineCopy() done. Total lines: {ByLineCopy(source, destination)}");
            Console.WriteLine($"ContentEquals() done. Files equal: {IsContentEquals(source, destination)}");

            Console.WriteLine($"InMemoryByByteCopy() done. Total bytes: {InMemoryByByteCopy(source, destination)}");
            Console.WriteLine($"ContentEquals() done. Files equal: {IsContentEquals(source, destination)}");

            //Console.WriteLine($"InMemoryByteCopy() done. Total bytes: {InMemoryByByteCopy(source, destination)}");

            //Console.WriteLine(IsContentEquals(source, destination));

            //etc

            Console.ReadKey();
        }
    }
}
