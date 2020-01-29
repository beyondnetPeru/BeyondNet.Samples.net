using Beauty.Barry.Domain.Department;
using Beauty.Dick.Domain.Interface;
using Jal.Monads;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Beauty.Barry.Infrastructure.DaoMongoDB
{
    public class DepartmentDao : IDepartmentRepository
    {
        MongoClient client;
        IMongoDatabase database;
        IMongoCollection<BsonDocument> collection;

        public DepartmentDao()
        {
            client = new MongoClient("mongodb://localhost:27017");
            database = client.GetDatabase("BeautyDB");
            collection = database.GetCollection<BsonDocument>("Department");
        }

        public Result<DepartmentEdit> Create(DepartmentEdit entity)
        {
            var document = new BsonDocument
            {
               { "id", entity.Id },
               { "code", entity.Code},
               { "name", entity.Name},
               { "description", entity.Description},
               { "status", entity.Status}
            };

            collection.InsertOne(document);

            return entity.ToResult();
        }

        public Result<DepartmentEdit> Delete(ISpecification<DepartmentEdit> spec)
        {
            throw new NotImplementedException();
        }

        public Result<bool> Exists(ISpecification<DepartmentEdit> spec)
        {
            throw new NotImplementedException();
        }

        public Result<DepartmentEdit> Fetch(ISpecification<DepartmentEdit> spec)
        {
            throw new NotImplementedException();
        }

        public Result<IEnumerable<DepartmentEdit>> FetchAll(ISpecification<DepartmentEdit> spec)
        {
            throw new NotImplementedException();
        }

        public Result<DepartmentEdit> Update(DepartmentEdit entity)
        {
            throw new NotImplementedException();
        }
    }
}
