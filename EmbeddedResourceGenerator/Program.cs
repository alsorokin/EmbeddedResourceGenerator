using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace EmbeddedResourceGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string root = Directory.GetCurrentDirectory();
            if (args.Length > 0 && !string.IsNullOrEmpty(args[0]))
            {
                root = args[0];
                if (!Directory.Exists(root))
                {
                    Console.Error.Write($"Cannot find directory: {root}");
                    Environment.Exit(1);
                }
            }

            Console.WriteLine($"Scanning {root}");
            var files = ScanDir(root);
        }

        static IEnumerable<string> ScanDir(string dir)
        {
            Console.WriteLine($"Starting directory scan: {dir}");
            List<string> result = new List<string> ();

            foreach (var file in Directory.GetFiles(dir))
            {
                Console.WriteLine($"Found file: {file}");
                result.Add(file);
            }
            foreach (var subDir in Directory.GetDirectories(dir))
            {
                Console.WriteLine($"Found directory: {dir}");
                result.AddRange(ScanDir(subDir));
            }

            return result;
        }
    }
}
