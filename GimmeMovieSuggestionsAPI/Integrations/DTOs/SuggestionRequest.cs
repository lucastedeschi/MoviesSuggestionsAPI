using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GimmeMovieSuggestionsAPI.Integrations.DTOs
{
    public class SuggestionRequest
    {
        public string UserEmail { get; set; }
        public string Audio { get; set; }
        public string Period { get; set; }
    }
}
