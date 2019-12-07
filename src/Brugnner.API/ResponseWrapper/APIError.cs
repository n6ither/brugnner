using Brugnner.API.Core.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Brugnner.API.ResponseWrapper
{
    /// <summary>
    /// An API error. It can hold business validation errors or more technical exception.
    /// </summary>
    [DataContract]
    public class APIError
    {
        /// <summary>
        /// Error message.
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Error details. In a development environment holds the call stack.
        /// </summary>
        [DataMember(Name = "details", EmitDefaultValue = false)]
        public string Details { get; set; }

        /// <summary>
        /// A list with the validations that weren't successfuly passed by the <see cref="APIResource"/>.
        /// </summary>
        [DataMember(Name = "validationErrors", EmitDefaultValue = false)]
        public IEnumerable<APIValidationError> ValidationErrors { get; set; }

        /// <summary>
        /// Creates a new instance of an API error.
        /// </summary>
        public APIError()
        {

        }

        /// <summary>
        /// Creates a new instance of an API error.
        /// </summary>
        /// <param name="message">Error message.</param>
        public APIError(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Creates a new instance of an API error.
        /// </summary>
        /// <param name="modelState">Model state associated with an <see cref="APIResource"/>.</param>
        public APIError(ModelStateDictionary modelState)
        {
            if (modelState != null && modelState.Any(m => m.Value.Errors.Count > 0))
            {
                Message = "Please correct the specified validation errors and try again.";
                ValidationErrors = modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(x => new APIValidationError(key, x.ErrorMessage)))
                .ToList();
            }
        }

        /// <summary>
        /// Creates a new instance of an API error.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="details">Error details.</param>
        public APIError(string message, string details) : this(message)
        {
            Details = details.Trim();
        }

        /// <summary>
        /// Creates a new instance of an API error.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="validationErrors">A list with the validations that weren't successfuly passed by the <see cref="APIResource"/>.</param>
        public APIError(string message, IEnumerable<APIValidationError> validationErrors) : this(message)
        {
            ValidationErrors = validationErrors;
        }
    }
}
