using System;
using System.Collections.Generic;
using CommandLine;

using Sample;

namespace DreamViewerCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(string.Compare("H", "h", ignoreCase: true));

            /*CommandLine.Parser.Default.ParseArguments<Options>(args)
    .WithParsed<Options>(opts => sample.RunOptionsAndReturnExitCode(opts))
    .WithNotParsed<Options>((errs) => sample.HandleParseError(errs));
    */

            //test args input
            args = new string[] { "sql", "-a", "init" };

            CommandLine.Parser.Default.ParseArguments<AddOptions, CommitOptions, SqlOptions>(args)
    .MapResult(
      (AddOptions opts) => sample.RunAddAndReturnExitCode(opts),
      (CommitOptions opts) => sample.RunCommitAndReturnExitCode(opts),
      (SqlOptions opts) => sample.RunSqlAndReturnExitCode(opts),
      errs => 1);
            //foreach (string item in args)
            //{

            //	if (string.Compare(item, "opt", ignoreCase: true) == 0)
            //	{
            //		Console.WriteLine("opt option");
            //	}
            //}
            Console.ReadKey(true); //Pause
        }

    }

}