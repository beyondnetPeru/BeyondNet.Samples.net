using System.Collections.Generic;
using Jal.Monads;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Beauty.Dick.Helpers.Interfaces
{
    public interface IMongoDbHelper
    {
        string DatabaseName { get; set; }

        Result Delete<T>(string documentname, string id);
        Result Delete<T>(string documentname, string property, string value);
        Result DeleteMany<T>(string documentname, FilterDefinition<T> filter);
        Result DeleteMany<T>(string documentname, string property, string value);
        Result<IList<T>> GetAllDocuments<T>(string documentname);
        Result<IList<T>> GetAllDocuments<T>(string documentname, ProjectionDefinition<T> fields);
        Result<long> GetCount<T>(string documentname, FilterDefinition<T> filter);
        Result<IList<BsonDocument>> GetDatabase();
        Result<T> GetDocumentById<T>(string documentname, string id);
        Result<T> GetDocumentByUserFilter<T>(string documentname, FilterDefinition<T> filter, ProjectionDefinition<T> fields);
        Result<IList<T>> GetDocumentsByFilter<T>(string documentname, FilterDefinition<T> filter);
        Result<IList<T>> GetDocumentsByFilter<T>(string documentname, FilterDefinition<T> filter, ProjectionDefinition<T> fields);
        Result<IList<T>> GetDocumentsByFilter<T>(string documentname, string property, string value);
        Result<IList<T>> GetDocumentsByFilter<T>(string documentname, string property, string value, ProjectionDefinition<T> fields);
        Result<IList<T>> GetPagedDocumentsByFilter<T>(string documentname, FilterDefinition<T> filter, int pageIndex, int pageSize);
        Result<IList<T>> GetPagedDocumentsByFilter<T>(string documentname, FilterDefinition<T> filter, ProjectionDefinition<T> fields, SortDefinition<T> sort, int pageIndex, int pageSize);
        Result<IList<T>> GetPagedDocumentsByFilter<T>(string documentname, FilterDefinition<T> filter, SortDefinition<T> sort, int pageIndex, int pageSize);
        Result<IList<T>> GetPagedDocumentsByFilter<T>(string documentname, int pageIndex, int pageSize);
        Result<IList<T>> GetPagedDocumentsByFilter<T>(string documentname, SortDefinition<T> sort, int pageIndex, int pageSize);
        Result Insert<T>(string documentName, T document);
        Result InsertMany<T>(string documentname, IList<T> documents);
        Result<bool> IsExistDocument<T>(string documentname, FilterDefinition<T> filter);
        Result<BsonDocument> RunCommand(string cmdText);
        Result Update<T>(string documentname, FilterDefinition<T> filter, UpdateDefinition<T> update);
        Result Update<T>(string documentname, string id, string property, string value);
        Result UpdateMany<T>(string documentname, FilterDefinition<T> filter, UpdateDefinition<T> update);
        Result UpdateReplaceOne<T>(string documentname, FilterDefinition<T> filter, T oldinfo);
        Result UpdateReplaceOne<T>(string documentname, string id, T oldinfo);
    }
}