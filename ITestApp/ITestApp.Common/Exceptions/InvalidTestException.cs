using System;
using System.Collections.Generic;
using System.Text;

namespace ITestApp.Common.Exceptions
{
    public class InvalidTestException : Exception
    {
        public InvalidTestException()
        {
        }

        public InvalidTestException(string message) : base(message)
        {
        }

        public InvalidTestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
