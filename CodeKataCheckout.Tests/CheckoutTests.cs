using System.Security.Principal;
using CodeKataCheckout.Data;

namespace CodeKataCheckout.Tests;

public class CheckoutTests
{
    private readonly ICheckout _checkout;
    public CheckoutTests()
    {
        _checkout = new Checkout(PricingRulesData.GetData());
    }
    
    [Fact]
    public void GetTotal_NoItemsScanned_ReturnsZero()
    {
        Assert.Equal(0, _checkout.GetTotal());
    }
    
    [Fact]
    public void GetTotal_SingleItemAScanned_ReturnsUnitPrice()
    {
        _checkout.Scan("A");
        Assert.Equal(50, _checkout.GetTotal());
    }

    [Fact]
    public void GetTotal_MultipleItemsWithUnitPricing_ReturnsSumOfUnitPrices()
    {
        _checkout.Scan("A");
        _checkout.Scan("B");
        _checkout.Scan("C");
        _checkout.Scan("D");
        
        Assert.Equal(115, _checkout.GetTotal());
    }

    [Fact]
    public void GetTotal_ScanningItemsOneByOne_ReturnsIncrementalTotalCorrectly()
    {
        _checkout.Scan("A");
        Assert.Equal(50, _checkout.GetTotal());
        _checkout.Scan("B");
        Assert.Equal(80, _checkout.GetTotal());
        _checkout.Scan("C");
        Assert.Equal(100, _checkout.GetTotal());
        _checkout.Scan("D");
        Assert.Equal(115, _checkout.GetTotal());
    }

    [Fact]
    public void GetTotal_ItemWithBulkDiscount_AppliesDiscountCorrectly()
    {
        for (int i = 0; i < 3; i++)
        {
            _checkout.Scan("A");
        }
        
        Assert.Equal(130, _checkout.GetTotal());
    }
    
    [Fact]
    public void GetTotal_ItemWithBulkDiscountOutOfOrderScanned_AppliesDiscountCorrectly()
    {
        _checkout.Scan("A");
        _checkout.Scan("B");
        _checkout.Scan("A");
        _checkout.Scan("A");
        
        Assert.Equal(160, _checkout.GetTotal());
    }
    
    [Fact]
    public void GetTotal_ItemWithBulkDiscountWithRepeatingDiscounts_AppliesDiscountCorrectly()
    {
        _checkout.Scan("C");
        _checkout.Scan("B");
        _checkout.Scan("C");
        _checkout.Scan("C");
        _checkout.Scan("C");
        _checkout.Scan("C");
        _checkout.Scan("C");
        
        Assert.Equal(130, _checkout.GetTotal());
    }
    
    [Fact]
    public void GetTotal_ItemWithBulkDiscount_ReturnsIncrementalTotalCorrectly()
    {
        _checkout.Scan("A");
        Assert.Equal(50, _checkout.GetTotal());
        _checkout.Scan("B");
        Assert.Equal(80, _checkout.GetTotal());
        _checkout.Scan("A");
        Assert.Equal(130, _checkout.GetTotal());
        _checkout.Scan("A");
        Assert.Equal(160, _checkout.GetTotal());
    }
    
    [Fact]
    public void GetTotal_ItemWithBulkDiscountWithRepeatingDiscounts_ReturnsIncrementalTotalCorrectly()
    {
        _checkout.Scan("C");
        Assert.Equal(20, _checkout.GetTotal());
        _checkout.Scan("B");
        Assert.Equal(50, _checkout.GetTotal());
        _checkout.Scan("C");
        Assert.Equal(70, _checkout.GetTotal());
        _checkout.Scan("C");
        Assert.Equal(80, _checkout.GetTotal());
        _checkout.Scan("C");
        Assert.Equal(100, _checkout.GetTotal());
        _checkout.Scan("C");
        Assert.Equal(120, _checkout.GetTotal());
        _checkout.Scan("C");
        Assert.Equal(130, _checkout.GetTotal());
    }
    
    [Fact]
    public void GetTotal_SameItemWithMultipleBulkDiscount_ReturnsTotalCorrectly()
    {
        for (int i = 0; i < 9; i++)
        {
            _checkout.Scan("A");
        }
        Assert.Equal(380, _checkout.GetTotal());
    }
    
    [Fact]
    public void GetTotal_MultipleSameItemWithMultipleBulkDiscount_ReturnsTotalCorrectly()
    {
        for (int i = 0; i < 12; i++)
        {
            _checkout.Scan("A");
            _checkout.Scan("B");
        }
        Assert.Equal(740, _checkout.GetTotal());
    }
}
