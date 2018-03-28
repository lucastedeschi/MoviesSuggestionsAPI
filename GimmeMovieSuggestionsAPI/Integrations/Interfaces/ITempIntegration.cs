using GimmeMovieSuggestionsAPI.Integrations.DTOs;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GimmeMovieSuggestionsAPI.Integrations.Interfaces
{
    public interface ITempIntegration
    {
        IEnumerable<TempMovieDTO> FindAll();
        void Delete(ObjectId id);
        void DeleteOld();
        IEnumerable<TempMovieDTO> FindByEmail(string name);
        void InsertOne(TempMovieDTO document);
    }
}
