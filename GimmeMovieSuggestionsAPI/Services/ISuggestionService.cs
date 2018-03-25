using GimmeMovieSuggestionsAPI.Integrations.DTOs;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GimmeMovieSuggestionsAPI.Services
{
    public interface ISuggestionService
    {
        List<MovieDTO> ProccessSuggestionRequest(SuggestionRequest req);
        MovieDTO ProccessImFeelingLuckyRequest(SuggestionRequest req);
    }
}
