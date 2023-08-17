namespace BookShop.Service.Services.Pagination;

public class PaginationParams
{
    private const int MinPageCount = 1;
    private const int MaxAmountData = 500;

    private int _amountData = 5;
    private int _pageNumber = 1;

    public int AmountData
    {
        get => _amountData > MaxAmountData ? MaxAmountData : _amountData <= 0 ? 1 : _amountData;
        set => _amountData = value;
    }
        
    public int PageNumber
    {
        get => _pageNumber < MinPageCount ? MinPageCount : _pageNumber;
        set => _pageNumber = value;
    }
}