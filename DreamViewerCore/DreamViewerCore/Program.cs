using System;
using System.Collections.Generic;
using CommandLine;

namespace DreamViewerCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(string.Compare("H", "h", ignoreCase: true));

            CommandLine.Parser.Default.ParseArguments<Options>(args)
    .WithParsed<Options>(opts => RunOptionsAndReturnExitCode(opts))
    .WithNotParsed<Options>((errs) => HandleParseError(errs));

            //foreach (string item in args)
            //{

            //	if (string.Compare(item, "opt", ignoreCase: true) == 0)
            //	{
            //		Console.WriteLine("opt option");
            //	}
            //}
        }

        private static void HandleParseError(IEnumerable<Error> errs)
        {
        }

        private static void RunOptionsAndReturnExitCode(Options opts)
        {
            IEnumerable<string> enumerable = opts.InputFiles;
            IEnumerator<string> e = enumerable.GetEnumerator();

            while (e.MoveNext())
            {
                Console.WriteLine(e.Current);
            }

            Console.WriteLine(opts.Verbose);
            Console.WriteLine(opts.stdina);
        }
    }


    class Options
    {
        [Option('r', "read", Required = true, HelpText = "Input files to be processed.")]
        public IEnumerable<string> InputFiles { get; set; }

        // Omitting long name, defaults to name of property, ie "--verbose"
        [Option('v',
          Default = false,
          HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        public bool stdina;
        [Option("stdin",
          Default = true,
          HelpText = "Read from stdin")]
        public bool stdin { get { return stdina; } set { stdina = !value; } }

        [Value(0, MetaName = "offset", HelpText = "File offset.")]
        public long? Offset { get; set; }
    }
}