using System.Security.Principal;

namespace CodeKataCheckout.Tests;

public class CheckoutTests
{
    [Fact]
    public void GetTotal_NoItemsScanned_ReturnsZero()
    {
        var checkout = new Checkout(new List<IPricingRule>());
        
        Assert.Equal(0, checkout.GetTotal());
        
    }
    
    [Fact]
    public void GetTotal_SingleItemAScanned_ReturnsUnitPrice()
    {
        var pricingRules = new List<IPricingRule>
        {
            new UnitPricingRule("A", 50)
        };

        var checkout = new Checkout(pricingRules);
        checkout.Scan("A");
        
        Assert.Equal(50, checkout.GetTotal());
    }

    [Fact]
    public void GetTotal_MultipleItemsWithUnitPricing_ReturnsSumOfUnitPrices()
    {
        var pricingRules = new List<IPricingRule>
        {
            new UnitPricingRule("A", 50),
            new UnitPricingRule("B", 30),
            new UnitPricingRule("C", 20),
            new UnitPricingRule("D", 15)
        };
        
        var checkout = new Checkout(pricingRules);
        
        checkout.Scan("A");
        checkout.Scan("B");
        checkout.Scan("C");
        checkout.Scan("D");
        
        Assert.Equal(115, checkout.GetTotal());
    }

    [Fact]
    public void GetTotal_ScanningItemsOneByOne_ReturnsIncrementalTotalCorrectly()
    {
        var pricingRules = new List<IPricingRule>
        {
            new UnitPricingRule("A", 50),
            new UnitPricingRule("B", 30),
            new UnitPricingRule("C", 20),
            new UnitPricingRule("D", 15)
        };
        
        var checkout = new Checkout(pricingRules);
        
        checkout.Scan("A");
        Assert.Equal(50, checkout.GetTotal());
        checkout.Scan("B");
        Assert.Equal(80, checkout.GetTotal());
        checkout.Scan("C");
        Assert.Equal(100, checkout.GetTotal());
        checkout.Scan("D");
        Assert.Equal(115, checkout.GetTotal());
    }

    [Fact]
    public void GetTotal_ItemWithBulkDiscount_AppliesDiscountCorrectly()
    {
        var pricingRules = new List<IPricingRule>
        {
            new UnitPricingRule("A", 50),
            new BulkDiscountRule("A", 50, 3, 130)
        };
        
        var checkout = new Checkout(pricingRules);

        for (int i = 0; i < 3; i++)
        {
            checkout.Scan("A");
        }
        
        Assert.Equal(130, checkout.GetTotal());
    }
    
    [Fact]
    public void GetTotal_ItemWithBulkDiscountOutOfOrderScanned_AppliesDiscountCorrectly()
    {
        var pricingRules = new List<IPricingRule>
        {
            new UnitPricingRule("A", 50),
            new UnitPricingRule("B", 30),
            new BulkDiscountRule("A", 50, 3, 130)
        };
        
        var checkout = new Checkout(pricingRules);
        
        checkout.Scan("A");
        checkout.Scan("B");
        checkout.Scan("A");
        checkout.Scan("A");
        
        Assert.Equal(160, checkout.GetTotal());
    }
}
