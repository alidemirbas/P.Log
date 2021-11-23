using System;

namespace P.LogManagement
{
    public interface ILog
    {
        string Title { get; set; }
        string Description { get; set; }
        DateTime DateTime { get; set; }
        string Namespace { get; set; }
        string ClassName { get; set; }
        string MethodName { get; set; }
        bool Success { get; set; }
    }
}
