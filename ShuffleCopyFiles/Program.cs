using System;

namespace ShuffleCopyFiles
{
    class Program
    {
        static void Main(String[] args)
        {
            FilesShuffler FileShufflerObj;
            if (args.Length == 3)
            {
                FileShufflerObj = new FilesShuffler(args[0], args[1]);
                if (args[2].ToLower().Equals("ovr"))
                {
                    FileShufflerObj.CopyFiles(true);
                }
                else
                {
                    Console.WriteLine("Usage");
                    Console.WriteLine("FilesShuffler.exe " + "\"" + "SourceFolder" + "\"" + " " + "\"" + "DestinationFolder " + "ovr");
                    Console.WriteLine("FilesShuffler.exe " + "\"" + "SourceFolder" + "\"" + " " + "\"" + "DestinationFolder");
                }
            }
            else if (args.Length == 2)
            {
                FileShufflerObj = new FilesShuffler(args[0], args[1]);
                FileShufflerObj.CopyFiles(false);
            }
            else
            {
                Console.WriteLine("Usage");
                Console.WriteLine("FilesShuffler.exe " + "\"" + "SourceFolder" + "\"" + " " + "\"" + "DestinationFolder " + "ovr");
                Console.WriteLine("FilesShuffler.exe " + "\"" + "SourceFolder" + "\"" + " " + "\"" + "DestinationFolder");
            }
            Console.ReadKey();
        }
    }
}