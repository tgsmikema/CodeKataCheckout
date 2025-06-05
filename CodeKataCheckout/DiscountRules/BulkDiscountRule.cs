namespace CodeKataCheckout;

public class BulkDiscountRule : IPricingRule, IDiscountRule
{
    public string Sku { get; }
    public int BulkQty { get;  }
    private readonly decimal _bulkPrice;
    private readonly decimal _unitPrice;

    public BulkDiscountRule(string sku, decimal unitPrice, int bulkQty, decimal bulkPrice)
    {
        Sku = sku;
        BulkQty = bulkQty;
        _bulkPrice = bulkPrice;
        _unitPrice = unitPrice;
    }
    
    public decimal CalculateDiscount(int quantity) 
    {
        int sets = quantity / BulkQty;
        decimal discountPerSet = decimal.Abs(_bulkPrice - BulkQty * _unitPrice);
        decimal totalDiscount = (sets * discountPerSet);
        return totalDiscount;
    }

    public decimal GetUnitPrice()
    {
        return _unitPrice;
    }
    
}