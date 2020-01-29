using BeyondNet.App.Ums.Helpers.Binders;
using BeyondNet.App.Ums.Helpers.Paginations;

namespace UMS.Infrastructure.Helpers.Paginations
{
    public interface IPaginationHelper
    {
        string CreateUsersResourceUrl(string routeName, PaginationParameters paginationParameters, ResourceUriType type);

        string GetMetadata<T>(PageList<T> pageList, string previousPage, string nextPage);
    }
}