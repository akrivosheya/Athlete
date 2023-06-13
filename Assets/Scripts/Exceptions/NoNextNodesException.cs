using System;

namespace Exceptions
{
    public class NoNextNodesException : Exception
    {
        public NoNextNodesException(string nodeName) : base(nodeName + " doesn't have proper next nodes.")
        {
        }
    }
}
