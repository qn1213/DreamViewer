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
            //string argstring = "sql -q insert into Tartist values ('napata')";
            //args = argstring.Split(' ',StringSplitOptions.RemoveEmptyEntries);
            //args = new string[] { "sql","-q", "insert into TType values ('Manga');","-v" };
            //args = new string[] { "sql", "--create", "-v" };
            args = new string[] { "sql", "-r", "select * from TArtist", "-v" };
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
            try
            {
                CommandLine.Parser.Default.ParseArguments<AddOptions, SqlOptions>(args)
    .MapResult(
      (SqlOptions opts) => sample.RunSqlAndReturnExitCode(opts),
      (AddOptions opts) => sample.RunAddAndReturnExitCode(opts),
      errs => 1);
            }
            catch (Exception e)
            {
                Console.WriteLine("StackTrace : " + e.StackTrace);
                //throw;
            }


///////////////////////////DEBUG
#if DEBUG
            //Console.ReadKey(true); //Pause
#endif
////////////////////////////////
        }

    }

}