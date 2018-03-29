using GimmeMovieSuggestionsAPI.Integrations;
using GimmeMovieSuggestionsAPI.Integrations.DTOs;
using GimmeMovieSuggestionsAPI.Integrations.Interfaces;
using GimmeMovieSuggestionsAPI.Utils;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GimmeMovieSuggestionsAPI.Services
{
    public class SuggestionService : ISuggestionService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ITempIntegration _tempIntegration;

        public SuggestionService(IHostingEnvironment hostingEnvironment, ITempIntegration tempIntegration)
        {
            _hostingEnvironment = hostingEnvironment;
            _tempIntegration = tempIntegration;
        }

        public List<MovieDTO> ProccessSuggestionRequest(SuggestionRequest req)
        {
            req = PrepareSuggestionRequest(req);
            var genresIds = ProccessGenresByText(req.Audio);

            var user = UsersManagementIntegration.GetUserByEmail(req.UserEmail);

            var movies = new List<MovieDTO>();
            var page = 1;
            var quantMovies = 20;
            while(movies.Count < quantMovies)
            {
                var moviesResult = TheMovieDbIntegration.GetMovies(genresIds, page, "true", "false").Results;
                if (moviesResult.Count == 0)
                    continue;

                int difference = quantMovies - movies.Count;
                moviesResult = moviesResult.Take(difference).ToList();

                movies.AddRange(FilterMoviesListByUser(moviesResult, user));
                page++;
            }

            movies = movies.OrderByDescending(x => x.Popularity).ToList();

            return movies;
        }

        public MovieDTO ProccessImFeelingLuckyRequest(SuggestionRequest req)
        {
            _tempIntegration.DeleteOld();

            var genresIds = ProccessGenresByTime(req.Time);

            var user = UsersManagementIntegration.GetUserByEmail(req.UserEmail);

            var movie = new MovieDTO();
            var page = 1;

            while (movie.Id == 0)
            {
                var moviesResult = TheMovieDbIntegration.GetMovies(genresIds, page, "true", "false").Results;
                if (moviesResult.Count == 0)
                {
                    genresIds = "";
                    continue;
                }

                foreach (var movieResult in moviesResult)
                {
                    var moviesList = new List<MovieDTO> { movieResult };
                    movie = FilterMoviesListByUser(moviesList, user, true).FirstOrDefault();
                    if (movie != null)
                        break;
                }

                page++;
            }

            _tempIntegration.InsertOne(new TempMovieDTO()
            {
                MovieId = movie.Id,
                UserEmail = user.Email,
                CreatedOn = DateTime.Now
            });
            return movie;
        }

        private SuggestionRequest PrepareSuggestionRequest(SuggestionRequest req)
        {
            req.Audio = req.Audio.ToLower();
            return req;
        }

        private string ProccessGenresByText(string text)
        {
            text = RemoveSpecialsChars(text.ToLower());
            var genres = TheMovieDbIntegration.GetGenres();
            var genresIds = "";

            text = text + new GenresSynonims().GetGenresBySynonims(text);

            foreach (var genre in genres.Genres)
            {
                var genreNameLower = RemoveSpecialsChars(genre.Name.ToLower());
                if (text.Contains(genreNameLower))
                {
                    if (genresIds.Equals(""))
                        genresIds = $"{genre.Id}";
                    else
                        genresIds = $"{genresIds},{genre.Id}";
                    text = text.Replace(genreNameLower, "");
                }     
            }

            return genresIds;
        }

        private string ProccessGenresByTime(string time)
        {
            try
            {
                var genres = TheMovieDbIntegration.GetGenres();

                var timeSplitted = time.Split(":");
                var hour = Convert.ToInt32(timeSplitted[0]);
                var minutes = Convert.ToInt32(timeSplitted[1]);

                if ((hour >= 20 && hour <= 23) || (hour >= 0 && hour <= 6))
                    return ProccessGenresByText("Terror Suspense");

                if (hour > 6 && hour < 20)
                    return ProccessGenresByText("Ação Comedia Aventura Drama Romance");

                return "";
            } catch(Exception)
            {
                return "";
            }
        }

        private List<MovieDTO> FilterMoviesListByUser(List<MovieDTO> movies, UserDTO user, bool isLucky = false)
        {
            foreach (var item in user.Movies.Watched)
                movies.RemoveAll(x => x.Id == item.Id);

            foreach (var item in user.Movies.WatchLater)
                movies.RemoveAll(x => x.Id == item.Id);

            foreach (var item in user.Movies.BlackList)
                movies.RemoveAll(x => x.Id == item.Id);

            if(isLucky)
            {
                var tempMoviesBlacklist = _tempIntegration.FindByEmail(user.Email);

                foreach (var item in tempMoviesBlacklist)
                    movies.RemoveAll(x => x.Id == item.MovieId);
            }
            
            return movies;
        }

        private string RemoveSpecialsChars(string texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }

    }
}
