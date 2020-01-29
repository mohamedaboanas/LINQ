using System;
using System.IO;
using System.Linq;

namespace Introduction
{
    internal class Program
    {
        private static void Main()
        {
            const string path = @"c:\windows";
            ShowLargeFileWithOutLinq(path);

            Console.WriteLine("*******");

            ShowLargeFileWithLinq(path);
        }

        private static void ShowLargeFileWithLinq(string path)
        {
            //var query = from file in new DirectoryInfo(path).GetFiles()
            //    orderby file.Length descending
            //    select file;

            //foreach (var file in query.Take(5))
            //{
            //    Console.WriteLine($"{file.Name,-20} : {file.Length,10:N0}");
            //}

            // lambda expretions

            var query = new DirectoryInfo(path).GetFiles()
                .OrderByDescending(f => f.Length)
                .Take(5);

            foreach (var file in query)
            {
                Console.WriteLine($"{file.Name,-20} : {file.Length,10:N0}");
            }
        }

        private static void ShowLargeFileWithOutLinq(string path)
        {
            var directory = new DirectoryInfo(path);
            var files = directory.GetFiles();

            Array.Sort(files, new FileInfoComparer());

            for (var i = 0; i < 5; i++)
            {
                var file = files[i];
                Console.WriteLine($"{file.Name,-20} : {file.Length,10:N0}");
            }
            //foreach (var file in files)
            //{
            //    Console.WriteLine($"{file.Name} : {file.Length}");
            //}
        }
    }
}
