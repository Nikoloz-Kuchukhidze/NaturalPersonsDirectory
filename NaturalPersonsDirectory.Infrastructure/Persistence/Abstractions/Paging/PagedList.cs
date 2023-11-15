using Microsoft.EntityFrameworkCore;
using NaturalPersonsDirectory.Domain.Primitives;
using Mapster;

namespace NaturalPersonsDirectory.Domain.Common.Paging;

public sealed class PagedList<TResponse> : IPagedList<TResponse>
{
    private PagedList(List<TResponse> items, int page, int pageSize, int totalPages, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalPages = totalPages;    
        TotalCount = totalCount;
    }

    public List<TResponse> Items { get; }
    public int Page { get; }
    public int PageSize { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => Page > 1;

    public static async Task<PagedList<TEntity>> Create<TEntity>(
        IQueryable<TEntity> query,
        int? page,
        int? pageSize) 
        where TEntity : IEntity
    {
        var totalCount = await query.CountAsync();

        var currentPage = page.HasValue && page > 0 ? page.Value : 1;
        var itemsPerPage = pageSize.HasValue && pageSize > 0 ? pageSize.Value : 10;

        var skip = (currentPage - 1) * itemsPerPage;

        var items = query
            .Skip(skip)
            .Take(itemsPerPage)
            .ToList();

        var totalPages = (int)Math.Ceiling(totalCount / (double)itemsPerPage);

        return new PagedList<TEntity>(
            items,
            currentPage,
            itemsPerPage,
            totalPages,
            totalCount);
    }

    public IPagedList<TDestination> Adapt<TDestination>()
    {
        return new PagedList<TDestination>(
            Items.Adapt<List<TResponse>, List<TDestination>>(),
            Page,
            PageSize,
            TotalPages,
            TotalCount);
    }
}
