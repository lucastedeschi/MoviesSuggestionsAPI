using GimmeMovieSuggestionsAPI.Integrations.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GimmeMovieSuggestionsAPI.Integrations
{
    public static class TheMovieDbIntegration
    {
        private static string _key = "bfcfc7229cafa99bb674a125fbad0bf0";

        public static GenresDTO GetGenres()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.themoviedb.org");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);

                HttpResponseMessage response = client.GetAsync($"/3/genre/movie/list?language=pt-BR&api_key={_key}").Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                GenresDTO data = JsonConvert.DeserializeObject<GenresDTO>(stringData);
                return data;
            }
        }

        public static DiscoverDTO GetMovies(string with_genres, string page, string include_video, string include_adult, string sort_by)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.themoviedb.org");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);

                HttpResponseMessage response = client.GetAsync($"/3/discover/movie?" +
                "with_genres=" + with_genres +
                //"&vote_average.lte=" + preferences.vote_average_lte +
                //"&vote_average.gte=" + preferences.vote_average_gte +
                //"&vote_count.lte=" + preferences.vote_count_lte +
                //"&vote_count.gte=" + preferences.vote_count_gte +
                //"&primary_release_year=" + preferences.primary_release_year +
                "&page=" + page +
                "&include_video=" + include_video +
                "&include_adult=" + include_adult +
                "&sort_by=" + sort_by +
                "&language=pt-BR&api_key=" + _key).Result;

                string stringData = response.Content.ReadAsStringAsync().Result;
                DiscoverDTO data = JsonConvert.DeserializeObject<DiscoverDTO>(stringData);
                return data;
            }
        }
    }
}
