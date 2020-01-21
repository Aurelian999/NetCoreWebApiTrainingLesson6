using System;
using System.Runtime.Serialization;

namespace StudentsAPIClient.Services.Exceptions
{
    [Serializable]
    internal class AccessTokenException : Exception
    {
        public AccessTokenException()
        {
        }

        public AccessTokenException(string message) : base(message)
        {
        }

        public AccessTokenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccessTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}