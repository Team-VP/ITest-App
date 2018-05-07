using System;

namespace ITestApp.Common.Exceptions
{
    public class TestNotFoundException : Exception
    {
        public TestNotFoundException()
        {
        }

        public TestNotFoundException(string message) : base(message)
        {
        }

        public TestNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
