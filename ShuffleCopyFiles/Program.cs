using System;

namespace ShuffleCopyFiles
{
    class Program
    {
        static void Main(String[] args)
        {
            Int32 FilesPerFolder = 256;
            FilesShuffler FileShufflerObj;
            if (args.Length == 3)
            {
                if (Int32.TryParse(args[2], out Int32 temp))
                {
                    FilesPerFolder = temp;
                    FileShufflerObj = new FilesShuffler(args[0], args[1], FilesPerFolder);
                    FileShufflerObj.CopyFiles();
                }
                else
                {
                    FileShufflerObj = new FilesShuffler(args[0], args[1], FilesPerFolder);
                    FileShufflerObj.CopyFiles();
                }
            }
            else
            {
                Console.WriteLine("Usage");
                Console.WriteLine("FilesShuffler.exe " + "\"" + "SourceFolder" + "\"" + " " + "\"" + "DestinationFolder " + "\"" + "128" + "\"");
                Console.WriteLine("FilesShuffler.exe " + "\"" + "SourceFolder" + "\"" + " " + "\"" + "DestinationFolder will use 256 files per folder");
            }
            Console.ReadKey();
        }
    }
}
