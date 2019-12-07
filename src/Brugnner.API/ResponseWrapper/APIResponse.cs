using System.Net;
using System.Runtime.Serialization;

namespace Brugnner.API.ResponseWrapper
{
    /// <summary>
    /// Encapsulate the response of an API endpoint to maintain a uniform result.
    /// </summary>
    [DataContract]
    public class APIResponse
    {
        /// <summary>
        /// The status code of the response.
        /// </summary>
        [DataMember(Name = "statusCode")]
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// A description about the response.
        /// </summary>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// An <see cref="APIError"/> related to the response.
        /// </summary>
        [DataMember(Name = "error", EmitDefaultValue = false)]
        public APIError Error { get; set; }

        /// <summary>
        /// A result related to the response.
        /// </summary>
        [DataMember(Name = "result", EmitDefaultValue = false)]
        public object Result { get; set; }

        /// <summary>
        /// Creates an instance of an <see cref="APIResponse"/>.
        /// </summary>
        /// <param name="statusCode">The status code of the response.</param>
        /// <param name="description">A description about the response.</param>
        /// <param name="result">A  result related to the response.</param>
        /// <param name="error">An <see cref="APIError"/> related to the response.</param>
        public APIResponse(HttpStatusCode statusCode, string description = null, object result = null, APIError error = null)
        {
            StatusCode = statusCode;
            Description = description;
            Result = result;
            Error = error;
        }
    }
}
