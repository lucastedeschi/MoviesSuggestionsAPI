using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GimmeMovieSuggestionsAPI.Integrations.DTOs;
using GimmeMovieSuggestionsAPI.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GimmeMovieSuggestionsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class LuckyController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ISuggestionService _suggestionService;

        public LuckyController(
            IHostingEnvironment hostingEnvironment,
            ISuggestionService suggestionService)
        {
            _hostingEnvironment = hostingEnvironment;
            _suggestionService = suggestionService;
        }

        [HttpGet(Name = "GetLucky")]
        public MovieDTO Get([FromQuery(Name = "userEmail")] string userEmail,
            [FromQuery(Name = "time")] string time)
        {
            var req = new SuggestionRequest()
            {
                UserEmail = userEmail,
                Time = time
            };

            return _suggestionService.ProccessImFeelingLuckyRequest(req);
        }
    }
}