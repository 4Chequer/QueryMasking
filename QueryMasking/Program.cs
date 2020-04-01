using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySqlConnector;
using QueryMasking.Maskers;

namespace QueryMasking
{
    class Program
    {
        // \d+ -> *
        // asd123 T=1
        // asd    T=2~3

        static void Main(string[] args)
        {
            Console.Write("비번:");

            var builder = new MySqlConnectionStringBuilder
            {
                Server = "127.0.0.1",
                UserID = "root",
                Port = 3306,
                Password = Console.ReadLine(),
                Database = "maskingtest",
            };

            var connection = new MySqlConnection(builder.ToString());

            connection.Open();

            InitalizeMaskingRule(connection);
            
            var dt = DateTime.Now;
            var result = MockingMask(connection, "friends");
            var duration = DateTime.Now - dt;

            Console.WriteLine(duration);
            Console.WriteLine(duration.TotalMilliseconds / 250000);
            Console.ReadLine();
        }

        static void InitalizeMaskingRule(MySqlConnection connection)
        {
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM masking_rule";

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string dbName = reader.GetString(3);
                    string tableName = reader.GetString(4);
                    string columnName = reader.GetString(5);
                    string maskingType = reader.GetString(6);

                    MaskingTool.MaskingRules.Add(new MaskingRule()
                    {
                        DataBaseName = dbName,
                        TableName = tableName,
                        FieldName = columnName,
                        MaskingType = maskingType,
                    });
                }
            }
        }

        static void RunMasking(object[,] data, string schemaName, string tableName, string[] columnNames)
        {
            int rowCount = data.GetLength(0);
            int columnCount = data.GetLength(1);

            Parallel.For(0, rowCount, row =>
            {
                for (int column = 0; column < columnCount; column++)
                {
                    var value = data[row, column];

                    if (value is string s)
                    {
                        data[row, column] = MaskingTool.Mask(s, schemaName, tableName, columnNames[column]);
                    }
                }
            });
        }

        static List<Dictionary<string, object>> MockingMask(MySqlConnection connection, string tableName)
        {
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM " + tableName;

            var result = new List<Dictionary<string, object>>();

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                DataTable schemaTable = reader.GetSchemaTable();

                var schema = (string)schemaTable.Rows[0][SchemaTableColumn.BaseSchemaName];
                var table = (string)schemaTable.Rows[0][SchemaTableColumn.BaseTableName];
                var columnNames = new string[reader.FieldCount];

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columnNames[i] = reader.GetName(i);
                }

                var bufferSize = 20000;
                var buffer = new object[bufferSize, reader.FieldCount];
                int index = 0;

                while (reader.Read())
                {
                    if (++index >= bufferSize)
                    {
                        RunMasking(buffer, schema, table, columnNames);
                        index = 0;
                    }

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        buffer[index, i] = reader.GetValue(i);
                    }
                }

                if (index > 0)
                {
                    RunMasking(buffer, schema, table, columnNames);
                    // 데이터 합치기는 알아서 잘
                }











                //// linear (CPU.core = 1)
                //while (reader.Read())
                //{
                //    var row = new Dictionary<string, object>();

                //    for (int i = 0; i < reader.FieldCount; i++)
                //    {
                //        string name = reader.GetName(i);
                //        object value = reader.GetValue(i);

                //        if (value is string s)
                //        {
                //            row[name] = MaskingTool.Mask(s, schema, table, name);
                //        }
                //        else
                //        {
                //            row[name] = value;
                //        }
                //    }
                //    result.Add(row);
                //}
            }

            return result;
        }
    }
}
