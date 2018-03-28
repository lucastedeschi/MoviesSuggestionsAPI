using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GimmeMovieSuggestionsAPI.Integrations.DTOs
{
    public class TempMovieDTO
    {
        public ObjectId Id { get; set; }
        public int MovieId { get; set; }
        public string UserEmail { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
