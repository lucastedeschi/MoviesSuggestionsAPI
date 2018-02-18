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
    [Route("api/[controller]")]
    public class SuggestionsController : Controller
    {
        [HttpPost]
        public DiscoverDTO Post([FromBody] SuggestionRequest req)
        {
            return SuggestionsService.ProccessSuggestionRequest(req);
        }
    }
}
