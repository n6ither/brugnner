using System;
using System.Runtime.Serialization;

namespace Brugnner.API.Core.Exceptions
{
    /// <summary>
    /// Represents an unexpected business behavior.
    /// </summary>
    [Serializable]
    public class BusinessException : Exception
    {
        /// <summary>
        /// Creates a new instance of <see cref="BusinessException"/>.
        /// </summary>
        public BusinessException()
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="BusinessException"/>.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public BusinessException(string message) : base(message)
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="BusinessException"/>.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public BusinessException(string message, Exception inner) : base(message, inner)
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="BusinessException"/>.
        /// </summary>
        /// <param name="info">Serialization information.</param>
        /// <param name="context">Streaming context.</param>
        protected BusinessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
