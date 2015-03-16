using System;
using System.Diagnostics;
using System.IO;
using Memoling.Tools.WiktionaryMapper.Converters;
using Memoling.Tools.WiktionaryParser.Data;
using Memoling.Tools.WiktionaryParser.Input;
using Memoling.Tools.WiktionaryParser.Output;

namespace Memoling.Tools.WiktionaryMapper
{
    class Program
    {
        static Stopwatch _stopwatch;
        static int _processedEntry;

        const string Dump = @"D:\enwiktionary-20150102-pages-articles-multistream.xml";
        const string Output = @"D:\wiki.json";

        static void Init()
        {
            _stopwatch = new Stopwatch();
        }

        static void Main()
        {
            Init();

            var data = CreateDataProcessor();
            var inputProcessor = new InputProcessor(data);

            PrintImportStarted();

            var source = new StreamReader(Dump);

            var i = 0;

            using (var writer = new JsonWriter(new WordConverter(), new StreamWriter(Output)))
            {
                foreach (var result in inputProcessor.Process(source))
                {
                    // If failed to Parse
                    if (result == null)
                    {
                        continue;
                    }
                    if (result.Context.Title == "deal")
                    {
                        
                    }
                    writer.Process(result);
                    PrintProgress();
                    i++;
                }
            }

            try
            {
                PrintFinished();
            }
            catch (Exception ex)
            {
                PrintException(ex);
            }
        }

        static IDataProcessor CreateDataProcessor()
        {
            var config = DefaultDataProcessorConfig.Build();
            return new DataProcessor(config);
        }

        #region Printing & UI

        static void PrintImportStarted()
        {
            Console.WriteLine("Importing");
            _stopwatch.Start();
            _processedEntry = 0;
        }

        static void PrintProgress()
        {
            _processedEntry++;
            if (_processedEntry % 100 == 0)
            {
                Console.Write("\r{0} ", _processedEntry);
            }
        }

        static void PrintFinished()
        {
            Console.WriteLine();
            Console.WriteLine(_stopwatch.Elapsed.ToString());
            Console.WriteLine("Done");
            Console.Read();
        }

        static void PrintException(Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine(ex.ToString());
            Console.Read();
        }

        #endregion
    }
}
