using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;

namespace Brugnner.API.ResponseWrapper
{
    /// <summary>
    /// Represents an error that occured during the API excution.
    /// </summary>
    [DataContract]
    public class APIException : Exception
    {
        /// <summary>
        /// Represents an <see cref="HttpStatusCode"/>.
        /// </summary>
        [DataMember(Name = "statusCode")]
        public int StatusCode { get; set; }

        /// <summary>
        /// A list of <see cref="APIValidationError"/>.
        /// </summary>
        [DataMember(Name = "errors")]
        public IEnumerable<APIValidationError> Errors { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="APIException"/>.
        /// </summary>
        /// <param name="ex">Exception.</param>
        /// <param name="statusCode"><see cref="HttpStatusCode"/>.</param>
        public APIException(Exception ex, int statusCode = 500) : base(ex.Message)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Creates a new instance of <see cref="APIException"/>.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="statusCode"><see cref="HttpStatusCode"/>.</param>
        /// <param name="errors">A list of <see cref="APIValidationError"/>.</param>
        public APIException(string message, int statusCode = 500, IEnumerable<APIValidationError> errors = null) : base(message)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
    }
}
