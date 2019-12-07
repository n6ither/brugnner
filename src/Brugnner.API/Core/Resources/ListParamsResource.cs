namespace Brugnner.API.Core.Resources
{
    /// <summary>
    /// Search and pagination parameters.
    /// </summary>
    public class ListParamsResource : APIResource
    {
        /// <summary>
        /// A string with filters in Dynamic LINQ syntax.
        /// </summary>
        public string Filters { get; set; }

        /// <summary>
        /// Field to order the results by.
        /// </summary>
        public string OrderByField { get; set; }

        /// <summary>
        /// Results order direction. Valid values are "asc" and "desc".
        /// </summary>
        public string OrderByDirection { get; set; }

        /// <summary>
        /// Indicates how many results will be skipped and not be part of the results.
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// Indicates how many results will be taken as part of the results.
        /// </summary>
        public int Take { get; set; }
    }
}
