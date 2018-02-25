using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GimmeMovieSuggestionsAPI.Integrations;
using GimmeMovieSuggestionsAPI.Integrations.DTOs;
using GimmeMovieSuggestionsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GimmeMovieSuggestionsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SuggestionsController : Controller
    {
        [HttpGet(Name = "Get")]
        public List<MovieDTO> Get([FromQuery(Name = "userEmail")] string userEmail,
            [FromQuery(Name = "audio")] string audio,
            [FromQuery(Name = "period")] string period)
        {
            var req = new SuggestionRequest()
            {
                UserEmail = userEmail,
                Audio = audio,
                Period = period
            };

            return SuggestionsService.ProccessSuggestionRequest(req);
        }
    }
}
