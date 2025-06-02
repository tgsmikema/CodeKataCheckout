namespace CodeKataCheckout;

public class PricingRule
{
    private readonly string _Sku;
    private readonly decimal _Price;

    public PricingRule(string sku, decimal price)
    {
        _Sku = sku;
        _Price = price;
    }
}