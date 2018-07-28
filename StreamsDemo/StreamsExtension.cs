using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace StreamsDemo
{
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    // C# 6.0 in a Nutshell. Joseph Albahari, Ben Albahari. O'Reilly Media. 2015
    // Chapter 15: Streams and I/O
    // Chapter 6: Framework Fundamentals - Text Encodings and Unicode
    // https://msdn.microsoft.com/ru-ru/library/system.text.encoding(v=vs.110).aspx

    public static class StreamsExtension
    {

        #region Public members

        #region TODO: Implement by byte copy logic using class FileStream as a backing store stream .

        public static int ByByteCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            using (FileStream sourceFile = File.OpenRead(sourcePath), 
                         destinationFile = File.OpenWrite(destinationPath))
            {
                int byteCount = 0;
                while (true)
                {
                    int b = sourceFile.ReadByte();
                    if (b == -1)
                    {
                        return byteCount;
                    }

                    destinationFile.WriteByte((byte)b);
                    byteCount++;
                }
            }
        }

        #endregion

        #region TODO: Implement by byte copy logic using class MemoryStream as a backing store stream.

        public static int InMemoryByByteCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);
            
            // TODO: step 1. Use StreamReader to read entire file in string
            string fileText;
            using (var sr = new StreamReader(sourcePath))
            {
                fileText = sr.ReadToEnd();
            }
            
            // TODO: step 2. Create byte array on base string content - use  System.Text.Encoding class
            byte[] bytes = Encoding.UTF8.GetBytes(fileText);
            var newBytes = new byte[bytes.Length];
            
            // TODO: step 3. Use MemoryStream instance to read from byte array (from step 2)
            using (var ms = new MemoryStream(bytes))
            {
                // TODO: step 4. Use MemoryStream instance (from step 3) to write it content in new byte array
                ms.Read(newBytes, 0, bytes.Length);
            }

            // TODO: step 5. Use Encoding class instance (from step 2) to create char array on byte array content
            char[] chars = Encoding.UTF8.GetChars(newBytes);

            // TODO: step 6. Use StreamWriter here to write char array content in new file
            using (var sw = new StreamWriter(destinationPath))
            {
                sw.Write(chars);
            }

            return bytes.Length;
        }

        #endregion

        #region TODO: Implement by block copy logic using FileStream buffer.

        public static int ByBlockCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            const int BUFFER_SIZE = 10000;
            int byteCount = 0;
            using (FileStream sourceFile = File.OpenRead(sourcePath),
                              destinationFile = File.OpenWrite(destinationPath))
            {
                var buffer = new byte[BUFFER_SIZE];
                while (true)
                {
                    int chunckLength = sourceFile.Read(buffer, 0, BUFFER_SIZE);

                    if (chunckLength == 0)
                    {
                        return byteCount;
                    }

                    destinationFile.Write(buffer, 0, chunckLength);

                    byteCount += chunckLength;
                }
            }
        }

        #endregion

        #region TODO: Implement by block copy logic using MemoryStream.

        public static int InMemoryByBlockCopy(string sourcePath, string destinationPath)
        {
            // TODO: Use InMemoryByByteCopy method's approach
            throw new NotImplementedException();
        }

        #endregion

        #region TODO: Implement by block copy logic using class-decorator BufferedStream.

        public static int BufferedCopy(string sourcePath, string destinationPath)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region TODO: Implement by line copy logic using FileStream and classes text-adapters StreamReader/StreamWriter

        public static int ByLineCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            using (FileStream sourceFile = File.OpenRead(sourcePath), 
                         destinationFile = File.OpenWrite(destinationPath))
            {
                using (var sourceReader = new StreamReader(sourceFile))
                using (var destinationWriter = new StreamWriter(destinationFile))
                {
                    int lineCount = 0;
                    while (true)
                    {
                        string line = sourceReader.ReadLine();
                        if (line == null)
                        {
                            return lineCount;
                        }

                        WriteToFile(sourceReader, destinationWriter, line);

                        lineCount++;
                    }
                }
            }

            void WriteToFile(StreamReader sourceReader, StreamWriter destinationWriter, string newLine)
            {
                if (!sourceReader.EndOfStream)
                {
                    destinationWriter.WriteLine(newLine);
                }
                else
                {
                    destinationWriter.Write(newLine);
                }
            }
        }

        #endregion

        #region TODO: Implement content comparison logic of two files 

        public static bool IsContentEquals(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            using (FileStream sourceFile = File.OpenRead(sourcePath), destinationFile = File.OpenRead(destinationPath))
            {
                using (var sourceReader = new StreamReader(sourceFile))
                using (var destinationReader = new StreamReader(destinationFile))
                {
                    while (true)
                    {
                        string sourceLine = sourceReader.ReadLine();
                        string destinationLine = destinationReader.ReadLine();

                        if (sourceLine != destinationLine)
                        {
                            return false;
                        }

                        // Both lines are null.
                        if (sourceLine == null)
                        {
                            return true;
                        }
                    }
                }
            }
        }

        #endregion

        #endregion

        #region Private members

        #region TODO: Implement validation logic

        /// <summary>
        /// Validates inputs.
        /// </summary>
        /// <param name="sourcePath">
        /// Source file path.
        /// </param>
        /// <param name="destinationPath">
        /// Destination file path.
        /// </param>
        /// <exception cref="FileNotFoundException">
        /// Thrown if files located in the sourcePath or destinationPath does not exist.
        /// </exception>
        private static void InputValidation(string sourcePath, string destinationPath)
        {
            if (sourcePath == null)
            {
                throw new ArgumentNullException(nameof(sourcePath));
            }

            if (destinationPath == null)
            {
                throw new ArgumentNullException(nameof(destinationPath));
            }

            if (!File.Exists(sourcePath))
            {
                throw new FileNotFoundException($"File {sourcePath} wasn't found.", sourcePath);
            }

            if (!File.Exists(destinationPath))
            {
                throw new FileNotFoundException($"File {destinationPath} wasn't found.", destinationPath);
            }
        }

        #endregion

        #endregion

    }
}
