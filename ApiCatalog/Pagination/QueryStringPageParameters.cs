namespace ApiCatalog.Pagination;

public class QueryStringPageParameters
{
    const int MaxSize = 50;
    public int CurrentPage { get; set; } = 1;
    private int _pageSize = 10;
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = (value > MaxSize) ? MaxSize : value;
        }
    }
}
