namespace CodeKataCheckout;

public interface IDiscountRule
{
    string Sku { get; }
    decimal CalculateDiscount(int quantity);
}