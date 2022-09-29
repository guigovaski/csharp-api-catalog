using AutoMapper.Configuration.Conventions;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalog.Pagination;

public class PagedList<T> : List<T>
{
    public int CurrentPage { get; }
    public int TotalPages { get; }
    public int PageSize { get; }
    public int TotalCount { get; }

    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PagedList(List<T> items, int totalCount, int currentPage, int pageSize)
    {
        TotalCount = totalCount;
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        AddRange(items);
    }

    public static async  Task<PagedList<T>> ToPagedList(IQueryable<T> src, int currentPage, int pageSize)
    {
        var count = await src.CountAsync();
        var items = await src.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedList<T>(items, count, currentPage, pageSize);
    }
}
