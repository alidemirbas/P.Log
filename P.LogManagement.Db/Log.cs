using System;

namespace P.LogManagement.Db
{
    public class Log : ILog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public string Namespace { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public bool Success { get; set; }

    }
}
