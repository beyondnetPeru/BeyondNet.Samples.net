using BeyondNet.App.Ums.Helpers.Binders;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UMS.Infrastructure.Helpers.Paginations;

namespace BeyondNet.App.Ums.Helpers.Paginations
{
    public class PaginationHelper : IPaginationHelper
    {
        private readonly IUrlHelper _urlHelper;

        public PaginationHelper(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }
        public string CreateUsersResourceUrl(string routeName, PaginationParameters paginationParameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return _urlHelper.Link(routeName,
                        new
                        {
                            fullName = paginationParameters.FullName,
                            orderBy = paginationParameters.OrderBy,
                            searchQuery = paginationParameters.SearchQuery,
                            pageNumber = paginationParameters.PageNumber - 1,
                            pageSize = paginationParameters.PageSize
                        });
                case ResourceUriType.NextPage:
                    return _urlHelper.Link(routeName,
                        new
                        {
                            fullName = paginationParameters.FullName,
                            orderBy = paginationParameters.OrderBy,
                            searchQuery = paginationParameters.SearchQuery,
                            pageNumber = paginationParameters.PageNumber + 1,
                            pageSize = paginationParameters.PageSize
                        });

                default:
                    return _urlHelper.Link(routeName,
                        new
                        {
                            fullName = paginationParameters.FullName,
                            orderBy = paginationParameters.OrderBy,
                            searchQuery = paginationParameters.SearchQuery,
                            pageNumber = paginationParameters.PageNumber,
                            pageSize = paginationParameters.PageSize
                        });
            }
        }

        public string GetMetadata<T>(PageList<T> pageList, string previousPage, string nextPage)
        {
            var paginationMetadata = new
            {
                totalCount = pageList.TotalCount,
                pageSize = pageList.PageSize,
                currentPage = pageList.CurrentPage,
                totalPages = pageList.TotalPages,
                previousPage,
                nextPage
            };

            return JsonConvert.SerializeObject(paginationMetadata);
        }
    }
}
