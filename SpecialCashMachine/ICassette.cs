namespace SpecialCashMachine
{
    public interface ICassette
    {
        decimal Denomination { get; set; }
        int Quantity { get; set; }
        string Currency { get; set; }
        decimal Total { get;  }
    }
}
