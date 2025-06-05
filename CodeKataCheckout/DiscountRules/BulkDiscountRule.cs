namespace CodeKataCheckout;

public class BulkDiscountRule : IPricingRule, IDiscountRule
{
    public string Sku { get; }
    public int BulkQty { get;  }
    public decimal BulkPrice { get; }
    private readonly decimal _unitPrice;

    public BulkDiscountRule(string sku, decimal unitPrice, int bulkQty, decimal bulkPrice)
    {
        Sku = sku;
        BulkQty = bulkQty;
        BulkPrice = bulkPrice;
        _unitPrice = unitPrice;
    }
    
    public decimal CalculateDiscount(int quantity) 
    {
        int sets = quantity / BulkQty;
        decimal discountPerSet = decimal.Abs(BulkPrice - BulkQty * _unitPrice);
        decimal totalDiscount = (sets * discountPerSet);
        return totalDiscount;
    }

    public decimal GetUnitPrice()
    {
        return _unitPrice;
    }
    
}