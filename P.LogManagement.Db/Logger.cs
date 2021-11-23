using System;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace P.LogManagement.Db
{
    public abstract class Logger : ILogger
    {
        protected abstract string ConnectionString { get; }

        public virtual void Log(ILog log)
        {
            var logType = log.GetType();
            var propInfos = logType.GetProperties();
            PropertyInfo propInfo;

            var tableName = logType.Name;
            var columnDefinitions = new StringBuilder();

            using (var connection = new SqlConnection(ConnectionString))//todo
            {
                connection.Open();

                string tableExistCmd = $"if exists(select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME='{tableName}') select 1 else select 0";

                using (SqlCommand command = new SqlCommand(tableExistCmd, connection))
                {
                    bool isExist = System.Convert.ToBoolean(command.ExecuteScalar());

                    if (!isExist)
                    {
                        for (int i = 0; i < propInfos.Length; i++)
                        {
                            propInfo = propInfos[i];

                            if (!propInfo.PropertyType.IsValueType && propInfo.PropertyType != typeof(string))
                                throw new Exception();//todo log'un komplex bir nesne barindirmasina gerek yok

                            columnDefinitions.AppendFormat("{0} {1} not null", propInfo.Name, TypeSqlConverter.GetDbType(propInfo.PropertyType));
                            if (propInfo.Name == "Id")
                                columnDefinitions.Append(" primary key identity");

                            if (i != propInfos.Length - 1)
                                columnDefinitions.Append(',');
                        }

                        var createTableCmd = $@"create table {tableName} ({columnDefinitions.ToString()})";

                        command.CommandText = createTableCmd;
                        int rowAffected = command.ExecuteNonQuery();
                    }
                }

                var columnNames = new StringBuilder();
                var sqlParameterNames = new StringBuilder();

                for (int i = 0; i < propInfos.Length; i++)
                {
                    propInfo = propInfos[i];

                    if (propInfo.Name == "Id")
                        continue;

                    columnNames.Append(propInfo.Name);
                    sqlParameterNames.AppendFormat("@{0}", propInfo.Name);

                    if (i != propInfos.Length - 1)
                    {
                        columnNames.Append(',');
                        sqlParameterNames.Append(',');
                    }
                }

                var insertCmd = $"insert into {tableName} ({columnNames.ToString()}) values ({sqlParameterNames})";

                using (SqlCommand command = new SqlCommand(insertCmd, connection))
                {
                    command.Parameters.Clear();

                    for (int i = 0; i < propInfos.Length; i++)
                    {
                        propInfo = propInfos[i];

                        if (propInfo.Name == "Id")
                            continue;

                        command.Parameters.AddWithValue($"@{propInfo.Name}", propInfo.GetValue(log));
                    }

                    int rowsAffected = command.ExecuteNonQuery();
                }
            };
        }
    }
}
