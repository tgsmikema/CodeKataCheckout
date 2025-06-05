namespace CodeKataCheckout;

public interface IDiscountRule
{
    string Sku { get; }
    public int BulkQty { get;  }
    public decimal BulkPrice { get; }
    decimal CalculateDiscount(int quantity);
}