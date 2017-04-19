using System;
using System.Diagnostics;

namespace SnakeTheResurrection.Utilities
{
    public static class DebugHelper
    {
        [Conditional("DEBUG")]
        public static void OperationInfo(string objectName, string operationName, bool success)
        {
            Debug.WriteLine($"{DateTime.Now:HH:mm:ss}: {objectName} {operationName} {(success ? "succeeded" : "failed")}");
        }
    }
}
