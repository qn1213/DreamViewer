using System;
using System.Collections.Generic;
using CommandLine;

using Sample;
//using DreamViewerCore.test.AttrTest;

namespace DreamViewerCore
{
    class Program
    {
        static void Main(string[] args)
        {

///////////////////////////DEBUG
#if DEBUG
            //test args input
            args = new string[] { "version" };
            Console.Write("executed arguments : ");
            foreach (var item in args)
            {
                Console.Write(item);
                Console.Write(",");
            }
            Console.WriteLine();

            //AttrTest.Parse<AddOptions>("str1");
#endif
            ////////////////////////////////



            //start parsing argument
            CommandLine.Parser.Default.ParseArguments<AddOptions, SqlOptions>(args)
    .MapResult(
      (SqlOptions opts) => sample.RunSqlAndReturnExitCode(opts),
      (AddOptions opts) => sample.RunAddAndReturnExitCode(opts),
      errs => 1);


///////////////////////////DEBUG
#if DEBUG
            Console.ReadKey(true); //Pause
#endif
////////////////////////////////
        }

    }

}