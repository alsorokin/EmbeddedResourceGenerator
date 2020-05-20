using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace EmbeddedResourceGenerator
{
    class Program
    {
        const string DefaultResultFileName = "result.txt";

        static void Main(string[] args)
        {
            string resultFile = DefaultResultFileName;
            string root = Directory.GetCurrentDirectory();
            if (args.Length > 0 && !string.IsNullOrEmpty(args[0]))
            {
                root = args[0];
                if (!Directory.Exists(root))
                {
                    Console.Error.WriteLine($"Could not find directory: {root}");
                    Environment.Exit(1);
                }
            }

            Console.WriteLine("Scanning directory:");
            Console.WriteLine(root);
            var files = Directory.GetFiles(root, "*", SearchOption.AllDirectories);
            if (files.Count () == 0)
            {
                Console.Error.WriteLine("Could not find any files.");
                Environment.Exit(1);
            }

            Console.WriteLine();
            Console.WriteLine("Found files:");
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }

            var result = string.Empty;
            foreach (var file in files)
            {
                result += $"<EmbeddedResource Include=\"{file}\">" + Environment.NewLine;
                result += $"    <LogicalName>{file}</LogicalName>" + Environment.NewLine;
                result += "</EmbeddedResource>"                    + Environment.NewLine;
            }

            if (File.Exists(resultFile))
            {
                Console.WriteLine("Result file already exists. Removing...");
                File.Delete(resultFile);
            }

            Console.WriteLine($"Writing result to file: {resultFile}");
            File.WriteAllText(resultFile, result);

            Console.WriteLine();
        }
    }
}
