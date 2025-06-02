namespace CodeKataCheckout;

public class SimplePricingRule : IPricingRule
{
    public string Sku { get; set; }

    private readonly decimal _unitPrice;

    public SimplePricingRule(string sku, decimal unitPrice)
    {
        this.Sku = sku;
        _unitPrice = unitPrice;
    }
    
    public decimal CalculatePrice()
    {
        return _unitPrice;
    }
}