using GimmeMovieSuggestionsAPI.Integrations.DTOs;
using GimmeMovieSuggestionsAPI.Integrations.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GimmeMovieSuggestionsAPI.Integrations
{
    public class TempIntegration : ITempIntegration
    {
        private readonly string _collectionName;
        private readonly MongoClient _mongoClient;

        public TempIntegration()
        {
            _collectionName = "tempMovies";
            _mongoClient = new MongoClient("mongodb://admin:123456@ds123619.mlab.com:23619/gimmesuggestionslucky");
        }

        public IMongoCollection<TempMovieDTO> GetCollection()
        {
            var database = _mongoClient.GetDatabase("gimmesuggestionslucky");

            return database.GetCollection<TempMovieDTO>(_collectionName);
        }

        public IEnumerable<TempMovieDTO> FindAll()
        {
            var collection = GetCollection();

            return collection.Find(x => true).ToList();
        }

        public void Delete(ObjectId id)
        {
            var collection = GetCollection();

            var filter = Builders<TempMovieDTO>.Filter.Eq("_id", id);
            collection.DeleteOne(filter);
        }

        public void DeleteOld()
        {
            var collection = GetCollection();

            var filter = Builders<TempMovieDTO>.Filter.Lt(x => x.CreatedOn, DateTime.Now.AddDays(-7));
            collection.DeleteMany(filter);
        }

        public IEnumerable<TempMovieDTO> FindByEmail(string userEmail)
        {
            var collection = GetCollection();

            var filter = Builders<TempMovieDTO>.Filter.Eq("UserEmail", userEmail);
            var result = collection.Find(filter).ToList();
            return result;
        }

        public void InsertOne(TempMovieDTO document)
        {
            var collection = GetCollection();
            collection.InsertOne(document);
        }

    }
}
