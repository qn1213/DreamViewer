using CommandLine;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;
using System.Resources;

namespace Sample
{
    class sample
    {
        //DirectoryInfo dbPath = new DirectoryInfo(System.IO.Directory.GetCurrentDirectory() + @"\data");
        private static DirectoryInfo dbPath = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\data");
        private static SQLiteConnection connection = null;

        #region OPTION_FUNCTIONS
        public static int RunSqlAndReturnExitCode(SqlOptions opts)
        {
            switch (opts.sqlaction)
            {
                case "init":
                    
                    if (!dbPath.Exists) dbPath.Create();

                    if (!System.IO.File.Exists(dbPath + @"\t.sqlite"))
                    {
                        SQLiteConnection.CreateFile(dbPath + @"\t.sqlite");
                    }

                    connection = new SQLiteConnection("Data Source=" + dbPath + @"\t.sqlite");
                    connection.Open();

                    //search
                    //string sql = "select * from artists";

                    //SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                    //SQLiteDataReader rdr = cmd.ExecuteReader();
                    Console.WriteLine("DB init executed!!");

                    SQLiteCommand command = new SQLiteCommand(connection);

                    //string sqltxt = global::DreamViewerCore.Properties.Resources.CreateTables;
                    command.CommandText = global::DreamViewerCore.Properties.Resources.CreateTables;
                    int result = command.ExecuteNonQuery();
                    break;
                default:
                    break;
            }
            return 0;
            //throw new NotImplementedException();
        }

        public static int RunCommitAndReturnExitCode(CommitOptions opts)
        {
            Console.WriteLine("xCommited!!");
            return 0;
            //throw new NotImplementedException();
        }

        public static int RunAddAndReturnExitCode(AddOptions opts)
        {
            IEnumerable<string> enumerable = opts.InputFiles;
            IEnumerator<string> e = enumerable.GetEnumerator();

            while (e.MoveNext())
            {
                Console.WriteLine("x - " + e.Current);
            }

            Console.WriteLine("xstdin : " + opts.stdin);

            if (opts.Verbose)
            {
                Console.WriteLine($"xVerbose output enabled. Current Arguments: -v {opts.Verbose}");
                Console.WriteLine("xQuick Start Example! App is in Verbose mode!");
            }
            else
            {
                Console.WriteLine($"xCurrent Arguments: -v {opts.Verbose}");
                Console.WriteLine("xQuick Start Example!");
            }
            return 0;
            //throw new NotImplementedException();
        }



        public static void RunOptionsAndReturnExitCode(Options opts)
        {
            IEnumerable<string> enumerable = opts.InputFiles;
            IEnumerator<string> e = enumerable.GetEnumerator();

            while (e.MoveNext())
            {
                Console.WriteLine(e.Current);
            }

            Console.WriteLine(opts.stdin);

            if (opts.Verbose)
            {
                Console.WriteLine($"Verbose output enabled. Current Arguments: -v {opts.Verbose}");
                Console.WriteLine("Quick Start Example! App is in Verbose mode!");
            }
            else
            {
                Console.WriteLine($"Current Arguments: -v {opts.Verbose}");
                Console.WriteLine("Quick Start Example!");
            }
        }
        public static void HandleParseError(IEnumerable<Error> errs)
        {
        }
        #endregion
    }

    #region OPTIONS
    class Options
    {
        [Option('r', "read", Required = true, HelpText = "Input files to be processed.")]
        public IEnumerable<string> InputFiles { get; set; }

        // Omitting long name, defaults to name of property, ie "--verbose"
        [Option('v',
          Default = false,
          HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        [Option("stdin",
          Default = false,
          HelpText = "Read from stdin")]
        public bool stdin { get; set; }

        [Value(0, MetaName = "offset", HelpText = "File offset.")]
        public long? Offset { get; set; }
    }


    [Verb("add", HelpText = "Test Sample")]
    class AddOptions
    {
        //normal options here
        [Option('r', "read", Required = true, HelpText = "xInput files to be processed.")]
        public IEnumerable<string> InputFiles { get; set; }

        // Omitting long name, defaults to name of property, ie "--verbose"
        [Option('v',
          Default = false,
          HelpText = "xPrints all messages to standard output.")]
        public bool Verbose { get; set; }

        [Option("stdin",
          Default = false,
          HelpText = "xRead from stdin")]
        public bool stdin { get; set; }

        [Value(0, MetaName = "offset", HelpText = "xFile offset.")]
        public long? Offset { get; set; }
    }
    [Verb("commit", HelpText = "Test Sample")]
    class CommitOptions
    {
        //commit options here
    }
    [Verb("sql", HelpText = "SQL Query options")]
    class SqlOptions
    {
        [Option('a', "action", Required = true, HelpText = "Select DB Actions\ninit - initialize or reset DB to default empty")]
        public string sqlaction { get; set; }
        //clone options here
    }
    #endregion
}
