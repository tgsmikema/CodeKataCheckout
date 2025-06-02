namespace CodeKataCheckout;

public interface IPricingRule
{
    string Sku { get; }
    decimal CalculatePrice ();
}