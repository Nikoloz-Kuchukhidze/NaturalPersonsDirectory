namespace NaturalPersonsDirectory.Domain.Common.Paging;

public interface IPagedList<TResponse>
{
    public List<TResponse> Items { get; }
    public int Page { get; }
    public int PageSize { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }
    public bool HasNextPage { get; }
    public bool HasPreviousPage { get; }


    IPagedList<TDestination> Adapt<TDestination>();
}
