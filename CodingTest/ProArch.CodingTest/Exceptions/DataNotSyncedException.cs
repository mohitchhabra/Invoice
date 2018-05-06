using System;
using System.Runtime.Serialization;

namespace ProArch.CodingTest.Summary
{
    [Serializable]
    public class DataNotSyncedException : Exception
    {
        public DataNotSyncedException()
        {
        }

        public DataNotSyncedException(string message) : base(message)
        {
        }

        public DataNotSyncedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DataNotSyncedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}