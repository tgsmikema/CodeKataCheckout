using System.Security.Principal;

namespace CodeKataCheckout.Tests;

public class CheckoutTests
{
    [Fact]
    public void Total_should_be_zero_when_no_items_scanned()
    {
        var checkout = new Checkout(new List<IPricingRule>());
        
        Assert.Equal(0, checkout.GetTotal());
        
    }
    
    [Fact]
    public void Total_should_be_correct_price_when_single_item_A_scanned()
    {
        var pricingRules = new List<IPricingRule>
        {
            new UnitPricing("A", 50)
        };

        var checkout = new Checkout(pricingRules);
        checkout.Scan("A");
        
        Assert.Equal(50, checkout.GetTotal());
    }

    [Fact]
    public void Total_should_be_correct_price_when_items_with_simple_pricing_scanned()
    {
        var pricingRules = new List<IPricingRule>
        {
            new UnitPricing("A", 50),
            new UnitPricing("B", 30),
            new UnitPricing("C", 20),
            new UnitPricing("D", 15)
        };
        
        var checkout = new Checkout(pricingRules);
        
        checkout.Scan("A");
        checkout.Scan("B");
        checkout.Scan("C");
        checkout.Scan("D");
        
        Assert.Equal(115, checkout.GetTotal());
    }

    [Fact]
    public void Increment_should_be_correct_when_items_with_simple_pricing_scanned()
    {
        var pricingRules = new List<IPricingRule>
        {
            new UnitPricing("A", 50),
            new UnitPricing("B", 30),
            new UnitPricing("C", 20),
            new UnitPricing("D", 15)
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
}
