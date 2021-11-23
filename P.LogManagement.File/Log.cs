using System;

namespace P.LogManagement.File
{
    public class Log : ILog
    {
        public string ClassName { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public string MethodName { get; set; }
        public string Namespace { get; set; }
        public bool Success { get; set; }
        public string Title { get; set; }
        
    }
}
