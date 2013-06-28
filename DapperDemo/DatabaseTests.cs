using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using Dapper;

namespace DapperDemo
{
    [TestFixture, Category("Database Test")]
    public abstract class DatabaseTests
    {
        private static bool setupComplete;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            SetupDatabase();
        }
        
        [SetUp]
        public void SetUp()
        {
            ResetDataInDatabase();
            TestConnection = OpenConnection();
        }

        [TearDown]
        public void Teardown()
        {
            TestConnection.Dispose();
        }

        /// <summary>
        /// SQLConnection made available to each executing test
        /// </summary>
        protected SqlConnection TestConnection { get; private set; }

        private static void SetupDatabase()
        {
            if (!setupComplete)
            {
                // Create database with schema used by tests
                DatabaseSetupHelper.RecreateDatabase("DapperDemo");
                const string createSchemaCommand = @"
create table Dog (
    Id int primary key not null identity(1,1),
    Name varchar(50) not null, 
    Age int not null, 
    OwnerName varchar(50)
)";
                using (var connection = OpenConnection())
                {
                    connection.Execute(createSchemaCommand);
                }
                setupComplete = true;
            }
        }

        // Resets all data in test database, leaving it in a known state for next test
        public static void ResetDataInDatabase()
        {
            const string resetCommand = @"
delete from dbo.Dog
DBCC CHECKIDENT (Dog, reseed, 0)
insert Dog
values ('Rover', '1', 'Frank'),
('Bosun', '2', 'Dave'),
('Fifi', '3', 'Leonard')
";
            using (var connection = OpenConnection())
            {
                connection.Execute(resetCommand);
            }
        }
        
        protected static SqlConnection OpenConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}