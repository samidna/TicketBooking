namespace TicketBooking.Application.DTOs.Pagination;
public class PagedResponse<T>
{
    public IEnumerable<T> Items { get; set; }
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }

    public PagedResponse(IEnumerable<T> items, int count, int page, int pageSize)
    {
        Items = items;
        TotalCount = count;
        CurrentPage = page;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }
}
