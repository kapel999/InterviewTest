using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace InterviewTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListController : ControllerBase
    {
        public ListController()
        {
        }

        [Route("DeleteRecord/{name}")]
        [HttpPost]
        public void DeleteRecord(string name)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder() { DataSource = "./SqliteDB.db" };
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var queryCmd = connection.CreateCommand();
                queryCmd.CommandText = @"DELETE FROM Employees WHERE name = '" + name + "'";
                queryCmd.ExecuteReader();
            }
        }

        [Route("AddRecord")]
        [HttpPost]
        public void AddRecord(string newName, string newValue)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder() { DataSource = "./SqliteDB.db" };
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var querycmd = connection.CreateCommand();
                querycmd.CommandText = @"INSERT INTO employees (name, value) VALUES ('" + newName + "', '" + newValue + "')";
                querycmd.ExecuteReader();
            }
        }

        [Route("EditRecord")]
        [HttpPost]
        public void EditRecord(string oldName, string newName, string oldValue, string newValue)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder() { DataSource = "./SqliteDB.db" };
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var querycmd = connection.CreateCommand();
                querycmd.CommandText = @"UPDATE employees SET name = '" + newName + "', value = '" + newValue + "' WHERE name = '" + oldName + "' AND value = '" + oldValue + "'";
                querycmd.ExecuteReader();
            }
        }
        
        [Route("SQLQueryOne")]
        [HttpPost]
        public void SQLQueryOne()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder() { DataSource = "./SqliteDB.db" };
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var querycmd = connection.CreateCommand();
                querycmd.CommandText = @"UPDATE Employees ";
                querycmd.CommandText += @"SET value = (CASE ";
                querycmd.CommandText += @"WHEN name LIKE 'E%' THEN value + 1 ";
                querycmd.CommandText += @"WHEN name LIKE 'G%' THEN value + 10 ";
                querycmd.CommandText += @"ELSE value + 100 END)";
                querycmd.ExecuteReader();
            }
        }
        
        [Route("SQLQueryTwo")]
        [HttpPost]
        public void SQLQueryTwo()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder() { DataSource = "./SqliteDB.db" };
            var queryTwoResult = "";
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var queryCmd = connection.CreateCommand();
                queryCmd.CommandText = @"SELECT CASE WHEN SUM (value) >= 11171 THEN SUM (value) ELSE 0 END ";
                queryCmd.CommandText += @"FROM employees ";
                queryCmd.CommandText += @"WHERE name LIKE 'A%' OR name LIKE 'B%' OR name LIKE 'C%'";

                using (var reader = queryCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        queryTwoResult = reader.GetString(0);
                        if (queryTwoResult != "0")
                        {
                            var queryCmdTwo = connection.CreateCommand();
                            queryCmdTwo.CommandText = @"INSERT INTO employees (name, value) VALUES ('Total', '" + queryTwoResult + "')";
                            queryCmdTwo.ExecuteReader();
                        }
                    }
                }
            }
        }


        /*
         * List API methods goe here
         * */
    }
}
