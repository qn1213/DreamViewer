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
        public static DirectoryInfo dbPath { get; } = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\data");
        public static FileInfo dbFile { get; } = new FileInfo(dbPath + @"\t.sqlite");
        public static SQLiteConnection dbcon { get; } = new SQLiteConnection("Data Source=" + dbFile + @";foreign keys=true;");

        private static SQLiteConnection connection = new SQLiteConnection(dbcon);
        private static SQLiteTransaction transaction = null;

        #region OPTION_FUNCTIONS
        public static int RunSqlAndReturnExitCode(SqlOptions opts)
        {
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();

                if (opts.createDB)
                {
                    if (!dbPath.Exists) dbPath.Create();
                    if (!dbFile.Exists) SQLiteConnection.CreateFile(dbFile.ToString());

                    SQLiteCommand command = new SQLiteCommand(global::DreamViewerCore.Properties.Resources.CreateTables, connection, transaction);

                    int result = command.ExecuteNonQuery();
                    Console.WriteLine("DB init transaction success");


                }

                if (!(opts.queryText is null))
                {

                    SQLiteCommand command = new SQLiteCommand(opts.queryText, connection, transaction);
                    int result = command.ExecuteNonQuery();
                    Console.WriteLine("DB query transaction success");
                }

                if (!(opts.readText is null))
                {
                    SQLiteCommand command = new SQLiteCommand(opts.readText, connection, transaction);
                    SQLiteDataReader reader = command.ExecuteReader();
                    Console.WriteLine(reader.GetTableName(0));
                    while (reader.Read())
                    {
                        Console.WriteLine(reader[0]);
                    }

                    reader.Close();
                }

                transaction.Commit();
                Console.WriteLine("RESULT: Success");
            }
            catch (Exception e)
            {

                Console.WriteLine("Exception : " + e.Message + "\n");
                if (opts.verbose)
                {
                    Console.WriteLine("vException : " + e + "\n");

                    throw e;
                }
                transaction.Rollback();

            }
            finally
            {
                Console.WriteLine("query was : " + opts.queryText);
                transaction.Dispose();
                connection.Dispose();
                
            }




            //search
            //string sql = "select * from artists";
            //SQLiteCommand cmd = new SQLiteCommand(sql, connection);
            //SQLiteDataReader rdr = cmd.ExecuteReader();

            return 0;
            //throw new NotImplementedException();
        }

        internal static int RunAddAndReturnExitCode(AddOptions opts)
        {
            Console.WriteLine(opts.Offset);
            Console.WriteLine(opts.Offset2);
            return 0;
        }




        #endregion
    }

    #region OPTIONS

    [Verb("sql", HelpText = "SQL Query options")]
    class SqlOptions
    {
        [Option("create", HelpText = "Select DB Actions\ninit - initialize or reset DB to default empty", SetName = "test")]
        public bool createDB { get; set; }

        [Option('q', "query", HelpText = "input query text", SetName = "Query")]
        public string queryText { get; set; }

        [Option('r', "read", HelpText = "input query text", SetName = "Read")]
        public string readText { get; set; }

        //clone options here
        [Option('v', "verbose",
          Default = false,
          HelpText = "set true console output verbosely")]
        public bool verbose { get; set; }

    }


    [Verb("add", HelpText = "Test Sample")]
    class AddOptions
    {
        //normal options here
        [Option('r', "read", HelpText = "xInput files to be processed.")]
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


        [Value(2, MetaName = "offset", HelpText = "xFile offset.")]
        public long? Offset { get; set; }
        [Value(3, MetaName = "offset2", HelpText = "xFile offset.")]
        public long? Offset2 { get; set; }

    }



    #endregion
}
