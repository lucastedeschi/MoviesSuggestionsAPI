﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GimmeMovieSuggestionsAPI.Integrations;
using GimmeMovieSuggestionsAPI.Integrations.DTOs;
using GimmeMovieSuggestionsAPI.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace GimmeMovieSuggestionsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SuggestionsController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ISuggestionService _suggestionService;

        public SuggestionsController(
            IHostingEnvironment hostingEnvironment,
            ISuggestionService suggestionService)
        {
            _hostingEnvironment = hostingEnvironment;
            _suggestionService = suggestionService;
        }

        [HttpGet(Name = "GetSuggestions")]
        public List<MovieDTO> Get([FromQuery(Name = "userEmail")] string userEmail,
            [FromQuery(Name = "audio")] string audio)
        {
            var req = new SuggestionRequest()
            {
                UserEmail = userEmail,
                Audio = audio
            };

            return _suggestionService.ProccessSuggestionRequest(req);
        }
    }
}
