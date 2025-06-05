namespace CodeKataCheckout;

public class UnitPricing : IPricingRule
{
    public string Sku { get; }
    private readonly decimal _unitPrice;

    public UnitPricing(string sku, decimal unitPrice)
    {
        Sku = sku;
        _unitPrice = unitPrice;
    }
    
    public decimal GetUnitPrice()
    {
        return _unitPrice;
    }
}