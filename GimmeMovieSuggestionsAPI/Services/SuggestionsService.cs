using GimmeMovieSuggestionsAPI.Integrations;
using GimmeMovieSuggestionsAPI.Integrations.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GimmeMovieSuggestionsAPI.Services
{
    public static class SuggestionsService
    {
        public static DiscoverDTO ProccessSuggestionRequest(SuggestionRequest req)
        {
            req = PrepareSuggestionRequest(req);

            var genresIds = ProccessGenres(req.Audio);

            var movies = TheMovieDbIntegration.GetMovies(genresIds, req.Page.ToString(), "false", "false", req.OrderBy);
            var user = UsersManagementIntegration.GetUserByEmail(req.UserEmail);
            movies = FilterMoviesListByUser(movies, user);
            
            movies.Results = movies.Results.OrderByDescending(x => x.Popularity).ToList<MovieDTO>();

            return movies;
        }

        private static SuggestionRequest PrepareSuggestionRequest(SuggestionRequest req)
        {
            req.Audio = req.Audio.ToLower();
            return req;
        }

        private static string ProccessGenres(string audio)
        {
            var genres = TheMovieDbIntegration.GetGenres();
            var genresIds = "";

            foreach (var genre in genres.Genres)
            {
                var genreNameLower = genre.Name.ToLower();
                if (audio.Contains(genreNameLower))
                {
                    if (genresIds.Equals(""))
                        genresIds = $"{genre.Id}";
                    else
                        genresIds = $"{genresIds},{genre.Id}";
                    audio = audio.Replace(genreNameLower, "");
                }     
            }

            return genresIds;
        }

        private static DiscoverDTO FilterMoviesListByUser(DiscoverDTO movies, UserDTO user)
        {
            foreach (var item in user.Movies.Watched)
                movies.Results.RemoveAll(x => x.Id == item.Id);

            foreach (var item in user.Movies.WatchLater)
                movies.Results.RemoveAll(x => x.Id == item.Id);

            foreach (var item in user.Movies.BlackList)
                movies.Results.RemoveAll(x => x.Id == item.Id);

            return movies;
        } 

    }
}
