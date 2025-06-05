namespace CodeKataCheckout;

public interface IDiscountRule
{
    string Sku { get; }
    public int BulkQty { get;  }
    decimal CalculateDiscount(int quantity);
}