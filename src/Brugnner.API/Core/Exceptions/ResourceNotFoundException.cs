using System;
using System.Runtime.Serialization;

namespace Brugnner.API.Core.Exceptions
{
    /// <summary>
    /// Represents an error when trying to work with a non-existent resource.
    /// </summary>
    [Serializable]
    public class ResourceNotFoundException : Exception
    {
        /// <summary>
        /// Creates a new instance of <see cref="ResourceNotFoundException"/>.
        /// </summary>
        public ResourceNotFoundException()
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="ResourceNotFoundException"/>.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public ResourceNotFoundException(string message) : base(message)
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="ResourceNotFoundException"/>.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public ResourceNotFoundException(string message, Exception inner) : base(message, inner)
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="ResourceNotFoundException"/>.
        /// </summary>
        /// <param name="info">Serialization information.</param>
        /// <param name="context">Streaming context.</param>
        protected ResourceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
