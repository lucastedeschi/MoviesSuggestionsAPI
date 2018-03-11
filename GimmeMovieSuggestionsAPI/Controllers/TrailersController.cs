using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GimmeMovieSuggestionsAPI.Integrations;
using GimmeMovieSuggestionsAPI.Integrations.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GimmeMovieSuggestionsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TrailersController : Controller
    {
        [HttpGet(Name = "GetTrailers")]
        public TrailerDTO Get([FromQuery(Name = "movieName")] string movieName)
        {
            return YouTubeIntegration.GetTrailer(movieName);
        }
    }
}
