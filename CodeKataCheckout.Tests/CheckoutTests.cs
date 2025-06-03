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
        var itemAPricingRule = new SimplePricingRule("A", 50);
        var pricingRules = new List<IPricingRule> { itemAPricingRule };

        var checkout = new Checkout(pricingRules);
        checkout.Scan("A");
        
        Assert.Equal(50, checkout.GetTotal());
    }

    [Fact]
    public void Total_should_be_correct_price_when_items_with_simple_pricing_scanned()
    {
        var itemAPricingRule = new SimplePricingRule("A", 50);
        var itemBPricingRule = new SimplePricingRule("B", 30);
        var itemCPricingRule = new SimplePricingRule("C", 20);
        var itemDPricingRule = new SimplePricingRule("D", 15);
        var pricingRules = new List<IPricingRule> { itemAPricingRule, itemDPricingRule, itemCPricingRule, itemBPricingRule };
        
        var checkout = new Checkout(pricingRules);
        
        checkout.Scan("A");
        checkout.Scan("B");
        checkout.Scan("C");
        checkout.Scan("D");
        
        Assert.Equal(115, checkout.GetTotal());
    }
}
