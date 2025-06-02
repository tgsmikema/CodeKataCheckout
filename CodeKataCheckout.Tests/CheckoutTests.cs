using System.Security.Principal;
namespace CodeKataCheckout.Tests;

public class CheckoutTests
{
    [Fact]
    public void Total_should_be_zero_when_no_items_scanned()
    {
        var checkout = new Checkout(new List<PricingRule>());
        
        Assert.Equal(0, checkout.Total());
        
    }
}
