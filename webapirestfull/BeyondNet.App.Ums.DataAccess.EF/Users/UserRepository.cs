using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BeyondNet.App.Ums.Domain.Common.Interface;
using BeyondNet.App.Ums.Domain.User;
using BeyondNet.App.Ums.Domain.User.Key;
using BeyondNet.App.Ums.Helpers.Binders;
using BeyondNet.App.Ums.Helpers.Paginations;

namespace BeyondNet.App.Ums.DataAccess.EF.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly UmsDbContext _umsDbContext;
        private readonly IPropertyMappingService _propertyMappingService;


        public UserRepository(UmsDbContext umsDbContext,IPropertyMappingService propertyMappingService)
        {
            _umsDbContext = umsDbContext;
            _propertyMappingService = propertyMappingService;
        }

        public IEnumerable<UserInfoReadModel> GetAll(PaginationParameters paginationParameters)
        {
            var collectionSorted = _umsDbContext.Users.ApplySort(paginationParameters.OrderBy, 
                                                 _propertyMappingService.GetPropertyMapping<UserEdit, UserInfoReadModel>());

            var collectionMapped = Mapper.Map<IEnumerable<UserInfoReadModel>>(collectionSorted);

            collectionMapped = collectionMapped.AsQueryable();

            if (!string.IsNullOrEmpty(paginationParameters.FullName))
            {
                var fullNameForWhereClause = paginationParameters.FullName.Trim().ToLowerInvariant();

                collectionMapped = collectionMapped.Where(p => p.FullName.ToLowerInvariant() == fullNameForWhereClause);
            }

            if (!string.IsNullOrEmpty(paginationParameters.SearchQuery))
            {
                var searchQueryForWhereClause = paginationParameters.SearchQuery.Trim().ToLowerInvariant();

                collectionMapped = collectionMapped.Where(p =>
                    p.FullName.ToLowerInvariant().Contains(searchQueryForWhereClause)
                    || p.UserName.ToLowerInvariant().Contains(searchQueryForWhereClause)
                    || p.Email.ToLowerInvariant().Contains(searchQueryForWhereClause));
            }

            return collectionMapped;
        }

        public IEnumerable<UserInfoReadModel> GetCollection(IEnumerable<Guid> userIds)
        {
            var users = _umsDbContext.Users.Find(userIds);

            return Mapper.Map<IEnumerable<UserInfoReadModel>>(users);
        }

        public UserEdit Get(Guid id)
        {
            var users = _umsDbContext.Users.FirstOrDefault(r => r.Id == id);

            return users;
        }

        public UserEdit FindOne(ISpecification<UserEdit> spec)
        {
            var user = _umsDbContext.Users.First(spec.SpecExpression);

            return user;
        }

        public IEnumerable<UserEdit> Find(ISpecification<UserEdit> spec)
        {
            var users = _umsDbContext.Users.Where(spec.SpecExpression);

            return users;
        }

        public UserEdit Create(UserEdit entity)
        {
            var user = _umsDbContext.Users.Add(entity);
            return user.Entity;
        }

        public UserEdit Update(UserEdit entity)
        {
            var user = _umsDbContext.Users.Add(entity);

            return user.Entity;
        }

        public void Delete(Guid id)
        {
            var user = _umsDbContext.Users.First(r => r.Id == id);

            _umsDbContext.Users.Remove(user);
        }

        public bool Exists(ISpecification<UserEdit> spec)
        {
            return _umsDbContext.Users.Where(spec.SpecExpression).Any();
        }

        public IEnumerable<KeyInfoReadModel> GetKeys(Guid userId)
        {
            var keys = _umsDbContext.Keys.Where(r => r.User.Id == userId);

            var result = Mapper.Map<IEnumerable<KeyInfoReadModel>>(keys);

            return result;
        }

        public KeyEdit GetKey(Guid userId, Guid id)
        {
            var key = _umsDbContext.Keys.First(r => r.Id == id && r.User.Id == userId);

            return key;
        }

        public KeyEdit CreateKey(Guid userId, KeyEdit keyEdit)
        {
            var key = _umsDbContext.Keys.Add(keyEdit);

            return key.Entity;
        }

        public KeyEdit UpdateKey(Guid userId, KeyEdit keyEdit)
        {
            var key = _umsDbContext.Keys.Update(keyEdit);

            return key.Entity;
        }

        public void DeleteKey(Guid userId, Guid id)
        {
            var key = _umsDbContext.Keys.First(r => r.Id == id && r.User.Id == userId);

            _umsDbContext.Keys.Remove(key);
        }
    }
}
