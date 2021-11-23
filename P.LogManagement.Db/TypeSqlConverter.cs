using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace P.LogManagement.Db
{
    public class TypeSqlConverter
    {
        static TypeSqlConverter()
        {
            _typeMap = new Dictionary<Type, string>();

            _typeMap[typeof(string)] = "nvarchar(1000)";//todo hos degil
            _typeMap[typeof(char[])] = "nvarchar(1000)";
            _typeMap[typeof(byte)] = "tinyint";
            _typeMap[typeof(short)] = "smallint";
            _typeMap[typeof(int)] = "int";
            _typeMap[typeof(long)] = "bigint";
            _typeMap[typeof(byte[])] = "image";
            _typeMap[typeof(bool)] = "bit";
            _typeMap[typeof(DateTime)] = "datetime2";
            _typeMap[typeof(DateTimeOffset)] = "DateTimeOffset";
            _typeMap[typeof(decimal)] = "Money";
            _typeMap[typeof(float)] = "real";
            _typeMap[typeof(double)] = "float";
            _typeMap[typeof(TimeSpan)] = "time";
        }

        private static Dictionary<Type, string> _typeMap;

        // Non-generic argument-based method
        public static string GetDbType(Type giveType)
        {
            // Allow nullable types to be handled
            giveType = Nullable.GetUnderlyingType(giveType) ?? giveType;

            if (_typeMap.ContainsKey(giveType))
            {
                return _typeMap[giveType];
            }

            throw new ArgumentException($"{giveType.FullName} is not a supported .NET class");
        }

        // Generic version
        public static string GetDbType<T>()
        {
            return GetDbType(typeof(T));
        }
    }
}
