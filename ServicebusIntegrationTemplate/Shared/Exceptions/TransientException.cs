using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;


namespace ServiceBusIntegrationTemplate.Shared.Exceptions
{
    public class TransientException : Exception, ISerializable
    {
        public TransientException()
        {
        }


        public TransientException(string message) : base(message)
        {
        }

        public TransientException(string message, Exception innerException) : base(message, innerException)
        {
        }


        protected TransientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }


    public class NonTransientException : Exception, ISerializable
    {
        public NonTransientException()
        {
        }


        public NonTransientException(string message) : base(message)
        {
        }


        public NonTransientException(string message, Exception innerException) : base(message, innerException)
        {
        }


        protected NonTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    public class DeferredException : Exception, ISerializable
    {
        public DeferredException()
        {
        }


        public DeferredException(string message) : base(message)
        {
        }


        public DeferredException(string message, Exception innerException) : base(message, innerException)
        {
        }


        protected DeferredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}