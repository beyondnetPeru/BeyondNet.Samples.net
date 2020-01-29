using Beauty.Dick.Helpers.Interfaces;
using Jal.Monads;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Beauty.Dick.Helpers.Impl
{

    public class MongoDbHelper : IMongoDbHelper
    {
        private string _databaseName = string.Empty;
        private IMongoClient _client = null;
        private IMongoDatabase _database = null;

        public MongoDbHelper(string connectionString)
        {
            _client = new MongoClient(connectionString);
        }

        public MongoDbHelper(string connectionString, string databaseName)
        {
            _client = new MongoClient(connectionString);

            _database = _client.GetDatabase(databaseName);
        }

        public string DatabaseName
        {
            get { return _databaseName; }
            set
            {
                _databaseName = value;
                _database = _client.GetDatabase(_databaseName);
            }
        }

        public Result<BsonDocument> RunCommand(string cmdText)
        {
            try
            {
                return _database.RunCommand<BsonDocument>(cmdText).ToResult();
            }
            catch (Exception ex)
            {

                return Result.Failure<BsonDocument>(ex.Message);
            }
        }

        public Result<IList<BsonDocument>> GetDatabase()
        {
            try
            {
                return _client.ListDatabases().ToList();
            }
            catch (Exception ex)
            {

                return Result.Failure<IList<BsonDocument>>(ex.Message);
            }
        }


        public Result<bool> IsExistDocument<T>(string documentname, FilterDefinition<T> filter)
        {
            try
            {
                return (_database.GetCollection<T>(documentname).CountDocuments(filter) > 0).ToResult();
            }
            catch (Exception ex)
            {
                return Result.Failure<bool>(ex.Message);
            }
        }


        public Result<long> GetCount<T>(string documentname, FilterDefinition<T> filter)
        {
            try
            {
                return _database.GetCollection<T>(documentname).CountDocuments(filter).ToResult();
            }
            catch (Exception ex)
            {
                return Result.Failure<long>(ex.Message);
            }
        }


        public Result<T> GetDocumentById<T>(string documentname, string id)
        {
            try
            {
                var oid = ObjectId.Parse(id);

                var filter = Builders<T>.Filter.Eq("_id", oid);

                var result = _database.GetCollection<T>(documentname).Find(filter);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return Result.Failure<T>(ex.Message);
            }
        }

        public Result<T> GetDocumentByUserFilter<T>(string documentname, FilterDefinition<T> filter, ProjectionDefinition<T> fields)
        {
            try
            {
                return _database.GetCollection<T>(documentname).Find(filter).Project<T>(fields).FirstOrDefault().ToResult();
            }
            catch (Exception ex)
            {

                return Result.Failure<T>(ex.Message);
            }
        }

        public Result<IList<T>> GetAllDocuments<T>(string documentname)
        {
            try
            {
                var filter = Builders<T>.Filter.Empty;

                return _database.GetCollection<T>(documentname).Find(filter).ToList();
            }
            catch (Exception ex)
            {
                return Result.Failure<IList<T>>(ex.Message);
            }
        }

        public Result<IList<T>> GetAllDocuments<T>(string documentname, ProjectionDefinition<T> fields)
        {
            try
            {
                var filter = Builders<T>.Filter.Empty;

                return _database.GetCollection<T>(documentname).Find(filter).Project<T>(fields).ToList();
            }
            catch (Exception ex)
            {
                return Result.Failure<IList<T>>(ex.Message);
            }

        }

        public Result<IList<T>> GetDocumentsByFilter<T>(string documentname, string property, string value)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq(property, value);

                return _database.GetCollection<T>(documentname).Find(filter).ToList();
            }
            catch (Exception ex)
            {
                return Result.Failure<IList<T>>(ex.Message);
            }
        }

        public Result<IList<T>> GetDocumentsByFilter<T>(string documentname, FilterDefinition<T> filter)
        {
            try
            {
                return _database.GetCollection<T>(documentname).Find(filter).ToList();
            }
            catch (Exception ex)
            {
                return Result.Failure<IList<T>>(ex.Message);
            }
        }


        public Result<IList<T>> GetDocumentsByFilter<T>(string documentname, string property, string value, ProjectionDefinition<T> fields)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq(property, value);

                return _database.GetCollection<T>(documentname).Find(filter).Project<T>(fields).ToList();
            }
            catch (Exception ex)
            {
                return Result.Failure<IList<T>>(ex.Message);
            }
        }

        public Result<IList<T>> GetDocumentsByFilter<T>(string documentname, FilterDefinition<T> filter, ProjectionDefinition<T> fields)
        {
            try
            {
                return _database.GetCollection<T>(documentname).Find(filter).Project<T>(fields).ToList();
            }
            catch (Exception ex)
            {
                return Result.Failure<IList<T>>(ex.Message);
            }
        }

        public Result<IList<T>> GetPagedDocumentsByFilter<T>(string documentname, FilterDefinition<T> filter, ProjectionDefinition<T> fields, SortDefinition<T> sort, int pageIndex, int pageSize)
        {
            try
            {
                var result = new List<T>();

                if (pageIndex != 0 && pageSize != 0)
                {
                    result = _database.GetCollection<T>(documentname).Find(filter).Project<T>(fields).Sort(sort).Skip(pageSize * (pageIndex - 1)).Limit(pageSize).ToList();
                }
                else
                {
                    result = _database.GetCollection<T>(documentname).Find(filter).Project<T>(fields).Sort(sort).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                return Result.Failure<IList<T>>(ex.Message);
            }
        }

        public Result<IList<T>> GetPagedDocumentsByFilter<T>(string documentname, FilterDefinition<T> filter, SortDefinition<T> sort, int pageIndex, int pageSize)
        {
            try
            {
                var result = new List<T>();

                if (pageIndex != 0 && pageSize != 0)
                {
                    result = _database.GetCollection<T>(documentname).Find(filter).Sort(sort).Skip(pageSize * (pageIndex - 1)).Limit(pageSize).ToList();
                }
                else
                {
                    result = _database.GetCollection<T>(documentname).Find(filter).Sort(sort).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                return Result.Failure<IList<T>>(ex.Message);
            }
        }

        public Result<IList<T>> GetPagedDocumentsByFilter<T>(string documentname, FilterDefinition<T> filter, int pageIndex, int pageSize)
        {
            try
            {
                var result = new List<T>();

                if (pageIndex != 0 && pageSize != 0)
                {
                    result = _database.GetCollection<T>(documentname).Find(filter).Skip(pageSize * (pageIndex - 1)).Limit(pageSize).ToList();
                }
                else
                {
                    result = _database.GetCollection<T>(documentname).Find(filter).ToList();
                }

                return result;
            }
            catch (Exception ex)
            {
                return Result.Failure<IList<T>>(ex.Message);
            }
        }

        public Result<IList<T>> GetPagedDocumentsByFilter<T>(string documentname, SortDefinition<T> sort, int pageIndex, int pageSize)
        {
            try
            {
                var result = new List<T>();

                var filter = Builders<T>.Filter.Empty;

                if (pageIndex != 0 && pageSize != 0)
                {
                    result = _database.GetCollection<T>(documentname).Find(filter).Sort(sort).Skip(pageSize * (pageIndex - 1)).Limit(pageSize).ToList();
                }
                else
                {
                    result = _database.GetCollection<T>(documentname).Find(filter).Sort(sort).ToList();
                }

                return result;
            }
            catch (Exception ex)
            {
                return Result.Failure<IList<T>>(ex.Message);
            }
        }

        public Result<IList<T>> GetPagedDocumentsByFilter<T>(string documentname, int pageIndex, int pageSize)
        {

            try
            {
                var result = new List<T>();

                var filter = Builders<T>.Filter.Empty;

                if (pageIndex != 0 && pageSize != 0)
                {
                    result = _database.GetCollection<T>(documentname).Find(filter).Skip(pageSize * (pageIndex - 1)).Limit(pageSize).ToList();
                }
                else
                {
                    result = _database.GetCollection<T>(documentname).Find(filter).ToList();
                }

                return result;
            }
            catch (Exception ex)
            {
                return Result.Failure<IList<T>>(ex.Message);
            }
        }


        public Result Insert<T>(string documentName, T document)
        {
            try
            {
                _database.GetCollection<T>(documentName).InsertOne(document);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public Result InsertMany<T>(string documentname, IList<T> documents)
        {
            try
            {
                _database.GetCollection<T>(documentname).InsertMany(documents);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public Result UpdateReplaceOne<T>(string documentname, string id, T oldinfo)
        {
            try
            {
                var oid = ObjectId.Parse(id);

                var filter = Builders<T>.Filter.Eq("_id", oid);

                _database.GetCollection<T>(documentname).ReplaceOne(filter, oldinfo);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Success(ex.Message);
            }
        }

        public Result UpdateReplaceOne<T>(string documentname, FilterDefinition<T> filter, T oldinfo)
        {
            try
            {
                _database.GetCollection<T>(documentname).ReplaceOne(filter, oldinfo);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Success(ex.Message);
            }
        }

        public Result Update<T>(string documentname, string id, string property, string value)
        {
            try
            {
                var oid = ObjectId.Parse(id);
                var filter = Builders<T>.Filter.Eq("_id", oid);
                var update = Builders<T>.Update.Set(property, value);

                _database.GetCollection<T>(documentname).UpdateOne(filter, update);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Success(ex.Message);
            }

        }

        public Result Update<T>(string documentname, FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            try
            {
                _database.GetCollection<T>(documentname).UpdateOne(filter, update);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Success(ex.Message);
            }
        }

        public Result UpdateMany<T>(string documentname, FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            try
            {
                _database.GetCollection<T>(documentname).UpdateMany(filter, update);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Success(ex.Message);
            }
        }

        public Result Delete<T>(string documentname, string id)
        {
            try
            {
                var oid = ObjectId.Parse(id);
                var filterid = Builders<T>.Filter.Eq("_id", oid);
                _database.GetCollection<T>(documentname).DeleteOne(filterid);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Success(ex.Message);
            }
        }

        public Result Delete<T>(string documentname, string property, string value)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq(property, value);

                _database.GetCollection<T>(documentname).DeleteOne(filter);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Success(ex.Message);
            }
        }

        public Result DeleteMany<T>(string documentname, string property, string value)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq(property, value);

                _database.GetCollection<T>(documentname).DeleteMany(filter);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Success(ex.Message);
            }
        }


        public Result DeleteMany<T>(string documentname, FilterDefinition<T> filter)
        {
            try
            {
                _database.GetCollection<T>(documentname).DeleteMany(filter);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Success(ex.Message);
            }
        }
    }
}