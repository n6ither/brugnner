using Brugnner.API.Core.Resources;
using Newtonsoft.Json;

namespace Brugnner.API.ResponseWrapper
{
    /// <summary>
    /// Represents a validation error associated with an <see cref="APIResource"/>
    /// </summary>
    public class APIValidationError
    {
        /// <summary>
        /// The field that threw the error.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }

        /// <summary>
        /// Error message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Creates a new instance of an <see cref="APIValidationError"/>.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="message"></param>
        public APIValidationError(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }
}
