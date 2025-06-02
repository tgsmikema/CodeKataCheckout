namespace CodeKataCheckout;

public interface ICheckout
{
    void Scan(string sku);
    decimal GetTotal();
}