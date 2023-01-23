using Microsoft.AspNetCore.Mvc;

namespace TechOnIt.Application.Common.Models;

public class Paginated
{
    private int _page;
    public int Page
    {
        get { return _page; }
        set { _page = value < 1 ? 1 : value; }
    }
}

public class PaginatedSearch
{
    public string? Keyword { get; set; }

    private int _page = 1;
    public int Page
    {
        get { return _page; }
        set { _page = value < 1 ? 1 : value; }
    }
}

public class PaginatedWithSize
{
    private int _page = 1;
    public int Page
    {
        get { return _page; }
        set { _page = value < 1 ? 1 : value; }
    }

    private int _pageSize;
    public int PageSize
    {
        get { return _pageSize; }
        set { _pageSize = value < 1 ? 10 : value; }
    }
}

public class PaginatedSearchWithSize
{
    public string? Keyword { get; set; }

    private int _page = 1;
    public int Page
    {
        get { return _page; }
        set { _page = value < 1 ? 1 : value; }
    }

    private int _pageSize;
    public int PageSize
    {
        get { return _pageSize; }
        set { _pageSize = value < 1 ? 10 : value; }
    }
}