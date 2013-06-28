using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace DapperDemo
{
    public class DatabaseSetupHelper
	{
        private const string DropTemplate = @"
if exists(select * from sysdatabases where name = '{0}')
begin
	alter database [{0}] set offline with rollback immediate 
	alter database [{0}] set online 
	drop database [{0}] 
end
";

        private const string CreateTemplate = @"
if not exists(select * from sysdatabases where name = '{0}')
begin
	create database [{0}]
end	
";
        /// <summary>
        /// Creates a database on the SQL Server specified by the "setup" connection string
        /// </summary>
        /// <param name="name"></param>
        public static void CreateDatabase(string name)
        {
            SetupDatabase(name, false, true);
        }

        /// <summary>
        /// Recreates a database on the SQL Server specified by the "setup" connection string. If a database
        /// exists it will be dropped, then created
        /// </summary>
        /// <param name="name"></param>
		public static void RecreateDatabase(string name)
		{
            SetupDatabase(name,true, true);
		}

        private static void SetupDatabase(string name, bool drop, bool create)
        {
            var commands = new StringBuilder();
            if (drop)
            {
                commands.AppendFormat(DropTemplate, name);
            }
            if (create)
            {
                commands.AppendFormat(CreateTemplate, name);
            }
            ExecuteSql(commands.ToString(), name);
		}

		private static void ExecuteSql(string commandText, string databaseName)
		{
		    var connectionString = ConfigurationManager.ConnectionStrings["setup"].ConnectionString;
			try
			{
				using (var connection = new SqlConnection(connectionString))
				{
					connection.Open();
					using (var command = new SqlCommand(commandText, connection))
					{
						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception e)
			{
                throw new Exception(string.Format("An error occurred while setting up database '{0}'", databaseName), e);
			}
		}
	}
}