using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GimmeMovieSuggestionsAPI.Integrations.DTOs
{
    public class UserDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("birth")]
        public DateTime Birth { get; set; }

        [JsonProperty("sex")]
        public string Sex { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("pictureUrl")]
        public string PictureUrl { get; set; }

        [JsonProperty("createdOn")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("updatedOn")]
        public DateTime UpdatedOn { get; set; }

        [JsonProperty("movies")]
        public UserMoviesDTO Movies { get; set; }
    }

    public class UserMoviesDTO {
        [JsonProperty("watched")]
        public List<UserMovieDTO> Watched { get; set; }

        [JsonProperty("watchLater")]
        public List<UserMovieDTO> WatchLater { get; set; }

        [JsonProperty("blacklist")]
        public List<UserMovieDTO> BlackList { get; set; }
    }

    public class UserMovieDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("rate")]
        public int? Rate { get; set; }
    }
}

