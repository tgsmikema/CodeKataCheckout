namespace CodeKataCheckout;

public class PricingRule
{
    public readonly string Sku;
    public readonly decimal Price;

    public PricingRule(string sku, decimal price)
    {
        this.Sku = sku;
        this.Price = price;
    }
}