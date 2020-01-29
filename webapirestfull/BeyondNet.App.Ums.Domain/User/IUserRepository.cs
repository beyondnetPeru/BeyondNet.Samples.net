using System;
using System.Collections.Generic;
using BeyondNet.App.Ums.Domain.Common.Interface;
using BeyondNet.App.Ums.Domain.User.Key;
using BeyondNet.App.Ums.Helpers.Paginations;

namespace BeyondNet.App.Ums.Domain.User
{
    public interface IUserRepository : IRepository<UserEdit, Guid>
    {
        IEnumerable<UserInfoReadModel> GetAll(PaginationParameters paginationParameters);

        IEnumerable<UserInfoReadModel> GetCollection(IEnumerable<Guid> idcollection);

        bool Exists(ISpecification<UserEdit> spec);

        IEnumerable<KeyInfoReadModel> GetKeys(Guid userId);

        KeyEdit GetKey(Guid userId, Guid id);

        KeyEdit CreateKey(Guid userId, KeyEdit keyEdit);

        KeyEdit UpdateKey(Guid userId, KeyEdit keyEdit);

        void DeleteKey(Guid userId, Guid id);
    }
}
