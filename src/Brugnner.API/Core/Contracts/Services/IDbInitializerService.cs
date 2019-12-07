namespace Brugnner.API.Core.Contracts.Services
{
    /// <summary>
    /// Represents an service tasked with the database initialization.
    /// </summary>
    public interface IDbInitializerService
    {
        /// <summary>
        /// Adds some data to the database.
        /// </summary>
        void SeedData();
    }
}
