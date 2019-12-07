using Brugnner.API.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Brugnner.API.Controllers
{
    /// <summary>
    /// Base controller for the API.
    /// </summary>
    [EnableCors("CorsPolicy")]
    [ApiController()]
    [ServiceFilter(typeof(ValidationFilter))]
    [Authorize]
    public abstract class BrugnnerController : Controller
    {

    }
}
