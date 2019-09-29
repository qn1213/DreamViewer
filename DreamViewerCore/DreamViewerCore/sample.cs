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

                    //connection.Close();
                    break;
                default:
                    break;
            }
            return 1;
            //throw new NotImplementedException();
        }

        internal static int RunAddAndReturnExitCode(AddOptions opts)
        {
            Console.WriteLine(opts.Offset);
            Console.WriteLine(opts.Offset2);
            return 1;
        }




        #endregion
    }

    #region OPTIONS

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


    [Verb("sql", HelpText = "SQL Query options")]
    class SqlOptions
    {
        [Option('a', "action", Required = true, HelpText = "Select DB Actions\ninit - initialize or reset DB to default empty")]
        public string sqlaction { get; set; }
        //clone options here
        [Option("stdin",
          Default = false,
          HelpText = "xRead from stdin")]
        public bool stdin { get; set; }
        [Option("stdin2",
          Default = false,
          HelpText = "xRead from stdin")]
        public bool stdin2 { get; set; }
    }
    #endregion
}
