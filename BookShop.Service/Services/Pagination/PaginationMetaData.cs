namespace BookShop.Service.Services.Pagination;
    
public class PaginationMetaData
{
    public int TotalListCount { get; }
    public int AmountData { get; }
    public int TotalPage { get; }
    public int CurrentPageNumber { get; } 
    public bool HasPrevious  => CurrentPageNumber > 1;
    public bool HasNext => CurrentPageNumber < TotalPage;

    public PaginationMetaData(int totalListCount, int amountData, int currentPageNumber)
    {
        TotalListCount = totalListCount;
        AmountData = amountData;
        CurrentPageNumber = currentPageNumber;
        TotalPage = (int)Math.Ceiling(totalListCount / (double)amountData);
    }
}